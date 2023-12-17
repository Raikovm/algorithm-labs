namespace DataStructures.Stack;

public class CustomStack<T> : IEnumerable<T>
{
    private T[] array = new T[16];
    public int Size { get; private set; }

    public void Push(T item)
    {
        if (Size == array.Length)
        {
            Array.Resize(ref array, array.Length * 2);
        }

        array[Size++] = item;
    }

    public T Pop() =>
        Size == 0
            ? throw new InvalidOperationException("Stack is empty")
            : array[--Size];

    public T Peek() =>
        Size == 0
            ? throw new InvalidOperationException("Stack is empty")
            : array[Size - 1];

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = Size - 1; i >= 0; i--)
        {
            yield return array[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
