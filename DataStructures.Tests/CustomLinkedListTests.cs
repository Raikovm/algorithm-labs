namespace DataStructures.Tests;

public class CustomLinkedListTests
{
    [Fact]
    public void AddLast_WithValue_ShouldIncreaseCount()
    {
        var list = new CustomLinkedList<int>();

        list.AddLast(1);

        list.Count.Should().Be(1);
    }

    [Fact]
    public void AddFirst_WithValue_ShouldIncreaseCount()
    {
        var list = new CustomLinkedList<string>();

        list.AddFirst("a");

        list.Count.Should().Be(1);
    }

    [Fact]
    public void Find_WithExistingValue_ShouldReturnNode()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);

        var result = list.Find(2);

        result.Should().NotBeNull();
        result.Value.Should().Be(2);
    }

    [Fact]
    public void Remove_WithExistingValue_ShouldReturnTrue()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);

        var result = list.Remove(1);

        result.Should().BeTrue();
        list.Count.Should().Be(1);
    }

    [Fact]
    public void First_WithNonEmptyList_ShouldReturnFirstValue()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);

        var result = list.First();

        result.Should().Be(1);
    }

    [Fact]
    public void InsertAfter_InsertsAfterSpecifiedNode()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        var node = list.Find(2);

        list.InsertAfter(node, 4);

        list.ToArray().Should().BeEquivalentTo(new[] { 1, 2, 4, 3 });
    }

    [Fact]
    public void ToArray_PrePopulated_MustBeEquivalent()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        list.AddFirst(4);
        list.AddFirst(5);
        list.Remove(1);

        var result = list.ToArray();

        result.Should().BeEquivalentTo(new[] { 5, 4, 2, 3 });
    }
}