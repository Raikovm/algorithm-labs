namespace InvertedIndices;

public class InvertedIndex
{
    private readonly Dictionary<string, List<string>> index = new();
    private static readonly char[] Separator = { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?' };

    public void AddDocument(string documentName, string documentContent)
    {
        var words = documentContent.Split(Separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.ToLowerInvariant());

        foreach (var word in words)
        {
            if (!index.TryGetValue(word, out List<string>? value))
            {
                value = new List<string>();
                index[word] = value;
            }

            if (!value.Contains(documentName))
            {
                value.Add(documentName);
            }
        }
    }

    public List<string> Search(string term) =>
        index.TryGetValue(term.ToLowerInvariant(), out List<string>? search)
            ? search
            : [];

    public void PrintIndex()
    {
        foreach (var entry in index.OrderBy(e => e.Key))
        {
            Console.WriteLine($"{entry.Key}: {string.Join(", ", entry.Value)}");
        }
    }
}