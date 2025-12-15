using Antlr4.Runtime.Tree;

namespace AntlrExample.Translator;

public class TranslatorVisitor : asmBaseVisitor<string[]>
{
    private readonly Dictionary<string, short> _symbolTable;
    private short _variableCounter = 16;

    public TranslatorVisitor(Dictionary<string, short> symbolTable)
    {
        _symbolTable = symbolTable;
    }

    private static readonly Dictionary<string, string> CompMap = new()
    {
        { "0",   "0101010" }, { "1",   "0111111" }, { "-1",  "0111010" },
        { "D",   "0001100" }, { "A",   "0110000" }, { "!D",  "0001101" },
        { "!A",  "0110001" }, { "-D",  "0001111" }, { "-A",  "0110011" },
        { "D+1", "0011111" }, { "A+1", "0110111" }, { "D-1", "0001110" },
        { "A-1", "0110010" }, { "D+A", "0000010" }, { "D-A", "0010011" },
        { "A-D", "0000111" }, { "D&A", "0000000" }, { "D|A", "0010101" },
        { "M",   "1110000" }, { "!M",  "1110001" }, { "-M",  "1110011" },
        { "M+1", "1110111" }, { "M-1", "1110010" }, { "D+M", "1000010" },
        { "D-M", "1010011" }, { "M-D", "1000111" }, { "D&M", "1000000" },
        { "D|M", "1010101" }
    };

    private static readonly Dictionary<string, string> JumpMap = new()
    {
        { "", "000" },
        { "JGT", "001" }, { "JEQ", "010" }, { "JGE", "011" },
        { "JLT", "100" }, { "JNE", "101" }, { "JLE", "110" },
        { "JMP", "111" }
    };

    public override string[] VisitProg(asmParser.ProgContext context)
    {
        return context.children.Select(child => child?.Accept(this))
                      .Where(x => x != null)
                      .SelectMany(x => x!)
                      .ToArray();
    }

    public override string[] VisitAInstruction(asmParser.AInstructionContext context)
    {
        short? value = null;
        var intToken = context.A_INT()?.GetText().TrimStart('@');
        if (intToken != null)
        {
            value = short.Parse(intToken);
        }

        var symbolToken = context.A_SYMBOL()?.GetText().TrimStart('@');
        if (symbolToken != null)
        {
            value = GetSymbolValue(symbolToken);
        }

        if (value == null)
        {
            throw new InvalidOperationException("A instruction is neither number or symbol");
        }

        return [Convert.ToString(value.Value, 2).PadLeft(16, '0')];
    }

    public override string[] VisitCInstruction(asmParser.CInstructionContext context)
    {
        var dest = VisitPart<asmParser.DestContext>(context, "000");
        var comp = VisitPart<asmParser.CompContext>(context, "0000000");
        var jump = VisitPart<asmParser.JumpContext>(context, "000");

        return [$"111{comp}{dest}{jump}"];
    }

    public override string[] VisitDest(asmParser.DestContext context)
    {
        var dest = context.GetText() ?? "";
        var res = "";
        res += dest.Contains('A') ? "1" : "0";
        res += dest.Contains('D') ? "1" : "0";
        res += dest.Contains('M') ? "1" : "0";
        return [res];
    }

    public override string[] VisitComp(asmParser.CompContext context)
    {
        var comp = context.GetText();
        return [CompMap[comp]];
    }

    public override string[] VisitJump(asmParser.JumpContext context)
    {
        var jump = context.GetText();
        return [JumpMap[jump]];
    }

    private string VisitPart<T>(asmParser.CInstructionContext context, string defaultValue)
        where T : IParseTree
    {
        var child = context.GetChild<T>(0);
        if (child == null)
            return defaultValue;
        var value = base.Visit(child);
        return value == null ? defaultValue : value[0];
    }

    private short GetSymbolValue(string symbol)
    {
        if (!_symbolTable.TryGetValue(symbol, out var value))
        {
            value = _variableCounter++;
            _symbolTable[symbol] = value;
        }

        return value;
    }
}