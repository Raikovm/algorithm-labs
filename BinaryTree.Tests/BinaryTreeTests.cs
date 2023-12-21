namespace BinaryTree.Tests;

public class BinarySearchTreeTests
{
    [Fact]
    public void Add_WithEmptyTree_SetsRoot()
    {
        BinarySearchTree binarySearchTree = new();

        binarySearchTree.Add("hello");

        binarySearchTree.Root.Should().NotBeNull();
        binarySearchTree.Root.Word.Should().Be("hello");
    }

    [Fact]
    public void Add_WithExistingTree_AddsToLeftIfLessThanRoot()
    {
        BinarySearchTree binarySearchTree = new();

        binarySearchTree.Add("hello");
        binarySearchTree.Add("apple");

        binarySearchTree.Root.Word.Should().Be("hello");
        binarySearchTree.Root.Left.Word.Should().Be("apple");
    }

    [Fact]
    public void Add_WithExistingTree_AddsToRightIfGreaterThanRoot()
    {
        BinarySearchTree binarySearchTree = new();

        binarySearchTree.Add("hello");
        binarySearchTree.Add("zebra");

        binarySearchTree.Root.Word.Should().Be("hello");
        binarySearchTree.Root.Right.Word.Should().Be("zebra");
    }
}