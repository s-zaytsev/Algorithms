namespace Algorithms;

public class BSTIterator
{
    private readonly Queue<int> _queue;

    public BSTIterator(TreeNode root)
    {
        _queue = new Queue<int>();
        Init(root);
    }

    public int Next()
    {
        return _queue.Dequeue();
    }

    public bool HasNext()
    {
        return _queue.Count > 0;
    }

    private void Init(TreeNode root)
    {
        if (root == null) return;

        Init(root.left);
        _queue.Enqueue(root.val);
        Init(root.right);
    }
}
