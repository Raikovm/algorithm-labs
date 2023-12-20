namespace DataStructures.Stack;

public class CustomStack<T> : IEnumerable<T>
{
    private T[] array = new T[16];
    public int Count { get; private set; }

    public void Push(T item)
    {
        if (Count == array.Length)
        {
            Resize();
        }

        array[Count++] = item;
    }

    public T Pop() =>
        Count == 0
            ? throw new InvalidOperationException("Stack is empty")
            : array[--Count];

    public T Peek() =>
        Count == 0
            ? throw new InvalidOperationException("Stack is empty")
            : array[Count - 1];

    private void Resize()
    {
        T[] newArray = new T[array.Length * 2];

        for (int i = 0; i < Count; i++) {
            newArray[i] = array[i];
        }

        array = newArray;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = Count - 1; i >= 0; i--)
        {
            yield return array[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
