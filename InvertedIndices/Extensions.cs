namespace InvertedIndices;

public static class Extensions
{
    public static IList<string> BruteForceSearch(this IEnumerable<Tuple<string, string>> documents, string term) =>
        documents
            .Where(t => t.Item2.Contains(term, StringComparison.InvariantCultureIgnoreCase))
            .Select(t => t.Item1)
            .ToList();
}