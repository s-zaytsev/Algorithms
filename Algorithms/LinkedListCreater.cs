namespace Algorithms;

internal static class LinkedListCreater
{
    public static ListNode Create(int[] nums)
    {
        var list = new ListNode();
        var pointer = list;

        foreach (var n in nums)
        {
            pointer.next = new ListNode(n);
            pointer = pointer.next;
        }

        return list.next;
    }
}
