# MathematicalExpressionEvaluator
C# library for parsing and evaluation of mathematical expressions with one or more variables. Written to support .NET 4.5 (and higher) and .NET 6.0. 

Project was initially created as a demonstration of Interpreter design pattern but was later extended with parser, employing [Shunting yard algorithm](https://en.wikipedia.org/wiki/Shunting_yard_algorithm).

## Features
Basic features of the library:
* Supports basic arithmentic operations, using both ASCII characters and mathematical signs:
    * addition (<code>+</code>), 
    * subtraction (<code>-</code>, U+2212 <code>−</code>), 
    * multiplication (<code>*</code>, U+00D7 <code>×</code>, U+22C5 <code>⋅</code>), 
    * division (<code>/</code>, U+00F7 <code>÷</code>), 
    * exponentiation (<code>^</code>). 
* Allows extra sign (<code>-</code>, U+2212 <code>−</code> or <code>+</code>) preceding a value, function or variable.
* Takes care of operator precedence. 
* Supports parentheses to override operator precedence. 
* Includes support for standard mathematical functions with one (e.g. _sin_, _sqrt_) or two arguments (e.g. _atan2_, _pow_).
* Includes basic mathematical constants (_e_, _π_).
* Additional custom functions and constants can be defined in the runtime.
* Parser reports error for an invalid expression, with exact position where error occured.

## Basic Usage
Below are some introductory examples of library usage.

### Simple expression with a single variable
How to evaluate expression 

_x_ + 3

for a value of _x_=2:
1. Create <code>Parser</code> object.
2. Invoke <code>Parse</code> method and pass the string with mathematical expression. <code>Parse</code> method returns <code>IExpression</code> object.
3. Create a <code>Context</code> object and pass the value of variable _x_ for which mathematical expression should be evaluated.
3. Invoke <code>Evaluate</code> method of <code>IExpression</code> interface and pass the context object. <code>Evaluate</code> method returns the value of mathematical expression for the context (i.e. value of variable _x_) provided.
```csharp
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
**Note**: Expression object returned by <code>Parse</code> method can be reused to evaluate expresion for different value of variable. This can be useful e.g. when you need to draw the expression for a range of values.  

### Expression with operators of different precedence
Evaluate expression

12 − 8 × 2<sup>_x_</sup> ÷ 4

for a value of _x_=2 (result should be 4):
```csharp
using JSribar.MathematicalExpressionEvaluator;
// ...
var parser = new Parser();
var mathExpression = "12 - 8 * 2 ^ x / 4";
var expression = parser.Parse(mathExpression);
var x = 2;
var context = new Context(x); 
var result = expression.Evaluate(context);
Console.WriteLine($"Value of {mathExpression} for x={x} is {result}");
```

### Expression with parentheses to override operator precedence
Evaluate expression

12 − (8 × 2)<sup>(_x_ ÷ 4)</sup>

for _x_=2 (result should be 8):
```csharp
using JSribar.MathematicalExpressionEvaluator;
// ...
var parser = new Parser();
var mathExpression = "12 - (8 * 2) ^ (x / 4)";
var expression = parser.Parse(mathExpression);
var x = 2;
var context = new Context(x); 
var result = expression.Evaluate(context);
Console.WriteLine($"Value of {mathExpression} for x={x} is {result}");
```

### Expression with mathematical function
Evaluate expression

_x_ + _tan_(_π_/_x_)

for _x_=4 (result should be 5):
```csharp
// ...
var mathExpression = "x + tan(PI / x)";
// ...
var context = new Context(4); 
// ...
```
__Note 1__: Left parenthesis must follow function name immediately, otherwise <code>ParserException</code> is thrown.

__Note 2__: Mathematical constant _π_ can be written also as a Greek character π in the expression string, i.e. <code>"x + tan(π / x)"</code>.

### Expression with preceding sign
Evaluate expression

−*x* + −*tan*(_π_/_x_)
for _x_=4 (result should be −5):
```csharp
// ...
var mathExpression = "-x +-tan(PI / x)";
// ...
var context = new Context(4); 
// ...
```
__Note__: Preceding sign must be followed immediately by constant, variable or function name (no whitespaces are allowed), otherwise <code>ParserException</code> is thrown.
