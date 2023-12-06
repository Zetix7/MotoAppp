
namespace MotoApp.Tests;

public class BasicStackTests
{
    [Test]
    public void Push3ValuesReturnFirstOnStack()
    {
        var stack = new BasicStack();
        stack.Push(10.3);
        stack.Push(71.9);
        stack.Push(11);

        var result = stack.Pop();

        Assert.AreEqual(11, result);
    }
}