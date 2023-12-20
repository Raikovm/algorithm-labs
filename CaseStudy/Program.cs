Console.WriteLine("Hello, World!");

Simulation simulation = new(5, 10);
await simulation.StartSimulation();


public class Customer
{
    public Customer(bool isSuspicious)
    {
        IsSuspicious = isSuspicious;
    }

    public bool IsSuspicious { get; init; }
}

public class CustomerFactory
{
    private readonly int chanceOfSuspicion;

    public CustomerFactory(int chanceOfSuspicion)
    {
        this.chanceOfSuspicion = chanceOfSuspicion;
    }

    public Customer CreateCustomer() =>
        new(Random.Shared.GetFromPercentage(chanceOfSuspicion));
}

public class QueueFactory
{
    private readonly CustomerFactory customerFactory = new(20);
    private readonly (int Min, int Max) openingTimeRange;
    private readonly (int Min, int Max) workingTimeRange;

    private readonly int customersPerQueue;

    public QueueFactory(int customersPerQueue, (int, int) workingTimeRange, (int Min, int Max) openingTimeRange)
    {
        this.customersPerQueue = customersPerQueue;
        this.workingTimeRange = workingTimeRange;
        this.openingTimeRange = openingTimeRange;
    }

    public CustomerQueue CreateEmptyQueue() =>
        new(Random.Shared.Next(workingTimeRange.Min, workingTimeRange.Max), openingTimeRange, workingTimeRange);

    public CustomerQueue CreateQueue()
    {
        var queue = CreateEmptyQueue();
        for (var i = 0; i < customersPerQueue; i++)
        {
            queue.Enqueue(customerFactory.CreateCustomer());
        }
        return queue;
    }
}

public class CustomerQueue : CustomQueue<Customer>
{
    private readonly CustomerFactory customerFactory = new(20);
    private readonly (int Min, int Max) openingTimeRange;
    private readonly (int Min, int Max) workingTimeRange;

    public CustomerQueue(int worksUntil, (int Min, int Max) openingTimeRange, (int Min, int Max) workingTimeRange)
    {
        WorksUntil = worksUntil;
        this.openingTimeRange = openingTimeRange;
        this.workingTimeRange = workingTimeRange;
    }

    public bool IsClosed { get; private set; } = false;
    public int WorksUntil { get; private set; }
    public int OpeningAt { get; private set; }

    public void Close()
    {
        var customers = this.ToArray();

        IsClosed = true;
        OpeningAt = WorksUntil + Random.Shared.Next(openingTimeRange.Min, openingTimeRange.Max);
        Clear();
    }

    public void Open()
    {
        WorksUntil = Random.Shared.Next(workingTimeRange.Min, workingTimeRange.Max);
        IsClosed = false;
    }
}

public class Simulation
{
    private const int TickInMs = 1000;
    private const int ChanceToServeCustomer = 50;
    private const int CustomersPerTick = 3;
    private readonly Stopwatch stopwatch = new();
    private readonly CustomerFactory customerFactory = new(50);

    private readonly List<CustomerQueue> queues;

    public Simulation(int queueCount, int customersPerQueue)
    {
        QueueFactory queueFactory = new(customersPerQueue, (TickInMs * 10, TickInMs * 25), (TickInMs * 1, TickInMs * 3));
        queues = new List<CustomerQueue>(queueCount);
        for (var i = 0; i < queueCount; i++)
        {
            queues.Add(queueFactory.CreateQueue());
        }
    }

    public async Task StartSimulation(CancellationToken cancellationToken = default)
    {
        stopwatch.Start();
        while (!cancellationToken.IsCancellationRequested)
        {
            Tick();

            await Task.Delay(TickInMs * 1, cancellationToken);
        }
    }

    private void Tick()
    {
        OpenQueues();
        CloseQueues();
        ServeCustomers();
        AddCustomers();
        TransferSuspendedCustomers();
        LogQueues();
    }

    private void OpenQueues()
    {
        foreach (var q in queues)
        {
            if (q.IsClosed && q.OpeningAt <= stopwatch.ElapsedMilliseconds)
            {
                q.Open();
            }
        }
    }

    private void CloseQueues()
    {
        List<Customer> leftCustomers = new();
        foreach (CustomerQueue q in queues.Where(q => stopwatch.ElapsedMilliseconds >= q.WorksUntil && !q.IsClosed))
        {
            leftCustomers.AddRange(q);
            q.Close();
        }
        List<CustomerQueue> workingQueues = queues.Where(q => !q.IsClosed).ToList();
        if (leftCustomers.Count == 0 || workingQueues.Count == 0)
        {
            return;
        }

        DistributeCustomers(leftCustomers, workingQueues);
    }

    private static void DistributeCustomers(List<Customer> customers, List<CustomerQueue> queues)
    {
        int chunkSize = customers.Count / queues.Count;
        if (chunkSize == 0)
        {
            chunkSize = 1;
        }

        List<Customer[]> customerChunks = customers.Chunk(chunkSize).ToList();
        for (var i = 0; i < queues.Count; i++)
        {
            var queue = queues[i];
            if (i >= customerChunks.Count)
            {
                break;
            }
            var chunk = customerChunks[i];
            foreach (Customer customer in chunk)
            {
                queue.Enqueue(customer);
            }
        }
    }

    private void ServeCustomers() =>
        queues.ForEach(q =>
        {
            if (q.Count == 0)
            {
                return;
            }
            if (Random.Shared.GetFromPercentage(ChanceToServeCustomer))
            {
                q.Dequeue();
            }
        });

    private void AddCustomers()
    {
        List<Customer> customers = Enumerable.Range(0, CustomersPerTick)
            .Select(_ => customerFactory.CreateCustomer()).ToList();

        DistributeCustomers(customers, queues);
    }

    private void TransferSuspendedCustomers()
    {
        foreach (CustomerQueue q in queues.Where(q => !q.IsClosed))
        {
            List<Customer> suspicious = q.Where(x => x.IsSuspicious).ToList();
            List<CustomerQueue> smallerQueues = queues.Where(x => x.Count < q.Count).ToList();
            if (smallerQueues.Count == 0)
            {
                continue;
            }
            DistributeCustomers(suspicious, smallerQueues);
            suspicious.ForEach(x => q.Dequeue(x));
        }
    }

    private void LogQueues()
    {
        queues.ForEach(q => Console.WriteLine($"Count {q.Count} works until {q.WorksUntil} is closed {q.IsClosed} opening at {q.OpeningAt}"));
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
        Console.WriteLine("----------------");
    }
}

