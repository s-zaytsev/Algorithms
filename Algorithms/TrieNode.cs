namespace Algorithms;

public class TrieNode
{
    public Dictionary<char, TrieNode> Children = [];
    public bool IsEndOfWord = false;
}
