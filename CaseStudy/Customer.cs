namespace Case_Study;

public class Customer
{
    public Customer(bool isSuspicious)
    {
        IsSuspicious = isSuspicious;
    }

    public bool IsSuspicious { get; init; }
}