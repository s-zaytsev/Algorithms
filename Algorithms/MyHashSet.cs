namespace Algorithms;

public class MyHashSet
{
    private readonly bool[] _set;

    public MyHashSet()
    {
        _set = new bool[1_000_001];
    }

    public void Add(int key)
    {
        _set[key] = true;
    }

    public void Remove(int key)
    {
        _set[key] = false;
    }

    public bool Contains(int key)
    {
        return _set[key];
    }
}
