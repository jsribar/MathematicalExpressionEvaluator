# MathematicalExpressionEvaluator
C# library for parsing and evaluation of mathematical expressions with one or more variables. Basic characteristics: 
* supports basic arithmentic operations: 
    * addition (<code>+</code>), 
    * subtraction (<code>-</code>, U+2212 <code>−</code>), 
    * multiplication (<code>*</code>, U+00D7 <code>×</code>, U+22C5 <code>⋅</code>), 
    * division (<code>/</code>, U+00F7 <code>÷</code>), 
    * exponentiation (<code>^</code>), 
* supports extra sign (<code>-</code>, U+2212 <code>−</code> or <code>+</code>) preceding a value, function or variable,
* takes care of operator precedence, 
* supports parentheses to override operator precedence, 
* includes standard mathematical functions with one or two arguments,
* includes basic mathematical constants (_e_, π),
* additional custom functions and constants can be defined in the runtime.

Project was initially created as a demonstration of Interpreter design pattern but was later extended with parser.

## Basic Usage
An example how to evaluate a simple expression with variable _x_:
1. Create <code>Parser</code> object.
2. Invoke <code>Parse</code> method and pass the string with mathematical expression. <code>Parse</code> method returns <code>IExpression</code> object.
3. Create a <code>Context</code> object and pass the value of variable _x_ for which mathematical expression should be evaluated.
3. Invoke <code>Evaluate</code> method of <code>IExpression</code> interface and pass the context object. <code>Evaluate</code> method returns the value of mathematical expression for the context (i.e. value of variable _x_) provided.
```
using JSribar.MathematicalExpressionEvaluator;
...
var parser = new Parser();
var mathExpression = "x + 3";
var expression = parser.Parse(mathExpression);
var x = 2;
var context = new Context(x); 
var result = expression.Evaluate(context);
Console.WriteLine($"Value of {mathExpression} for x={x} is {result}");
```
