namespace Algorithms;


public class PrefixTree
{
    private readonly TrieNode root;

    public PrefixTree()
    {
        root = new TrieNode();
    }

    public void Insert(string word)
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

    public bool Search(string word)
    {
        var current = root;
        foreach (var letter in word)
        {
            if (!current.Children.ContainsKey(letter))
            {
                return false;
            }

            current = current.Children[letter];
        }

        return current.IsEndOfWord;
    }

    public bool StartsWith(string prefix)
    {
        var current = root;
        foreach (var letter in prefix)
        {
            if (!current.Children.ContainsKey(letter))
            {
                return false;
            }

            current = current.Children[letter];
        }

        return true;
    }
}

public class WordDictionary
{
    private readonly TrieNode root;

    public WordDictionary()
    {
        root = new TrieNode();
    }

    public void AddWord(string word)
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

    public bool Search(string word)
    {
        var current = root;

        return SearchDFS(word, 0, current);
    }

    private bool SearchDFS(string word, int index, TrieNode node)
    {
        if (index >= word.Length)
        {
            return node.IsEndOfWord;
        }

        if (word[index] != '.')
        {
            if (node.Children.ContainsKey(word[index]))
            {
                var next = node.Children[word[index]];
                if (next == null)
                {
                    return false;
                }

                return SearchDFS(word, index + 1, next);
            }
            else
            {
                return false;
            }
        }
        else
        {
            foreach (var child in node.Children)
            {
                var isFound = SearchDFS(word, index + 1, child.Value);
                if (isFound)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
