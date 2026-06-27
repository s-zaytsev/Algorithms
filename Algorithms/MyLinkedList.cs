namespace Algorithms;

public class MyLinkedList
{
    private class LinkedListNode
    {
        public int Value { get; set; }
        public LinkedListNode Next { get; set; }

        public LinkedListNode() { }

        public LinkedListNode(int value, LinkedListNode next = null)
        {
            Value = value;
            Next = next;
        }
    }

    private LinkedListNode _head;
    private LinkedListNode _tail;
    private int _count;

    public MyLinkedList()
    {
        _head = null;
        _tail = null;
        _count = 0;
    }

    public int Get(int index)
    {
        if (index < 0 || index >= _count) return -1;

        var pointer = _head;
        while (index > 0)
        {
            pointer = pointer.Next;
            index--;
        }

        return pointer.Value;
    }

    public void AddAtHead(int val)
    {
        var node = new LinkedListNode(val, _head);
        _head = node;
        if (_count == 0) _tail = node;
        _count++;
    }

    public void AddAtTail(int val)
    {
        var node = new LinkedListNode(val);
        if (_count == 0)
        {
            _head = node;
            _tail = node;
        }
        else
        {
            _tail.Next = node;
            _tail = node;
        }
        _count++;
    }

    public void AddAtIndex(int index, int val)
    {
        if (index < 0 || index > _count) return;
        if (index == 0)
        {
            AddAtHead(val);
            return;
        }
        if (index == _count)
        {
            AddAtTail(val);
            return;
        }

        var pointer = _head;
        for (int i = 0; i < index - 1; i++)
        {
            pointer = pointer.Next;
        }

        pointer.Next = new LinkedListNode(val, pointer.Next);
        _count++;
    }

    public void DeleteAtIndex(int index)
    {
        if (index < 0 || index >= _count) return;

        if (index == 0)
        {
            _head = _head.Next;
            if (_count == 1) _tail = null;
        }
        else
        {
            var pointer = _head;
            for (int i = 0; i < index - 1; i++)
            {
                pointer = pointer.Next;
            }

            pointer.Next = pointer.Next?.Next;
            if (pointer.Next == null) _tail = pointer;
        }

        _count--;
    }
}
