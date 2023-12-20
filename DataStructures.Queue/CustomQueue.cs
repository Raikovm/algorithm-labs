namespace DataStructures.Queue;

public class CustomQueue<T> : IEnumerable<T>
{
    private T[] array = new T[16];
    private int head = 0;
    private int tail = 0;

    public int Count { get; private set; }

    public void Enqueue(T item)
    {
        if (Count == array.Length)
        {
            Resize();
        }

        array[tail] = item;
        tail = (tail + 1) % array.Length;
        Count++;
    }

    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty");

        T item = array[head];
        head = (head + 1) % array.Length;
        Count--;

        return item;
    }

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty");

        return array[head];
    }

    private void Resize()
    {
        T[] newArray = new T[array.Length * 2];

        for (int i = 0; i < Count; i++) {
            newArray[i] = array[(head + i) % array.Length];
        }

        array = newArray;
        head = 0;
        tail = Count;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = head; i < head + Count; i++) {
            yield return array[i % array.Length];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
