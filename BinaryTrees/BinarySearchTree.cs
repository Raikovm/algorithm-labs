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

            int result = string.CompareOrdinal(word, current.Word);
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
}

public class TreeNode(string word)
{
    public string Word { get; set; } = word;
    public TreeNode? Left { get; set; }
    public TreeNode? Right { get; set; }
}