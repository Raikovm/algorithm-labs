namespace Case_Study;

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