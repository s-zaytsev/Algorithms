namespace Algorithms;

public class MyQueue
{
    private Stack<int> output;
    private Stack<int> input;

    public MyQueue()
    {
        output = new Stack<int>();
        input = new Stack<int>();
    }

    public void Push(int x)
    {
        input.Push(x);
    }

    public int Pop()
    {
        if (output.Count == 0)
        {
            while (input.Count > 0)
            {
                output.Push(input.Pop());
            }
        }

        return output.Pop();
    }

    public int Peek()
    {
        if (output.Count == 0)
        {
            while (input.Count > 0)
            {
                output.Push(input.Pop());
            }
        }

        return output.Peek();
    }

    public bool Empty()
    {
        return input.Count == 0 && output.Count == 0;
    }
}