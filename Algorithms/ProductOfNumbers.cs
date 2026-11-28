namespace Algorithms;

public class ProductOfNumbers
{
    private readonly int[] _products;
    private int _index = 0;

    private int _zeroIndex = -1;

    public ProductOfNumbers()
    {
        _products = new int[102];
        _products[_index++] = 1;
    }

    public void Add(int num)
    {
        if (num == 0)
        {
            _products[_index] = 1;
            _zeroIndex = _index;
        }
        else _products[_index] = _products[_index - 1] * num;

        _index++;
    }

    public int GetProduct(int k)
    {
        if (k > _index) return 0;

        if (_index - k <= _zeroIndex) return 0;

        return _products[_index - 1] / _products[_index - 1 - k];
    }
}