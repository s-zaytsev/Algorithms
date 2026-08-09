namespace Algorithms;

internal class LinkedListRandomNode
{

    private readonly ListNode _list;
    private readonly Random _random;

    public LinkedListRandomNode(ListNode head)
    {
        _list = head;
        _random = new Random();
    }

    public int GetRandom()
    {
        var result = _list.val;
        var count = 1;

        var pointer = _list;
        while (pointer != null)
        {
            if (_random.Next(count) == 0)
                result = pointer.val;

            pointer = pointer.next;
            count++;
        }

        return result;
    }
}
