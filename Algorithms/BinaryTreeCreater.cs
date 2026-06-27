namespace Algorithms;

public static class BinaryTreeCreater
{
    public static TreeNode Create(int?[] array)
    {
        if (array.Length == 0 || array[0] == null)
            return null;

        var root = new TreeNode(array[0].Value);
        CreateDFS(array, root, 0);

        return root;
    }

    private static void CreateDFS(int?[] array, TreeNode root, int index)
    {
        var leftIndex = 2 * index + 1;
        var rightIndex = 2 * index + 2;

        if (leftIndex < array.Length && array[leftIndex] != null)
        {
            root.left = new TreeNode(array[leftIndex]!.Value);
            CreateDFS(array, root.left, leftIndex);
        }

        if (rightIndex < array.Length && array[rightIndex] != null)
        {
            root.right = new TreeNode(array[rightIndex]!.Value);
            CreateDFS(array, root.right, rightIndex);
        }
    }
}
