namespace BinaryTrees;

public class BinarySearchTree
{
    public BinarySearchTree()
    {

    }

    public BinarySearchTree(TreeNode? root)
    {
        Root = root;
    }

    public TreeNode? Root { get; set; }

    public void Add(string word)
    {
        TreeNode newNode = new(word);
        if (Root == null)
        {
            Root = newNode;
            return;
        }

        TreeNode? current = Root;
        while (true)
        {
            TreeNode parent = current;

            int result = string.CompareOrdinal(word, current.Value);
            if (result < 0)
            {
                current = current.Left;
                if (current == null)
                {
                    parent.Left = newNode;
                    break;
                }

                continue;
            }

            if (result > 0)
            {
                current = current.Right;
                if (current != null)
                {
                    continue;
                }

                parent.Right = newNode;
            }

            break;
        }
    }

    public static void TraverseForward(TreeNode node, Action<TreeNode> visit)
    {
        if (node.Left != null)
        {
            TraverseForward(node.Left, visit);
        }

        visit(node);

        if (node.Right != null)
        {
            TraverseForward(node.Right, visit);
        }
    }

    public static void TraverseBackward(TreeNode node, Action<TreeNode> visit)
    {
        if (node.Right != null)
        {
            TraverseBackward(node.Right, visit);
        }

        visit(node);

        if (node.Left != null)
        {
            TraverseBackward(node.Left, visit);
        }
    }

    public TreeNode? Search(string value)
    {
        TreeNode? current = Root;
        while (current != null)
        {
            int compare = string.CompareOrdinal(value, current.Value);
            switch (compare)
            {
                case < 0:
                    current = current.Left;
                    break;
                case > 0:
                    current = current.Right;
                    break;
                default:
                    return current;
            }
        }
        return null;
    }

    public static BinarySearchTree CreateFromFile(string filePath)
    {
        List<string> words = [];
        using StreamReader sr = new(filePath);
        while (sr.ReadLine() is { } line)
        {
            var strings = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            words.AddRange(strings);
        }

        BinarySearchTree tree = new();

        words.ForEach(word => tree.Add(word));

        return tree;
    }
}

public class TreeNode(string value)
{
    public string Value { get; set; } = value;
    public TreeNode? Left { get; set; }
    public TreeNode? Right { get; set; }
}