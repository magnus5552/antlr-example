using Antlr4.Runtime;
using NUnit.Framework;

namespace AntlrExample.Calculator;

public class CalculatorTests
{
    [TestCase("1 + 2", 3.0)]
    [TestCase("-1 + -2", -3.0)]
    [TestCase("1 / 2", 0.5)]
    [TestCase("1 / 4", 0.25)]
    [TestCase("1/4 + 1/4", 0.5)]
    [TestCase("4 / 1 * 2", 8.0)]
    [TestCase("4 / 0", double.PositiveInfinity)]
    [TestCase("-4 / 0", double.NegativeInfinity)]
    [TestCase("2 * 3", 6.0)]
    [TestCase("-2 * 3", -6.0)]
    [TestCase("2 ^ 3", 8.0)]
    [TestCase("(2 + 3) * 2 - 1", 9.0)]
    [TestCase("1 - 2 * 3 + 4", -1.0)]
    [TestCase("1 + 2 * 3 - 4", 3.0)]
    [TestCase("1+2", 3.0)]
    [TestCase("-1+-2", -3.0)]
    [TestCase("1/2", 0.5)]
    [TestCase("1/4", 0.25)]
    [TestCase("1/4+1/4", 0.5)]
    [TestCase("4/1*2", 8.0)]
    [TestCase("4/0", double.PositiveInfinity)]
    [TestCase("-4/0", double.NegativeInfinity)]
    [TestCase("2*3", 6.0)]
    [TestCase("-2*3", -6.0)]
    [TestCase("2^3", 8.0)]
    [TestCase("(2+3)*2-1", 9.0)]
    [TestCase("1-2*3+4", -1.0)]
    [TestCase("1+2*3-4", 3.0)]
    [TestCase("3-4", -1.0)]
    [TestCase("3--4", 7.0)]
    [TestCase("-(1+2)", -3.0)]
    public void CalculatorTest(string input, double expected)
    {
        var stream = CharStreams.fromString(input);
        var lexer = new CalculatorLexer(stream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new CalculatorParser(tokens)
        {
            ErrorHandler = new BailErrorStrategy()
        };
        var visitor = new CalculatorVisitor();
        var actual = visitor.Visit(parser.start());
        Assert.AreEqual(actual, expected);
    }
}