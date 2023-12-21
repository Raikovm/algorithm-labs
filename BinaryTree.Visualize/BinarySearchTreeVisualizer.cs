namespace BinaryTree.Visualize;

public static class BinarySearchTreeVisualizer
{
    public static void Visualize(this BinarySearchTree tree)
    {
        if (tree.Root == null)
        {
            Console.WriteLine("Tree is empty");
            return;
        }

        Visualize(tree.Root, 0);
    }

    private static void Visualize(TreeNode? node, int level)
    {
        if (node == null) return;

        var indent = new string('-', level * 2);

        Console.WriteLine($"{indent}{node.Value}");

        Visualize(node.Left, level + 1);
        Visualize(node.Right, level + 1);
    }
}
