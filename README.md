# MathematicalExpressionEvaluator
C# library for parsing and evaluation of mathematical expressions with one or more variables. Written to support .NET 4.5 (and higher) and .NET 6.0. 

Project was initially created as a demonstration of [Interpreter design pattern](https://en.wikipedia.org/wiki/Interpreter_pattern) but was later extended with parser, employing [Shunting yard algorithm](https://en.wikipedia.org/wiki/Shunting_yard_algorithm).

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
2. Invoke <code>Parse</code> method and pass the string with mathematical expression. On success, <code>Parse</code> method returns final <code>IExpression</code> object evaluated as a composition of expressions from operations parsed.
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

**Note 1**: Spaces around operators and operands are optional. Mathematical expression in the example above could be also written as:
```csharp
//...
var mathExpression = " x+3 ";
```
**Note 2**: Expression object returned by <code>Parse</code> method can be reused to evaluate expression for different values of the variable. For example, to evaluate the expression for a range of _x_:

```csharp
var parser = new Parser();
var mathExpression = "x + 3";
var expression = parser.Parse(mathExpression);
Console.WriteLine($"Values of {mathExpression}{Environment.NewLine}x\tvalue");
for (int x = 0; x <= 10; ++x)
{
    Console.WriteLine($"{x}\t{expression.Evaluate(new Context(x))}");
}
```

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
var expression = parser.Parse("12 - (8 * 2) ^ (x / 4)");
var result = expression.Evaluate(new Context(2));
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

### Adding custom function and constant
Custom function can be added in runtime by <code>AddFunction</code> or <code>AddFunction2</code> methods, for functions with single or two arguments, respectively. Custom constants can be added by <code>AddConstant</code> method.

```csharp
// User defined function:
static double Hypotenuse(double a, double b)
{
    return Math.Sqrt(a * a + b * b);
}
// ...
var parser = new Parser();
// Add user defined mathematical function 'Hypotenuse':
parser.AddFuncion2("hypotenuse", Hypotenuse);
// Add mathematical constant 'two' with value of 2:
parser.AddConstant("two", 2);
parser.Parse("hypotenuse(x, 2 * two)");
var result = parser.Evaluate(new Context(3)); 
// ...
```

__Note 1__: Functions and constants must be added before invoking <code>Parse</code> method with expression that uses them in order to parse the expression correctly.

__Note 2__: If name of function or constant is already used for existing function or constant, parser throws <code>IdentifierException</code>.

### Using different identifier for a variable
Parser assumes that variable is named _x_ by default. If you need to use different identifier for a variable, simply provide the identifier to the <code>Parser</code> constructor:

```csharp
// Use 'time' instead of default 'x' identifier for variable:
var parser = new Parser("time");
parser.Parse("sin(time / (2 * PI))");
// ...
```

__Note__: If name of identifier is already used for existing function or constant, parser throws <code>IdentifierException</code>.

### Using multiple variables
To evaluate expression with multiple variables, variable identifiers must be passed to the <code>Parser</code> as a collection of strings. Actual values of variables for a given context must be passed to the <code>Context</code> constructor as a <code>Dictionary<string, double></code>, with variable identifiers and corresponding values.

```csharp
// Use 'x' and 'y' identifiers:
var parser = new Parser("x", "y");
// Expression with 2 variables:
parser.Parse("sin(x + y)");
// Provide values: x=2, y=3:
var result = parser.Evaluate(new Context(new Dictionary<string, double> { { "x", 2 }, { "y", 3 } }));
// ...
```
