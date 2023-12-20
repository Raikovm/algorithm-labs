namespace DataStructures.Queue;

public class CustomQueue<T> : IEnumerable<T>
{
    private CustomLinkedList<T> list = new();

    public int Count => list.Count;

    public void Enqueue(T item)
    {
        list.AddLast(item);
    }

    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty");

        T item = list.First() ?? throw new InvalidOperationException();
        list.Remove(item);
        return item;
    }

    public T Dequeue(T item)
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty");

        list.Remove(item);
        return item;
    }

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty");

        return list.First() ?? throw new InvalidOperationException();
    }

    public void Clear()
    {
        list = new CustomLinkedList<T>();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
