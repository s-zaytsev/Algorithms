namespace Algorithms;

using System.Collections.Concurrent;
using System.Numerics;
using System.Text;

class LeetCode
{
    public int MyAtoi(string s)
    {
        if (s.Length == 0) return 0;
        var result = 0;

        bool isNegative = false;
        int index = 0;

        while (index < s.Length)
        {

            if (char.IsWhiteSpace(s[index]))
            {
                index++;
                continue;
            }

            if (s[index] == '-')
            {
                isNegative = true;
                index++;
                break;
            }

            if (s[index] == '+')
            {
                index++;
                break;
            }
            
            if (s[index] != ' ' || s[index] != '-' || s[index] != '+') break;
        }

        while (index < s.Length)
        {
            if (!char.IsDigit(s[index])) break;

            var value = s[index] - '0';

            if (result > (int.MaxValue - value) / 10)
            {
                return !isNegative ? int.MaxValue : int.MinValue;
            }

            result = result * 10 + value;

            index++;
        }

        return !isNegative ? result : -result;
    }

    public void DeleteNode(ListNode node)
    {
        node.val = node.next.val;
        node.next = node.next.next;
    }

    public int[] SortArrayByParity(int[] nums)
    {
        var left = 0;
        var right = nums.Length - 1;

        while (left < right)
        {
            if (nums[left] % 2 != 0)
            {
                var temp = nums[left];
                nums[left] = nums[right];
                nums[right] = temp;
                right--;
            }
            else
            {
                left++;
            }
        }

        return nums;
    }

    public int CountSegments(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return 0;

        var result = 0;
        var index = 0;

        while (index < s.Length)
        {
            if (s[index] != ' ')
            {
                result++;
            }

            while (index < s.Length && s[index] != ' ')
            {
                index++;
            }

            while (index < s.Length && s[index] == ' ')
            {
                index++;
            }
        }

        return result;
    }

    public string Convert(string s, int numRows)
    {
        if (numRows == 1) return s;

        var lines = new List<char>[numRows];
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = [];
        }

        var desc = false;
        var index = 0;

        foreach (var c in s)
        {
            lines[index].Add(c);

            if (desc)
            {
                index--;
                if (index < 0)
                {
                    desc = false;
                    index = 1;
                }
            }
            else
            {
                index++;
                if (index >= numRows)
                {
                    desc = true;
                    index = numRows - 2;
                }
            }
        }

        var sb = new StringBuilder();

        foreach (var line in lines)
        {
            sb.Append(string.Join("", line));
        }

        return sb.ToString();
    }

    public ListNode ReverseKGroup(ListNode head, int k)
    {
        if (k <= 0) return head;

        ListNode headPointer = head;
        ListNode result = new();
        ListNode resultPointer = result;

        while (headPointer != null)
        {
            var temp = headPointer;
            var count = 0;

            while (temp != null && count != k)
            {
                count++;
                temp = temp.next;
            }

            if (count < k)
            {
                resultPointer.next = headPointer;
                return result.next;
            }

            ListNode current = headPointer;
            ListNode prev = null;

            for (int i = 0; i < k; i++)
            {
                var next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }

            resultPointer.next = prev;

            while (resultPointer.next != null)
            {
                resultPointer = resultPointer.next;
            }

            headPointer = current;
        }

        return result.next;
    }

    public int AddDigits(int num)
    {
        var result = num;

        while (result >= 10)
        {
            var temp = result;
            var number = 0;

            while (temp > 0)
            {
                number += temp % 10;
                temp /= 10;
            }

            result = number;
        }

        return result;
    }

    public int[] SeparateDigits(int[] nums)
    {
        var list = new List<int>();

        foreach (int n in nums)
        {
            var temp = n;
            var tempList = new List<int>();

            while (temp > 0)
            {
                tempList.Add(temp % 10);
                temp = temp / 10;
            }


            for (int i = tempList.Count - 1; i >= 0; i--)
            {
                list.Add(tempList[i]);
            }

        }

        return [.. list];
    }


    // Input: grid = [[1,2,3,4],[5,6,7,8],[9,10,11,12],[13,14,15,16]], k = 2
    // Output: [[3, 4, 8, 12],[2, 11, 10, 16],[1, 7, 6, 15],[5, 9, 13, 14]]

    //[
    //[10,  1,  4,  8],
    //[6,   6,  3,  10],
    //[7,   4,  7,  10],
    //[1,   10, 6,  1],
    //[2,   1,  1,  10],
    //[3,   8,  9,  2],
    //[7,   1,  10, 10],
    //[7,   1,  4,  9],
    //[2,   2,  4,  2],
    //[10,  7,  5,  10]
    //]

    //[
    //[1,4,8,10],
    //[10,3,7,10],
    //[6,6,6,1],
    //[7,4,1,10],
    //[1,10,9,2],
    //[2,1,10,10],
    //[3,8,4,9],
    //[7,1,4,2],
    //[7,2,2,10],
    //[2,10,7,5]]

    //[
    //[1,4,8,10],
    //[10,3,7,10],
    //[6,6,6,1],
    //[7,4,1,10],
    //[1,10,9,2],
    //[2,1,10,10],
    //[3,8,4,9],
    //[7,1,4,2],
    //[7,1,2,10],
    //[2,10,7,5]]
    public int[][] RotateGrid(int[][] grid, int k)
    {
        var rows = grid.Length;
        var columns = grid[0].Length;
        var startRow = 0;
        var startColumn = 0;

        while (rows > 1 && columns > 1)
        {
            var count = k % (rows * 2 + columns * 2 - 2);
            for (int time = 0; time < count; time++)
            {
                var temp = grid[startRow][startColumn];

                for (int i = 0; i < columns - 1; i++)
                {
                    grid[startRow][i + startColumn] = grid[startRow][i + startColumn + 1];
                }

                for (int i = 0; i < rows - 1; i++)
                {
                    grid[i + startRow][startColumn + columns - 1] = grid[i + startRow + 1][startColumn + columns - 1];
                }

                for (int i = startColumn + columns - 1; i > startColumn; i--)
                {
                    grid[startRow + rows - 1][i] = grid[startRow + rows - 1][i - 1];
                }

                for (int i = startRow + rows - 1; i > startRow + 1; i--)
                {
                    grid[i][startColumn] = grid[i - 1][startColumn];
                }

                grid[startRow + 1][startColumn] = temp;
            }

            rows -= 2;
            columns -= 2;
            startRow++;
            startColumn++;
        }

        return grid;
    }

    public int RemoveElement(int[] nums, int val)
    {
        var left = 0;
        var right = nums.Length - 1;
        var result = 0;

        while (left <= right)
        {
            if (nums[left] != val)
            {
                result++;
                left++;
            }
            else
            {
                var temp = nums[right];
                nums[right] = nums[left];
                nums[left] = temp;
                right--;
            }
        }

        return result;
    }

    public int KthSmallest(TreeNode root, int k)
    {
        if (root == null || k <= 0) return 0;

        var queue = new Queue<TreeNode>();
        var priorityQueue = new PriorityQueue<int, int>();

        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            priorityQueue.Enqueue(node.val, node.val);

            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }

        var result = 0;

        for (int i = 0; i < k && priorityQueue.Count != 0; i++)
        {
            result = priorityQueue.Dequeue();
        }

        return result;
    }

    public char[][] RotateTheBox(char[][] boxGrid)
    {
        var result = new char[boxGrid[0].Length][];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new char[boxGrid.Length];
        }

        for (int i = 0; i < boxGrid[0].Length; i++)
        {
            for (int j = boxGrid.Length - 1; j >= 0; j--)
            {
                result[i][j] = boxGrid[Math.Abs(boxGrid.Length - 1 - j)][i];
            }
        }

        for (int i = result.Length - 1; i >= 0; i--)
        {
            for (int j = 0; j < result[i].Length; j++)
            {
                if (result[i][j] == '.')
                {
                    int row = i - 1;

                    while (row >= 0)
                    {
                        if (result[row][j] == '*') break;

                        if (result[row][j] == '#')
                        {
                            result[row][j] = '.';
                            result[i][j] = '#';
                            break;
                        }
                        row--;
                    }
                }
            }
        }

        return result;
    }


    public ListNode RotateRight(ListNode head, int k)
    {
        if (head?.next == null) return head;

        var steps = 0;

        var headPointer = head;

        while (headPointer != null)
        {
            steps++;
            headPointer = headPointer.next;
        }

        k = k % steps;

        var leftPointer = head;
        var rightPointer = head;

        for (int i = 0; i < k; i++)
        {
            while (rightPointer != null)
            {
                if (rightPointer.next?.next == null)
                {
                    rightPointer.next?.next = leftPointer;
                    leftPointer = rightPointer.next;
                    rightPointer.next = null;

                    rightPointer = leftPointer;
                    break;
                }

                rightPointer = rightPointer.next;
            }
        }

        return leftPointer;
    }

    public void Rotate(int[][] matrix)
    {
        var length = matrix.Length;
        var squareCount = length / 2;

        for (int i = 0; i < squareCount; i++)
        {
            for (int j = i; j < length - i - 1; j++)
            {
                var temp = matrix[i][j];
                matrix[i][j] = matrix[length - j - 1][i];
                matrix[length - j - 1][i] = matrix[length - i - 1][length - j - 1];
                matrix[length - i - 1][length - j - 1] = matrix[j][length - i - 1];
                matrix[j][length - i - 1] = temp;
            }
        }
    }

    public int Reverse(int x)
    {
        long n = x;
        long result = 0L;

        if (x < 0) n *= -1;

        while (n > 0)
        {
            result *= 10;
            result += n % 10;

            if (result > int.MaxValue || result < int.MinValue)
                return 0;

            n /= 10;
        }

        return x >= 0 ? (int)result : (int)-result;
    }

    //  Input: nums = [4,3,2,6]
    //  Output: 26
    //  Explanation:
    //  F(0) = (0 * 4) + (1 * 3) + (2 * 2) + (3 * 6) = 0 + 3 + 4 + 18 = 25
    //  F(1) = (0 * 6) + (1 * 4) + (2 * 3) + (3 * 2) = 0 + 4 + 6 + 6 = 16
    //  F(2) = (0 * 2) + (1 * 6) + (2 * 4) + (3 * 3) = 0 + 6 + 8 + 9 = 23
    //  F(3) = (0 * 3) + (1 * 2) + (2 * 6) + (3 * 4) = 0 + 2 + 12 + 12 = 26
    //  So the maximum value of F(0), F(1), F(2), F(3) is F(3) = 26.
    public int MaxRotateFunction(int[] nums)
    {
        var result = 0;
        var dp = new int[nums.Length];

        for (int i = 0; i < nums.Length; i++)
        {
            var n = nums[i] * i;
            dp[0] = dp[0] + n;

            var dpIndex = 1;

            for (int j = 0; j < nums.Length - 1; j++)
            {
                var index = nums.Length - 1 - i;
                n = nums[index] * i;
                dp[dpIndex] = dp[index] + n;
                dpIndex++;
            }
        }

        for (int i = 0; i < dp.Length; i++)
        {
            result = Math.Max(result, dp[i]);
        }

        return result;
    }

    public double MyPow(double x, int n)
    {
        long degree = n;

        if (degree < 0)
        {
            x = 1 / x;
            degree = -degree;
        }

        return MyPowRecursion(x, degree);
    }

    private double MyPowRecursion(double x, long n)
    {
        if (n == 0) return 1.0;

        double half = MyPowRecursion(x, n / 2);

        if (n % 2 == 0)
        {
            return half * half;
        }
        else
        {
            return half * half * x;
        }
    }

    public int MaxPathScore(int[][] grid, int k)
    {
        Span<(int cost, int value, int rate)> dp = stackalloc (int cost, int score, int rate)[grid[0].Length];

        for (int i = 0; i < grid[0].Length; i++)
        {
            var value = grid[0][i];
            var cost = grid[0][i] > 0 ? 1 : 0;
            dp[i] = (cost, value, value - cost);
        }

        for (int i = 1; i < grid.Length; i++)
        {
            var value = grid[i][0];
            var cost = value > 0 ? 1 : 0;

            dp[0] = (dp[0].cost + cost, dp[0].value + value, dp[0].rate = dp[0].rate + value - cost);

            for (int j = 1; j < grid[i].Length; j++)
            {
                value = grid[i][j];
                cost = value > 0 ? 1 : 0;

                if (dp[j].rate <= dp[j - 1].rate)
                    dp[j] = (dp[j - 1].cost + cost, dp[j - 1].value + value, dp[j - 1].rate = dp[j - 1].rate + value - cost);
                else
                    dp[j] = (dp[j].cost + cost, dp[j].value + value, dp[j].rate = dp[j].rate + value - cost);
            }
        }

        return dp[^1].cost > k ? -1 : dp[^1].value;
    }

    public ListNode SwapPairs(ListNode head)
    {
        var node = head;

        while (node?.next != null)
        {
            var temp = node.val;
            node.val = node.next.val;
            node.next.val = temp;

            node = node.next;
        }

        return head;
    }

    public int MinOperations(int[][] grid, int x)
    {
        var array = new int[grid.Length * grid[0].Length];

        var index = 0;

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                array[index++] = grid[i][j];
            }
        }

        Array.Sort(array);

        var targetNumber = array[array.Length / 2];

        var result = 0;

        for (int i = 0; i < array.Length; i++)
        {
            var temp = array[i];
            var shouldBeLess = temp > targetNumber;

            while (temp != targetNumber)
            {
                if (shouldBeLess && temp < targetNumber) return -1;
                if (!shouldBeLess && temp > targetNumber) return -1;

                if (shouldBeLess) temp -= x;
                else temp += x;

                result++;
            }
        }

        return result;
    }

    enum DirectionFrom
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }

    private IDictionary<int, IDictionary<DirectionFrom, HashSet<int>>> roads =
        new Dictionary<int, IDictionary<DirectionFrom, HashSet<int>>>()
    {
        { 1, new Dictionary<DirectionFrom, HashSet<int>>()
            {
                { DirectionFrom.Left, [1, 4, 6] },
                { DirectionFrom.Right, [1, 3, 5] },
            }
        },
        { 2, new Dictionary<DirectionFrom, HashSet<int>>()
            {
                { DirectionFrom.Up, [2, 3, 4] },
                { DirectionFrom.Down, [2, 5, 6] },
            }
        },
        { 3, new Dictionary<DirectionFrom, HashSet<int>>()
            {
                { DirectionFrom.Left, [1, 4, 6] },
                { DirectionFrom.Down, [2, 5, 6] },
            }
        },
        { 4, new Dictionary<DirectionFrom, HashSet<int>>()
            {
                { DirectionFrom.Down, [2, 5, 6] },
                { DirectionFrom.Right, [1, 3, 5] },
            }
        },
        { 5, new Dictionary<DirectionFrom, HashSet<int>>()
            {
                { DirectionFrom.Left, [1, 4, 6] },
                { DirectionFrom.Up, [2, 3, 4] },
            }
        },
        { 6, new Dictionary<DirectionFrom, HashSet<int>>()
            {
                { DirectionFrom.Up, [2, 3, 4] },
                { DirectionFrom.Right, [1, 3, 5] },
            }
        },

    };

    public bool HasValidPath(int[][] grid)
    {
        var queue = new Queue<(int row, int column)>();
        var visited = new bool[grid.Length, grid[0].Length];

        queue.Enqueue((0, 0));
        visited[0, 0] = true;

        var directions = new (int rowShift, int columnShift)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

        while (queue.Count > 0)
        {
            var (row, column) = queue.Dequeue();

            if (row == grid.Length - 1 && column == grid[row].Length - 1)
                return true;

            foreach (var (rowShift, columnShift) in directions)
            {
                var shiftedRow = rowShift + row;
                var shiftedColumn = columnShift + column;

                if (shiftedRow < 0 ||
                    shiftedColumn < 0 ||
                    shiftedRow >= grid.Length ||
                    shiftedColumn >= grid[shiftedRow].Length ||
                    visited[shiftedRow, shiftedColumn])
                    continue;

                var directionForm = DirectionFrom.Up;

                if (rowShift == -1) directionForm = DirectionFrom.Down;
                else if (columnShift == -1) directionForm = DirectionFrom.Right;
                else if (columnShift == 1) directionForm = DirectionFrom.Left;

                if (!roads[grid[shiftedRow][shiftedColumn]].ContainsKey(directionForm))
                    continue;

                if (!roads[grid[shiftedRow][shiftedColumn]][directionForm].Contains(grid[row][column]))
                    continue;

                queue.Enqueue((shiftedRow, shiftedColumn));
                visited[shiftedRow, shiftedColumn] = true;
            }
        }

        return false;
    }

    public bool IsPowerOfTwo(int n)
    {
        if (n == 1) return true;
        if (n <= 0 || n % 2 != 0) return false;
        return IsPowerOfTwo(n / 2);
    }

    public ListNode RemoveElements(ListNode head, int val)
    {
        ListNode root = head;
        ListNode result = new ListNode();
        ListNode resultHead = result;

        while (root != null)
        {
            if (root.val != val)
            {
                resultHead.next = new(root.val);
                resultHead = resultHead.next;
            }

            root = root.next;
        }

        return result.next;
    }

    public int FurthestDistanceFromOrigin(string moves)
    {
        var result = 0;
        var emptyMoves = 0;

        for (int i = 0; i < moves.Length; i++)
        {
            if (moves[i] == 'L') result--;
            else if (moves[i] == 'R') result++;
            else emptyMoves++;
        }

        return Math.Abs(result) + emptyMoves;
    }

    public int LengthOfLIS1(int[] nums)
    {
        if (nums.Length < 2) return nums.Length;

        var result = 1;

        Span<int> dp = stackalloc int[nums.Length + 1];
        dp[0] = 1;

        for (int i = 1; i < nums.Length; i++)
        {
            dp[i] = 1;

            for (int j = 0; j < i; j++)
            {
                if (nums[i] > nums[j])
                {
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                    result = Math.Max(result, dp[i]);
                }
            }
        }

        return result;
    }

    public int NumDistinct(string s, string t)
    {
        var dp = new int[s.Length + 1, t.Length + 1];

        for (int i = 0; i <= s.Length; i++)
        {
            dp[i, 0] = 1;
        }

        for (int i = 1; i < dp.GetLength(0); i++)
        {
            for (int j = 1; j < dp.GetLength(1); j++)
            {
                if (s[i - 1] == t[j - 1]) dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j];
                else dp[i, j] = dp[i - 1, j];
            }
        }

        return dp[s.Length, t.Length];
    }

    public int MinimumDeleteSum(string s1, string s2)
    {
        var dp = new int[s1.Length + 1, s2.Length + 1];

        for (int i = 1; i <= s1.Length; i++)
        {
            dp[i, 0] = dp[i - 1, 0] + s1[i - 1];
        }

        for (int i = 1; i <= s2.Length; i++)
        {
            dp[0, i] = dp[0, i - 1] + s2[i - 1];
        }

        for (int i = 1; i < dp.GetLength(0); i++)
        {
            for (int j = 1; j < dp.GetLength(1); j++)
            {
                if (s1[i - 1] == s2[j - 1]) dp[i, j] = dp[i - 1, j - 1];
                else dp[i, j] = Math.Min(dp[i - 1, j] + s1[i - 1], dp[i, j - 1] + s2[j - 1]);
            }
        }

        return dp[s1.Length, s2.Length];
    }

    public int MinDistance(string word1, string word2)
    {
        var dp = new int[word1.Length + 1][];

        for (int i = 0; i <= word1.Length; i++)
        {
            dp[i] = new int[word2.Length + 1];
            dp[i][0] = i;
        }

        for (int i = 0; i <= word2.Length; i++)
        {
            dp[0][i] = i;
        }

        for (int i = 1; i < dp.Length; i++)
        {
            for (int j = 1; j < dp[i].Length; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                    dp[i][j] = dp[i - 1][j - 1];
                else
                    dp[i][j] = Math.Min(Math.Min(dp[i - 1][j - 1], dp[i - 1][j]), dp[i][j - 1]) + 1;
            }
        }

        return dp[word1.Length][word2.Length];
    }

    public int LongestPalindromeSubseq(string s)
    {
        var length = s.Length;

        var dp = new int[length, length];

        for (int i = 0; i < length; i++)
        {
            dp[i, i] = 1;
        }

        for (int i = 0; i < length - 1; i++)
        {
            if (s[i] == s[i + 1])
            {
                dp[i, i + 1] = 2;
            }
            else dp[i, i + 1] = 1;
        }

        for (int len = 3; len <= length; len++)
        {
            for (int index = 0; index <= length - len; index++)
            {
                var secondIndex = index + len - 1;

                if (s[index] == s[secondIndex])
                    dp[index, secondIndex] = dp[index + 1, secondIndex - 1] + 2;

                else
                    dp[index, secondIndex] = Math.Max(dp[index + 1, secondIndex], dp[index, secondIndex - 1]);
            }
        }

        return dp[0, length - 1];
    }

    public int[] KWeakestRows(int[][] mat, int k)
    {
        var queue = new PriorityQueue<int, (double percent, int row)>();

        for (int row = 0; row < mat.Length; row++)
        {
            var soldiers = 0;

            for (int column = 0; column < mat[row].Length; column++)
            {
                if (mat[row][column] == 1) soldiers++;
            }

            queue.Enqueue(row, ((soldiers * 1.0 / mat[row].Length) * 100, row));
        }

        var result = new int[k];

        for (int i = 0; i < k && queue.Count > 0; i++)
        {
            result[i] = queue.Dequeue();
        }

        return result;
    }

    public ListNode MergeKLists(ListNode[] lists)
    {
        var queue = new PriorityQueue<int, int>();

        foreach (var list in lists)
        {
            var head = list;

            while (head != null)
            {
                queue.Enqueue(head.val, head.val);
                head = head.next;
            }
        }

        ListNode result = new ListNode();

        var headResult = result;

        while (queue.Count > 0)
        {
            headResult.next = new ListNode(queue.Dequeue());
            headResult = headResult.next;
        }

        return result.next;
    }

    public bool CanMakeArithmeticProgression(int[] arr)
    {
        if (arr.Length < 2) return true;
        Array.Sort(arr);

        var step = arr[1] - arr[0];

        for (int i = 1; i < arr.Length - 1; i++)
        {
            if (arr[i + 1] - arr[i] != step) return false;
        }

        return true;
    }

    public int MirrorDistance(int n)
    {
        var number = n;
        var reverseNumber = 0;

        while (number >= 10)
        {
            reverseNumber *= 10;
            reverseNumber += number % 10;
            number /= 10;
        }

        reverseNumber *= 10;
        reverseNumber += number;

        return Math.Abs(n - reverseNumber);
    }

    public bool WordBreak(string s, IList<string> wordDict)
    {
        var longestWordLength = 0;
        var wordSet = new HashSet<string>();

        foreach (var word in wordDict)
        {
            wordSet.Add(word);
            longestWordLength = Math.Max(longestWordLength, word.Length);
        }

        var length = s.Length + 1;
        var dp = new bool[length];
        dp[0] = true;

        for (int lengthOfSubstring = 0; lengthOfSubstring < length; lengthOfSubstring++)
        {
            for (int index = Math.Max(0, lengthOfSubstring - longestWordLength); index < lengthOfSubstring; index++)
            {
                var word = s[index..lengthOfSubstring];

                Console.WriteLine($"Ищем слово {word} в словаре..(j={index}, i={lengthOfSubstring}, dp[j]={dp[index]})");

                if (dp[index] && wordSet.Contains(word))
                {
                    Console.WriteLine("Нашли!");

                    dp[lengthOfSubstring] = true;
                    break;
                }

                Console.WriteLine("Слово не найдено...");
            }
        }

        return dp[length - 1];
    }

    public string LongestPalindrome(string s)
    {
        var length = s.Length;
        if (length == 0) return string.Empty;

        var dp = new bool[length, length];

        var start = 0;
        var maxLength = 1;

        for (int i = 0; i < length; i++)
        {
            dp[i, i] = true;
        }

        for (int i = 0; i < length - 1; i++)
        {
            if (s[i] == s[i + 1])
            {
                dp[i, i + 1] = true;
                start = i;
                maxLength = 2;
            }
        }

        for (int i = 3; i <= length; i++)
        {
            for (int j = 0; j <= length - i; j++)
            {
                var k = j + i - 1;
                if (s[j] == s[k] && dp[j + 1, k - 1])
                {
                    dp[j, k] = true;
                    start = j;
                    maxLength = i;
                }
            }
        }

        return s.Substring(start, maxLength);
    }

    public int ClosestTarget(string[] words, string target, int startIndex)
    {
        if (words[startIndex] == target) return 0;

        var left = startIndex - 1;
        var right = startIndex + 1;

        var steps = 1;

        while (left != startIndex && right != startIndex)
        {
            if (left < 0) left = words.Length - 1;
            if (right > words.Length - 1) right = 0;

            if (words[left] == target || words[right] == target)
                return steps;

            right++;
            left--;

            steps++;
        }

        return -1;
    }

    public int MaximalSquare(char[][] matrix)
    {
        var result = 0;
        var numbers = new int[matrix.Length, matrix[0].Length];

        for (int row = 0; row < matrix.Length; row++)
        {
            for (int column = 0; column < matrix[row].Length; column++)
            {
                if (matrix[row][column] == '1')
                {
                    if (row > 0 && column > 0)
                    {
                        numbers[row, column] = Math.Min(
                            Math.Min(numbers[row - 1, column], numbers[row, column - 1]),
                            numbers[row - 1, column - 1]) + 1;

                        result = Math.Max(result, numbers[row, column]);
                        continue;
                    }
                    else
                    {
                        numbers[row, column] = 1;
                    }

                    result = Math.Max(result, numbers[row, column]);
                }
            }
        }

        return result * result;
    }

    public int[] PlusOne(int[] digits)
    {
        if (digits.Length == 0) return digits;

        var addExtra = true;

        for (int i = digits.Length - 1; i >= 0; i--)
        {
            var number = digits[i];
            if (addExtra) number += 1;
            addExtra = number >= 10;
            digits[i] = number % 10;
        }

        if (addExtra) return [1, .. digits];

        return digits;
    }

    public int MinFallingPathSum(int[][] matrix)
    {
        var length = matrix[0].Length;
        var prev = new int[length];
        var current = new int[length];

        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                var value = matrix[i][j] + prev[j];

                if (j > 0) value = Math.Min(value, matrix[i][j] + prev[j - 1]);
                if (j < matrix[i].Length - 1) value = Math.Min(value, matrix[i][j] + prev[j + 1]);

                current[j] = value;
            }

            prev = [.. current];
        }

        var result = int.MaxValue;

        foreach (var number in current)
        {
            result = Math.Min(result, number);
        }

        return result;
    }

    public int MinimumTotal(IList<IList<int>> triangle)
    {
        var dp = new int[triangle[^1].Count];

        for (int i = 0; i < triangle[^1].Count; i++)
        {
            dp[i] = triangle[^1][i];
        }

        for (int i = triangle.Count - 2; i >= 0; i--)
        {
            for (int j = 0; j < triangle[i].Count; j++)
            {
                dp[j] = Math.Min(dp[j], dp[j + 1]) + triangle[i][j];
            }
        }

        return dp[0];
    }

    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        if (obstacleGrid[^1][^1] == 1 || obstacleGrid[0][0] == 1) return 0;

        var line = new int[obstacleGrid[0].Length];

        for (int i = 0; i < line.Length; i++)
        {
            if (obstacleGrid[0][i] == 1) break;
            line[i] = 1;
        }

        if (obstacleGrid.Length == 1 || obstacleGrid[0].Length == 1) return line[^1];

        for (int row = 1; row < obstacleGrid.Length; row++)
        {
            if (obstacleGrid[row][0] == 1) line[0] = 0;
            for (int column = 1; column < obstacleGrid[row].Length; column++)
            {
                if (obstacleGrid[row][column] == 1)
                {
                    line[column] = 0;
                }
                else
                {
                    line[column] = line[column - 1] + line[column];
                }
            }
        }

        return line[^1];
    }

    public int MinPathSum(int[][] grid)
    {
        if (grid.Length == 1 || grid[0].Length == 1)
        {
            var result = 0;

            for (int row = 0; row < grid.Length; row++)
            {
                for (int column = 0; column < grid[row].Length; column++)
                {
                    result += grid[row][column];
                }
            }

            return result;
        }

        var line = new int[grid[0].Length];

        line[0] = grid[0][0];
        for (int i = 1; i < line.Length; i++)
        {
            line[i] = grid[0][i] + line[i - 1];
        }

        for (int row = 1; row < grid.Length; row++)
        {
            line[0] = line[0] + grid[row][0];
            for (int column = 1; column < grid[row].Length; column++)
            {
                line[column] = Math.Min(line[column - 1], line[column]) + grid[row][column];
            }
        }

        return line[^1];
    }

    public int UniquePaths(int m, int n)
    {
        if (m == 1 || n == 1) return 1;

        var line1 = new int[n];
        var line2 = new int[n];

        for (int i = 0; i < n; i++)
        {
            line1[i] = 1;
        }

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                line2[j] = line2[j] + line1[j - 1];
                line1[j] = line2[j];
            }
        }

        return line2[^1];
    }


    //Not solved
    public int DeleteAndEarn(int[] nums)
    {
        var dict = new SortedDictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (!dict.ContainsKey(nums[i])) dict.Add(nums[i], 0);
            dict[nums[i]] += nums[i];
        }

        var prev = dict.First().Key;
        var current = dict.ElementAt(1).Key;

        var max = 0;

        for (int i = 2; i < dict.Count; i++)
        {
            var currentKey = dict.ElementAt(i).Key;
            if (currentKey - 1 == current)
            {

            }
            else
            {
            }
        }

        return max;
    }

    public int Rob(int[] nums)
    {
        if (nums.Length == 0) return 0;
        if (nums.Length == 1) return nums[0];
        if (nums.Length == 2) return Math.Max(nums[0], nums[1]);

        var prev = nums[0];
        var current = Math.Max(nums[0], nums[1]);

        for (int i = 2; i < nums.Length; i++)
        {
            var next = Math.Max(prev + nums[i], current);
            prev = current;
            current = next;
        }

        return current;
    }

    public int MinCostClimbingStairs(int[] cost)
    {
        if (cost.Length == 0) return 0;
        if (cost.Length == 1) return cost[0];

        var prev = cost[0];
        var current = cost[1];

        for (int i = 2; i < cost.Length; i++)
        {
            var next = Math.Min(prev, current) + cost[i];
            prev = current;
            current = next;
        }

        return Math.Min(prev, current);
    }

    public int Tribonacci(int n)
    {
        if (n == 0) return 0;
        if (n < 3) return 1;

        var prevprev = 0;
        var prev = 1;
        var current = 1;

        for (int i = 3; i <= n; i++)
        {
            var next = prevprev + prev + current;
            prevprev = prev;
            prev = current;
            current = next;
        }

        return current;
    }

    public int ShortestPathAllKeys(string[] grid)
    {
        var keyCount = 0;
        var startedRow = 0;
        var startedColumn = 0;

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '@')
                {
                    startedRow = i;
                    startedColumn = j;
                }

                if (char.IsLetter(grid[i][j]) && char.IsLower(grid[i][j])) keyCount++;
            }
        }

        if (keyCount == 0) return 0;

        int allKeysMask = (1 << keyCount) - 1;

        var queue = new Queue<(int row, int column, int keysMask, int length)>();
        queue.Enqueue((startedRow, startedColumn, 0, 0));

        var visited = new bool[grid.Length, grid[0].Length, 1 << keyCount];
        visited[startedRow, startedColumn, 0] = true;

        Span<(short rowShift, short columnShift)> directions = [(-1, 0), (0, -1), (1, 0), (0, 1)];

        while (queue.Count > 0)
        {
            var (row, column, keysMask, length) = queue.Dequeue();
            var keys = keysMask;

            if (char.IsLetter(grid[row][column]) && char.IsLower(grid[row][column]))
            {
                int bit = 1 << (grid[row][column] - 'a');
                if ((keys & bit) == 0)
                {
                    int newKeys = keys | bit;
                    visited[row, column, newKeys] = true;
                    queue.Enqueue((row, column, newKeys, length));

                    if (newKeys == allKeysMask)
                        return length;

                    keys = newKeys;
                }
            }

            if (char.IsLetter(grid[row][column]) &&
                char.IsUpper(grid[row][column]) &&
                (keys & (1 << (grid[row][column] - 'A'))) == 0)
                continue;

            foreach (var (rowShift, columnShift) in directions)
            {
                var shiftedRow = row + rowShift;
                var shiftedColumn = column + columnShift;

                if (shiftedRow < 0 ||
                    shiftedColumn < 0 ||
                    shiftedRow >= grid.Length ||
                    shiftedColumn >= grid[shiftedRow].Length ||
                    grid[shiftedRow][shiftedColumn] == '#' ||
                    visited[shiftedRow, shiftedColumn, keys])
                    continue;

                visited[shiftedRow, shiftedColumn, keys] = true;
                queue.Enqueue((shiftedRow, shiftedColumn, keys, length + 1));
            }
        }

        return -1;
    }


    public int OrangesRotting(int[][] grid)
    {
        int freshOranges = 0;
        var queue = new Queue<(int row, int column)>();

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 1) freshOranges++;
                if (grid[i][j] == 2) queue.Enqueue((i, j));
            }
        }

        if (freshOranges == 0) return 0;

        Span<(short rowShift, short columnShift)> directions = [(-1, 0), (0, -1), (1, 0), (0, 1)];

        int minutes = 0;

        while (queue.Count > 0)
        {
            var size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                var (row, column) = queue.Dequeue();

                foreach (var (rowShift, columnShift) in directions)
                {
                    var shiftedRow = row + rowShift;
                    var shiftedColumn = column + columnShift;

                    if (shiftedRow < 0 ||
                        shiftedRow >= grid.Length ||
                        shiftedColumn < 0 ||
                        shiftedColumn >= grid[shiftedRow].Length ||
                        grid[shiftedRow][shiftedColumn] != 1)
                        continue;

                    freshOranges--;
                    grid[shiftedRow][shiftedColumn] = 2;
                    queue.Enqueue((shiftedRow, shiftedColumn));
                }
            }

            minutes++;
        }

        return freshOranges == 0 ? minutes - 1 : -1;
    }

    public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
    {
        var cct = new CriticalConnectionsTarjian(n, connections);
        var criticalConnections = cct.FindConnections();
        return criticalConnections;
    }

    private class CriticalConnectionsTarjian
    {
        private readonly IList<int>[] _graph;
        private int _time;
        private readonly int[] _identifiers;
        private readonly int[] _low;
        private readonly List<IList<int>> _connections;

        public CriticalConnectionsTarjian(int n, IList<IList<int>> connections)
        {
            _graph = new List<int>[n];
            _identifiers = new int[n];
            _low = new int[n];
            _connections = [];

            for (int i = 0; i < n; i++)
            {
                _graph[i] = [];
                _identifiers[i] = -1;
            }

            foreach (var connection in connections)
            {
                _graph[connection[0]].Add(connection[1]);
                _graph[connection[1]].Add(connection[0]);
            }
        }

        public List<IList<int>> FindConnections()
        {
            for (int i = 0; i < _identifiers.Length; i++)
            {
                if (_identifiers[i] == -1) DFS(i, -1);
            }

            return _connections;
        }

        private void DFS(int node, int parent)
        {
            _identifiers[node] = _low[node] = _time++;

            foreach (int neighbor in _graph[node])
            {
                if (neighbor == parent) continue;
                if (_identifiers[neighbor] == -1)
                {
                    DFS(neighbor, node);
                    _low[node] = Math.Min(_low[node], _low[neighbor]);

                    if (_low[neighbor] > _identifiers[node]) _connections.Add([node, neighbor]);
                }
                else
                {
                    _low[node] = Math.Min(_low[node], _identifiers[neighbor]);
                }
            }
        }
    }

    public bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        var roomsCount = rooms.Count;
        var visitedRooms = 0;

        var visited = new HashSet<int>(roomsCount);
        var queue = new Queue<int>();
        queue.Enqueue(0);
        visited.Add(0);

        while (queue.Count > 0)
        {
            var room = queue.Dequeue();
            visitedRooms++;

            foreach (var next in rooms[room])
            {
                if (visited.Contains(next)) continue;
                queue.Enqueue(next);
                visited.Add(next);
            }
        }

        return visitedRooms == roomsCount;
    }
    public bool IsBipartite(int[][] graph)
    {
        int n = graph.Length;
        int[] colors = new int[n];

        var queue = new Queue<int>();

        for (int i = 0; i < n; i++)
        {
            if (colors[i] != 0) continue;

            queue.Enqueue(i);
            colors[i] = 1;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var color = colors[node];

                foreach (var neighbor in graph[node])
                {
                    if (colors[neighbor] == color) return false;
                    if (colors[neighbor] == 0)
                    {
                        colors[neighbor] = -color;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return true;
    }

    public bool JudgeCircle(string moves)
    {
        var x = 0;
        var y = 0;

        foreach (var l in moves)
        {
            if (l == 'U')
            {
                y++;
            }
            else if (l == 'D')
            {
                y--;
            }
            else if (l == 'L')
            {
                x--;
            }
            else if (l == 'R')
            {
                x++;
            }
        }

        return x == 0 && y == 0;
    }

    public int MinCostConnectPoints(int[][] points)
    {
        var n = points.Length;
        var pq = new PriorityQueue<(int u, int v), int>();
        var dsu = new MinCostConnectPointsDSU(n);

        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                int cost = Math.Abs(points[i][0] - points[j][0]) +
                           Math.Abs(points[i][1] - points[j][1]);
                pq.Enqueue((i, j), cost);
            }

        int result = 0;
        int edgesUsed = 0;

        while (pq.Count > 0 && edgesUsed < n - 1)
        {
            pq.TryDequeue(out var edge, out int cost);
            if (dsu.Union(edge.u, edge.v))
            {
                result += cost;
                edgesUsed++;
            }
        }

        return result;
    }

    private class MinCostConnectPointsDSU
    {
        private readonly int[] _parents;

        public MinCostConnectPointsDSU(int n)
        {
            _parents = new int[n];

            for (int i = 0; i < n; i++)
            {
                _parents[i] = i;
            }
        }

        public bool Union(int node1, int node2)
        {
            var root1 = Find(node1);
            var root2 = Find(node2);

            if (root1 != root2)
            {
                _parents[root1] = root2;
                return true;
            }

            return false;
        }

        public int Find(int node)
        {
            if (_parents[node] != node)
                _parents[node] = Find(_parents[node]);

            return _parents[node];
        }
    }

    public string SmallestStringWithSwaps(string s, IList<IList<int>> pairs)
    {
        int n = s.Length;
        var dsu = new SmallestStringWithSwapsDSU(n);

        foreach (var pair in pairs)
            dsu.Union(pair[0], pair[1]);

        var groups = new Dictionary<int, List<int>>();

        for (int i = 0; i < n; i++)
        {
            int root = dsu.Find(i);

            if (!groups.ContainsKey(root))
                groups[root] = [];

            groups[root].Add(i);
        }

        var result = new char[n];
        foreach (var group in groups.Values)
        {
            var chars = group.Select(i => s[i]).OrderBy(c => c).ToList();
            var indices = group.OrderBy(i => i).ToList();

            for (int i = 0; i < indices.Count; i++)
                result[indices[i]] = chars[i];
        }

        return new string(result);
    }

    private class SmallestStringWithSwapsDSU
    {
        private readonly int[] _parents;
        private readonly int[] _ranks;

        public SmallestStringWithSwapsDSU(int n)
        {
            _parents = new int[n];
            _ranks = new int[n];

            for (int i = 0; i < n; i++)
            {
                _parents[i] = i;
                _ranks[i] = 1;
            }
        }

        public void Union(int node1, int node2)
        {
            var root1 = Find(node1);
            var root2 = Find(node2);

            if (root1 == root2) return;

            if (_ranks[root1] < _ranks[root2])
            {
                _parents[root2] = root1;
                _ranks[root1] += _ranks[root2];
            }
            else
            {
                _parents[root1] = root2;
                _ranks[root2] += _ranks[root1];
            }
        }

        public int Find(int node)
        {
            if (_parents[node] != node)
                _parents[node] = Find(_parents[node]);

            return _parents[node];
        }
    }

    public int MakeConnected(int n, int[][] connections)
    {
        if (connections.Length < n - 1) return -1;

        var dsu = new MakeConnectedDSU(n);

        foreach (var connection in connections)
        {
            dsu.Union(connection[0], connection[1]);
        }

        return dsu.GetRequiredConnections() - 1;
    }

    private class MakeConnectedDSU
    {
        private readonly int[] _parents;
        private readonly int[] _sizes;

        public MakeConnectedDSU(int n)
        {
            _parents = new int[n];
            _sizes = new int[n];

            for (int i = 0; i < n; i++)
            {
                _parents[i] = i;
                _sizes[i] = 1;
            }
        }

        public int GetRequiredConnections()
        {
            var roots = new HashSet<int>();

            for (int i = 0; i < _parents.Length; i++)
            {
                roots.Add(Find(i));
            }

            return roots.Count;
        }

        public void Union(int node1, int node2)
        {
            var root1 = Find(node1);
            var root2 = Find(node2);

            if (root1 == root2) return;

            if (_sizes[root1] < _sizes[root2])
            {
                _parents[root1] = root2;
                _sizes[root2] += _sizes[root1];
            }
            else
            {
                _parents[root2] = root1;
                _sizes[root1] += _sizes[root2];
            }
        }

        private int Find(int node)
        {
            if (_parents[node] != node)
                _parents[node] = Find(_parents[node]);

            return _parents[node];
        }
    }

    public int LongestConsecutive(int[] nums)
    {
        if (nums.Length == 0) return 0;

        var parents = new Dictionary<int, int>();
        var size = new Dictionary<int, int>();

        void Union(int node1, int node2)
        {
            var root1 = Find(node1);
            var root2 = Find(node2);

            if (root1 == root2) return;

            if (size[root1] < size[root2])
            {
                Console.WriteLine($"Changing parents {parents[root1]} on {root2}");
                parents[root1] = root2;
                Console.WriteLine($"Changing size {size[root2]} + {size[root1]}");
                size[root2] += size[root1];
            }
            else
            {
                Console.WriteLine($"Changing parents {parents[root2]} on {root1}");
                parents[root2] = root1;
                Console.WriteLine($"Changing size {size[root1]} + {size[root2]}");
                size[root1] += size[root2];
            }
        }

        int Find(int node)
        {
            if (parents[node] != node) parents[node] = Find(parents[node]);
            return parents[node];
        }

        foreach (int num in nums)
        {
            Console.Write("Parents\t");
            DisplayDictionary(parents);

            Console.Write("Sizes\t");
            DisplayDictionary(size);

            if (parents.ContainsKey(num)) continue;

            parents[num] = num;
            size[num] = 1;

            if (parents.ContainsKey(num - 1))
            {
                Console.WriteLine($"Found {num} - 1");
                Union(num, num - 1);
            }

            if (parents.ContainsKey(num + 1))
            {
                Console.WriteLine($"Found {num} + 1");
                Union(num, num + 1);
            }
        }

        return size.Values.Max();
    }

    private void DisplayDictionary(IDictionary<int, int> dictionary)
    {
        foreach (var pair in dictionary)
        {
            Console.Write($"[{pair.Key}-{pair.Value}]");
        }
        Console.WriteLine();
    }

    public int[] FindRedundantConnection(int[][] edges)
    {
        var dsu = new FindRedundantConnectionDSU(edges.Length);

        for (int i = 0; i < edges.Length; i++)
        {
            if (!dsu.Union(edges[i][0], edges[i][1]))
            {
                return [edges[i][0], edges[i][1]];
            }
        }

        return [0];
    }

    private class FindRedundantConnectionDSU
    {
        private readonly int[] parents;
        private readonly int[] ranks;

        public FindRedundantConnectionDSU(int size)
        {
            parents = new int[size + 1];
            ranks = new int[size + 1];

            for (int i = 0; i < size + 1; i++)
            {
                parents[i] = i;
            }
        }

        public bool Union(int node1, int node2)
        {
            var root1 = Find(node1);
            var root2 = Find(node2);

            if (root1 == root2)
            {
                return false;
            }

            if (ranks[root1] < ranks[root2])
            {
                parents[root1] = root2;
                ranks[root1] += ranks[root2];
            }
            else
            {
                parents[root2] = root1;
                ranks[root2] += ranks[root1];
            }

            return true;
        }

        public int Find(int node)
        {
            if (parents[node] != node)
                parents[node] = Find(parents[node]);

            return parents[node];
        }
    }

    public int FindCircleNum(int[][] isConnected)
    {
        int n = isConnected.Length;
        var dsu = new FindCircleNumDSU(n);

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (isConnected[i][j] == 1) dsu.Union(i, j);
            }
        }

        var roots = new HashSet<int>();
        for (int i = 0; i < n; i++)
            roots.Add(dsu.Find(i));

        return roots.Count;
    }

    private class FindCircleNumDSU
    {
        private readonly int[] parents;
        private readonly int[] ranks;

        public FindCircleNumDSU(int n)
        {
            parents = new int[n];
            ranks = new int[n];

            for (int i = 0; i < n; i++)
            {
                parents[i] = i;
                ranks[i] = 1;
            }
        }

        public void Union(int node1, int node2)
        {
            var root1 = Find(node1);
            var root2 = Find(node2);

            if (root1 == root2) return;

            if (ranks[root1] < ranks[root2])
            {
                parents[root1] = root2;
            }
            else if (ranks[root1] > ranks[root2])
            {
                parents[root2] = root1;
            }
            else
            {
                parents[root2] = root1;
                ranks[root1]++;
            }
        }

        public int Find(int node)
        {
            if (parents[node] != node)
                parents[node] = Find(parents[node]);

            return parents[node];
        }
    }

    public IList<int> EventualSafeNodes(int[][] graph)
    {
        var myGraph = new List<int>[graph.Length];
        var degree = new int[graph.Length];

        for (int i = 0; i < graph.Length; i++)
        {
            myGraph[i] = [];
        }

        for (int i = 0; i < graph.Length; i++)
        {
            for (int j = 0; j < graph[i].Length; j++)
            {
                myGraph[graph[i][j]].Add(i);
            }

            degree[i] = graph[i].Length;
        }

        var queue = new Queue<int>();

        for (int i = 0; i < degree.Length; i++)
        {
            if (degree[i] == 0) queue.Enqueue(i);
        }

        var result = new List<int>();

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            foreach (var neighbor in myGraph[node])
            {
                degree[neighbor]--;

                if (degree[neighbor] == 0)
                    queue.Enqueue(neighbor);
            }
        }

        for (int i = 0; i < degree.Length; i++)
        {
            if (degree[i] == 0) result.Add(i);
        }

        return result;
    }

    public IList<int> FindMinHeightTrees(int n, int[][] edges)
    {
        if (n == 1) return [0];

        var graph = new List<int>[n];
        var inDegree = new int[n];

        for (int i = 0; i < n; i++)
        {
            graph[i] = [];
        }

        foreach (var edge in edges)
        {
            graph[edge[0]].Add(edge[1]);
            graph[edge[1]].Add(edge[0]);
            inDegree[edge[0]]++;
            inDegree[edge[1]]++;
        }

        var queue = new Queue<int>();

        for (int i = 0; i < inDegree.Length; i++)
        {
            if (inDegree[i] == 1) queue.Enqueue(i);
        }

        var remaming = n;

        while (remaming > 2 && queue.Count > 0)
        {
            var size = queue.Count;
            remaming -= size;

            for (int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                foreach (var next in graph[node])
                {
                    inDegree[next]--;
                    if (inDegree[next] == 1) queue.Enqueue(next);
                }
            }
        }

        return [.. queue];
    }

    public IList<string> FindAllRecipes(string[] recipes, IList<IList<string>> ingredients, string[] supplies)
    {
        if (recipes.Length == 0 || supplies.Length == 0) return [];

        var graph = new Dictionary<string, List<string>>();
        var inDegree = new Dictionary<string, int>();

        for (int i = 0; i < recipes.Length; i++)
        {
            var recipe = recipes[i];
            inDegree[recipe] = ingredients[i].Count;

            foreach (var ingredient in ingredients[i])
            {
                if (!graph.ContainsKey(ingredient)) graph.Add(ingredient, []);
                graph[ingredient].Add(recipe);
            }
        }

        var result = new List<string>();
        var queue = new Queue<string>();

        foreach (var supply in supplies)
        {
            queue.Enqueue(supply);
        }

        while (queue.Count > 0)
        {
            var ingridient = queue.Dequeue();

            if (!graph.ContainsKey(ingridient)) continue;

            foreach (var recipe in graph[ingridient])
            {
                inDegree[recipe]--;
                if (inDegree[recipe] == 0)
                {
                    result.Add(recipe);
                    queue.Enqueue(recipe);
                }
            }
        }

        return result;
    }

    public IList<bool> CheckIfPrerequisite(int numCourses, int[][] prerequisites, int[][] queries)
    {
        var graph = new List<int>[numCourses];

        for (int i = 0; i < numCourses; i++)
        {
            graph[i] = [];
        }

        foreach (var p in prerequisites)
        {
            graph[p[0]].Add(p[1]);
        }

        var queue = new Queue<int>();
        var result = new bool[queries.Length];

        for (int i = 0; i < queries.Length; i++)
        {
            var from = queries[i][0];
            var to = queries[i][1];
            queue.Enqueue(from);

            var visited = new bool[numCourses];

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (var next in graph[node])
                {
                    if (visited[next]) continue;
                    if (next == to)
                    {
                        result[i] = true;
                        queue.Clear();
                        break;
                    }

                    queue.Enqueue(next);
                    visited[next] = true;
                }
            }
        }

        return result;
    }

    public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
    {
        var graph = new List<(int node, int cost)>[n];

        for (int i = 0; i < n; i++)
        {
            graph[i] = [];
        }

        foreach (var flight in flights)
        {
            graph[flight[0]].Add((flight[1], flight[2]));
        }

        var queue = new PriorityQueue<(int node, int stops), int>();
        queue.Enqueue((src, -1), 0);

        var visited = new int[n, k + 1];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= k; j++)
            {
                visited[i, j] = int.MaxValue;
            }
        }

        while (queue.Count > 0)
        {
            queue.TryDequeue(out var current, out var currentCost);

            if (current.node == dst)
                return currentCost;

            foreach (var (node, cost) in graph[current.node])
            {
                var stopsCount = current.stops + 1;
                var newCost = currentCost + cost;

                if (stopsCount > k) continue;

                if (newCost < visited[node, stopsCount])
                {
                    queue.Enqueue((node, stopsCount), newCost);
                    visited[node, stopsCount] = newCost;
                }
            }
        }

        return -1;
    }

    public double MaxProbability(int n, int[][] edges, double[] succProb, int start_node, int end_node)
    {
        var graph = new List<(int node, double success)>[n];
        var visited = new double[n];

        for (int i = 0; i < n; i++)
        {
            graph[i] = [];
        }

        for (int i = 0; i < edges.Length; i++)
        {
            graph[edges[i][0]].Add((edges[i][1], succProb[i]));
            graph[edges[i][1]].Add((edges[i][0], succProb[i]));
        }

        var queue = new PriorityQueue<int, double>();
        queue.Enqueue(start_node, -1D);
        visited[start_node] = double.MinValue;

        while (queue.Count > 0)
        {
            queue.TryDequeue(out var currentNode, out var success);

            if (currentNode == end_node)
            {
                return Math.Abs(success);
            }

            foreach (var neighbor in graph[currentNode])
            {
                var newSuccess = success * neighbor.success;
                if (visited[neighbor.node] <= newSuccess) continue;

                queue.Enqueue(neighbor.node, newSuccess);
                visited[neighbor.node] = newSuccess;
            }
        }

        return 0;
    }

    public int MinimumEffortPath(int[][] heights)
    {
        var efforts = new int[heights.Length, heights[0].Length];

        for (int i = 0; i < heights.Length; i++)
        {
            for (int j = 0; j < heights[i].Length; j++)
            {
                efforts[i, j] = int.MaxValue;
            }
        }

        efforts[0, 0] = 0;

        var queue = new PriorityQueue<(int row, int column), int>();
        queue.Enqueue((0, 0), 0);

        Span<(short rowShift, short columnShift)> directions = [(-1, 0), (0, -1), (1, 0), (0, 1)];

        while (queue.Count > 0)
        {
            queue.TryDequeue(out (int row, int column) node, out var effort);

            if (node.row == heights.Length - 1 && node.column == heights[node.row].Length - 1)
            {
                return effort;
            }

            foreach (var (rowShift, columnShift) in directions)
            {
                var shiftedRow = rowShift + node.row;
                var shiftedColumn = columnShift + node.column;

                if (shiftedRow < 0 ||
                    shiftedRow >= heights.Length ||
                    shiftedColumn < 0 ||
                    shiftedColumn >= heights[shiftedRow].Length)
                    continue;

                var shiftedEffort = Math.Abs(heights[node.row][node.column] - heights[shiftedRow][shiftedColumn]);

                if (shiftedEffort >= efforts[shiftedRow, shiftedColumn])
                    continue;

                queue.Enqueue((shiftedRow, shiftedColumn), Math.Max(shiftedEffort, effort));
                efforts[shiftedRow, shiftedColumn] = Math.Max(shiftedEffort, effort);
            }
        }

        return -1;
    }

    public int NetworkDelayTime(int[][] times, int n, int k)
    {
        var graph = new List<(int node, int distance)>[n + 1];
        var distances = new int[n + 1];

        for (int i = 0; i <= n; i++)
        {
            graph[i] = [];
        }

        Array.Fill(distances, int.MaxValue);

        distances[k] = 0;

        foreach (var time in times)
        {
            graph[time[0]].Add((time[1], time[2]));
        }

        var queue = new Queue<(int currentNode, int currentDistance)>();
        queue.Enqueue((k, 0));

        while (queue.Count > 0)
        {
            var (currentNode, currentDistance) = queue.Dequeue();
            if (currentDistance > distances[currentNode]) continue;

            foreach (var (node, distance) in graph[currentNode])
            {
                var newDistance = currentDistance + distance;
                if (newDistance >= distances[node]) continue;
                distances[node] = newDistance;
                queue.Enqueue((node, newDistance));
            }
        }

        var max = 0;

        for (int i = 1; i <= n; i++)
        {
            if (distances[i] == int.MaxValue) return -1;
            max = Math.Max(max, distances[i]);
        }

        return max;
    }

    public int NumIslands(char[][] grid)
    {
        var result = 0;
        var visited = new bool[grid.Length, grid[0].Length];
        var queue = new Queue<(int row, int column)>();

        Span<(short rowShift, short columnShift)> directions = [(-1, 0), (0, -1), (1, 0), (0, 1)];

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '1' && !visited[i, j])
                {
                    result++;
                    queue.Enqueue((i, j));
                    visited[i, j] = true;

                    while (queue.Count > 0)
                    {
                        var (row, column) = queue.Dequeue();

                        foreach (var (rowShift, columnShift) in directions)
                        {
                            var shiftedRow = row + rowShift;
                            var shiftedColumn = column + columnShift;

                            if (shiftedRow < 0 ||
                                shiftedRow >= grid.Length ||
                                shiftedColumn < 0 ||
                                shiftedColumn >= grid[0].Length ||
                                grid[shiftedRow][shiftedColumn] == '0' ||
                                visited[shiftedRow, shiftedColumn])
                                continue;

                            queue.Enqueue((shiftedRow, shiftedColumn));
                            visited[shiftedRow, shiftedColumn] = true;
                        }
                    }
                }
            }
        }

        return result;
    }

    public int ShortestPath(int[][] grid, int k)
    {
        var visited = new int[grid.Length, grid[0].Length, k + 1];

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                for (int h = 0; h <= k; h++)
                {
                    visited[i, j, h] = int.MaxValue;
                }
            }
        }

        var queue = new Queue<(int row, int column, int brokenWalls)>();
        queue.Enqueue((0, 0, 0));
        visited[0, 0, 0] = 0;

        Span<(short rowShift, short columnShift)> directions = [(-1, 0), (0, -1), (1, 0), (0, 1)];

        while (queue.Count > 0)
        {
            var (row, column, brokenWalls) = queue.Dequeue();
            var steps = visited[row, column, brokenWalls];

            if (row == grid.Length - 1 && column == grid[row].Length - 1)
            {
                return steps;
            }

            foreach (var (rowShift, columnShift) in directions)
            {
                var shiftedRow = row + rowShift;
                var shiftedColumn = column + columnShift;

                if (shiftedRow < 0 || shiftedRow >= grid.Length || shiftedColumn < 0 || shiftedColumn >= grid[0].Length)
                    continue;

                int newBrokenWalls = brokenWalls + (grid[shiftedRow][shiftedColumn] == 1 ? 1 : 0);

                if (newBrokenWalls > k)
                    continue;

                if (steps + 1 >= visited[shiftedRow, shiftedColumn, newBrokenWalls])
                    continue;

                visited[shiftedRow, shiftedColumn, newBrokenWalls] = steps + 1;
                queue.Enqueue((shiftedRow, shiftedColumn, newBrokenWalls));
            }
        }

        return -1;
    }

    public string SmallestEquivalentString(string s1, string s2, string baseStr)
    {
        var parents = new int[26];

        for (int i = 0; i < parents.Length; i++)
        {
            parents[i] = i;
        }

        for (int i = 0; i < s1.Length; i++)
        {
            SmallestEquivalentStringUnion(s1[i] - 'a', s2[i] - 'a', parents);
        }

        Span<char> span = stackalloc char[baseStr.Length];

        for (int i = 0; i < baseStr.Length; i++)
        {
            var root = SmallestEquivalentStringFind(baseStr[i] - 'a', parents);
            span[i] = (char)(root + 'a');
        }

        return new string(span);
    }

    private void SmallestEquivalentStringUnion(int node1, int node2, int[] parents)
    {
        var root1 = SmallestEquivalentStringFind(node1, parents);
        var root2 = SmallestEquivalentStringFind(node2, parents);

        if (root1 == root2) return;

        if (root1 < root2) parents[root2] = root1;
        else parents[root1] = root2;
    }

    private int SmallestEquivalentStringFind(int node, int[] parents)
    {
        if (parents[node] == node) return node;
        return SmallestEquivalentStringFind(parents[node], parents);
    }

    public bool EquationsPossible(string[] equations)
    {
        var parents = new int[26];
        var ranks = new int[26];

        for (int i = 0; i < parents.Length; i++)
        {
            parents[i] = i;
        }

        foreach (var equation in equations)
        {
            if (equation[1] == '=')
            {
                EquationsPossibleUnion(parents[equation[0] - 'a'], parents[equation[3] - 'a'], parents, ranks);
            }
        }

        foreach (var equation in equations)
        {
            if (equation[1] == '!')
            {
                var node1 = EquationsPossibleFind(parents[equation[0] - 'a'], parents);
                var node2 = EquationsPossibleFind(parents[equation[3] - 'a'], parents);

                if (EquationsPossibleFind(node1, parents) == EquationsPossibleFind(node2, parents))
                    return false;
            }
        }

        return true;
    }

    private void EquationsPossibleUnion(int node1, int node2, int[] parents, int[] ranks)
    {
        var root1 = EquationsPossibleFind(node1, parents);
        var root2 = EquationsPossibleFind(node2, parents);

        if (root1 == root2) return;

        if (ranks[root1] < ranks[root2]) parents[root1] = root2;
        else if (ranks[root1] > ranks[root2]) parents[root2] = root1;
        else
        {
            parents[root2] = root1;
            ranks[root1]++;
        }
    }

    private int EquationsPossibleFind(int node, int[] parents)
    {
        if (parents[node] == node) return parents[node];
        return EquationsPossibleFind(parents[node], parents);
    }

    public bool AreSimilar(int[][] mat, int k)
    {
        var arr = new int[mat.Length][];

        for (int i = 0; i < mat.Length; i++)
        {
            arr[i] = new int[mat[i].Length];
            Array.Copy(mat[i], arr[i], mat[i].Length);
        }

        for (int i = 0; i < k; i++)
        {
            for (int row = 0; row < arr.Length; row++)
            {
                if (row % 2 == 0)
                {
                    var last = arr[row][0];
                    for (int c = 0; c < arr[row].Length - 1; c++)
                    {
                        arr[row][c] = arr[row][c + 1];
                    }

                    arr[row][arr[row].Length - 1] = last;
                }
                else
                {
                    var first = arr[row][arr[row].Length - 1];
                    for (int c = arr[row].Length - 1; c >= 1; c--)
                    {
                        arr[row][c] = arr[row][c - 1];
                    }

                    arr[row][0] = first;
                }
            }
        }

        for (int row = 0; row < arr.Length; row++)
        {
            for (int column = 0; column < arr[row].Length; column++)
            {
                if (mat[row][column] != arr[row][column])
                    return false;
            }
        }

        return true;
    }


    public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
    {
        var parents = new int[accounts.Count];
        var sizes = new int[accounts.Count];

        var graph = new Dictionary<string, int>();

        var result = new List<IList<string>>();

        for (int i = 0; i < parents.Length; i++)
        {
            parents[i] = i;
            sizes[i] = i;
        }

        for (int i = 0; i < accounts.Count; i++)
        {
            for (int j = 1; j < accounts[i].Count; j++)
            {
                if (!graph.ContainsKey(accounts[i][j]))
                {
                    graph.Add(accounts[i][j], i);
                }
                else
                {
                    AccountsMergeUnion(graph[accounts[i][j]], i, parents, sizes);
                }
            }
        }

        var dict = new Dictionary<int, HashSet<string>>();

        foreach (var pair in graph)
        {
            var parentIndex = AccountsMergeFind(pair.Value, parents);

            if (!dict.ContainsKey(parentIndex)) dict[parentIndex] = [];
            dict[parentIndex].Add(pair.Key);
        }

        foreach (var pair in dict)
        {
            var list = new List<string>
            {
                accounts[pair.Key][0]
            };

            list.AddRange(pair.Value.Order(StringComparer.Ordinal));
            result.Add(list);
        }

        return result;
    }

    private void AccountsMergeUnion(int node1, int node2, int[] parents, int[] sizes)
    {
        int root1 = AccountsMergeFind(node1, parents);
        int root2 = AccountsMergeFind(node2, parents);

        if (root1 == root2) return;

        if (sizes[root1] < sizes[root2])
        {
            parents[root1] = root2;
            sizes[root2] += sizes[root1];
        }
        else
        {
            parents[root2] = root1;
            sizes[root1] += sizes[root2];
        }
    }

    private int AccountsMergeFind(int node, int[] parents)
    {
        if (parents[node] != node)
        {
            parents[node] = AccountsMergeFind(parents[node], parents);
        }

        return parents[node];
    }

    public int MaximalNetworkRank(int n, int[][] roads)
    {
        var graph = new HashSet<int>[n];

        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = [];
        }

        foreach (var road in roads)
        {
            graph[road[0]].Add(road[1]);
            graph[road[1]].Add(road[0]);
        }

        var max = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                var rank = graph[i].Count + graph[j].Count;

                if (graph[i].Contains(j)) rank--;

                max = Math.Max(max, rank);
            }
        }

        return max;
    }

    public IList<int> FindSmallestSetOfVertices(int n, IList<IList<int>> edges)
    {
        var result = new List<int>();
        var parents = new bool[n];

        foreach (var edge in edges)
        {
            parents[edge[1]] = true;
        }

        for (int i = 0; i < parents.Length; i++)
        {
            if (!parents[i]) result.Add(i);
        }

        return result;
    }

    public int FindJudge(int n, int[][] trust)
    {
        var trusts = new int[n + 1];
        var trustedBy = new int[n + 1];

        foreach (var i in trust)
        {
            trusts[i[0]]++;
            trustedBy[i[1]]++;
        }

        for (var i = 1; i <= n; i++)
        {
            if (trusts[i] == 0 && trustedBy[i] == n - 1)
            {
                return i;
            }
        }

        return -1;
    }

    public int ClosedIsland(int[][] grid)
    {
        var islandCount = 0;

        for (int i = 0; i < grid.Length; i++)
            if (grid[i][0] == 0)
                ClosedIslandDFS(grid, i, 0);

        for (int i = 0; i < grid.Length; i++)
            if (grid[i][^1] == 0)
                ClosedIslandDFS(grid, i, grid[i].Length - 1);

        for (int i = 0; i < grid[0].Length; i++)
            if (grid[0][i] == 0)
                ClosedIslandDFS(grid, 0, i);

        for (int i = 0; i < grid[0].Length; i++)
            if (grid[^1][i] == 0)
                ClosedIslandDFS(grid, grid.Length - 1, i);

        for (int row = 0; row < grid.Length; row++)
        {
            for (int column = 0; column < grid[row].Length; column++)
            {
                if (grid[row][column] == 0)
                {
                    ClosedIslandDFS(grid, row, column);
                    islandCount++;
                }
            }
        }

        return islandCount;
    }

    private void ClosedIslandDFS(int[][] grid, int row, int column)
    {
        if (row < 0 || row >= grid.Length || column < 0 || column >= grid[row].Length)
        {
            return;
        }

        if (grid[row][column] == 1 || grid[row][column] == 2)
        {
            return;
        }

        grid[row][column] = 2;

        ClosedIslandDFS(grid, row + 1, column);
        ClosedIslandDFS(grid, row - 1, column);
        ClosedIslandDFS(grid, row, column + 1);
        ClosedIslandDFS(grid, row, column - 1);
    }

    public int NumEnclaves(int[][] grid)
    {
        var result = 0;

        for (int i = 0; i < grid.Length; i++)
            if (grid[i][0] == 1)
                NumEnclavesDFS(grid, i, 0);

        for (int i = 0; i < grid.Length; i++)
            if (grid[i][^1] == 1)
                NumEnclavesDFS(grid, i, grid[i].Length - 1);

        for (int i = 0; i < grid[0].Length; i++)
            if (grid[0][i] == 1)
                NumEnclavesDFS(grid, 0, i);

        for (int i = 0; i < grid[0].Length; i++)
            if (grid[^1][i] == 1)
                NumEnclavesDFS(grid, grid.Length - 1, i);

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
                if (grid[i][j] == 1) result++;
        }

        return result;
    }

    private void NumEnclavesDFS(int[][] grid, int row, int column)
    {
        if (row < 0 || row >= grid.Length || column < 0 || column >= grid[row].Length)
        {
            return;
        }

        if (grid[row][column] == 0) return;

        grid[row][column] = 0;

        NumEnclavesDFS(grid, row + 1, column);
        NumEnclavesDFS(grid, row - 1, column);
        NumEnclavesDFS(grid, row, column + 1);
        NumEnclavesDFS(grid, row, column - 1);
    }

    public IList<int> DistanceK(TreeNode root, TreeNode target, int k)
    {
        if (root == null) return [];

        var graph = new Dictionary<int, List<int>>();

        var graphQueue = new Queue<TreeNode>();
        graphQueue.Enqueue(root);

        while (graphQueue.Count > 0)
        {
            var node = graphQueue.Dequeue();
            if (!graph.ContainsKey(node.val)) graph[node.val] = [];

            if (node.left != null)
            {
                if (!graph.ContainsKey(node.left.val)) graph[node.left.val] = [];
                graph[node.left.val].Add(node.val);
                graph[node.val].Add(node.left.val);
                graphQueue.Enqueue(node.left);
            }

            if (node.right != null)
            {
                if (!graph.ContainsKey(node.right.val)) graph[node.right.val] = [];
                graph[node.right.val].Add(node.val);
                graph[node.val].Add(node.right.val);
                graphQueue.Enqueue(node.right);
            }
        }

        var queue = new Queue<int>();
        queue.Enqueue(target.val);

        var visited = new HashSet<int>();

        var length = 0;

        while (queue.Count > 0 && length < k)
        {
            var size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                var currentNode = queue.Dequeue();

                visited.Add(currentNode);

                foreach (var neighbor in graph[currentNode])
                {
                    if (!visited.Contains(neighbor)) queue.Enqueue(neighbor);
                }
            }

            length++;
        }

        return [.. queue];
    }

    public int ShortestPathBinaryMatrix(int[][] grid)
    {
        var result = -1;

        if (grid[0][0] != 0) return result;

        var queue = new Queue<(int row, int column, int length)>();
        queue.Enqueue((0, 0, 1));

        Span<(short rowShift, short columnShift)> directions =
        [
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1),
            (1, 1),
            (-1, 1),
            (1, -1),
            (-1, -1)

        ];

        while (queue.Count > 0)
        {
            var (row, column, length) = queue.Dequeue();

            if (row == grid.Length - 1 && column == grid.Length - 1)
            {
                return length;
            }

            foreach (var (rowShift, columnShift) in directions)
            {
                var shiftedRow = row + rowShift;
                var shiftedColumn = column + columnShift;

                if (shiftedRow < 0 ||
                    shiftedColumn < 0 ||
                    shiftedRow >= grid.Length ||
                    shiftedColumn >= grid[shiftedRow].Length ||
                    grid[shiftedRow][shiftedColumn] == 1)
                    continue;

                queue.Enqueue((shiftedRow, shiftedColumn, length + 1));
                grid[shiftedRow][shiftedColumn] = 1;
            }
        }

        return result;
    }

    public int[][] UpdateMatrix(int[][] mat)
    {
        var queue = new Queue<(int row, int column)>();

        for (int row = 0; row < mat.Length; row++)
        {
            for (int column = 0; column < mat[row].Length; column++)
            {
                if (mat[row][column] == 0) queue.Enqueue((row, column));
                else mat[row][column] = int.MaxValue;
            }
        }

        Span<(short rowShift, short columnShift)> directions = [(-1, 0), (0, -1), (1, 0), (0, 1)];

        while (queue.Count > 0)
        {
            var (row, column) = queue.Dequeue();

            foreach (var (rowShift, columnShift) in directions)
            {
                var shiftedRow = row + rowShift;
                var shiftedColumn = column + columnShift;

                if (shiftedRow >= 0 &&
                    shiftedColumn >= 0 &&
                    shiftedRow < mat.Length &&
                    shiftedColumn < mat[shiftedRow].Length &&
                    mat[shiftedRow][shiftedColumn] > mat[shiftedRow][shiftedColumn] + 1)
                {
                    queue.Enqueue((shiftedRow, shiftedColumn));
                    mat[shiftedRow][shiftedColumn] = mat[row][column] + 1;
                }
            }
        }

        return mat;
    }

    public bool CanReach(int[] arr, int start)
    {
        var queue = new Queue<int>();
        queue.Enqueue(start);

        var visited = new bool[arr.Length];

        while (queue.Count > 0)
        {
            var index = queue.Dequeue();

            if (visited[index]) continue;
            visited[index] = true;

            var value = arr[index];

            if (value == 0) return true;

            if (index + value < arr.Length)
            {
                queue.Enqueue(value + index);
            }

            if (index - value >= 0)
            {
                queue.Enqueue(index - value);
            }

        }

        return false;
    }

    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        if (beginWord == endWord) return 0;
        if (wordList.Count == 0) return 0;
        var words = wordList.ToHashSet();

        if (!words.Contains(endWord)) return 0;

        var queue = new Queue<(string currentWOrd, int length)>();
        queue.Enqueue((beginWord, 0));

        while (queue.Count > 0)
        {
            var (currentWord, length) = queue.Dequeue();
            if (currentWord == endWord)
                return length;

            foreach (var word in words)
            {
                var difference = 0;
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] != currentWord[i]) difference++;
                }

                if (difference > 1) continue;
                queue.Enqueue((word, length + 1));
                words.Remove(word);
            }
        }

        return 0;
    }

    public int MinMutation(string startGene, string endGene, string[] bank)
    {
        if (bank.Length == 0) return -1;
        var bankSet = new HashSet<string>(bank);
        if (!bank.Contains(endGene)) return -1;
        var genes = new char[] { 'A', 'G', 'T', 'C' };

        var queue = new Queue<(char[] g, int l)>();
        queue.Enqueue(([.. startGene], 0));

        var visited = new HashSet<string>();
        visited.Add(startGene);

        while (queue.Count > 0)
        {
            var (gene, length) = queue.Dequeue();

            for (int i = 0; i < gene.Length; i++)
            {
                var letter = gene[i];
                foreach (var g in genes)
                {
                    if (g == gene[i]) continue;
                    gene[i] = g;

                    var newGene = new string(gene);

                    if (visited.Contains(newGene)) continue;
                    visited.Add(newGene);

                    if (newGene == endGene)
                        return length + 1;

                    if (bankSet.Contains(newGene))
                    {
                        queue.Enqueue(([.. newGene], length + 1));
                    }
                }
                gene[i] = letter;
            }
        }

        return -1;
    }

    public int ShortestBridge(int[][] grid)
    {
        int n = grid.Length;
        bool[,] visited = new bool[n, n];

        Queue<(int row, int column)> queue = new Queue<(int row, int column)>();
        bool found = false;
        var directions = new (short rowShift, short columnShift)[4] { (-1, 0), (0, -1), (1, 0), (0, 1) };


        for (int row = 0; row < n; row++)
        {
            if (found) break;
            for (int column = 0; column < n; column++)
            {
                if (grid[row][column] == 1)
                {
                    ShortestBridgeDFS(grid, visited, queue, row, column);
                    found = true;
                    break;
                }
            }
        }

        int steps = 0;

        while (queue.Count > 0)
        {
            int size = queue.Count;
            for (int i = 0; i < size; i++)
            {
                var (row, column) = queue.Dequeue();

                foreach (var (rowShift, columnShift) in directions)
                {
                    var newRow = rowShift + row;
                    var newColumn = columnShift + column;

                    if (newRow >= 0 && newRow < n && newColumn >= 0 && newColumn < n && !visited[newRow, newColumn])
                    {
                        if (grid[newRow][newColumn] == 1)
                        {
                            return steps;
                        }

                        visited[newRow, newColumn] = true;
                        queue.Enqueue((newRow, newColumn));
                    }
                }
            }
            steps++;
        }

        return -1;
    }

    private void ShortestBridgeDFS(int[][] grid, bool[,] visited, Queue<(int r, int c)> queue, int row, int column)
    {
        int n = grid.Length;
        if (row < 0 || row >= n || column < 0 || column >= n || visited[row, column] || grid[row][column] != 1)
        {
            return;
        }

        visited[row, column] = true;
        queue.Enqueue((row, column));

        ShortestBridgeDFS(grid, visited, queue, row + 1, column);
        ShortestBridgeDFS(grid, visited, queue, row - 1, column);
        ShortestBridgeDFS(grid, visited, queue, row, column + 1);
        ShortestBridgeDFS(grid, visited, queue, row, column - 1);
    }

    public int NearestExit(char[][] maze, int[] entrance)
    {
        var result = int.MaxValue;
        var visited = new bool[maze.Length, maze[0].Length];

        var queue = new Queue<(int row, int column, int length)>();
        queue.Enqueue((entrance[0], entrance[1], -1));

        while (queue.Count > 0)
        {
            var (row, column, length) = queue.Dequeue();

            if (row < 0 || row >= maze.Length || column < 0 || column >= maze[row].Length)
            {
                if (length > 0) result = Math.Min(result, length);
                continue;
            }

            if (visited[row, column]) continue;

            visited[row, column] = true;
            length++;

            if (maze[row][column] == '+') continue;

            queue.Enqueue((row + 1, column, length));
            queue.Enqueue((row - 1, column, length));
            queue.Enqueue((row, column + 1, length));
            queue.Enqueue((row, column - 1, length));
        }

        return result == int.MaxValue ? -1 : result;
    }

    private List<int>[] _graph;
    private int[] _discovery;
    private int[] _lowLink;
    private bool[] _visited;
    private List<IList<int>> _criticalConnections;
    private int _timer;

    public IList<IList<int>> CriticalConnections1(int n, IList<IList<int>> connections)
    {
        _graph = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            _graph[i] = [];
        }

        _discovery = new int[n];
        _lowLink = new int[n];
        _visited = new bool[n];
        _criticalConnections = [];
        _timer = 0;

        foreach (var connection in connections)
        {
            int edge = connection[0];
            int vertex = connection[1];
            _graph[edge].Add(vertex);
            _graph[vertex].Add(edge);
        }

        for (int i = 0; i < n; i++)
        {
            if (!_visited[i])
            {
                CriticalConnectionsDFS(i, -1);
            }
        }

        return _criticalConnections;
    }

    private void CriticalConnectionsDFS(int node, int parentNode)
    {
        _visited[node] = true;
        _discovery[node] = _lowLink[node] = ++_timer;

        foreach (var neighbor in _graph[node])
        {
            if (neighbor == parentNode) continue;

            if (!_visited[neighbor])
            {
                CriticalConnectionsDFS(neighbor, node);

                _lowLink[node] = Math.Min(_lowLink[node], _lowLink[neighbor]);

                if (_lowLink[neighbor] > _discovery[node])
                {
                    _criticalConnections.Add([node, neighbor]);
                }
            }
            else
            {
                _lowLink[node] = Math.Min(_lowLink[node], _discovery[neighbor]);
            }
        }
    }

    public int MinReorder(int n, int[][] connections)
    {
        var result = 0;
        var graph = new List<(int node, bool incomming)>[n];

        for (int i = 0; i < n; i++)
        {
            graph[i] = [];
        }

        foreach (var connection in connections)
        {
            var vertex = connection[0];
            var edge = connection[1];
            graph[vertex].Add((edge, false));
            graph[edge].Add((vertex, true));
        }

        var queue = new Queue<int>();
        queue.Enqueue(0);

        var visited = new bool[n];
        visited[0] = true;

        while (queue.Count > 0)
        {
            var startNode = queue.Dequeue();

            foreach ((int node, bool incoming) in graph[startNode])
            {
                if (visited[node]) continue;
                if (!incoming) result++;

                queue.Enqueue(node);
                visited[node] = true;
            }
        }

        return result;
    }

    public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
    {
        IList<IList<int>> result = new List<IList<int>>();

        AllPathsSourceTargetDFS(graph, 0, [], result);

        return result;
    }

    private void AllPathsSourceTargetDFS(int[][] graph, int node, IList<int> current, IList<IList<int>> paths)
    {
        current.Add(node);

        if (node == graph.Length - 1)
        {
            paths.Add(current);
            return;
        }

        foreach (var neighbor in graph[node])
        {
            AllPathsSourceTargetDFS(graph, neighbor, [.. current], paths);
        }
    }

    public int NumOfMinutes(int n, int headID, int[] manager, int[] informTime)
    {
        var graph = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            graph[i] = [];
        }

        for (int i = 0; i < manager.Length; i++)
        {
            if (manager[i] != -1) graph[manager[i]].Add(i);
        }

        var result = 0;

        var queue = new Queue<(int node, int minutes)>();
        queue.Enqueue((headID, 0));

        while (queue.Count > 0)
        {
            var (node, minutes) = queue.Dequeue();
            result = Math.Max(minutes, result);

            foreach (var neighbor in graph[node])
            {
                queue.Enqueue((neighbor, minutes + informTime[node]));
            }
        }

        return result;
    }

    public int[] ShortestAlternatingPaths(int n, int[][] redEdges, int[][] blueEdges)
    {
        var redGraph = new List<int>[n];
        var blueGraph = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            redGraph[i] = [];
            blueGraph[i] = [];
        }

        foreach (var edge in redEdges) redGraph[edge[0]].Add(edge[1]);

        foreach (var edge in blueEdges) blueGraph[edge[0]].Add(edge[1]);

        var result = new int[n];
        Array.Fill(result, -1);

        var queue = new Queue<(int node, int length, short color)>();
        queue.Enqueue((0, 0, 1));
        queue.Enqueue((0, 0, 2));

        var visited = new HashSet<(int node, short color)>
        {
            (0, 1),
            (0, 2)
        };

        while (queue.Count > 0)
        {
            var size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                var (node, length, color) = queue.Dequeue();
                if (result[node] == -1) result[node] = length;

                if (color != 1)
                {
                    foreach (int neighbor in redGraph[node])
                    {
                        if (!visited.Contains((neighbor, 1)))
                        {
                            visited.Add((neighbor, 1));
                            queue.Enqueue((neighbor, length + 1, 1));
                        }
                    }
                }

                if (color != 2)
                {
                    foreach (int neighbor in blueGraph[node])
                    {
                        if (!visited.Contains((neighbor, 2)))
                        {
                            visited.Add((neighbor, 2));
                            queue.Enqueue((neighbor, length + 1, 2));
                        }
                    }
                }
            }
        }

        return result;
    }

    public int FindCircleNum1(int[][] isConnected)
    {
        int result = 0;
        var visited = new bool[isConnected.Length];

        for (int i = 0; i < isConnected.Length; i++)
        {
            if (!visited[i])
            {
                result++;
                FindCircleNumDFS1(i, isConnected, visited);
            }
        }

        return result;
    }

    private void FindCircleNumDFS1(int node, int[][] isConnected, bool[] visited)
    {
        visited[node] = true;

        for (int i = 0; i < isConnected.Length; i++)
        {
            if (isConnected[node][i] == 1 && !visited[i])
            {
                FindCircleNumDFS1(i, isConnected, visited);
            }
        }
    }

    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        var mergedArray = new int[nums1.Length + nums2.Length];
        var left = 0;
        var right = 0;
        var current = 0;

        while (left < nums1.Length || right < nums2.Length)
        {
            if (left == nums1.Length)
            {
                mergedArray[current] = nums2[right];
                right++;
                current++;
                continue;
            }

            if (right == nums2.Length)
            {
                mergedArray[current] = nums1[left];
                left++;
                current++;
                continue;
            }

            if (nums1[left] <= nums2[right])
            {
                mergedArray[current] = nums1[left];
                left++;
            }
            else
            {
                mergedArray[current] = nums2[right];
                right++;
            }

            current++;
        }

        var hasCenter = mergedArray.Length % 2 != 0;

        if (hasCenter)
        {
            return mergedArray[mergedArray.Length / 2];
        }
        else
        {
            return (double)(mergedArray[mergedArray.Length / 2 - 1] + mergedArray[mergedArray.Length / 2]) / 2;
        }
    }

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        var result = new ListNode();

        var l1Head = l1;
        var l2Head = l2;
        var resultHead = result;

        var extraNumber = 0;

        while (l1Head != null || l2Head != null)
        {
            var sum = (l1Head?.val ?? 0) + (l2Head?.val ?? 0) + extraNumber;

            extraNumber = sum >= 10 ? 1 : 0;

            resultHead.next = new ListNode(sum % 10);

            l1Head = l1Head?.next;
            l2Head = l2Head?.next;
            resultHead = resultHead.next;
        }

        if (extraNumber != 0)
        {
            resultHead.next = new ListNode(extraNumber);
        }

        return result.next;
    }

    public IList<string> LetterCasePermutation(string s)
    {
        var result = new List<string>();
        var subset = s.ToCharArray();
        LetterCasePermutationDfs(0, subset, result);
        return result;
    }

    private void LetterCasePermutationDfs(int index, Span<char> subset, List<string> result)
    {
        if (index >= subset.Length)
        {
            result.Add(subset.ToString());
            return;
        }

        LetterCasePermutationDfs(index + 1, subset, result);

        if (!char.IsDigit(subset[index]))
        {
            subset[index] = char.IsUpper(subset[index]) ? char.ToLower(subset[index]) : char.ToUpper(subset[index]);
            LetterCasePermutationDfs(index + 1, subset, result);
        }
    }

    public int Trap(int[] height)
    {
        var result = 0;

        int left = 0;
        int right = 0;

        while (right < height.Length)
        {
            if (left == right)
            {
                right++;
                continue;
            }

            if (height[right] > height[left])
            {
                left = right;
                right++;
                continue;
            }

            result += height[left] - height[right];

            right++;
        }

        return result;
    }

    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        ListNode previous = null;
        ListNode current = head;

        ListNode next = null;

        while (current != null)
        {
            next = current.next;
            current.next = previous;
            previous = current;
            current = next;
        }

        current = previous;
        previous = null;

        int count = 0;

        while (current != null)
        {
            count++;

            next = current.next;
            if (count != n)
            {
                current.next = previous;
                previous = current;
            }
            current = next;
        }

        return previous;
    }

    public void ReorderList(ListNode head)
    {
        if (head == null) return;

        ListNode slow = head;
        ListNode fast = head;

        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        ListNode previous = null;
        ListNode current = slow;
        ListNode temp = null;

        while (current != null)
        {
            temp = current.next;
            current.next = previous;
            previous = current;
            current = temp;
        }

        ListNode first = head;
        ListNode second = previous;

        while (second.next != null)
        {
            temp = first.next;
            first.next = second;
            first = temp;

            temp = second.next;
            second.next = first;
            second = temp;
        }
    }

    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        ListNode result = new ListNode(0);

        var head1 = list1;
        var head2 = list2;

        var head = result;

        while (head1 != null || head2 != null)
        {
            if ((head1?.val ?? int.MaxValue) <= (head2?.val ?? int.MaxValue))
            {
                head.next = new ListNode(head1.val);
                head1 = head1?.next;
            }
            else
            {
                head.next = new ListNode(head2.val);
                head2 = head2.next;
            }
            head = head.next;
        }

        return result.next;
    }

    public ListNode ReverseList(ListNode head)
    {
        ListNode prev = null;
        ListNode current = head;

        while (current != null)
        {
            ListNode next = current.next;

            current.next = prev;
            prev = current;

            current = next;
        }

        return prev;
    }

    public int Search(int[] nums, int target)
    {
        if (nums.Length == 0) return -1;
        if (nums.Length == 1) return target == nums[0] ? 0 : -1;

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                return mid;
            }

            if (nums[left] <= nums[mid])
            {
                if (target > nums[mid] || target < nums[left])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }
            else
            {
                if (target < nums[right] || target > nums[right])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
        }

        return -1;
    }

    public int FindMin(int[] nums)
    {
        var left = 0;
        var right = nums.Length - 1;

        while (left < right)
        {
            var index = left + (right - left) / 2;

            if (nums[index] < nums[right])
            {
                right = index;
            }
            else
            {
                left = index + 1;
            }
        }

        return nums[left];
    }

    public string MinWindow(string s, string t)
    {
        var result = string.Empty;

        if (s.Length < t.Length) return result;

        var dict = new Dictionary<char, int>();

        foreach (char c in t)
        {
            dict[c] = 1;
        }

        var left = 0;
        var right = 0;

        while (right < s.Length)
        {
            while (!dict.ContainsKey(s[left]))
            {
                left++;
                if (dict.ContainsKey(s[left]))
                {
                    if (right < left) right = left + 1;
                    dict[s[left]]--;
                }
            }

            if (dict.ContainsKey(s[right]))
            {
                dict[s[right]]--;
            }

            right++;

            if (right - left >= t.Length && dict.Sum(x => x.Value) <= 0)
            {
                var substing = s.Substring(left, right - left);

                if (result.Length == 0 || substing.Length < result.Length) result = substing;

                dict[s[left]]++;
                left++;
            }
        }

        return result;
    }

    public int CharacterReplacement(string s, int k)
    {
        int result = 0;
        int[] freq = new int[26];

        int left = 0;
        int right = 0;

        while (right < s.Length)
        {
            freq[s[right] - 'A']++;

            int maxFrequemnt = freq.Max();

            if (right - left + 1 - maxFrequemnt <= k)
            {
                result = Math.Max(result, right - left + 1);
            }
            else
            {
                freq[s[left] - 'A']--;
                left++;
            }

            right++;
        }

        return result;
    }

    public int LengthOfLongestSubstring(string s)
    {
        int max = 0;

        int left = 0;
        int right = 0;

        HashSet<char> hashSet = new HashSet<char>(s.Length);

        while (right < s.Length)
        {
            while (hashSet.Contains(s[right]))
            {
                hashSet.Remove(s[left]);
                left++;
            }

            hashSet.Add(s[right]);

            right++;
            max = Math.Max(max, right - left);
        }

        return max;
    }

    public int MaxProfit(int[] prices)
    {
        int maxProfit = 0;

        int left = 0;
        int right = 0;

        while (right < prices.Length)
        {
            if (prices[left] == prices[right])
            {
                right++;
                continue;
            }

            if (prices[left] >= prices[right])
            {
                left = right;
                continue;
            }

            maxProfit = Math.Max(maxProfit, prices[right] - prices[left]);
            right++;
        }

        return maxProfit;
    }

    public int MaxArea(int[] heights)
    {
        int left = 0;
        int right = heights.Length - 1;

        int max = 0;

        while (left < right)
        {
            int sum = Math.Min(heights[left], heights[right]) * (right - left);

            max = Math.Max(max, sum);

            if (heights[left] <= heights[right]) left++;
            else right--;
        }

        return max;
    }

    public IList<IList<int>> ThreeSum(int[] nums)
    {
        for (int i = 1; i < nums.Length; i++)
        {
            int temp = nums[i];
            int j = i - 1;

            while (j >= 0 && temp < nums[j])
            {
                nums[j + 1] = nums[j];
                j--;
            }

            nums[j + 1] = temp;
        }

        List<IList<int>> result = new List<IList<int>>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1]) continue;

            int left = i + 1;
            int right = nums.Length - 1;

            while (left < right)
            {
                var sum = nums[i] + nums[left] + nums[right];

                if (sum > 0)
                {
                    right--;
                    continue;
                }

                if (sum < 0)
                {
                    left++;
                    continue;
                }

                result.Add([nums[i], nums[left], nums[right]]);
                left++;
                right--;

                while (left < right && nums[left] == nums[left - 1])
                {
                    left++;
                }
            }
        }

        return result;
    }

    public int[] TwoSum(int[] numbers, int target)
    {
        int left = 0;
        int right = numbers.Length - 1;

        while (left < right)
        {
            if (numbers[left] + numbers[right] == target)
            {
                return [left + 1, right + 1];
            }

            if (numbers[left] + numbers[right] > target)
            {
                right--;
            }
            else
            {
                left++;
            }
        }

        return [];
    }

    public int LongestConsecutive1(int[] nums)
    {
        if (nums.Length < 2) return nums.Length;

        var hashSet = new HashSet<int>(nums.Length);

        foreach (int num in nums)
        {
            hashSet.Add(num);
        }

        var result = 0;

        foreach (int num in hashSet)
        {
            var max = 1;

            var temp = num;
            while (hashSet.Contains(temp - 1))
            {
                max++;
                temp = hashSet.First(x => x == temp - 1);
            }

            if (max > result) result = max;
        }

        return result;
    }

    public int DiagonalSum(int[][] mat)
    {
        var sum = 0;

        for (int i = 0; i < mat.Length; i++)
        {
            sum += mat[i][i];

            if (mat.Length - 1 - i != i) sum += mat[i][mat.Length - 1 - i];
        }

        return sum;
    }

    private int DiagonalSumDFS(int[][] mat, int row, int col, int sum, bool isForward)
    {
        if (row < 0 || row > mat.Length - 1 || col < 0 || col > mat.Length - 1)
        {
            return sum;
        }

        var currentSum = sum + mat[row][col];

        if (isForward)
        {
            return DiagonalSumDFS(mat, ++row, ++col, currentSum, isForward);
        }
        else
        {
            return DiagonalSumDFS(mat, --row, ++col, currentSum, isForward);
        }
    }

    public bool CheckValid(int[][] matrix)
    {
        for (int row = 0; row < matrix.Length; row++)
        {
            bool[] visited = new bool[matrix.Length + 1];
            for (int col = 0; col < matrix[row].Length; col++)
            {
                if (visited[matrix[row][col]]) return false;
                visited[matrix[row][col]] = true;
            }
        }

        for (int col = 0; col < matrix.Length; col++)
        {
            bool[] visited = new bool[matrix.Length + 1];
            for (int row = 0; row < matrix[col].Length; row++)
            {
                if (visited[matrix[row][col]]) return false;
                visited[matrix[row][col]] = true;
            }
        }

        return true;
    }

    public bool IsValidSudoku(char[][] board)
    {
        var rows = 9;
        var cols = 9;
        var squares = 9;

        for (int row = 0; row < rows; row++)
        {
            HashSet<char> hashSet = new HashSet<char>(rows);
            for (int col = 0; col < cols; col++)
            {
                if (board[row][col] == '.') continue;
                if (hashSet.Contains(board[row][col])) return false;
                hashSet.Add(board[row][col]);
            }
        }

        for (int col = 0; col < cols; col++)
        {
            HashSet<char> hashSet = new HashSet<char>(cols);
            for (int row = 0; row < rows; row++)
            {
                if (board[row][col] == '.') continue;
                if (hashSet.Contains(board[row][col])) return false;
                hashSet.Add(board[row][col]);
            }
        }

        for (int square = 0; square < squares; square++)
        {
            HashSet<char> hashSet = new HashSet<char>(squares);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int row = (square / 3) * 3 + i;
                    int col = (square % 3) * 3 + j;
                    if (board[row][col] == '.') continue;
                    if (hashSet.Contains(board[row][col])) return false;
                    hashSet.Add(board[row][col]);
                }
            }
        }

        return true;
    }

    public void Solve(char[][] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                SolveDFS(board, i, j);
            }
        }
    }

    private void SolveDFS(char[][] board, int line, int index)
    {
        if (board[line][index] != 'X') return;
    }

    public int[] ProductExceptSelf(int[] nums)
    {
        int[] result = new int[nums.Length];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = 1;
        }

        for (int i = 1; i < nums.Length; i++)
        {
            result[i] = result[i - 1] * nums[i - 1];
        }

        int prefix = 1;

        for (int i = nums.Length - 1; i >= 0; i--)
        {
            result[i] *= prefix;
            prefix *= nums[i];
        }


        return result;
    }

    public List<string> Decode(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return [];
        }
        var result = s.Split(";");
        return [.. result];
    }

    public int LongestIncreasingPath(int[][] matrix)
    {
        var longestIncreasingPath = 0;
        var cache = new int[matrix.Length][];

        for (int i = 0; i < cache.Length; i++)
        {
            cache[i] = new int[matrix[0].Length];
        }

        for (var i = 0; i < matrix.Length; i++)
        {
            for (var j = 0; j < matrix[i].Length; j++)
            {
                var pathLength = LongestIncreasingPathRecurse(matrix, cache, i, j, -1, 0);
                longestIncreasingPath = Math.Max(longestIncreasingPath, pathLength);
            }
        }

        return longestIncreasingPath;
    }

    private int LongestIncreasingPathRecurse(int[][] matrix, int[][] cache, int lineNum, int index, int previousValue, int sum)
    {
        if (lineNum < 0 ||
            lineNum > matrix.Length - 1 ||
            index < 0 ||
            index > matrix[lineNum].Length - 1 ||
            previousValue >= matrix[lineNum][index])
        {
            return 0;
        }

        if (cache[lineNum][index] != 0)
        {
            return cache[lineNum][index];
        }

        var top = LongestIncreasingPathRecurse(
            matrix,
            cache,
            lineNum - 1,
            index,
            matrix[lineNum][index],
            sum + 1);

        var right = LongestIncreasingPathRecurse(
            matrix,
            cache,
            lineNum,
            index + 1,
            matrix[lineNum][index],
            sum + 1);

        var bottom = LongestIncreasingPathRecurse(
            matrix,
            cache,
            lineNum + 1,
            index,
            matrix[lineNum][index],
            sum + 1);

        var left = LongestIncreasingPathRecurse(
            matrix,
            cache,
            lineNum,
            index - 1,
            matrix[lineNum][index],
            sum + 1);

        int max = 1 + Math.Max(Math.Max(left, right), Math.Max(bottom, top));

        cache[lineNum][index] = max;

        return max;
    }

    public long ParallelSum(int[] numbers)
    {
        var sum = 0L;

        Parallel.ForEach(
            Partitioner.Create(0, numbers.Length),
            () => 0L,
            (range, state, localSum) =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    localSum += numbers[i];
                }
                return localSum;
            },
            localSum => Interlocked.Add(ref sum, localSum)
            );

        return sum;
    }

    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        if (numCourses == 0) return [];

        var graph = new List<int>[numCourses];
        var state = new int[numCourses];

        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = [];
        }

        foreach (var prerequisite in prerequisites)
        {
            graph[prerequisite[1]].Add(prerequisite[0]);
        }

        var result = new List<int>(numCourses);

        for (int i = 0; i < graph.Length; i++)
        {
            if (FindOrderHasCycle(i, state, graph, result)) return [];
        }

        for (int i = 0; i < result.Count / 2; i++)
        {
            int temp = result[i];
            result[i] = result[result.Count - 1 - i];
            result[result.Count - 1 - i] = temp;
        }

        return [.. result];
    }

    private bool FindOrderHasCycle(int numCourse, int[] state, List<int>[] graph, List<int> order)
    {
        if (state[numCourse] == 1) return true;
        if (state[numCourse] == 2) return false;

        state[numCourse] = 1;

        foreach (int neighbor in graph[numCourse])
        {
            if (FindOrderHasCycle(neighbor, state, graph, order)) return true;
        }

        state[numCourse] = 2;
        order.Add(numCourse);

        return false;
    }

    public bool CanFinish(int numCourses, int[][] prerequisites)
    {
        if (numCourses == 0) return true;

        var graph = new List<int>[numCourses];
        var state = new int[numCourses];

        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = [];
        }

        foreach (var prerequisite in prerequisites)
        {
            graph[prerequisite[1]].Add(prerequisite[0]);
        }

        for (int i = 0; i < numCourses; i++)
        {
            if (CanFinishHasCycle(i, state, graph)) return false;
        }

        return true;
    }

    private bool CanFinishHasCycle(int numCourse, int[] state, List<int>[] graph)
    {
        if (state[numCourse] == 1) return true;
        if (state[numCourse] == 2) return false;

        state[numCourse] = 1;

        foreach (int neighbor in graph[numCourse])
        {
            if (CanFinishHasCycle(neighbor, state, graph)) return true;
        }

        state[numCourse] = 2;

        return false;
    }

    public int[] TopKFrequent(int[] nums, int k)
    {
        var dict = new Dictionary<int, int>();

        foreach (var n in nums)
        {
            if (dict.ContainsKey(n))
            {
                dict[n]++;
            }
            else
            {
                dict.Add(n, 1);
            }
        }

        dict = dict.OrderByDescending(x => x.Value).ToDictionary();

        var result = new int[k];
        for (int i = 0; i < k; i++)
        {
            result[i] = dict.Keys.ElementAt(i);
        }

        return result;
    }

    public IList<string> FullJustify(string[] words, int maxWidth)
    {
        var result = new List<string>();
        var sb = new StringBuilder();

        var step = 0;
        var spaces = -1;

        while (step < words.Length)
        {
            sb.Append(words[step] + " ");
            spaces++;

            if (sb.Length - 1 > maxWidth)
            {
                spaces--;
                sb.Remove(sb.Length - words[step].Length - 1, words[step].Length);

                var str = sb.ToString().Trim();
                var spacesCount = maxWidth - str.Length + spaces;

                if (spaces > 0)
                {
                    spacesCount = spacesCount / spaces;
                }

                if (spacesCount == 0) spacesCount = 1;

                if (spaces > 0)
                {
                    str = str.Replace(" ", new string(' ', spacesCount));
                }
                else
                {
                    str += new string(' ', spacesCount);
                }

                result.Add(str);
                sb.Clear();
                spaces = -1;
                continue;
            }

            if (step >= words.Length - 1)
            {
                spaces--;
                var str = sb.ToString().Trim();
                var spacesCount = maxWidth - str.Length + spaces;

                if (spaces > 0)
                {
                    spacesCount = spacesCount / spaces;
                }

                if (spacesCount == 0) spacesCount = 1;

                str = str + new string(' ', spacesCount);

                result.Add(str);
            }

            step++;
        }

        return result;
    }

    public List<IList<string>> GroupAnagrams(string[] strs)
    {
        IDictionary<string, IList<string>> result = new Dictionary<string, IList<string>>();

        foreach (string str in strs)
        {
            int[] letters = new int[26];

            foreach (char c in str)
            {
                letters[c - 'a']++;
            }

            string s = string.Join(",", letters);

            if (result.ContainsKey(s))
            {
                result[s].Add(str);
            }
            else
            {
                result.Add(str, new List<string> { str });
            }
        }

        return result.Values.ToList();
    }

    public int[] Decrypt(int[] code, int k)
    {
        bool isAsc = k > 0;
        int[] result = new int[code.Length];

        if (k == 0) return result;

        for (int i = 0; i < code.Length; i++)
        {
            int sum = 0;

            var size = Math.Abs(k);

            if (isAsc)
            {
                int step = i + 1;
                for (int j = 0; j < size; j++)
                {
                    if (step > code.Length - 1) step = 0;
                    sum += code[step];
                    step++;
                }
            }
            else
            {
                int step = i - 1;
                for (int j = 0; j < size; j++)
                {
                    if (step < 0) step = code.Length - 1;
                    sum += code[step];
                    step--;
                }
            }

            result[i] = sum;
        }

        return result;
    }

    private const string GOLD_MEDAL = "Gold Medal";
    private const string SILVER_MEDAL = "Silver Medal";
    private const string BRONZE_MEDAL = "Bronze Medal";

    public string[] FindRelativeRanks(int[] score)
    {
        var result = new string[score.Length];
        var sortedScores = new int[score.Length];

        Array.Copy(score, sortedScores, score.Length);

        for (int i = 1; i < sortedScores.Length; i++)
        {
            var temp = sortedScores[i];
            var j = i - 1;

            while (j >= 0 && sortedScores[j] < temp)
            {
                sortedScores[j + 1] = sortedScores[j];
                j--;
            }

            sortedScores[j + 1] = temp;
        }

        var dict = new Dictionary<int, int>(sortedScores.Length);
        for (int i = 0; i < sortedScores.Length; i++)
        {
            dict.Add(sortedScores[i], i + 1);
        }

        for (int i = 0; i < score.Length; i++)
        {
            var value = dict[score[i]];

            if (value == 1) result[i] = GOLD_MEDAL;
            else if (value == 2) result[i] = SILVER_MEDAL;
            else if (value == 3) result[i] = BRONZE_MEDAL;
            else result[i] = value.ToString();
        }

        return result;
    }
    public class MinStack
    {
        private Stack<int> _main;
        private Stack<int> _mins;

        public MinStack()
        {
            _main = new Stack<int>();
            _mins = new Stack<int>();
        }

        public void Push(int val)
        {
            if (_mins.Count > 0)
            {
                _mins.Push(Math.Min(GetMin(), val));
            }
            else
            {
                _mins.Push(val);
            }

            _main.Push(val);
        }

        public void Pop()
        {
            _main.Pop();
            _mins.Pop();
        }

        public int Top()
        {
            return _main.Peek();
        }

        public int GetMin()
        {
            return _mins.Peek();
        }
    }

    public int DayOfYear(string date)
    {
        return DateTime.Parse(date).DayOfYear;
    }

    public int MaxDistinct(string s)
    {
        var hashSet = new HashSet<char>(s.ToCharArray());
        return hashSet.Count;
    }

    public int MaxFreqSum(string s)
    {
        var vowels = new Dictionary<char, int>
        {
            {'a', 0 },
            {'e', 0 },
            {'i', 0 },
            {'o', 0 },
            {'u', 0 }
        };

        var consonants = new Dictionary<char, int>();

        foreach (char c in s)
        {
            if (vowels.ContainsKey(c))
            {
                vowels[c]++;
            }
            else
            {
                if (consonants.ContainsKey(c))
                {
                    consonants[c]++;
                }
                else
                {
                    consonants.Add(c, 1);
                }
            }
        }

        var result = 0;

        if (vowels.Values.Count > 0) result += vowels.Values.Max();
        if (consonants.Values.Count > 0) result += consonants.Values.Max();

        return result;
    }

    public string ReversePrefix(string word, char ch)
    {
        var charArray = word.ToCharArray();

        int left = 0;
        int right = 0;

        while (right < charArray.Length)
        {
            if (charArray[right] == ch)
            {
                right++;
                break;
            }
            right++;
        }

        if (right >= charArray.Length)
        {
            return word;
        }

        while (left <= right)
        {
            var temp = charArray[left];
            charArray[left] = charArray[right];
            charArray[right] = temp;

            left++;
            right--;
        }

        return new string(charArray.ToArray());
    }


    public bool DetectCapitalUse(string word)
    {
        if (word.Length == 1) return true;

        bool isUpperCase = char.IsUpper(word[1]);

        if (isUpperCase)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (char.IsLower(word[i])) return false;
            }
        }
        else
        {
            for (int i = 1; i < word.Length; i++)
            {
                if (char.IsUpper(word[i])) return false;
            }
        }

        return true;
    }

    public string AddStrings(string num1, string num2)
    {
        return (BigInteger.Parse(num1) + BigInteger.Parse(num2)).ToString();
    }

    public IList<int> LargestValues(TreeNode root)
    {
        if (root == null) return [];

        var result = new List<int>();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var maxValue = int.MinValue;
            var size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();

                if (node.val > maxValue)
                {
                    maxValue = node.val;
                }

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }

            result.Add(maxValue);
        }

        return result;
    }

    public int[] FindMode(TreeNode root)
    {
        if (root == null) return [];
        if (root.left == null && root.right == null) return [root.val];

        IDictionary<int, int> modes = new Dictionary<int, int>();
        FindModeDFS(root, modes);

        modes = modes.OrderByDescending(x => x.Value).ToDictionary(g => g.Key, g => g.Value);

        var maxValue = modes.First();

        var result = new List<int>();

        foreach (var pair in modes)
        {
            if (pair.Value < maxValue.Value)
            {
                break;
            }

            result.Add(pair.Key);
        }

        return result.ToArray();
    }

    private void FindModeDFS(TreeNode node, IDictionary<int, int> modes)
    {
        if (node == null) return;

        if (!modes.ContainsKey(node.val))
        {
            modes.Add(node.val, 1);
        }
        else
        {
            modes[node.val]++;
        }

        FindModeDFS(node.left, modes);
        FindModeDFS(node.right, modes);
    }

    public int HeightChecker(int[] heights)
    {
        var result = 0;

        for (int i = 1; i < heights.Length; i++)
        {
            var temp = heights[i];
            int j = i - 1;

            if (heights[j] > temp) result++;

            while (j >= 0 && heights[j] > temp)
            {
                heights[j + 1] = heights[j];
                j--;
            }

            heights[j + 1] = temp;
        }

        return result;
    }

    public int[] CountBits(int n)
    {
        int[] result = new int[n + 1];

        for (int i = 1; i <= n; i++)
        {
            result[i] = result[i >> 1] + i % 2;
        }

        return result;
    }

    public string[] UncommonFromSentences(string s1, string s2)
    {
        var dict = new Dictionary<string, int>();

        foreach (string s in s1.Split(" "))
        {
            if (dict.ContainsKey(s)) dict[s]++;
            else dict.Add(s, 1);
        }

        foreach (string s in s2.Split(" "))
        {
            if (dict.ContainsKey(s)) dict[s]++;
            else dict.Add(s, 1);
        }

        var result = new List<string>();

        foreach (var pair in dict)
        {
            if (pair.Value == 1) result.Add(pair.Key);
        }

        return result.ToArray();
    }

    public int TitleToNumber(string columnTitle)
    {
        var columnNumber = 0;

        for (int i = 0; i < columnTitle.Length; i++)
        {
            columnNumber = columnNumber * 26 + (columnTitle[i] - 'A' + 1);
        }

        return columnNumber;
    }

    public int SumNumbers(TreeNode root)
    {
        if (root == null) return 0;
        return SumNumbersDFS(root, "");
    }

    private int SumNumbersDFS(TreeNode root, string number)
    {
        number += root.val;
        if (root.left == null && root.right == null)
            return int.Parse(number);

        var left = 0;
        var right = 0;

        if (root.left != null)
            left = SumNumbersDFS(root.left, number);

        if (root.right != null)
            right = SumNumbersDFS(root.right, number);

        return left + right;
    }

    public string ReverseOnlyLetters(string s)
    {
        var left = 0;
        var right = s.Length - 1;

        var charArray = new char[s.Length];

        while (left < right)
        {
            if (!char.IsLetter(s[left]))
            {
                charArray[left] = s[left];
                left++;
                continue;
            }

            if (!char.IsLetter(s[right]))
            {
                charArray[right] = s[right];
                right--;
                continue;
            }

            charArray[left] = s[right];
            charArray[right] = s[left];

            left++;
            right--;
        }

        return new string(charArray);
    }

    public ListNode MiddleNode(ListNode head)
    {
        ListNode slow = head;
        ListNode fast = head;

        while (fast?.next != null)
        {
            slow = slow?.next;
            fast = fast?.next?.next;
        }

        return slow;
    }

    public bool IsSubsequence(string s, string t)
    {
        if (s.Length == 0) return true;
        if (t.Length == 0) return false;

        var left = 0;
        var right = 0;

        while (right < t.Length)
        {
            if (s[left] == t[right])
            {
                left++;
            }

            right++;

            if (left == s.Length)
            {
                return true;
            }
        }

        return false;
    }

    public IList<IList<int>> LevelOrder(Node root)
    {
        if (root == null) return [];

        var result = new List<IList<int>>();
        var queue = new Queue<Node>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var size = queue.Count;
            var levelNodes = new List<int>();

            for (int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                levelNodes.Add(node.val);

                if (node.children != null)
                {
                    foreach (var child in node.children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            result.Add(levelNodes);
        }

        return result;
    }

    public int ReverseBits(int n)
    {
        int result = 0;
        for (int i = 0; i < 32; i++)
        {
            result <<= 1;
            result = result | (n & 1);
            n >>= 1;
        }
        return result;
    }

    public IList<int> RightSideView(TreeNode root)
    {
        if (root == null) return [];

        var result = new List<int>();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();

                if (i == size - 1) result.Add(node.val);

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }
        }

        return result;
    }

    public IList<IList<int>> LevelOrderBottom(TreeNode root)
    {
        if (root == null) return [];

        var result = new List<IList<int>>();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var size = queue.Count;
            var levelNodes = new int[size];

            for (int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                levelNodes[i] = node.val;

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }

            result.Insert(0, levelNodes);
        }

        return result;
    }

    public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
    {
        if (root == null) return [];

        var result = new List<IList<int>>();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        int level = 0;

        while (queue.Count > 0)
        {
            ++level;

            var size = queue.Count;
            var levelNodes = new int[size];

            for (int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                var pasteIndex = level % 2 != 0 ? i : size - i - 1;

                levelNodes[pasteIndex] = node.val;

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }

            result.Add(levelNodes);
        }

        return result;
    }

    public int FindCenter(int[][] edges)
    {
        IDictionary<int, int> dict = new Dictionary<int, int>();

        foreach (int[] edge in edges)
        {
            foreach (int i in edge)
            {
                if (dict.ContainsKey(i))
                {
                    dict[i]++;
                }
                else
                {
                    dict.Add(i, 1);
                }
            }
        }

        int result = -1;
        int maxCountOfConnections = 0;

        foreach (var pair in dict)
        {
            if (pair.Value > maxCountOfConnections)
            {
                maxCountOfConnections = pair.Value;
                result = pair.Key;
            }
        }

        return result;
    }

    public bool CheckTree(TreeNode root)
    {
        if (root == null) return false;

        if (root.val == root.left?.val + root.right?.val) return true;

        return CheckTree(root.left) || CheckTree(root.right);
    }

    public TreeNode SearchBST(TreeNode root, int val)
    {
        if (root == null) return null;
        if (root.val == val) return root;

        var left = SearchBST(root.left, val);
        if (left != null) return left;

        var right = SearchBST(root.right, val);
        if (right != null) return right;

        return null;
    }

    public int CountNodes(TreeNode root)
    {
        if (root == null) return 0;
        if (root.left == null && root.right == null) return 1;

        var result = 0;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            result++;

            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }

        return result;
    }


    public bool BackspaceCompare(string s, string t)
    {
        var sStack = new Stack<char>();
        var tStack = new Stack<char>();

        foreach (char c in s)
        {
            if (c == '#')
            {
                if (sStack.Count() > 0) sStack.Pop();
            }
            else
            {
                sStack.Push(c);
            }
        }

        foreach (char c in t)
        {
            if (c == '#')
            {
                if (tStack.Count() > 0) tStack.Pop();
            }
            else
            {
                tStack.Push(c);
            }
        }

        if (sStack.Count() != tStack.Count())
        {
            return false;
        }

        while (sStack.Count() > 0)
        {
            if (sStack.Pop() != tStack.Pop()) return false;
        }

        return true;
    }

    public string ClearDigits(string s)
    {
        var stack = new Stack<char>();

        foreach (char letter in s)
        {
            if (char.IsDigit(letter))
            {
                if (stack.Count > 0)
                {
                    stack.Pop();
                }
            }
            else
            {
                stack.Push(letter);
            }
        }

        var charArray = new char[stack.Count];
        var stackSize = stack.Count;

        for (int i = 0; i < stackSize; i++)
        {
            charArray[charArray.Length - i - 1] = stack.Pop();
        }

        return new string(charArray);
    }

    public int[][] FlipAndInvertImage(int[][] image)
    {
        var rowSize = image[0].Length - 1;

        for (int i = 0; i < image.Length; i++)
        {
            var left = 0;
            var right = rowSize;

            while (left <= right)
            {
                var temp = image[i][left];
                image[i][left] = image[i][right] == 0 ? 1 : 0;
                image[i][right] = temp == 0 ? 1 : 0;

                left++;
                right--;
            }
        }

        return image;
    }

    public int NumJewelsInStones(string jewels, string stones)
    {
        var hashSet = new HashSet<char>(jewels);

        var result = 0;

        foreach (char s in stones)
        {
            if (hashSet.Contains(s))
            {
                result++;
            }
        }

        return result;
    }

    public string RestoreString(string s, int[] indices)
    {
        Span<char> result = stackalloc char[s.Length];

        for (int i = 0; i < s.Length; i++)
        {
            result[indices[i]] = s[i];
        }

        return result.ToString();
    }

    public string ToLowerCase(string s)
    {
        var array = s.ToCharArray();

        for (int i = 0; i < array.Length; i++)
        {
            if (!char.IsLower(array[i]))
            {
                array[i] = char.ToLower(array[i]);
            }
        }

        return new string(array);
    }

    /*    public int SumRootToLeaf(TreeNode root)
        {
            var result = 0;
            var strs = new List<string>();
            SumRootToLeafDFS(root, "", strs);

            foreach (var str in strs)
            {
                result += Convert.ToInt32(str, 2);
            }

            return result;
        }*/

    private void SumRootToLeafDFS(TreeNode root, string current, IList<string> strs)
    {
        current += root.val;

        if (root.left == null && root.right == null)
        {
            strs.Add(current);
            return;
        }

        if (root.left != null)
        {
            SumRootToLeafDFS(root.left, current, strs);
        }

        if (root.right != null)
        {
            SumRootToLeafDFS(root.right, current, strs);

        }
    }

    public bool IsUnivalTree(TreeNode root)
    {
        if (root == null) return false;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.val != root.val)
            {
                return false;
            }

            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }

        return true;
    }

    public int RangeSumBST(TreeNode root, int low, int high)
    {
        if (root == null) return 0;
        var result = 0;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.val >= low && node.val <= high)
            {
                result += node.val;
            }

            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }

        return result;
    }

    public TreeNode IncreasingBST(TreeNode root)
    {
        if (root == null) return null;
        var result = new TreeNode();
        IncreasingBSTDFS(root, result);

        return result.right;
    }

    private TreeNode IncreasingBSTDFS(TreeNode root, TreeNode node)
    {
        if (root == null) return node;

        node = IncreasingBSTDFS(root.left, node);

        node.right = new TreeNode(root.val);
        node = node.right;

        node = IncreasingBSTDFS(root.right, node);

        return node;
    }

    public bool LeafSimilar(TreeNode root1, TreeNode root2)
    {
        if (root1 == null || root2 == null) return false;

        var stack1 = new Stack<int>();
        var stack2 = new Stack<int>();

        LeafSimilarDFS(root1, stack1);
        LeafSimilarDFS(root2, stack2);

        if (stack1.Count != stack2.Count) return false;

        while (stack1.Count > 0)
        {
            if (stack1.Pop() != stack2.Pop()) return false;
        }

        return true;
    }

    private void LeafSimilarDFS(TreeNode node, Stack<int> stack)
    {
        if (node == null) return;

        if (node.left == null && node.right == null)
        {
            stack.Push(node.val);
            return;
        }

        LeafSimilarDFS(node.left, stack);
        LeafSimilarDFS(node.right, stack);
    }

    public int[][] FloodFill(int[][] image, int sr, int sc, int color)
    {
        var startColor = image[sr][sc];
        if (startColor != color)
        {
            FloodFillRecursion(image, sr, sc, startColor, color);
        }

        return image;
    }

    private void FloodFillRecursion(int[][] image, int sr, int sc, int color, int newColor)
    {
        if (sr < 0 || sr > image.Length - 1) return;
        if (sc < 0 || sc > image[sr].Length - 1) return;

        if (image[sr][sc] != color || image[sr][sc] == newColor) return;

        image[sr][sc] = newColor;

        FloodFillRecursion(image, sr + 1, sc, color, newColor);
        FloodFillRecursion(image, sr - 1, sc, color, newColor);
        FloodFillRecursion(image, sr, sc + 1, color, newColor);
        FloodFillRecursion(image, sr, sc - 1, color, newColor);
    }

    public string DefangIPaddr(string address)
    {
        return address.Replace(".", "[.]");
    }

    public IList<string> BuildArray(int[] target, int n)
    {
        var result = new List<string>();
        var step = 1;
        var targetStep = 0;

        var push = "Push";
        var pop = "Pop";

        while (step <= n && targetStep < target.Length)
        {
            if (target[targetStep] > step)
            {
                result.Add(push);
                result.Add(pop);
            }
            else if (target[targetStep] == step)
            {
                result.Add(push);
                targetStep++;
            }

            step++;
        }

        return result;
    }

    public IList<string> SummaryRanges(int[] nums)
    {
        var result = new List<string>();
        if (nums.Length == 0) return result;
        if (nums.Length == 1)
        {
            result.Add(nums[0].ToString());
            return result;
        }

        var lastIndex = 0;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i - 1] + 1 != nums[i])
            {
                if (lastIndex < i - 1) result.Add($"{nums[lastIndex]}->{nums[i - 1]}");
                if (lastIndex == i - 1) result.Add($"{nums[lastIndex]}");
                lastIndex = i;
            }

            if (i == nums.Length - 1)
            {
                if (lastIndex < i) result.Add($"{nums[lastIndex]}->{nums[i]}");
                if (lastIndex == i) result.Add($"{nums[lastIndex]}");
            }
        }

        return result;
    }

    public int IslandPerimeter(int[][] grid)
    {
        var perimeter = 0;

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    if (j > 0 && grid[i][j - 1] == 0 || j == 0) perimeter++;
                    if (i > 0 && grid[i - 1][j] == 0 || i == 0) perimeter++;
                    if (j < grid[i].Length - 1 && grid[i][j + 1] == 0 || j == grid[i].Length - 1) perimeter++;
                    if (i < grid.Length - 1 && grid[i + 1][j] == 0 || i == grid.Length - 1) perimeter++;
                }
            }
        }

        return perimeter;
    }

    public IList<IList<int>> PathSum(TreeNode root, int targetSum)
    {
        var result = new List<IList<int>>();
        if (root == null) return result;

        PathSumDFS(root, targetSum, result, 0, []);
        return result;
    }

    private void PathSumDFS(TreeNode node, int targetSum, IList<IList<int>> pathes, int sum, List<int> currentPath)
    {
        if (node == null) return;

        currentPath.Add(node.val);

        if (node.left == null && node.right == null)
        {
            if (sum + node.val == targetSum)
            {
                pathes.Add(currentPath.ToArray());
            }
        }
        else
        {
            PathSumDFS(node.left, targetSum, pathes, sum + node.val, currentPath);
            PathSumDFS(node.right, targetSum, pathes, sum + node.val, currentPath);
        }

        currentPath.RemoveAt(currentPath.Count - 1);
    }

    public string ReverseWords(string s)
    {
        var charArray = s.ToCharArray();

        var left = 0;
        var mid = 0;
        var right = 0;

        while (right < charArray.Length)
        {
            if (charArray[right] == ' ' || right == charArray.Length - 1)
            {
                mid = right;

                if (right != charArray.Length - 1)
                {
                    mid -= 1;
                }

                while (left < mid)
                {
                    var temp = charArray[left];
                    charArray[left] = charArray[mid];
                    charArray[mid] = temp;

                    left++;
                    mid--;
                }

                left = right + 1;
                mid = right + 1;
            }

            right++;
        }

        return new string(charArray);
    }

    public char FindTheDifference(string s, string t)
    {
        int result = 0;

        foreach (char c in s)
            result ^= c;

        foreach (char c in t)
            result ^= c;

        return (char)result;
    }

    public bool IsSubtree(TreeNode root, TreeNode subRoot)
    {
        if (root == null || subRoot == null) return false;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.val == subRoot.val &&
                node.left?.val == subRoot.left?.val &&
                node.right?.val == subRoot.right?.val &&
                (node.left?.left?.val == subRoot.left?.left?.val &&
                node.left?.right?.val == subRoot.left?.right?.val &&
                node.right?.left?.val == subRoot.right?.left?.val &&
                node.right?.right?.val == subRoot.right?.right?.val))
            {
                return true;
            }

            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }

        return false;
    }


    public int FindSecondMinimumValue(TreeNode root)
    {
        if (root == null) return -1;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        int min = root.val;
        int? secondMin = null;

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.val < min)
            {
                min = node.val;
            }

            if (node.val > min && !secondMin.HasValue)
            {
                secondMin = node.val;
            }

            if (node.val > min && node.val < secondMin.Value)
            {
                secondMin = node.val;
            }

            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }

        return secondMin ?? -1;
    }

    public bool FindTarget(TreeNode root, int k)
    {
        if (root == null) return false;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        var hashSet = new HashSet<int>();

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (hashSet.Contains(node.val))
            {
                return true;
            }

            hashSet.Add(k - node.val);
            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }

        return false;
    }

    public IList<double> AverageOfLevels(TreeNode root)
    {
        if (root == null) return null;

        var result = new List<double>();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            double sum = 0;
            var size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                sum += node.val;

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }

            result.Add(sum / size);
        }

        return result;
    }

    public int GetMinimumDifference(TreeNode root)
    {
        if (root?.left == null && root?.right == null) return int.MaxValue;

        var left = Math.Abs(root.val - root?.left?.val ?? int.MaxValue);
        var right = Math.Abs(root.val - root?.right?.val ?? int.MaxValue);
        return Math.Min(Math.Min(left, right), Math.Min(GetMinimumDifference(root.left), GetMinimumDifference(root.right)));
    }

    public int SumOfLeftLeaves(TreeNode root)
    {
        if (root == null) return 0;

        if (root.left != null && root.left.left == null && root.left.right == null)
        {
            return root.left.val + SumOfLeftLeaves(root.right);
        }
        else
        {
            return SumOfLeftLeaves(root.left) + SumOfLeftLeaves(root.right);
        }
    }

    private int SumOfLeftLeavesDFS(TreeNode node)
    {
        if (node == null) return 0;

        if (node.left != null && node.left.left == null && node.left.right == null)
        {
            return node.left.val + SumOfLeftLeavesDFS(node.right);
        }
        else
        {
            return SumOfLeftLeavesDFS(node.left) + SumOfLeftLeavesDFS(node.right);
        }
    }

    public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
    {
        if (root1 == null) return root2;
        if (root2 == null) return root1;

        var result = new TreeNode(root1.val + root2.val);

        result.left = MergeTrees(root1.left, root2.left);
        result.right = MergeTrees(root1.right, root2.right);

        return result;
    }

    public IList<int> Postorder(Node root)
    {
        var result = new List<int>();
        PostorderDFS(root, result);
        return result;
    }

    public void PostorderDFS(Node root, IList<int> nodes)
    {
        if (root == null) return;

        if (root.children == null)
        {
            nodes.Add(root.val);
            return;
        }

        foreach (var node in root.children)
        {
            PostorderDFS(node, nodes);
        }

        nodes.Add(root.val);
    }

    public int MaxDepth(Node root)
    {
        if (root == null) return 0;
        if (root.children == null) return 1;

        var max = 1;

        foreach (var child in root.children)
        {
            max = Math.Max(max, MaxDepth(child) + 1);
        }

        return max;
    }

    public IList<int> Preorder(Node root)
    {
        var result = new List<int>();
        PreorderDFS(root, result);
        return result;
    }

    private void PreorderDFS(Node root, List<int> nodes)
    {
        if (root == null) return;

        nodes.Add(root.val);

        if (root.children == null) return;

        foreach (var node in root.children)
        {
            PreorderDFS(node, nodes);
        }
    }

    public IList<int> PostorderTraversal(TreeNode root)
    {
        if (root == null) return [];
        var result = new List<int>();

        PostorderTraversalDFS(root, result);
        return result;
    }

    private void PostorderTraversalDFS(TreeNode root, IList<int> values)
    {
        if (root == null) return;

        if (root.left != null)
        {
            PostorderTraversalDFS(root.left, values);
        }

        if (root.right != null)
        {
            PostorderTraversalDFS(root.right, values);
        }

        values.Add(root.val);
    }

    public IList<string> FizzBuzz(int n)
    {
        var result = new List<string>();
        var sb = new StringBuilder();

        for (int i = 1; i <= n; i++)
        {
            if (i % 3 == 0)
            {
                sb.Append("Fizz");
            }

            if (i % 5 == 0)
            {
                sb.Append("Buzz");
            }

            if (i % 3 != 0 && i % 5 != 0)
            {
                sb.Append(i);
            }

            result.Add(sb.ToString());
            sb.Clear();
        }

        return result;
    }


    public IList<string> BinaryTreePaths(TreeNode root)
    {
        var result = new List<string>();
        BinaryTreePaths(root, "", result);
        return result;
    }

    private void BinaryTreePaths(TreeNode node, string path, IList<string> pathes)
    {
        if (node == null) return;

        path += node.val;

        if (node.left == null && node.right == null)
        {
            pathes.Add(path);
        }
        else
        {
            path += "->";
            BinaryTreePaths(node.left, path, pathes);
            BinaryTreePaths(node.right, path, pathes);
        }
    }

    public string ReverseVowels(string s)
    {
        if (s.Length < 2)
        {
            return s;
        }

        var hashSet = new HashSet<char>
        {
            'a', 'e', 'i', 'o', 'u', 'y'
        };

        var left = 0;
        var right = s.Length - 1;

        Span<char> result = stackalloc char[s.Length];
        s.CopyTo(result);

        while (left < right)
        {
            if (hashSet.Contains(char.ToLower(result[left])) && hashSet.Contains(char.ToLower(result[right])))
            {
                var temp = result[left];
                result[left] = result[right];
                result[right] = temp;
                right--;
                left++;
            }
            else if (hashSet.Contains(char.ToLower(result[left])) && !hashSet.Contains(char.ToLower(result[right])))
            {
                right--;
            }
            else if (!hashSet.Contains(char.ToLower(result[left])) && hashSet.Contains(char.ToLower(result[right])))
            {
                left++;
            }
            else
            {
                left++;
                right--;
            }

        }

        return result.ToString();
    }

    public void MoveZeroes(int[] nums)
    {
        var left = 0;
        var right = 0;

        while (left < nums.Length && right < nums.Length)
        {
            if (nums[left] == 0 && nums[right] == 0)
            {
                right++;
            }
            else if (nums[left] == 0)
            {
                var temp = nums[left];
                nums[left] = nums[right];
                nums[right] = temp;

                left++;
                right++;
            }
            else
            {
                left++;
            }
        }
    }

    public int[] Intersect(int[] nums1, int[] nums2)
    {
        var result = new List<int>();
        var dict = new Dictionary<int, int>();

        foreach (var x in nums1)
        {
            if (dict.ContainsKey(x))
            {
                dict[x]++;
            }
            else
            {
                dict.Add(x, 1);
            }
        }

        foreach (var x in nums2)
        {
            if (dict.ContainsKey(x) && dict[x] > 0)
            {
                result.Add(x);
                dict[x]--;
            }
        }

        return result.ToArray();
    }

    public int FirstBadVersion(int n)
    {
        var left = 0;
        var right = n;

        while (left < right)
        {
            var mid = left + (right - left) / 2;

            if (IsBadVersion(mid))
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }

        return right;
    }

    private bool IsBadVersion(int n)
    {
        return n == 4;
    }

    public bool IsBalanced(TreeNode root)
    {
        return CheckBalance(root) != -1;
    }

    private int CheckBalance(TreeNode node)
    {
        if (node == null) return 0;

        int left = CheckBalance(node.left);
        if (left == -1) return -1;

        int right = CheckBalance(node.right);
        if (right == -1) return -1;

        if (Math.Abs(left - right) > 1) return -1;

        return Math.Max(left, right) + 1;
    }

    public bool IsPalindrome(ListNode head)
    {
        if (head == null) return false;
        if (head.next == null) return true;

        ListNode left = head;
        ListNode right = head;

        while (right != null && right.next != null)
        {
            left = left.next;
            right = right.next?.next;
        }

        ListNode current = left;
        ListNode prev = null;

        while (current != null)
        {
            var next = current.next;
            current.next = prev;
            prev = current;
            current = next;
        }

        left = head;
        right = prev;

        while (right != null)
        {
            if (left?.val != right?.val)
            {
                return false;
            }

            left = left.next;
            right = right.next;
        }

        return true;
    }

    public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
    {
        ListNode pA = headA;
        ListNode pB = headB;

        while (pA != pB)
        {
            pA = (pA == null) ? headB : pA.next;
            pB = (pB == null) ? headA : pB.next;
        }

        return pA;
    }

    private ListNode ReverseNode(ListNode node)
    {
        ListNode prev = null;
        var current = node;

        while (current != null)
        {
            var temp = current.next;
            current.next = prev;
            prev = current;
            current = temp;
        }

        return prev;
    }

    public IList<int> PreorderTraversal(TreeNode root)
    {
        var result = new List<int>();
        if (root == null)
        {
            return result;
        }


        result.Add(root.val);
        result.AddRange(PreorderTraversal(root.left));
        result.AddRange(PreorderTraversal(root.right));

        return result;
    }

    public IList<int> GetRow(int rowIndex)
    {
        if (rowIndex == 0)
        {
            return [1];
        }

        if (rowIndex == 1)
        {
            return [1, 1];
        }

        var result = new int[rowIndex];
        result[0] = 1;
        result[1] = 1;

        for (int i = 2; i < rowIndex; i++)
        {
            var temp = new List<int>();
            temp.Add(1);

            for (int j = 1; j < i; j++)
            {
                temp.Add(result[j] + result[j - 1]);
            }

            temp.Add(1);

            result = temp.ToArray();
        }

        return result;
    }

    public int MinDepth(TreeNode root)
    {
        if (root == null) return 0;

        if (root.left == null)
        {
            return MinDepth(root.right) + 1;
        }

        if (root.right == null)
        {
            return MinDepth(root.left) + 1;
        }

        return Math.Min(MinDepth(root.left), MinDepth(root.right)) + 1;
    }

    public int RomanToInt(string s)
    {
        var dict = new Dictionary<char, int>()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };

        var result = 0;
        var step = 0;

        while (step < s.Length)
        {
            if (step == s.Length - 1)
            {
                result += dict[s[step]];
            }
            else if ((s[step + 1] == 'V' || s[step + 1] == 'X') && s[step] == 'I')
            {
                result += dict[s[step + 1]] - dict[s[step]];
                step++;
            }
            else if ((s[step + 1] == 'L' || s[step + 1] == 'C') && s[step] == 'X')
            {
                result += dict[s[step + 1]] - dict[s[step]];
                step++;
            }
            else if ((s[step + 1] == 'D' || s[step + 1] == 'M') && s[step] == 'C')
            {
                result += dict[s[step + 1]] - dict[s[step]];
                step++;
            }
            else
            {
                result += dict[s[step]];
            }

            step++;
        }

        return result;
    }

    public int[] SmallerNumbersThanCurrent(int[] nums)
    {
        var result = new int[nums.Length];

        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = 0; j < nums.Length; j++)
            {
                if (nums[i] > nums[j])
                {
                    result[i]++;
                }
            }
        }

        return result;
    }

    public bool IsSameTree(TreeNode p, TreeNode q)
    {
        if (p == null && q == null) return true;
        if (p?.val != q?.val) return false;
        return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
    }

    public bool ValidPalindrome(string s)
    {
        var left = 0;
        var right = s.Length - 1;

        return ValidPalindrome(s, left, right, true);
    }

    private bool ValidPalindrome(string s, int left, int right, bool canSkip)
    {
        while (left < right)
        {
            if (s[left] != s[right])
            {
                if (!canSkip)
                {
                    return false;
                }

                return ValidPalindrome(s, left + 1, right, false) ||
                        ValidPalindrome(s, left, right - 1, false);
            }

            left++;
            right--;
        }

        return true;
    }

    public IList<int> FindDisappearedNumbers(int[] nums)
    {
        var result = new List<int>();

        for (var i = 0; i < nums.Length; i++)
        {
            nums[Math.Abs(nums[i]) - 1] = -Math.Abs(nums[Math.Abs(nums[i]) - 1]);
        }

        for (var i = 0; i < nums.Length; i++)
        {
            if (nums[i] > 0)
            {
                result.Add(i + 1);
            }
        }

        return result;
    }

    public int MissingNumber(int[] nums)
    {
        var expected = 0;
        var current = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            current ^= nums[i];
            expected ^= i + 1;
        }

        return expected ^ current;
    }

    public int SingleNumber(int[] nums)
    {
        var result = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            result = result ^ nums[i];
        }

        return result;
    }

    public IList<IList<int>> Permute(int[] nums)
    {
        List<IList<int>> result = new List<IList<int>>();
        BackTrack(new List<int>(), nums, result);
        return result;
    }

    private void BackTrack(List<int> current, int[] nums, List<IList<int>> result)
    {
        if (current.Count == nums.Length)
        {
            result.Add(new List<int>(current));
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (current.Contains(nums[i]))
            {
                continue;
            }

            current.Add(nums[i]);

            BackTrack(current, nums, result);

            current.RemoveAt(current.Count - 1);
        }
    }

    /*    public int Search(int[] nums, int target)
        {
            if (nums.Length == 1)
            {
                return nums[0] == target ? 0 : -1;
            }

            var left = 0;
            var right = nums.Length - 1;

            while (right >= left)
            {
                var pivot = left + (right - left) / 2;

                if (nums[pivot] == target)
                {
                    return pivot;
                }

                if (nums[right] == target)
                {
                    return right;
                }

                if (nums[left] == target)
                {
                    return left;
                }

                if (nums[left] < nums[pivot])
                {
                    if (nums[left] <= target && target < nums[pivot])
                    {
                        right = pivot - 1;
                    }
                    else
                    {
                        left = pivot + 1;
                    }
                }
                else
                {
                    if (nums[pivot] < target && target <= nums[right])
                    {
                        left = pivot + 1;
                    }
                    else
                    {
                        right = pivot - 1;
                    }
                }
            }

            return -1;
        }
    */

    public int RemoveDuplicates(int[] nums)
    {
        if (nums.Length <= 1)
        {
            return nums.Length;
        }

        var left = 0;
        var right = 1;
        var length = 1;

        while (right < nums.Length)
        {
            if (nums[left] != nums[right])
            {
                left++;
                length++;
                nums[left] = nums[right];
            }

            right++;
        }

        return length;
    }

    public int LengthOfLIS(int[] nums)
    {
        if (nums.Length < 2)
        {
            return nums.Length;
        }

        var dp = new int[nums.Length];

        for (int i = 0; i < nums.Length; i++)
        {
            dp[i] = 1;

            for (var j = 0; j < i; j++)
            {
                if (nums[j] < nums[i])
                {
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }
        }

        return dp.Max();
    }

    int Rob(int[] nums, int i)
    {
        if (i < 0) return 0;
        if (i == 0) return nums[0];

        int robCurrent = nums[i] + Rob(nums, i - 2);
        int skipCurrent = Rob(nums, i - 1);

        return Math.Max(robCurrent, skipCurrent);
    }

    public int CoinChange(int[] coins, int amount)
    {
        var dp = new int[amount + 1];
        Array.Fill(dp, amount + 1);

        dp[0] = 0;

        for (var i = 1; i <= amount; i++)
        {
            foreach (var coin in coins)
            {
                if (coin <= i)
                {
                    dp[i] = Math.Min(dp[i], 1 + dp[i - coin]);
                }
            }
        }

        return dp[amount] > amount ? -1 : dp[amount];

    }

    public IList<IList<int>> Generate(int numRows)
    {
        var result = new List<IList<int>>();

        if (numRows >= 1)
        {
            result.Add(new List<int> { 1 });
        }

        if (numRows >= 2)
        {
            result.Add(new List<int> { 1, 1 });
        }

        for (int i = 3; i <= numRows; i++)
        {
            var row = new List<int>() { 1 };

            for (int j = 1; j < i - 1; j++)
            {
                row.Add(result.Last()[j - 1] + result.Last()[j]);
            }

            row.Add(1);

            result.Add(row);
        }

        return result;
    }

    public int ClimbStairs(int n)
    {
        if (n == 1) return 1;
        if (n == 2) return 2;

        int prev1 = 1;
        int prev2 = 2;
        int current = 0;

        for (int i = 3; i <= n; i++)
        {
            current = prev1 + prev2;
            prev1 = prev2;
            prev2 = current;
        }

        return current;
    }

    public bool ValidPath(int n, int[][] edges, int source, int destination)
    {
        if (source == destination) return true;

        var graph = new List<int>[n];

        foreach (var edge in edges)
        {
            var v = edge[0];
            var u = edge[1];

            if (graph[v] == null)
            {
                graph[v] = [];
            }

            if (graph[u] == null)
            {
                graph[u] = [];
            }

            graph[v].Add(u);
            graph[u].Add(v);
        }

        var visited = new bool[n];

        return DFS(graph, source, destination, visited);
    }

    private bool DFS(List<int>[] graph, int source, int destination, bool[] visited)
    {
        visited[source] = true;
        var edge = graph[source];

        if (edge == null)
        {
            return false;
        }

        foreach (var e in edge)
        {
            if (visited[e]) continue;

            if (e == destination) return true;
            var success = DFS(graph, e, destination, visited);
            if (success) return true;
        }

        return false;
    }

    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root == null)
        {
            return new TreeNode(val);
        }

        if (val < root.val)
        {
            root.left = InsertIntoBST(root.left, val);
        }
        else
        {
            root.right = InsertIntoBST(root.right, val);
        }

        return root;
    }

    public bool IsValidBST(TreeNode root)
    {
        if (root == null) return true;

        return IsValidBST(root, long.MinValue, long.MaxValue);
    }

    public bool IsValidBST(TreeNode node, long min, long max)
    {
        if (node == null) return true;

        if (node.val <= min || node.val >= max)
        {
            return false;
        }

        return IsValidBST(node.left, min, node.val) &&
            IsValidBST(node.right, node.val, max);
    }

    public IList<double> LevelOrder(TreeNode root)
    {
        if (root == null) return null;

        var result = new List<double>();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var levelNumber = queue.Count;
            var sum = 0D;

            for (int i = 0; i < levelNumber; i++)
            {
                var node = queue.Dequeue();
                sum += node.val;

                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }

            result.Add(sum / levelNumber);
        }

        return result;
    }

    public int DiameterOfBinaryTree(TreeNode root)
    {
        if (root == null) return 0;

        var maxDiameter = 0;

        GetDepth(root, ref maxDiameter);

        return maxDiameter;
    }

    public int GetDepth(TreeNode node, ref int maxDiameter)
    {
        if (node == null) return 0;
        var leftDepth = GetDepth(node.left, ref maxDiameter);
        var rightDepth = GetDepth(node.right, ref maxDiameter);

        var diameterThroughNode = leftDepth + rightDepth;
        maxDiameter = Math.Max(maxDiameter, diameterThroughNode);

        return Math.Max(leftDepth, rightDepth) + 1;
    }

    public bool HasPathSum(TreeNode root, int targetSum)
    {
        if (root == null)
        {
            return false;
        }

        if (targetSum - root.val == 0 && root.left == null && root.right == null)
        {
            return true;
        }

        return HasPathSum(root.left, targetSum - root.val) || HasPathSum(root.right, targetSum - root.val);
    }

    public bool IsSymmetric(TreeNode root)
    {
        if (root == null)
        {
            return true;
        }

        return IsMirror(root.left, root.right);
    }

    private bool IsMirror(TreeNode left, TreeNode right)
    {
        if (left == null && right == null)
        {
            return true;
        }

        if (left?.val != right?.val)
        {
            return false;
        }

        return IsMirror(left?.left, right?.right) && IsMirror(left.right, right.left);
    }

    public IList<int> InorderTraversal(TreeNode root)
    {
        var list = new List<int>();
        var result = InorderTraversal(root, list);

        return result;
    }

    private IList<int> InorderTraversal(TreeNode root, IList<int> list)
    {
        if (root == null)
        {
            return list;
        }

        InorderTraversal(root.left, list);

        list.Add(root.val);

        InorderTraversal(root.right, list);

        return list;
    }

    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null)
        {
            return root;
        }

        var temp = root.left;
        root.left = InvertTree(root.right);
        root.right = InvertTree(temp);

        return root;
    }

    public int MaxDepth(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        var leftTreeDepth = MaxDepth(root.left);
        var rightTreeDepth = MaxDepth(root.right);

        return Math.Max(leftTreeDepth, rightTreeDepth);
    }

    public TreeNode SortedArrayToBST(int[] nums)
    {
        if (nums.Length <= 0)
        {
            return null;
        }

        var mid = nums.Length / 2;
        var leftArray = new int[mid];
        var rightArray = new int[nums.Length - mid - 1];

        Array.Copy(nums, leftArray, mid);
        Array.Copy(nums, mid + 1, rightArray, 0, nums.Length - mid - 1);

        var tree = new TreeNode(nums[mid], SortedArrayToBST(leftArray), SortedArrayToBST(rightArray));
        return tree;
    }

    public int FirstUniqChar(string s)
    {
        var dict = new Dictionary<char, int>();

        foreach (var letter in s)
        {
            if (dict.ContainsKey(letter))
            {
                dict[letter]++;
            }
            else
            {
                dict.Add(letter, 1);
            }
        }

        for (var i = 0; i < s.Length; i++)
        {
            if (dict[s[i]] == 1)
            {
                return i;
            }
        }

        return -1;
    }

    /*    public int[] TopKFrequent(int[] nums, int k)
        {
            var dictionary = new Dictionary<int, int>();

            foreach (var n in nums)
            {
                if (dictionary.ContainsKey(n))
                {
                    dictionary[n]++;
                }
                else
                {
                    dictionary.Add(n, 1);
                }
            }

            var arrays = new List<int>[nums.Length];

            foreach (var pair in dictionary)
            {
                if (arrays[pair.Value] == null)
                {
                    arrays[pair.Value] = [];
                }

                arrays[pair.Value].Add(pair.Key);
            }

            var filledArray = new List<int>();

            for (var i = arrays.Length - 1; i >= 0 && filledArray.Count < k; i--)
            {
                if (arrays[i] != null)
                {
                    filledArray.AddRange(arrays[i]);
                }
            }

            return filledArray.Take(k).ToArray();
        }*/

    public bool ContainsDuplicate(int[] nums)
    {
        return nums.Length == new HashSet<int>(nums).Count;
    }

    public void SortColors(int[] nums)
    {
        var left = 0;
        var current = 0;
        var right = nums.Length - 1;

        while (current < right)
        {
            if (nums[current] == 2)
            {
                var temp = nums[current];
                nums[current] = nums[right];
                nums[right] = temp;

                right--;
                continue;
            }
            else if (nums[current] == 0)
            {
                var temp = nums[current];
                nums[current] = nums[left];
                nums[left] = temp;

                left--;
            }

            current++;
        }
    }

    public bool IsAnagram(string s, string t)
    {
        if (s.Length != t.Length)
        {
            return false;
        }

        var length = s.Length;
        var leftArray = new int[length];
        var rightArray = new int[length];

        for (var i = 0; i < length; i++)
        {
            leftArray[i] = s[i];
            rightArray[i] = t[i];
        }

        for (var i = 0; i < length; i++)
        {
            var temp1 = leftArray[i];
            var temp2 = rightArray[i];
            var j = i - 1;
            var k = i - 1;

            while (j >= 0 && leftArray[j] > temp1)
            {
                leftArray[j + 1] = leftArray[j];
                j--;
            }

            leftArray[j + 1] = temp1;

            while (k >= 0 && rightArray[k] > temp2)
            {
                rightArray[k + 1] = rightArray[k];
                k--;
            }

            rightArray[k + 1] = temp2;
        }

        for (var i = 0; i < length; i++)
        {
            if (leftArray[i] != rightArray[i])
            {
                return false;
            }
        }


        return true;
    }

    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        var index1 = m - 1;
        var index2 = n - 1;

        for (int i = nums1.Length - 1; i >= 0; i--)
        {
            if (index1 < 0)
            {
                nums1[i] = nums2[index2];
                index2--;
                continue;
            }

            if (index2 < 0)
            {
                nums1[i] = nums1[index1];
                index1--;
                continue;
            }

            if (nums1[index1] <= nums2[index2])
            {
                nums1[i] = nums2[index2];
                index2--;
            }
            else
            {
                nums1[i] = nums1[index1];
                index1--;
            }
        }
    }

    public void InsertSort(int[] nums)
    {
        for (int i = 1; i < nums.Length; i++)
        {
            var temp = nums[i];
            var j = i - 1;

            while (j >= 0 && nums[j] > temp)
            {
                nums[j + 1] = nums[j];
                j--;
            }

            nums[j + 1] = temp;
        }
    }


    public bool IsValid(string s)
    {
        var dict = new Dictionary<char, char>
        {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' },
        };

        var stack = new Stack<char>();

        foreach (var letter in s)
        {
            if (dict.ContainsValue(letter))
            {
                stack.Push(letter);
            }
            else if (stack.Count == 0 || stack.Pop() != dict[letter])
            {
                return false;
            }
        }

        return stack.Count == 0;
    }

    public bool IsPalindrome(string s)
    {
        var left = 0;
        var right = s.Length - 1;

        while (left < right)
        {
            if (!char.IsLetterOrDigit(s[left]))
            {
                left++;
                continue;
            }

            if (!char.IsLetterOrDigit(s[right]))
            {
                right--;
                continue;
            }

            if (char.ToLower(s[left]) != char.ToLower(s[right]))
            {
                return false;
            }

            left++;
            right--;
        }

        return true;
    }

    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head?.next == null)
        {
            return head;
        }

        var currentNode = head;

        while (currentNode != null)
        {
            if (currentNode.next == null)
            {
                break;
            }

            if (currentNode.val == currentNode.next.val)
            {
                currentNode.next = currentNode.next?.next;
                continue;
            }

            currentNode = currentNode.next;
        }

        return head;
    }

    public int CalPoints(string[] operations)
    {
        var sum = 0;
        var stack = new Stack<int>();
        var supportStack = new Stack<int>();

        foreach (var operation in operations)
        {
            if (int.TryParse(operation, out var point))
            {
                if (stack.Count > 0)
                {
                    supportStack.Push(stack.Peek());
                }

                stack.Push(point);
                sum += stack.Peek();
                continue;
            }

            if (stack.Count == 0)
            {
                continue;
            }

            if (operation == "C")
            {
                sum -= stack.Pop();
                if (supportStack.Count > 0)
                {
                    supportStack.Pop();
                }
            }

            if (operation == "D")
            {
                supportStack.Push(stack.Peek());
                stack.Push(stack.Peek() * 2);
                sum += stack.Peek();
            }

            if (operation == "+")
            {
                var temp = stack.Peek();

                stack.Push(stack.Peek() + supportStack.Peek());
                supportStack.Push(temp);

                sum += stack.Peek();
            }
        }

        return sum;
    }

    /*    public int[] TwoSum(int[] numbers, int target)
        {
            if (numbers.Length < 2)
            {
                return new int[0];
            }

            var left = 0;
            var right = numbers.Length - 1;

            while (left < right)
            {
                var sum = numbers[left] + numbers[right];
                if (sum == target)
                {
                    return new int[] { left + 1, right + 1 };
                }

                if (sum > target)
                {
                    right--;
                }
                else
                {
                    left++;
                }
            }

            return new int[0];
        }*/


    //Input: nums = [2,5,1,3,4,7], n = 3
    //Output: [2, 3, 5, 4, 1, 7]
    //Explanation: Since x1 = 2, x2 = 5, x3 = 1, y1 = 3, y2 = 4, y3 = 7 then the answer is [2, 3, 5, 4, 1, 7].
    public int[] Shuffle(int[] nums, int n)
    {
        var shuffleArray = new int[nums.Length];

        for (int i = 0; i < nums.Length / 2; i++)
        {
            shuffleArray[i * 2] = nums[i];
            shuffleArray[i * 2 + 1] = nums[i + n];
        }

        return shuffleArray;
    }

    public int[] FindErrorNums(int[] nums)
    {
        for (int i = 0; i < nums.Length - 1; i++)
        {
            if (nums[i] == nums[i + 1])
            {
                return new int[] { nums[i], nums[i] + 1 };
            }
        }

        return new int[0];
    }

    public int SearchInsert(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return -1;
        }

        if (target > nums[nums.Length - 1])
        {
            return nums.Length;
        }

        if (target < nums[0])
        {
            return 0;
        }

        var result = 0;
        var left = 0;
        var right = nums.Length - 1;

        while (left <= right)
        {
            var mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                result = mid;
                break;
            }


            if (nums[mid] > target)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
                result = mid + 1;
            }
        }

        return result;
    }

    public int Fib(int n)
    {
        if (n == 0 || n == 1)
        {
            return n;
        }

        return Fib(n - 1) + Fib(n - 2);
    }

    public void DrawMatrix(int[][] matrix)
    {
        for (var i = 0; i < matrix.Length; i++)
        {
            for (var j = 0; j < matrix[i].Length; j++)
            {
                Console.Write(matrix[i][j] + " ");
            }
            Console.WriteLine();
        }
    }

    public bool HasCycle(ListNode head)
    {
        if (head.next == null)
        {
            return false;
        }

        var slow = head;
        var fast = head.next;

        while (fast != null)
        {
            if (slow == fast)
            {
                return true;
            }

            if (fast.next?.next == null)
            {
                return false;
            }

            slow = slow.next;
            fast = fast.next.next;
        }

        return false;
    }
}
