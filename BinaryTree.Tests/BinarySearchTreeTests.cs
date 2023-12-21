namespace BinaryTree.Tests;

public class BinarySearchTreeTests
{
    [Fact]
    public void Add_WithEmptyTree_SetsRoot()
    {
        BinarySearchTree binarySearchTree = new();

        binarySearchTree.Add("hello");

        binarySearchTree.Root.Should().NotBeNull();
        binarySearchTree.Root.Value.Should().Be("hello");
    }

    [Fact]
    public void Add_WithExistingTree_AddsToLeftIfLessThanRoot()
    {
        BinarySearchTree binarySearchTree = new();

        binarySearchTree.Add("hello");
        binarySearchTree.Add("apple");

        binarySearchTree.Root.Value.Should().Be("hello");
        binarySearchTree.Root.Left.Value.Should().Be("apple");
    }

    [Fact]
    public void Add_WithExistingTree_AddsToRightIfGreaterThanRoot()
    {
        BinarySearchTree binarySearchTree = new();

        binarySearchTree.Add("hello");
        binarySearchTree.Add("zebra");

        binarySearchTree.Root.Value.Should().Be("hello");
        binarySearchTree.Root.Right.Value.Should().Be("zebra");
    }

    [Fact]
    public void TraverseForward_WithRootNode_VisitsNodesInOrder()
    {
        TreeNode root = new("root")
        {
            Left = new TreeNode("left"),
            Right = new TreeNode("right")
        };
        BinarySearchTree tree = new(root);

        List<string> visitedNodes = new();
        BinarySearchTree.TraverseForward(root, n => visitedNodes.Add(n.Value));

        visitedNodes.Should().Equal("left", "root", "right");
    }

    [Fact]
    public void TraverseBackward_WithRootNode_VisitsNodesInReverseOrder()
    {
        TreeNode root = new("root")
        {
            Left = new TreeNode("left"),
            Right = new TreeNode("right")
        };
        BinarySearchTree tree = new(root);

        List<string> visitedNodes = new();
        BinarySearchTree.TraverseBackward(root, n => visitedNodes.Add(n.Value));

        visitedNodes.Should().Equal("right", "root", "left");
    }

    [Fact]
    public void Search_WithEmptyTree_ReturnsNull()
    {
        BinarySearchTree tree = new();
        var result = tree.Search("test");

        result.Should().BeNull();
    }

    [Fact]
    public void Search_WithExistingNode_ReturnsNode()
    {
        BinarySearchTree tree = new();
        tree.Add("a");
        tree.Add("r");
        tree.Add("d");
        tree.Add("e");
        tree.Add("j");
        tree.Add("i");

        var result = tree.Search("a");

        result.Should().NotBeNull();
        result.Value.Should().Be("a");
    }
}