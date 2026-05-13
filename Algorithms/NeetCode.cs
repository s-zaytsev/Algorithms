using System.Text;

namespace Algorithms;

public class NeetCode
{

    public int RemoveDuplicates(int[] nums)
    {
        int left = 1;
        for (int right = 1; right < nums.Length; right++)
        {
            if (nums[right] != nums[right - 1])
            {
                nums[left] = nums[right];
                left++;
            }
        }

        return left;
    }

    public bool WordPattern(string pattern, string s)
    {
        var words = s.Split(' ');
        if (pattern.Length != words.Length) return false;

        var charToWord = new Dictionary<char, string>();
        var wordToChar = new Dictionary<string, char>();

        for (int i = 0; i < pattern.Length; i++)
        {
            if (charToWord.ContainsKey(pattern[i]) && charToWord[pattern[i]] != words[i]) return false;
            if (wordToChar.ContainsKey(words[i]) && wordToChar[words[i]] != pattern[i]) return false;

            charToWord[pattern[i]] = words[i];
            wordToChar[words[i]] = pattern[i];
        }

        return true;
    }

    public List<List<int>> Generate(int numRows)
    {
        var result = new List<List<int>>();
        if (numRows == 0) return result;
        result.Add([1]);
        if (numRows == 1) return result;
        result.Add([1, 1]);

        for (int i = 3; i <= numRows; i++)
        {
            var row = new List<int>() { 1 };
            for (int j = 1; j < i - 1; j++)
            {
                row.Add(result[i - 2][j - 1] + result[i - 2][j]);
            }

            row.Add(1);
            result.Add(row);
        }

        return result;
    }

    public int ScoreOfString(string s)
    {
        int result = 0;

        for (int i = 1; i < s.Length; i++)
        {
            var score = Math.Abs((int)s[i] - (int)s[i - 1]);
            result += score;
        }

        return result;
    }

    public int GoodNodes(TreeNode root)
    {
        var result = 0;
        if (root == null) return 0;

        var queue = new Queue<(TreeNode node, int maxValue)>();
        queue.Enqueue((root, root.val));

        while (queue.Count > 0)
        {
            var (node, maxValue) = queue.Dequeue();

            if (node.val >= maxValue) result++;

            if (node.left != null) queue.Enqueue((node.left, Math.Max(maxValue, node.val)));
            if (node.right != null) queue.Enqueue((node.right, Math.Max(maxValue, node.val)));
        }

        return result;
    }

    public bool LemonadeChange(int[] bills)
    {
        int five = 0;
        int ten = 0;

        foreach (int bill in bills)
        {
            if (bill == 5)
            {
                five++;
            }

            if (bill == 10)
            {
                ten++;
                five--;
            }

            if (bill == 20)
            {
                if (ten > 0)
                {
                    five--;
                    ten--;
                }
                else
                {
                    five -= 3;
                }
            }

            if (five < 0 || ten < 0) return false;
        }

        return true;
    }

    public int Tribonacci(int n)
    {
        if (n < 2) return 1;
        if (n < 3) return n;

        var prevprev = 1;
        var prev = 1;
        var current = 2;

        for (var i = 3; i < n; i++)
        {
            var temp = prevprev + prev + current;
            prevprev = prev;
            prev = current;
            current = temp;
        }

        return current;
    }

    public int Change(int amount, int[] coins)
    {
        Span<int> current = stackalloc int[amount + 1];
        current[0] = 1;

        for (int coinIndex = coins.Length - 1; coinIndex >= 0; coinIndex--)
        {
            for (int a = 1; a <= amount; a++)
            {
                if (a - coins[coinIndex] >= 0)
                {
                    current[a] = current[a - coins[coinIndex]] + current[a];
                }
            }
        }

        return current[^1];
    }

    public int LongestCommonSubsequence(string text1, string text2)
    {
        var dp = new int[text1.Length + 1, text2.Length + 1];

        for (int i = text1.Length - 1; i >= 0; i--)
        {
            for (int j = text2.Length - 1; j >= 0; j--)
            {
                if (text1[i] == text2[j])
                {
                    dp[i, j] = dp[i + 1, j + 1] + 1;
                }
                else
                {
                    dp[i, j] = Math.Max(dp[i + 1, j], dp[i, j + 1]);
                }
            }
        }

        for (int i = 0; i < dp.GetLength(0); i++)
        {
            for (int j = 0; j < dp.GetLength(1); j++)
            {
                Console.Write(dp[i, j] + "\t");
            }
            Console.WriteLine();
        }

        return dp[0, 0];
    }

    public int UniquePaths(int m, int n)
    {
        Span<int> previous = stackalloc int[n];
        Span<int> current = stackalloc int[n];

        for (int i = 0; i < n; i++) previous[i] = 1;

        for (int row = 1; row < m; row++)
        {
            for (int column = 0; column < n; column++)
            {
                if (column == 0)
                {
                    current[column] = previous[column];
                }
                else
                {
                    current[column] = previous[column] + current[column - 1];
                }
            }

            previous = current;
        }

        return previous[n - 1];
    }

    public int EvalRPN(string[] tokens)
    {
        Stack<int> stack = new Stack<int>();
        foreach (string c in tokens)
        {
            if (c == "+")
            {
                stack.Push(stack.Pop() + stack.Pop());
            }
            else if (c == "-")
            {
                int a = stack.Pop();
                int b = stack.Pop();
                stack.Push(b - a);
            }
            else if (c == "*")
            {
                stack.Push(stack.Pop() * stack.Pop());
            }
            else if (c == "/")
            {
                int a = stack.Pop();
                int b = stack.Pop();
                stack.Push((int)((double)b / a));
            }
            else
            {
                stack.Push(int.Parse(c));
            }
        }
        return stack.Pop();
    }

    public int LengthOfLIS(int[] nums)
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

    public bool WordBreak(string s, List<string> wordDict)
    {
        var dp = new bool[s.Length + 1];
        dp[s.Length] = true;

        // Идём с конца строки. 
        for (int i = s.Length - 1; i >= 0; i--)
        {
            // Перебираем все слова.
            foreach (var word in wordDict)
            {
                // Если длина слова плюс индекс меньше или равен длине строки
                // и отрезок равный длине слова от индекса равный самому слову
                // в dp[i] кладём значение от dp[i + word.Length]
                if (i + word.Length <= s.Length && s.Substring(i, word.Length) == word)
                {
                    dp[i] = dp[i + word.Length];
                }

                if (dp[i])
                {
                    break;
                }
            }
        }

        return dp[0];
    }

    public int MaxProduct(int[] nums)
    {
        if (nums.Length == 0) return 0;

        var result = nums[0];
        var min = 1;
        var max = 1;

        for (int i = 0; i < nums.Length; i++)
        {
            var temp = nums[i] * max;

            max = Math.Max(Math.Max(nums[i] * max, nums[i] * min), nums[i]);
            min = Math.Min(Math.Min(temp, min * nums[i]), nums[i]);
            result = Math.Max(result, max);
        }

        return result;
    }

    public int CoinChange(int[] coins, int amount)
    {
        if (amount == 0) return 0;

        var dp = new int[amount + 1];

        for (int i = 0; i < dp.Length; i++)
        {
            dp[i] = amount + 1;
        }

        dp[0] = 0;

        foreach (var coin in coins)
        {
            for (int i = coin; i <= amount; i++)
            {
                dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
            }
        }

        return dp[amount] > amount ? -1 : dp[amount];
    }

    public int NumDecodings(string s)
    {
        int dp1 = 1;
        int dp2 = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            int dp = 0;
            if (s[i] != '0')
            {
                dp = dp1;
                if (i + 1 < s.Length && (s[i] == '1' ||
                    s[i] == '2' && s[i + 1] < '7'))
                {
                    dp += dp2;
                }
            }
            dp2 = dp1;
            dp1 = dp;
        }
        return dp1;
    }

    public int CountSubstrings(string s)
    {
        int result = 0;

        for (int i = 0; i < s.Length; i++)
        {
            result += ExtendedCountSubstrings(s, i, i);
            result += ExtendedCountSubstrings(s, i, i + 1);
        }

        return result;
    }

    private int ExtendedCountSubstrings(string s, int left, int right)
    {
        int count = 0;
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            count++;
            left--;
            right++;
        }

        return count;
    }

    public string LongestPalindrome(string s)
    {
        (int startIndex, int length) result = (0, 0);

        bool[,] dp = new bool[s.Length, s.Length];

        for (int left = s.Length - 1; left >= 0; left--)
        {
            for (int right = left; right < s.Length; right++)
            {
                bool sameLetters = s[left] == s[right];
                bool enoughLength = right - left <= 2;

                if (sameLetters && (enoughLength || dp[left + 1, right - 1]))
                {
                    dp[left, right] = true;

                    int currentLength = right - left + 1;

                    if (result.length < currentLength)
                    {
                        result = (left, currentLength);
                    }
                }
            }
        }

        return s.Substring(result.startIndex, result.length);
    }

    public int Rob(int[] nums)
    {
        if (nums.Length == 0) return 0;
        if (nums.Length == 1) return nums[0];

        return Math.Max(RobHoses(nums[1..]), RobHoses(nums[..^1]));
    }

    private int RobHoses(int[] nums)
    {
        int sum1 = 0;
        int sum2 = 0;

        foreach (int num in nums)
        {
            int currentSum = Math.Max(sum1 + num, sum2);
            sum1 = sum2;
            sum2 = currentSum;
        }

        return sum2;
    }

    public int MinCostClimbingStairs(int[] cost)
    {
        var dp = new int[cost.Length];
        dp[0] = cost[0];
        dp[1] = cost[1];

        for (int i = 2; i < cost.Length; i++)
        {
            dp[i] = Math.Min(dp[i - 1], dp[i - 2]) + cost[i];
        }

        return Math.Min(dp[^2], dp[^1]);
    }

    public int ClimbStairs(int n)
    {
        if (n <= 2) return n;

        int[] dp = new int[n + 1];
        dp[1] = 1;
        dp[2] = 2;

        for (int i = 3; i <= n; i++)
        {
            dp[i] = dp[i - 2] + dp[i - 1];
        }

        return dp[n];
    }

    public int SwimInWater(int[][] grid)
    {
        var size = grid.Length;
        var visited = new bool[size, size];
        var queue = new PriorityQueue<(int row, int column, int height), int>();
        var directions = new (short row, short column)[] { (-1, 0), (0, -1), (1, 0), (0, 1) };

        queue.Enqueue((0, 0, grid[0][0]), grid[0][0]);
        visited[0, 0] = true;

        while (queue.Count > 0)
        {
            var (row, column, height) = queue.Dequeue();

            if (row == size - 1 && column == size - 1)
                return height;

            foreach (var direction in directions)
            {
                var neighborRow = row + direction.row;
                var neighborColumn = column + direction.column;

                if (neighborRow < 0 ||
                    neighborRow >= size ||
                    neighborColumn < 0 ||
                    neighborColumn >= size)
                    continue;

                if (visited[neighborRow, neighborColumn])
                    continue;

                visited[neighborRow, neighborColumn] = true;

                var maxHeight = Math.Max(height, grid[neighborRow][neighborColumn]);

                queue.Enqueue((neighborRow, neighborColumn, maxHeight), maxHeight);
            }

        }

        return size * size;
    }

    public int MinCostConnectPoints(int[][] points)
    {
        var graph = new Dictionary<int, List<(int node, int cost)>>();

        for (int i = 0; i < points.Length; i++)
        {
            {
                var x1 = points[i][0];
                var x2 = points[i][1];

                if (!graph.ContainsKey(i)) graph.Add(i, []);

                for (int j = i + 1; j < points.Length; j++)
                {
                    var y1 = points[j][0];
                    var y2 = points[j][1];

                    var cost = Math.Abs(x1 - y1) + Math.Abs(x2 - y2);

                    if (!graph.ContainsKey(j)) graph.Add(j, []);

                    graph[i].Add((j, cost));
                    graph[j].Add((i, cost));
                }
            }
        }

        var result = 0;
        var visited = new HashSet<int>();

        var queue = new PriorityQueue<(int node, int cost), int>();
        queue.Enqueue((0, 0), 0);

        while (visited.Count < graph.Count && queue.Count > 0)
        {
            var (node, cost) = queue.Dequeue();

            if (visited.Contains(node)) continue;

            result += cost;

            visited.Add(node);

            foreach (var (neighbor, neighborCost) in graph[node])
            {
                if (visited.Contains(neighbor)) continue;
                queue.Enqueue((neighbor, neighborCost), neighborCost);
            }
        }

        return result;
    }

    public List<string> FindItinerary(List<List<string>> tickets)
    {
        var graph = new Dictionary<string, PriorityQueue<string, string>>();
        var startingTiket = "JFK";

        foreach (var ticket in tickets)
        {
            var source = ticket[0];
            var destination = ticket[1];

            if (!graph.ContainsKey(source))
            {
                graph[source] = new();
            }

            graph[source].Enqueue(destination, destination);
        }

        var stack = new Stack<string>();
        stack.Push(startingTiket);


        var result = new List<string>();

        while (stack.Count > 0)
        {
            var source = stack.Peek();
            if (!graph.ContainsKey(source) || graph[source].Count == 0)
            {
                result.Insert(0, stack.Pop());
            }
            else
            {
                var ticket = graph[source].Dequeue();
                stack.Push(ticket);
            }
        }

        return result;
    }

    public int NetworkDelayTime(int[][] times, int n, int k)
    {
        // Инициализируем граф в котором:
        // ключ это вершина из которой идёт сигнал
        // значение это пара длительности сигнала и конечная вершина.
        var graph = new Dictionary<int, List<(int time, int node)>>();

        // Заполняем граф.
        foreach (var time in times)
        {
            int sourceNode = time[0];
            int targetNode = time[1];
            int signalTime = time[2];

            if (!graph.ContainsKey(sourceNode))
            {
                graph[sourceNode] = [];
            }

            graph[sourceNode].Add((signalTime, targetNode));
        }

        // Инициализируем таблицу времени сигналов.
        var signalTimes = new Dictionary<int, int>();

        // Заполняем максимальным значением.
        for (int i = 1; i <= n; i++)
        {
            signalTimes[i] = int.MaxValue;
        }

        // Устанавливаем значение в 0 для стартовой вершины, из которой начинается расчет.
        signalTimes[k] = 0;

        // Ининциализируем очередь, в которой
        // значение это пара исходной вершины и минимальным временем.
        var queue = new PriorityQueue<(int sourceNode, int minTime), int>();

        // Добавляем в очередь первое значение, которое является
        queue.Enqueue((k, 0), 0);

        // Пока очередь не пустая, выполняем цикл.
        while (queue.Count > 0)
        {
            // Достаем из очереди пару из Вершины и Минимального времени.
            var (sourceNode, minTime) = queue.Dequeue();

            // Если минимальное время больше, чем уже расчитанное в таблице, то пропускаем эту вершину.
            if (minTime > signalTimes[sourceNode]) continue;

            // Если в графе нет данной вершины, то пропускаем её.
            if (!graph.ContainsKey(sourceNode)) continue;

            // В цикле достаем из графа соседей текущей вершины и время сигнала до них.
            foreach (var (time, neighborNode) in graph[sourceNode])
            {
                // Прибавляем к минимальному времени время сигнала до соседней вершины.
                var newTime = minTime + time;

                // Если новое время сигнала меньше, чем уже расчитанное время до соседней вершины,
                // то записываем это время в таблицу сигналов и добавляем в очередь данную вершину 
                if (newTime < signalTimes[neighborNode])
                {
                    signalTimes[neighborNode] = newTime;
                    queue.Enqueue((neighborNode, time), newTime);
                }
            }

        }

        int result = 0;

        // Пробегаемся по всем узлам и если какой-либо из них не пройден, то возвращаем - 1
        // иначе вовзращаем минимальный.
        for (int i = 1; i <= n; i++)
        {
            if (signalTimes[i] == int.MaxValue) return -1;
            result = Math.Max(result, signalTimes[i]);
        }

        return result;
    }


    public int[] FindRedundantConnection(int[][] edges)
    {
        int length = edges.Length + 1;

        var size = new int[length];
        var parent = new int[length];

        for (int i = 0; i < length; i++)
        {
            parent[i] = i;
            size[i] = i;
        }

        foreach (var edge in edges)
        {
            if (!FindRedundantConnectionUnion(edge[0], edge[1], parent, size))
            {
                return edge;
            }
        }

        return [];
    }

    private bool FindRedundantConnectionUnion(int n1, int n2, int[] parent, int[] size)
    {
        int root1 = FindRedundantConnectionFind(n1, parent);
        int root2 = FindRedundantConnectionFind(n2, parent);

        if (root1 == root2) return false;

        if (size[root1] < size[root2])
        {
            parent[root1] = root2;
            size[root2] += size[root1];
        }
        else
        {
            parent[root2] = root1;
            size[root1] += size[root2];
        }

        return true;
    }

    private int FindRedundantConnectionFind(int n, int[] parent)
    {
        if (parent[n] != n)
        {
            parent[n] = FindRedundantConnectionFind(parent[n], parent);
        }

        return parent[n];
    }

    public int CountComponents(int n, int[][] edges)
    {
        var graph = new List<int>[n];
        var visited = new HashSet<int>(n);
        var result = 0;

        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = [];
        }

        foreach (var edge in edges)
        {
            graph[edge[1]].Add(edge[0]);
            graph[edge[0]].Add(edge[1]);
        }

        for (int i = 0; i < n; i++)
        {
            if (visited.Contains(i)) continue;
            CountComponentsDFS(i, graph, visited);
            result++;
        }

        return result;
    }

    private void CountComponentsDFS(int n, List<int>[] graph, HashSet<int> visited)
    {
        if (visited.Contains(n)) return;

        visited.Add(n);

        foreach (var neighor in graph[n])
        {
            CountComponentsDFS(neighor, graph, visited);
        }
    }

    public bool ValidTree(int n, int[][] edges)
    {
        var graph = new List<int>[n];
        var visited = new bool[n];

        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = [];
        }

        foreach (var item in edges)
        {
            var edge = item[1];
            var vertex = item[0];

            graph[edge].Add(vertex);
            graph[vertex].Add(edge);
        }

        if (ValidTreeHasCircle(0, -1, graph, visited))
            return false;

        return visited.Count(x => x) == n;
    }

    public bool ValidTreeHasCircle(int n, int parantN, List<int>[] graph, bool[] visited)
    {
        if (visited[n]) return true;

        visited[n] = true;

        foreach (var item in graph[n])
        {
            if (item == parantN) continue;
            if (ValidTreeHasCircle(item, n, graph, visited)) return true;
        }

        return false;
    }

    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        if (numCourses == 0) return [];

        var result = new List<int>(numCourses);

        var graph = new List<int>[numCourses];
        var state = new int[numCourses];

        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = [];
        }

        foreach (var prerequisite in prerequisites)
        {
            var edge = prerequisite[0];
            var vertex = prerequisite[1];

            graph[edge].Add(vertex);
        }

        for (int i = 0; i < numCourses; i++)
        {
            if (FindOrderHasCircle(i, state, graph, result)) return [];
        }

        return [.. result];
    }

    public bool FindOrderHasCircle(int courseNum, int[] state, List<int>[] graph, List<int> result)
    {
        if (state[courseNum] == 1) return true;
        if (state[courseNum] == 2) return false;

        state[courseNum] = 1;

        foreach (var neighbor in graph[courseNum])
        {
            if (FindOrderHasCircle(neighbor, state, graph, result)) return true;
        }

        state[courseNum] = 2;
        result.Add(courseNum);
        return false;
    }

    public bool CanFinish(int numCourses, int[][] prerequisites)
    {
        if (numCourses == 0) return true;
        var graph = new List<int>[numCourses];

        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = [];
        }

        foreach (var prerequisite in prerequisites)
        {
            var edge = prerequisite[1];
            var vertice = prerequisite[0];

            graph[edge].Add(vertice);
        }

        var visited = new int[numCourses];

        for (int i = 0; i < numCourses; i++)
        {
            if (CanFinishHasCircle(i, graph, visited)) return false;
        }

        return true;
    }

    private bool CanFinishHasCircle(int courseNum, List<int>[] graph, int[] visited)
    {
        if (visited[courseNum] == 1) return true;
        if (visited[courseNum] == 2) return false;

        visited[courseNum] = 1;

        foreach (var course in graph[courseNum])
        {
            if (CanFinishHasCircle(course, graph, visited)) return true;
        }

        visited[courseNum] = 2;
        return false;
    }

    public void Solve(char[][] board)
    {
        int rows = board.Length;
        int columns = board[0].Length;

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (board[row][column] == 'O' &&
                    (row == 0 || column == 0 || row == rows - 1 || column == columns - 1))
                {
                    SolveDFS(board, row, column);
                }
            }
        }

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (board[row][column] == 'O')
                {
                    board[row][column] = 'X';
                }

                if (board[row][column] == 'T')
                {
                    board[row][column] = 'O';
                }
            }
        }
    }

    private void SolveDFS(char[][] board, int row, int column)
    {
        if (column < 0 || row < 0 || row >= board.Length || column >= board[row].Length)
        {
            return;
        }

        if (board[row][column] != 'O') return;

        board[row][column] = 'T';

        SolveDFS(board, row + 1, column);
        SolveDFS(board, row - 1, column);
        SolveDFS(board, row, column + 1);
        SolveDFS(board, row, column - 1);
    }

    public void DisplayBoard(char[][] board)
    {
        foreach (char[] row in board)
        {
            foreach (char col in row)
            {
                Console.Write(col + " ");
            }

            Console.WriteLine();
        }
    }

    private int[][] directions = new int[][] {
        new int[] { 1, 0 }, new int[] { -1, 0 },
        new int[] { 0, 1 }, new int[] { 0, -1 }
    };

    public List<List<int>> PacificAtlantic(int[][] heights)
    {
        int ROWS = heights.Length, COLS = heights[0].Length;
        bool[,] pac = new bool[ROWS, COLS];
        bool[,] atl = new bool[ROWS, COLS];

        for (int c = 0; c < COLS; c++)
        {
            Dfs(0, c, pac, heights);
            Dfs(ROWS - 1, c, atl, heights);
        }
        for (int r = 0; r < ROWS; r++)
        {
            Dfs(r, 0, pac, heights);
            Dfs(r, COLS - 1, atl, heights);
        }

        List<List<int>> res = new List<List<int>>();
        for (int r = 0; r < ROWS; r++)
        {
            for (int c = 0; c < COLS; c++)
            {
                if (pac[r, c] && atl[r, c])
                {
                    res.Add(new List<int> { r, c });
                }
            }
        }
        return res;
    }

    private void Dfs(int r, int c, bool[,] ocean, int[][] heights)
    {
        ocean[r, c] = true;
        foreach (var dir in directions)
        {
            int nr = r + dir[0], nc = c + dir[1];
            if (nr >= 0 && nr < heights.Length && nc >= 0 &&
                nc < heights[0].Length && !ocean[nr, nc] &&
                heights[nr][nc] >= heights[r][c])
            {
                Dfs(nr, nc, ocean, heights);
            }
        }
    }

    public int OrangesRotting(int[][] grid)
    {
        Queue<(int, int)> queue = new Queue<(int, int)>();
        int freshBananas = 0;
        int count = 0;

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 2)
                {
                    queue.Enqueue((i, j));
                }

                if (grid[i][j] == 1)
                {
                    freshBananas++;
                }
            }
        }

        while (freshBananas > 0 && queue.Count > 0)
        {
            int size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                (int, int) rottenBanan = queue.Dequeue();

                foreach ((int, int) direction in _directions)
                {
                    int row = direction.Item1 + rottenBanan.Item1;
                    int column = direction.Item2 + rottenBanan.Item2;

                    if (row < 0 || row == grid.Length || column < 0 || column > grid[0].Length || grid[row][column] != 1)
                    {
                        continue;
                    }

                    grid[row][column] = 2;
                    queue.Enqueue((row, column));
                    freshBananas--;
                }
            }

            count++;
        }

        return freshBananas == 0 ? count : -1;
    }

    public void islandsAndTreasure(int[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 0)
                {
                    islandsAndTreasureDFS(grid, i, j, 0);
                }
            }
        }
    }

    private void islandsAndTreasureDFS(int[][] grid, int row, int column, int path)
    {
        if (column < 0 || row < 0 || row >= grid.Length || column >= grid[row].Length)
        {
            return;
        }

        if (grid[row][column] == -1) return;

        if (grid[row][column] != 0)
        {
            if (path > grid[row][column]) return;
            else grid[row][column] = path;
        }

        islandsAndTreasureDFS(grid, row + 1, column, path + 1);
        islandsAndTreasureDFS(grid, row - 1, column, path + 1);
        islandsAndTreasureDFS(grid, row, column + 1, path + 1);
        islandsAndTreasureDFS(grid, row, column - 1, path + 1);
    }

    public Node CloneGraph(Node node)
    {
        var dict = new Dictionary<Node, Node>();
        return CloneGraphDFS(node, dict);
    }

    private Node CloneGraphDFS(Node node, Dictionary<Node, Node> dict)
    {
        if (node == null) return null;
        if (dict.ContainsKey(node)) return dict[node];

        var copy = new Node(node.val);
        dict[node] = copy;

        foreach (var child in node.children)
        {
            copy.children.Add(CloneGraphDFS(child, dict));
        }

        return copy;
    }

    public int MaxAreaOfIsland(int[][] grid)
    {
        var result = 0;

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    result = Math.Max(result, MaxAreaOfIslandDFS(grid, i, j));
                }
            }
        }

        return result;
    }

    public int MaxAreaOfIslandDFS(int[][] grid, int row, int column)
    {
        if (column < 0 || row < 0 || row >= grid.Length || column >= grid[row].Length)
        {
            return 0;
        }

        if (grid[row][column] == 0)
        {
            return 0;
        }

        grid[row][column] = 0;

        var result = 1;

        result += MaxAreaOfIslandDFS(grid, row + 1, column);
        result += MaxAreaOfIslandDFS(grid, row - 1, column);
        result += MaxAreaOfIslandDFS(grid, row, column + 1);
        result += MaxAreaOfIslandDFS(grid, row, column - 1);

        return result;
    }

    public int NumIslands(char[][] grid)
    {
        var result = 0;

        var rows = grid.Length;
        var cols = grid[0].Length;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == '1')
                {
                    result++;
                    NumIslandsDFS(grid, i, j);
                }
            }
        }

        return result;
    }

    public void NumIslandsDFS(char[][] grid, int row, int column)
    {
        if (column < 0 || row < 0 || row >= grid.Length || column >= grid[row].Length)
        {
            return;
        }

        if (grid[row][column] == '0')
        {
            return;
        }

        grid[row][column] = '0';

        NumIslandsDFS(grid, row + 1, column);
        NumIslandsDFS(grid, row - 1, column);
        NumIslandsDFS(grid, row, column + 1);
        NumIslandsDFS(grid, row, column - 1);
    }

    public List<string> FindWords(char[][] board, string[] words)
    {
        if (words.Length == 0) return [];

        var result = new HashSet<string>();
        var root = FindWordsGetTrieNode(words);
        var visited = new bool[board.Length, board[0].Length];

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                FindWordsDFS(board, i, j, root, [], result, visited);
            }
        }

        return result.ToList();
    }

    private void FindWordsDFS(
        char[][] board,
        int row,
        int column,
        TrieNode node,
        List<char> current,
        HashSet<string> result,
        bool[,] visited)
    {

        if (column < 0 || row < 0 || row >= board.Length || column >= board[row].Length)
        {
            return;
        }

        if (visited[row, column])
        {
            return;
        }

        if (!node.Children.ContainsKey(board[row][column]))
        {
            return;
        }

        visited[row, column] = true;
        current.Add(board[row][column]);
        node = node.Children[board[row][column]];

        if (node.IsEndOfWord)
        {
            result.Add(string.Join("", current));
        }

        FindWordsDFS(board, row + 1, column, node, current, result, visited);
        FindWordsDFS(board, row, column + 1, node, current, result, visited);
        FindWordsDFS(board, row - 1, column, node, current, result, visited);
        FindWordsDFS(board, row, column - 1, node, current, result, visited);

        current.RemoveAt(current.Count - 1);
        visited[row, column] = false;
    }

    private TrieNode FindWordsGetTrieNode(string[] words)
    {
        var root = new TrieNode();

        foreach (var word in words)
        {
            var current = root;

            foreach (var letter in word)
            {
                if (!current.Children.ContainsKey(letter))
                {
                    current.Children.Add(letter, new());
                }

                current = current.Children[letter];
            }

            current.IsEndOfWord = true;
        }

        return root;
    }


    private readonly IDictionary<char, string> _letters = new Dictionary<char, string>()
    {
        { '2', "abc" },
        { '3', "def" },
        { '4', "ghi" },
        { '5', "jkl" },
        { '6', "mno" },
        { '7', "pqrs" },
        { '8', "tuv" },
        { '9', "wxyz" },
    };

    public List<string> LetterCombinations(string digits)
    {
        if (digits.Length == 0) return [];
        var result = new List<string>();
        LetterCombinationsDFS(digits, 0, [], result);
        return result;
    }

    private void LetterCombinationsDFS(
        string digits,
        int index,
        List<char> current,
        List<string> letterCombinations)
    {
        if (index >= digits.Length)
        {
            letterCombinations.Add(string.Join("", current));
            return;
        }

        var letters = _letters[digits[index]];

        for (int i = 0; i < letters.Length; i++)
        {
            current.Add(letters[i]);
            LetterCombinationsDFS(digits, index + 1, current, letterCombinations);
            current.RemoveAt(current.Count - 1);
        }
    }

    public List<List<string>> Partition(string s)
    {
        var result = new List<List<string>>();

        PartitionDFS(s, 0, [], result);

        return result;
    }

    private void PartitionDFS(
        string s,
        int index,
        List<string> partition,
        List<List<string>> result)
    {
        if (index >= s.Length)
        {
            result.Add([.. partition]);
            return;
        }

        for (int i = index; i < s.Length; i++)
        {
            if (PartitionIsPalindrome(s, index, i))
            {
                partition.Add(s.Substring(index, i - index + 1));
                PartitionDFS(s, i + 1, partition, result);
                partition.RemoveAt(partition.Count - 1);
            }
        }
    }

    private bool PartitionIsPalindrome(string s, int left, int right)
    {
        while (left < right)
        {
            if (s[left] != s[right])
            {
                return false;
            }
            left++;
            right--;
        }

        return true;
    }


    private readonly (short, short)[] _directions = [(-1, 0), (0, -1), (1, 0), (0, 1)];

    public bool Exist(char[][] board, string word)
    {
        var rows = board.Length;
        var columns = board[0].Length;

        bool[,] visited = new bool[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (ExistDFS(board, word, i, j, 0, visited))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool ExistDFS(
        char[][] board,
        string word,
        int row,
        int column,
        int letterIndex,
        bool[,] visited)
    {
        if (letterIndex >= word.Length) return true;

        if (column < 0 || row < 0 || row >= board.Length || column >= board[row].Length)
        {
            return false;
        }

        if (visited[row, column]) return false;

        if (word[letterIndex] != board[row][column])
        {
            return false;
        }

        var result = false;

        visited[row, column] = true;
        foreach (var direction in _directions)
        {
            result = result || ExistDFS(board, word, row + direction.Item1, column + direction.Item2, letterIndex + 1, visited);
        }
        visited[row, column] = false;

        return result;
    }

    public List<string> GenerateParenthesis(int n)
    {
        var parenthesis = new List<string>();
        GenerateParenthesisDFS(n, 0, 0, "", parenthesis);
        return parenthesis;
    }

    private void GenerateParenthesisDFS(
        int length,
        int open,
        int closed,
        string line,
        List<string> parenthesis)
    {
        if (open == length && closed == length)
        {
            parenthesis.Add(line);
            return;
        }

        if (open < length)
        {
            GenerateParenthesisDFS(length, open + 1, closed, line + "(", parenthesis);
        }

        if (closed < open)
        {
            GenerateParenthesisDFS(length, open, closed + 1, line + ")", parenthesis);
        }
    }

    public List<List<int>> SubsetsWithDup(int[] nums)
    {
        Array.Sort(nums);
        var subset = new List<int>();
        var subsets = new List<List<int>>();

        SubsetsWithDupDFS(nums, 0, subset, subsets);

        return subsets;
    }

    private void SubsetsWithDupDFS(int[] nums, int index, List<int> subset, List<List<int>> subsets)
    {
        if (index >= nums.Length)
        {
            subsets.Add([.. subset]);
            return;
        }

        subset.Add(nums[index]);
        SubsetsWithDupDFS(nums, index + 1, subset, subsets);

        subset.RemoveAt(subset.Count - 1);

        while (index + 1 < nums.Length && nums[index] == nums[index + 1])
        {
            index++;
        }

        SubsetsWithDupDFS(nums, index + 1, subset, subsets);
    }

    public List<List<int>> Permute(int[] nums)
    {
        var result = new List<List<int>>();
        PermuteDFS(nums, [], new bool[nums.Length], result);
        return result;
    }

    private void PermuteDFS(
        int[] nums,
        List<int> permutation,
        bool[] picked,
        List<List<int>> result)
    {
        if (permutation.Count == nums.Length)
        {
            result.Add([.. permutation]);
            Console.WriteLine($"Added [{string.Join(",", permutation)}]");
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            Console.WriteLine($"Checking {i}...");
            if (!picked[i])
            {
                Console.WriteLine($"{i} isn't picked.");
                permutation.Add(nums[i]);
                Console.WriteLine($"Added {nums[i]}");
                picked[i] = true;
                PermuteDFS(nums, permutation, picked, result);
                Console.WriteLine($"Premutation before removing: [{string.Join(",", permutation)}]");
                permutation.RemoveAt(permutation.Count - 1);
                Console.WriteLine($"Premutation after removing: [{string.Join(",", permutation)}]");
                picked[i] = false;
            }
            else
            {
                Console.WriteLine($"{i} is already picked...");
            }
            Console.WriteLine();
        }
    }

    public List<List<int>> CombinationSum2(int[] candidates, int target)
    {
        var combinations = new List<List<int>>();
        Array.Sort(candidates);
        CombinationSum2DFS(candidates, 0, 0, target, [], combinations);
        return combinations;
    }

    private void CombinationSum2DFS(
        int[] candidates,
        int index,
        int sum,
        int target,
        List<int> combination,
        List<List<int>> combinations)
    {
        if (sum == target)
        {
            combinations.Add([.. combination]);
            return;
        }

        if (sum > target || index >= candidates.Length)
        {
            return;
        }

        combination.Add(candidates[index]);
        CombinationSum2DFS(candidates, index + 1, sum + candidates[index], target, combination, combinations);
        combination.RemoveAt(combination.Count - 1);

        while (index + 1 < candidates.Length && candidates[index] == candidates[index + 1])
        {
            index++;
        }

        CombinationSum2DFS(candidates, index + 1, sum, target, combination, combinations);
    }

    public List<List<int>> CombinationSum(int[] nums, int target)
    {
        var result = new List<List<int>>();
        CombinationSumDFS(nums, 0, 0, target, [], result);
        return result;
    }

    private void CombinationSumDFS(int[] nums, int index, int sum, int target, List<int> subset, List<List<int>> result)
    {
        if (sum == target)
        {
            result.Add([.. subset]);
            return;
        }

        if (sum > target || index >= nums.Length)
        {
            return;
        }

        subset.Add(nums[index]);
        CombinationSumDFS(nums, index, sum + nums[index], target, subset, result);
        subset.RemoveAt(subset.Count - 1);
        CombinationSumDFS(nums, index + 1, sum, target, subset, result);
    }

    public int LengthOfLastWord(string s)
    {
        var result = 0;
        var index = s.Length - 1;

        while (index >= 0)
        {
            if (s[index] == ' ') index--;
            break;
        }

        if (index < 0) return result;

        while (index >= 0)
        {
            if (s[index] == ' ') break;
            result++;
            index--;
        }

        return result;
    }

    public List<List<int>> Subsets(int[] nums)
    {
        var res = new List<List<int>>();
        var subset = new List<int>();
        Dfs(nums, 0, subset, res);
        return res;
    }

    private void Dfs(int[] nums, int i, List<int> subset, List<List<int>> res)
    {
        Console.WriteLine();
        Console.WriteLine("Start method.");
        Console.Write($"nums: [{string.Join(",", nums)}] index: {i} subset: [{string.Join(", ", subset)}]. Result: ");

        Console.Write(" [");
        foreach (var r in res)
        {
            Console.Write("[");
            Console.Write(string.Join(",", r));
            Console.Write("]");
        }
        Console.Write("]");
        Console.WriteLine();

        if (i >= nums.Length)
        {
            Console.WriteLine();
            res.Add(new List<int>(subset));
            Console.WriteLine($"Added to result [{string.Join(", ", subset)}]");
            return;
        }

        subset.Add(nums[i]);
        Console.WriteLine($"Added to subset: {nums[i]}. Subset: [{string.Join(", ", subset)}]");
        Dfs(nums, i + 1, subset, res);
        Console.Write($"Deleted from subset by index {subset.Count - 1}. ");
        subset.RemoveAt(subset.Count - 1);
        Console.WriteLine($"Subset: [{string.Join(", ", subset)}]");
        Dfs(nums, i + 1, subset, res);
        Console.WriteLine("End method");
        Console.WriteLine();
    }

    public int LastStoneWeight(int[] stones)
    {
        var queue = new PriorityQueue<int, int>(stones.Length);

        foreach (int stone in stones)
        {
            queue.Enqueue(-stone, -stone);
        }

        while (queue.Count > 1)
        {
            var first = queue.Dequeue();
            var second = queue.Dequeue();

            if (first < second)
            {
                queue.Enqueue(first - second, first - second);
            }
        }

        return Math.Abs(queue.Peek());
    }

    public int RemoveElement(int[] nums, int val)
    {
        var result = new List<int>();

        foreach (int n in nums)
        {
            if (n != val) result.Add(n);
        }

        nums = result.ToArray();
        return result.Count;
    }

    public string MergeAlternately(string word1, string word2)
    {
        var sb = new StringBuilder(word1.Length + word2.Length);
        var left = 0;
        var right = 0;

        while (left < word1.Length || right < word2.Length)
        {
            if (left > right)
            {
                if (right < word2.Length)
                {
                    sb.Append(word2[right]);
                }
                right++;
            }

            if (left <= right)
            {
                if (left < word1.Length)
                {
                    sb.Append(word1[left]);
                }
                left++;
            }
        }

        return sb.ToString();
    }

    public bool SearchMatrix(int[][] matrix, int target)
    {
        int left = 0;
        int right = matrix.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (matrix[mid][0] <= target && matrix[mid][^1] >= target)
            {
                int innerLeft = 0;
                int innerRight = matrix[mid].Length - 1;

                while (innerLeft <= innerRight)
                {
                    int innerMid = innerLeft + (innerRight - innerLeft) / 2;

                    if (matrix[mid][innerMid] == target)
                    {
                        return true;
                    }

                    if (matrix[mid][innerMid] > target)
                    {
                        innerRight = innerMid - 1;
                    }

                    if (matrix[mid][innerMid] < target)
                    {
                        innerLeft = innerMid + 1;
                    }
                }

                return false;
            }

            if (matrix[mid][0] > target)
            {
                right = mid - 1;
            }

            if (matrix[mid][^1] < target)
            {
                left = mid + 1;
            }
        }

        return false;
    }


    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null) return root;

        var temp = root.left;
        root.left = root.right;
        root.right = temp;

        InvertTree(root.left);
        InvertTree(root.right);

        return root;
    }

    public int MaxDepth(TreeNode root)
    {
        if (root == null) return 0;

        return Math.Min(MaxDepth(root.left) + 1, MaxDepth(root.right) + 1);
    }

    public bool IsSameTree(TreeNode p, TreeNode q)
    {
        if (p == null && q == null) return true;
        if (p == null && q != null) return false;
        if (p != null && q == null) return false;

        var leftQueue = new Queue<TreeNode>();
        var rightQueue = new Queue<TreeNode>();

        leftQueue.Enqueue(p);
        rightQueue.Enqueue(q);

        while (leftQueue.Count > 0)
        {
            var leftSize = leftQueue.Count;
            var rightSize = rightQueue.Count;

            if (leftSize != rightSize) return false;

            for (int i = 0; i < leftSize; i++)
            {
                var leftNode = leftQueue.Dequeue();
                var rightNode = rightQueue.Dequeue();

                if (leftNode?.val != rightNode?.val) return false;

                if (leftNode != null)
                {
                    leftQueue.Enqueue(leftNode.left);
                    leftQueue.Enqueue(leftNode.right);
                }

                if (rightNode != null)
                {
                    rightQueue.Enqueue(rightNode.left);
                    rightQueue.Enqueue(rightNode.right);
                }
            }
        }

        return true;
    }

    public bool IsSubtree(TreeNode root, TreeNode subRoot)
    {
        if (subRoot == null) return true;
        if (root == null) return false;

        if (SameTree(root, subRoot)) return true;

        return IsSubtree(root.left, subRoot) ||
               IsSubtree(root.right, subRoot);
    }

    public bool SameTree(TreeNode root, TreeNode subRoot)
    {
        if (root == null && subRoot == null)
        {
            return true;
        }

        if (root != null && subRoot != null && root.val == subRoot.val)
        {
            return SameTree(root.left, subRoot.left) &&
                   SameTree(root.right, subRoot.right);
        }

        return false;
    }

    public int KthSmallest(TreeNode root, int k)
    {
        if (root == null) return 0;

        var numbers = new HashSet<int>();

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                numbers.Add(node.val);

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }
        }

        var orderedNumbers = numbers.Order();

        for (int i = 0; i < k; i++)
        {
            if (i == k - 1)
            {
                return orderedNumbers.ElementAt(i);
            }
        }

        return -1;
    }

    public int MaxPathSum(TreeNode root)
    {
        if (root == null) return 0;

        return root.val + MaxPathSumDFS(root.left) + MaxPathSumDFS(root.right);
    }

    private int MaxPathSumDFS(TreeNode root)
    {
        if (root == null) return 0;
        return root.val + Math.Max(MaxPathSumDFS(root.left), MaxPathSumDFS(root.right));
    }
}

public class KthLargest
{
    private int _k;
    private PriorityQueue<int, int> _heap;

    public KthLargest(int k, int[] nums)
    {
        _k = k;

        _heap = new PriorityQueue<int, int>(nums.Length);

        foreach (int num in nums)
        {
            _heap.Enqueue(num, num);
            if (_heap.Count > _k)
            {
                _heap.Dequeue();
            }
        }
    }

    public int Add(int val)
    {
        _heap.Enqueue(val, val);

        if (_heap.Count > _k)
        {
            _heap.Dequeue();
        }

        return _heap.Peek();
    }
}
