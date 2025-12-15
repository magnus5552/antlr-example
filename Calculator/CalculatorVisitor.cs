namespace AntlrExample.Calculator;

public class CalculatorVisitor : CalculatorBaseVisitor<double>
{
    public override double VisitNumber(CalculatorParser.NumberContext context)
    {
        return double.Parse(context.NUMBER().GetText());
    }

    public override double VisitParentheses(CalculatorParser.ParenthesesContext context)
    {
        return base.Visit(context.inner);
    }

    public override double VisitNegation(CalculatorParser.NegationContext context)
    {
        return -base.Visit(context.right);
    }

    public override double VisitPower(CalculatorParser.PowerContext context)
    {
        return Math.Pow(base.Visit(context.left), base.Visit(context.right));
    }

    public override double VisitAdditionOrSubtraction(CalculatorParser.AdditionOrSubtractionContext context)
    {
        if (context.@operator.Text == "+")
            return base.Visit(context.left) + base.Visit(context.right);
        return base.Visit(context.left) - base.Visit(context.right);
    }

    public override double VisitMultiplicationOrDivision(CalculatorParser.MultiplicationOrDivisionContext context)
    {
        if (context.@operator.Text == "*")
            return base.Visit(context.left) * base.Visit(context.right);
        return base.Visit(context.left) / base.Visit(context.right);
    }
}