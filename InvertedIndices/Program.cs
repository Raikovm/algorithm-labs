const string searchToken = "apples";

InvertedIndex index = new();

foreach (var filePath in Directory.GetFiles("./Documents", "*.txt"))
{
    string content = File.ReadAllText(filePath);
    index.AddDocument(filePath, content);
}

index.PrintIndex();
Console.WriteLine("-----------------------");

Stopwatch stopwatch = Stopwatch.StartNew();
IEnumerable<Tuple<string, string>> documents = Directory.GetFiles("./Documents", "*.txt")
    .Select(document => new Tuple<string, string>(document, File.ReadAllText(document)))
    .ToArray();
IList<string> bruteForceResult = documents
    .BruteForceSearch(searchToken);
stopwatch.Stop();
Console.WriteLine($"Brute force search results: {string.Join(", ", bruteForceResult)}");
Console.WriteLine($"Brute force search took {stopwatch.ElapsedTicks} ticks");

stopwatch.Restart();
List<string> indexSearchResult = index.Search(searchToken);
Console.WriteLine($"Index search results: {string.Join(", ", indexSearchResult)}");
stopwatch.Stop();
Console.WriteLine($"Index search took {stopwatch.ElapsedTicks} ticks");
