namespace DataStructures.LinkedList;

public class CustomLinkedList<T> : IEnumerable<T>
{
    private CustomLinkedListNode<T>? head;
    private CustomLinkedListNode<T>? tail;

    public int Count { get; private set; }

    public void AddFirst(T value)
    {
        CustomLinkedListNode<T>? newCustomLinkedListNode = new(value);
        if (head == null)
        {
            head = tail = newCustomLinkedListNode;
        }
        else
        {
            newCustomLinkedListNode.Next = head;
            head.Previous = newCustomLinkedListNode;
            head = newCustomLinkedListNode;
        }
        Count++;
    }

    public void AddLast(T value)
    {
        CustomLinkedListNode<T> newCustomLinkedListNode = new(value);
        if (tail == null)
        {
            head = tail = newCustomLinkedListNode;
        }
        else
        {
            tail.Next = newCustomLinkedListNode;
            newCustomLinkedListNode.Previous = tail;
            tail = newCustomLinkedListNode;
        }
        Count++;
    }

    public void InsertAfter(CustomLinkedListNode<T> node, T value)
    {
        CustomLinkedListNode<T> newNode = new(value);

        if (node.Next == null)
        {
            node.Next = newNode;
            newNode.Previous = node;
            tail = newNode;
        }
        else
        {
            newNode.Next = node.Next;
            node.Next.Previous = newNode;
            node.Next = newNode;
            newNode.Previous = node;
        }

        Count++;
    }

    public CustomLinkedListNode<T>? Find(T value)
    {
        CustomLinkedListNode<T>? current = head;
        while (current != null)
        {
            if (current.Value != null && current.Value.Equals(value))
            {
                return current;
            }
            current = current.Next;
        }
        return null;
    }

    public bool Remove(T value)
    {
        CustomLinkedListNode<T>? current = head;
        while (current != null)
        {
            if (current.Value != null && current.Value.Equals(value))
            {
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    head = current.Next;
                }

                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    tail = current.Previous;
                }

                Count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    public T? Last() => tail != null ? tail.Value : default;

    public T? First() => head != null ? head.Value : default;


    public IEnumerator<T> GetEnumerator()
    {
        CustomLinkedListNode<T>? current = head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class CustomLinkedListNode<T>
{
    public CustomLinkedListNode(T value)
    {
        Value = value;
    }

    public T Value { get; set; }
    public CustomLinkedListNode<T>? Previous { get; set; }
    public CustomLinkedListNode<T>? Next { get; set; }
}


