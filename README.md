# Antlr4

## Сгенерировать парсер для hack
```powershell
java -jar .\antlr-4.13.2-complete.jar -Dlanguage=CSharp -visitor .\Translator\asm.g4 -o AsmParser -Xexact-output-dir
```

## Сгенерировать парсер для калькулятора
```powershell
antlr4 -Dlanguage=CSharp -visitor .\Calculator\Calculator.g4 -o CalculatorParser -Xexact-output-dir
```