using Antlr4.Runtime;

namespace AntlrExample.Translator;

public class Translator
{
    public static string[] TranslateFile(string path)
    {
        var stream = CharStreams.fromPath(path);
        var lexer = new asmLexer(stream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new asmParser(tokens)
        {
            ErrorHandler = new BailErrorStrategy()
        };
        var labelVisitor = new LabelVisitor();
        labelVisitor.Visit(parser.prog());
        var labels = labelVisitor.Labels;
        tokens.Seek(0);
        var visitor = new TranslatorVisitor(labels);
        return visitor.Visit(parser.prog());
    }
}