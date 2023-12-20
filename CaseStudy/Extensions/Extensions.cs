namespace Case_Study.Extensions;

public static class Extensions
{
    public static bool GetFromPercentage(this Random random, int percentage) =>
        random.Next(100) < percentage;
}