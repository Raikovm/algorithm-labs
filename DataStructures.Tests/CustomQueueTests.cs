using DataStructures.Queue;

namespace DataStructures.Tests;

public class CustomQueueTests
{
    [Fact]
    public void Enqueue_IncreasesCount()
    {
        var queue = new CustomQueue<int>();

        queue.Enqueue(1);

        queue.Count.Should().Be(1);
    }

    [Fact]
    public void Dequeue_DecreasesCount()
    {
        var queue = new CustomQueue<int>();
        queue.Enqueue(1);

        queue.Dequeue();

        queue.Count.Should().Be(0);
    }

    [Fact]
    public void Dequeue_ReturnsExpectedItem()
    {
        var queue = new CustomQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);

        var result = queue.Dequeue();

        result.Should().Be(1);
    }

    [Fact]
    public void Peek_ReturnsExpectedItem()
    {
        var queue = new CustomQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);

        var result = queue.Peek();

        result.Should().Be(1);
    }

    [Fact]
    public void GetEnumerator_ReturnsExpectedItems()
    {
        var queue = new CustomQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);

        var results = queue.ToList();

        results.Should().Equal(1, 2);
    }

    [Fact]
    public void ToArray_ReturnsItemsInOrder()
    {
        var queue = new CustomQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Dequeue();
        queue.Enqueue(4);
        queue.Dequeue();
        queue.Enqueue(5);

       var results = queue.ToArray();

       results.Should().BeEquivalentTo(new [] { 3, 4, 5 });
    }

    [Fact]
    public void Queue_AddALot()
    {
        var queue = new CustomQueue<int>();
        var items = Enumerable.Range(0, 100).ToList();

        items.ForEach(queue.Enqueue);

        queue.ToList().Should().BeEquivalentTo(items);
    }
}
