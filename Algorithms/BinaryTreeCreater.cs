namespace Algorithms;

public static class BinaryTreeCreater
{
    public static TreeNode Create(int?[] array)
    {
        if (array.Length == 0)  return null;

        TreeNode root = new TreeNode(array[0] ?? 0);
        CreateDFS(array, root, 0);

        return root;
    }

    private static void CreateDFS(int?[] array, TreeNode root, int index)
    {
        var leftIndex = 2 * index + 1;
        var rightIndex = leftIndex + 1;

        if (leftIndex < array.Length)
        {
            var value = array[leftIndex];
            if (value == null) return;

            root.left = new TreeNode((int)value);
            CreateDFS(array, root.left, leftIndex);
        }

        if (rightIndex < array.Length)
        {
            var value = array[rightIndex];
            if (value == null) return;

            root.right = new TreeNode((int)value);
            CreateDFS(array, root.right, rightIndex);
        }
    }
}
