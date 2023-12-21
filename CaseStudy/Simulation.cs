namespace Case_Study;

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
        foreach (CustomerQueue q in queues.Where(q => q.IsClosed && q.OpeningAt <= stopwatch.ElapsedMilliseconds))
        {
            q.Open();
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

    public static void DistributeCustomers(List<Customer> customers, List<CustomerQueue> queues)
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