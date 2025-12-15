namespace AntlrExample.Calculator;

public class CalculatorVisitor : CalculatorBaseVisitor<double>
{
    public override double VisitNumber(CalculatorParser.NumberContext context)
    {
        throw new NotImplementedException();
    }

    public override double VisitParentheses(CalculatorParser.ParenthesesContext context)
    {
        throw new NotImplementedException();
    }

    public override double VisitNegation(CalculatorParser.NegationContext context)
    {
        throw new NotImplementedException();
    }

    public override double VisitPower(CalculatorParser.PowerContext context)
    {
        throw new NotImplementedException();
    }

    public override double VisitAdditionOrSubtraction(CalculatorParser.AdditionOrSubtractionContext context)
    {
        throw new NotImplementedException();
    }

    public override double VisitMultiplicationOrDivision(CalculatorParser.MultiplicationOrDivisionContext context)
    {
        throw new NotImplementedException();
    }
}