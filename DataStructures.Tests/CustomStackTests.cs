namespace DataStructures.Tests;

public class CustomStackTests
{
    [Fact]
    public void Push_IncreasesSize()
    {
        var stack = new CustomStack<int>();

        stack.Push(1);

        stack.Count.Should().Be(1);
    }

    [Fact]
    public void Pop_DecreasesSize()
    {
        var stack = new CustomStack<int>();
        stack.Push(1);

        stack.Pop();

        stack.Count.Should().Be(0);
    }

    [Fact]
    public void Pop_ThrowsWhenEmpty()
    {
        var stack = new CustomStack<int>();

        Action act = () => stack.Pop();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Stack is empty");
    }

    [Fact]
    public void Peek_ReturnsLastPushedItem()
    {
        var stack = new CustomStack<int>();
        stack.Push(1);
        stack.Push(2);

        var result = stack.Peek();

        result.Should().Be(2);
    }

    [Fact]
    public void ToArray_ReturnsItemsInOrder()
    {
        var stack = new CustomStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Pop();
        stack.Push(3);
        stack.Push(4);
        stack.Pop();

        var result = stack.ToArray();

        result.Should().BeEquivalentTo(new[] { 1, 3 });
    }

    [Fact]
    public void Queue_AddALot()
    {
        var stack = new CustomStack<int>();
        var items = Enumerable.Range(0, 100).ToList();

        items.ForEach(stack.Push);

        stack.ToList().Should().BeEquivalentTo(items);
    }
}