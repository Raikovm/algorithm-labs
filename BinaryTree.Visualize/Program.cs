Console.OutputEncoding = System.Text.Encoding.UTF8;

string dataPath = $"{Environment.CurrentDirectory}/Files/";

List<string> files = Directory.GetFiles(dataPath).ToList();

foreach (var file in files)
{
    BinarySearchTree tree = BinarySearchTree.CreateFromFile(file);
    Console.WriteLine(file);
    if (tree.Root is null)
    {
        Console.WriteLine("Tree is empty");
        continue;
    }

    Console.WriteLine("Traversing forward:");
    BinarySearchTree.TraverseForward(tree.Root, x => Console.Write(x.Value + " "));
    Console.WriteLine();

    Console.WriteLine("Traversing backward:");
    BinarySearchTree.TraverseBackward(tree.Root, x => Console.Write(x.Value + " "));
    Console.WriteLine();

    Console.WriteLine("Tree visualization:");
    tree.Visualize();
    Console.WriteLine();
    Console.WriteLine();
}