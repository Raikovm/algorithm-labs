namespace Case_Study;

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