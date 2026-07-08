namespace Algorithms;

public class BrowserHistory
{
    private class BrowserHistoryNode
    {
        public string Value { get; set; }
        public BrowserHistoryNode Next { get; set; }
        public BrowserHistoryNode Prev { get; set; }

        public BrowserHistoryNode() { }

        public BrowserHistoryNode(
            string value,
            BrowserHistoryNode next = null,
            BrowserHistoryNode prev = null)
        {
            Value = value;
            Next = next;
            Prev = prev;
        }
    }

    private BrowserHistoryNode _currentNode;

    public BrowserHistory(string homepage)
    {
        _currentNode = new BrowserHistoryNode(homepage);
    }

    public void Visit(string url)
    {
        _currentNode.Next = new BrowserHistoryNode(url, null, _currentNode);
        _currentNode = _currentNode.Next;
    }

    public string Back(int steps)
    {
        for (int i = 0; i < steps && _currentNode.Prev != null; i++)
        {
            _currentNode = _currentNode.Prev;
        }

        return _currentNode.Value;
    }

    public string Forward(int steps)
    {
        for (int i = 0; i < steps && _currentNode.Next != null; i++)
        {
            _currentNode = _currentNode.Next;
        }

        return _currentNode.Value;
    }
}
