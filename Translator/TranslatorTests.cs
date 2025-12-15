using NUnit.Framework;

namespace AntlrExample.Translator;

public class TranslatorTests
{
    [TestCase("Add.asm")]
    [TestCase("Max.asm")]
    [TestCase("Rect.asm")]
    [TestCase("Pong.asm")]
    public void TranslateSampleProgram(string filename)
    {
        var asmFile = Path.Combine("Samples", filename);
        var actualHack = Translator.TranslateFile(asmFile);
        var hackFile = Path.ChangeExtension(asmFile, ".hack");
        var expectedHack = File.ReadAllLines(hackFile);
        Assert.AreEqual(expectedHack, actualHack);
    }
}