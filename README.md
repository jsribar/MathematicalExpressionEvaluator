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
