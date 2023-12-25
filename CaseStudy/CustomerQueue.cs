namespace Case_Study;

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