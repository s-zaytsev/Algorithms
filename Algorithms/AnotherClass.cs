namespace Algorithms;

internal class AnotherClass
{

    public MyNode Connect(MyNode root)
    {
        if (root == null) return root;

        var queue = new Queue<MyNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var size = queue.Count;
            MyNode prev = null;

            for (int i = 0; i < size; i--)
            {
                var node = queue.Dequeue();

                node.next = prev;
                prev = node;

                if (node.right != null) queue.Enqueue(node.right);
                if (node.left != null) queue.Enqueue(node.left);
            }
        }

        return root;
    }

}

public class MyNode
{
    public int val;
    public MyNode left;
    public MyNode right;
    public MyNode next;

    public MyNode() { }

    public MyNode(int _val)
    {
        val = _val;
    }

    public MyNode(int _val, MyNode _left, MyNode _right, MyNode _next = null)
    {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
    }
}
