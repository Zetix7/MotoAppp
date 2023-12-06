using MotoApp;

var stack = new BasicStack();
stack.Push(4.7);
stack.Push(47);
stack.Push(43.67);

double sum = 0;

while (stack.Count > 0)
{
    double item = stack.Pop();
    Console.WriteLine($"Item: {item}");
    sum += item;
}

Console.WriteLine(sum);