# Mathematical Expression Evaluator
C# library for parsing and evaluation of mathematical expressions with one or more variables. Written to support .NET 4.5 (and higher), .NET 5.0 and .NET 6.0. 

Project was initially created as a demonstration of [Interpreter design pattern](https://en.wikipedia.org/wiki/Interpreter_pattern) but was later extended with parser, employing [Shunting yard algorithm](https://en.wikipedia.org/wiki/Shunting_yard_algorithm).

## Features
Basic features of the library:
* Supports basic arithmentic operations, using both ASCII characters and Unicode mathematical signs:
    * addition (`+`), 
    * subtraction (`-`, U+2212 `−`), 
    * multiplication (`*`, U+00D7 `×`, U+22C5 `⋅`), 
    * division (`/`, `:`, U+00F7 `÷`), 
    * exponentiation (`^`). 
* Allows extra sign (`-`, U+2212 `−` or `+`) preceding a value, function or variable.
* Takes care of operator precedence. 
* Supports parentheses to override operator precedence. 
* Supports standard mathematical functions with one (e.g. _sin_, _sqrt_) or two arguments (e.g. _atan2_, _pow_) that can be invoked using corresponding identifiers.
* Supports identifiers for basic mathematical constants (_e_, _π_).
* Numeric values in expression can be in floating point (e.g. 3.14159) or in scientific format (e.g. 314159e-5). __Note__: only decimal point is allowed as a decimal separator because comma is used as function argument separator.
* Additional custom functions and constants can be defined in the runtime.
* Parser reports error for invalid expression, with exact position where error occured.

## Basic Usage
Below are some introductory examples of library usage.

### Simple expression with a single variable
How to evaluate expression 

_x_ + 3

for a value of _x_=2:
1. Create `Parser` object.
2. Invoke `Parse` method and pass the string with mathematical expression. On success, `Parse` method returns final `IExpression` object evaluated as a composition of expressions from operations parsed.
3. Invoke `Evaluate` method of the object returned by `Parse` method in step 2. Value of the variable must be passed as argument to the  method and method returns the value of mathematical expression for the value of variable _x_ provided.

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
**Note 2**: Expression object returned by `Parse` method can be reused to evaluate expression for different values of the variable. For example, to evaluate the expression for a range of _x_:

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
__Note 1__: Left parenthesis must follow function name immediately, otherwise `ParserException` is thrown.

__Note 2__: Mathematical constant _π_ can be written also as a Greek character π in the expression string, i.e. `"x + tan(π / x)"`.

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
__Note__: Preceding sign must be followed immediately by constant, variable or function name (no whitespaces are allowed), otherwise `ParserException` is thrown.

### Adding custom function and constant
Custom function can be added in runtime by `AddFunction` or `AddFunction2` methods, for functions with single or two arguments, respectively. Custom constants can be added by `AddConstant` method.

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

__Note 1__: Functions and constants must be added before invoking `Parse` method with expression that uses them in order to parse the expression correctly.

__Note 2__: If name of function or constant is already used for existing function or constant, parser throws `IdentifierException`.

### Using different identifier for a variable
Parser assumes that variable is named _x_ by default. If you need to use different identifier for a variable, simply provide the identifier to the `Parser` constructor:

```csharp
// Use 'time' instead of default 'x' identifier for variable:
var parser = new Parser("time");
var expression = parser.Parse("sin(time / (2 * PI))");
// ...
```

__Note__: If name of identifier is already used for existing function or constant, parser throws `IdentifierException`.

### Using multiple variables
To evaluate expression with multiple variables, variable identifiers must be passed to the `Parser` as a collection of strings. Actual values of variables for a given context must be passed to the `Evaluate` method as a collection of `string` - `double` tuples, with variable identifiers and corresponding values.

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
| Identifier                           | Function invoked                                             | Remark             |
| ------------------------------------ | ------------------------------------------------------------ | ------------------ |
| `abs`                     | [`Math.Abs`](https://docs.microsoft.com/en-us/dotnet/api/system.math.abs) |                    |
| `acos`                    | [`Math.Acos`](https://docs.microsoft.com/en-us/dotnet/api/system.math.acos) |                    |
| `acosh`                   | [`Math.Acosh`](https://docs.microsoft.com/en-us/dotnet/api/system.math.acosh) | .NET 5.0 or higher |
| `asin`                    | [`Math.Asin`](https://docs.microsoft.com/en-us/dotnet/api/system.math.asin) |                    |
| `asinh`                   | [`Math.Asinh`](https://docs.microsoft.com/en-us/dotnet/api/system.math.asinh) | .NET 5.0 or higher |
| `atan`                    | [`Math.Atan`](https://docs.microsoft.com/en-us/dotnet/api/system.math.atan) |                    |
| `atan2`                   | [`Math.Atan2`](https://docs.microsoft.com/en-us/dotnet/api/system.math.atan2) | two arguments      |
| `atanh`                   | [`Math.Atanh`](https://docs.microsoft.com/en-us/dotnet/api/system.math.atanh) | .NET 5.0 or higher |
| `cbrt`                    | [`Math.Cbrt`](https://docs.microsoft.com/en-us/dotnet/api/system.math.cbrt) | .NET 5.0 or higher |
| `cos`                     | [`Math.Cos`](https://docs.microsoft.com/en-us/dotnet/api/system.math.cos) |                    |
| `cosh`                    | [`Math.Cosh`](https://docs.microsoft.com/en-us/dotnet/api/system.math.cosh) |                    |
| `exp`                     | [`Math.Exp`](https://docs.microsoft.com/en-us/dotnet/api/system.math.exp) |                    |
| `ln`                      | [`Math.Log`](https://docs.microsoft.com/en-us/dotnet/api/system.math.log) |                    |
| `log`, `log10` | [`Math.Log10`](https://docs.microsoft.com/en-us/dotnet/api/system.math.log10) |                    |
| `log2`                    | [`Math.Log2`](https://docs.microsoft.com/en-us/dotnet/api/system.math.log2) | .NET 5.0 or higher |
| `pow`                     | [`Math.Atan2`](https://docs.microsoft.com/en-us/dotnet/api/system.math.pow) | two arguments      |
| `sin`                     | [`Math.Sin`](https://docs.microsoft.com/en-us/dotnet/api/system.math.sin) |                    |
| `sinh`                    | [`Math.Sinh`](https://docs.microsoft.com/en-us/dotnet/api/system.math.sinh) |                    |
| `sqrt`                    | [`Math.Sqrt`](https://docs.microsoft.com/en-us/dotnet/api/system.math.sqrt) |                    |
| `tan` , `tg`   | [`Math.Tan`](https://docs.microsoft.com/en-us/dotnet/api/system.math.tan) |                    |
| `tanh`                    | [`Math.Tanh`](https://docs.microsoft.com/en-us/dotnet/api/system.math.tanh) |                    |

## Built-in Mathematical Constants
Following mathematical constants are built-in and directly available:
| Identifier                      | Field invoked                                                |
| ------------------------------- | ------------------------------------------------------------ |
| `E`                  | [`Math.E`](https://docs.microsoft.com/en-us/dotnet/api/system.math.e) |
| `PI`, `π` | [`Math.PI`](https://docs.microsoft.com/en-us/dotnet/api/system.math.pi) |

## Error Reporting
Errors are reported through `ParserException` and `IdentifierException`.

### `ParserException`
This exception will be thrown by `Parse` method when error is encountered in expression string being parsed. `ParserException` contains two properties/fields:
* `Message` - string with error message,
* `Position` - integer with the position in input expression string where error occured.

### `IdentifierException`
This exception will be thrown:
1. When custom identifier passed to the `Parser` has invalid format (it must not be empty, must start with letter followed by a sequence of letters and digits only).
2. When user defined function or constant identifier passed to `AddConstant`, `AddFunction` or `AddFunction2` method has invalid format or is already used for some function or constant.
3. When not all identifier values are supplied to the `Evaluate` method.

Exception contains two properties/fields:
* `Message` - string with error message,
* `Identifier` - string with identifier that caused the error.

## How to Add New Function or Constant into Code
To add a new function, open `Parser.Functions.cs` file:
1. Append new `Operator` enumeration value for the function.
2. Append entry to `functionTokenMap` dictionary with new function identifer as a key and enumeration value from the step 1 as a value.
3. Append entry to `functionMap` dictionary with enumeration value from step 1 as a key and the function as a value. __Note__: if function accepts two arguments, it should be appended to `functionMap2` dictionary.

To add a new constant, open `Parser.Constants.cs` file and append new entry to `mathematicalConstantsMap` with new constant identifier as dictionary key and constant value as entry value.
