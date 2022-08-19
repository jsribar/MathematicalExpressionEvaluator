# Mathematical Expression Evaluator
C# library for parsing and evaluation of mathematical expressions with one or more variables. Written to support .NET 4.5 (and higher), .NET 5.0 and .NET 6.0. 

Project was initially created as a demonstration of [Interpreter design pattern](https://en.wikipedia.org/wiki/Interpreter_pattern) but was later extended with parser, employing [Shunting yard algorithm](https://en.wikipedia.org/wiki/Shunting_yard_algorithm).

## Features
Basic features of the library:
* Supports basic arithmentic operations, using both ASCII characters and Unicode mathematical signs:
    * addition (<code>+</code>), 
    * subtraction (<code>-</code>, U+2212 <code>−</code>), 
    * multiplication (<code>*</code>, U+00D7 <code>×</code>, U+22C5 <code>⋅</code>), 
    * division (<code>/</code>, <code>:</code>, U+00F7 <code>÷</code>), 
    * exponentiation (<code>^</code>). 
* Allows extra sign (<code>-</code>, U+2212 <code>−</code> or <code>+</code>) preceding a value, function or variable.
* Takes care of operator precedence. 
* Supports parentheses to override operator precedence. 
* Supports standard mathematical functions with one (e.g. _sin_, _sqrt_) or two arguments (e.g. _atan2_, _pow_) that can be invoked using corresponding identifiers.
* Supports identifiers for basic mathematical constants (_e_, _π_).
* Numeric values in expression can be in floating point (e.g. 3.14159) or in scientific format (e.g. 314159e-5). __Note__: only decimal point is allowed as a decimal separator since comma is used as function argument separator.
* Additional custom functions and constants can be defined in the runtime.
* Parser reports error for invalid expression, with exact position where error occured.

## Basic Usage
Below are some introductory examples of library usage.

### Simple expression with a single variable
How to evaluate expression 

_x_ + 3

for a value of _x_=2:
1. Create <code>Parser</code> object.
2. Invoke <code>Parse</code> method and pass the string with mathematical expression. On success, <code>Parse</code> method returns final <code>IExpression</code> object evaluated as a composition of expressions from operations parsed.
3. Invoke <code>Evaluate</code> method of the object returned by <code>Parse</code> method in step 2. Value of the variable must be passed as argument to the  method and method returns the value of mathematical expression for the value of variable _x_ provided.

```csharp
using JSribar.MathematicalExpressionEvaluator;
...
var parser = new Parser();
var mathExpression = "x + 3";
var expression = parser.Parse(mathExpression);
var x = 2;
var result = expression.Evaluate(x);
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
    Console.WriteLine($"{x}\t{expression.Evaluate(x)}");
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
var result = expression.Evaluate(x);
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
var result = expression.Evaluate(x);
Console.WriteLine($"Value of {mathExpression} for x={x} is {result}");
```

### Expression with mathematical function
Evaluate expression

_x_ + _tan_(_π_/_x_)

for _x_=4 (result should be 5):

```csharp
// ...
var mathExpression = "x + tan(PI / x)";
var result = mathExpression.Evaluate(4);
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
var result = mathExpression.Evaluate(4); 
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
var expression = parser.Parse("hypotenuse(x, 2 * two)");
var result = expression.Evaluate(3); 
// ...
```

__Note 1__: Functions and constants must be added before invoking <code>Parse</code> method with expression that uses them in order to parse the expression correctly.

__Note 2__: If name of function or constant is already used for existing function or constant, parser throws <code>IdentifierException</code>.

### Using different identifier for a variable
Parser assumes that variable is named _x_ by default. If you need to use different identifier for a variable, simply provide the identifier to the <code>Parser</code> constructor:

```csharp
// Use 'time' instead of default 'x' identifier for variable:
var parser = new Parser("time");
var expression = parser.Parse("sin(time / (2 * PI))");
// ...
```

__Note__: If name of identifier is already used for existing function or constant, parser throws <code>IdentifierException</code>.

### Using multiple variables
To evaluate expression with multiple variables, variable identifiers must be passed to the <code>Parser</code> as a collection of strings. Actual values of variables for a given context must be passed to the <code>Context</code> constructor as a collection of <code>string</code> - <code>double></code> tuples, with variable identifiers and corresponding values.

```csharp
// Use 'x' and 'y' identifiers:
var parser = new Parser("x", "y");
// Expression with 2 variables:
var expression = parser.Parse("sin(x + y)");
// Provide values: x=2, y=3:
var result = expression.Evaluate(("x", 2), ("y", 3));
// ...
```

## Built-in Functions
Following functions are built-in and directly available:
| Identifier                           | Function invoked                                                                         | Remark        |
| ------------------------------------ | ---------------------------------------------------------------------------------------- | ------------- |
| <code>abs</code>                     | [<code>Math.Abs</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.abs)     |               |
| <code>acos</code>                    | [<code>Math.Acos</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.acos)   |               |
| <code>acosh</code>                   | [<code>Math.Acosh</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.acosh) | .NET 6.0 only |
| <code>asin</code>                    | [<code>Math.Asin</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.asin)   |               |
| <code>asinh</code>                   | [<code>Math.Asinh</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.asinh) | .NET 6.0 only |
| <code>atan</code>                    | [<code>Math.Atan</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.atan)   |               |
| <code>atan2</code>                   | [<code>Math.Atan2</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.atan2) | two arguments |
| <code>atanh</code>                   | [<code>Math.Atanh</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.atanh) | .NET 6.0 only |
| <code>cos</code>                     | [<code>Math.Cos</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.cos)     |               |
| <code>cosh</code>                    | [<code>Math.Cosh</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.cosh)   |               |
| <code>exp</code>                     | [<code>Math.Exp</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.exp)     |               |
| <code>ln</code>                      | [<code>Math.Log</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.log)     |               |
| <code>log</code>, <code>log10</code> | [<code>Math.Log10</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.log10) |               |
| <code>log2</code>                    | [<code>Math.Log2</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.log2)   | .NET 6.0 only |
| <code>pow</code>                     | [<code>Math.Atan2</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.pow)   | two arguments |
| <code>sin</code>                     | [<code>Math.Sin</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.sin)     |               |
| <code>sinh</code>                    | [<code>Math.Sinh</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.sinh)   |               |
| <code>sqrt</code>                    | [<code>Math.Sqrt</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.sqrt)   |               |
| <code>tan</code> , <code>tg</code>   | [<code>Math.Tan</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.tan)     |               |
| <code>tanh</code>                    | [<code>Math.Tanh</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.tanh)   |               |

## Built-in Mathematical Constants
Following mathematical constants are built-in and directly available:
| Identifier                           | Field invoked                                                                       |
| ------------------------------------ | ----------------------------------------------------------------------------------- |
| <code>E</code>                       | [<code>Math.E</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.e)    |
| <code>PI</code>, <code>π</code>      | [<code>Math.PI</code>](https://docs.microsoft.com/en-us/dotnet/api/system.math.pi)  |

## Error Reporting
Errors are reported through <code>ParserException</code> and <code>IdentifierException</code>.

### <code>ParserException</code>
This exception will be thrown by <code>Parse</code> method when error is encountered in expression string being parsed. <code>ParserException</code> contains two properties/fields:
* <code>Message</code> - string with error message,
* <code>Position</code> - integer with the position in input expression string where error occured.

### <code>IdentifierException</code>
This exception will be thrown:
1. When custom identifier passed to the <code>Parser</code> has invalid format (it must not be empty, must start with letter followed by a sequence of letters and digits only).
2. When user defined function or constant identifier passed to <code>AddConstant</code>, <code>AddFunction</code> or <code>AddFunction2</code> method has invalid format or is already used for some function or constant.
3. When not all identifier values are supplied to the <code>Context</code> constructor.

Exception contains two properties/fields:
* <code>Message</code> - string with error message,
* <code>Identifier</code> - string with identifier that caused the error.

