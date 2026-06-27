namespace Algorithms;

public class LRUCache
{
    private class Node
    {
        public int Key { get; set; }
        public int Value { get; set; }
        public Node Prev { get; set; }
        public Node Next { get; set; }
    }

    private readonly IDictionary<int, Node> _dict;
    private int _capacity;

    private Node _head;
    private Node _tail;

    public LRUCache(int capacity)
    {
        _dict = new Dictionary<int, Node>();
        _capacity = capacity;
    }

    public int Get(int key)
    {
        if (_dict.ContainsKey(key))
        {
            var node = _dict[key];
            UpdateHead(node);
            return node.Value;
        }
        else return -1;
    }

    public void Put(int key, int value)
    {
        var node = new Node { Key = key, Value = value };

        if (_head == null && _tail == null)
        {
            _head = node;
            _tail = node;

            _dict.Add(key, node);

            _capacity--;
            return;
        }

        if (_dict.ContainsKey(key))
        {
            _dict[key].Value = value;
            UpdateHead(_dict[key]);
        }
        else if (_capacity > 0)
        {
            _dict.Add(key, node);

            AddHead(node);

            _capacity--;
        }
        else
        {
            _dict.Add(key, node);
            _dict.Remove(_tail.Key);

            AddHead(node);
            ReduceTail();
        }
    }

    private void AddHead(Node node)
    {
        node.Next = _head;
        _head.Prev = node;
        _head = node;
    }

    private void UpdateHead(Node node)
    {
        if (node.Key == _head.Key) return;

        if (node.Key == _tail.Key)
        {
            ReduceTail();
            AddHead(node);
        }
        else
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
            node.Prev = null;
            node.Next = _head;
            _head.Prev = node;
            _head = node;
        }
    }

    private void ReduceTail()
    {
        _tail = _tail.Prev;
    }
}
