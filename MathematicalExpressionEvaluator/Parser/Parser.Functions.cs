/******************************************************************************
Copyright(c) 2022, Julijan Šribar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
******************************************************************************/

using JSribar.MathematicalExpressionEvaluator.Expressions;
using System;
using System.Collections.Generic;

namespace JSribar.MathematicalExpressionEvaluator
{
    public partial class Parser
    {
        /// <summary>
        ///   Adds a new custom function that accepts single argument.
        /// </summary>
        /// <param name="name">
        ///   Identifier of the function.
        /// </param>
        /// <param name="function">
        ///   Function to be invoked.
        /// </param>
        public void AddFunction(string name, MathFunction.Function function)
        {
            CheckIdentifier(name);
            functionTokenMap.Add(name, (Operator)nextOperator);
            functionMap.Add((Operator)nextOperator, function);
            ++nextOperator;
        }

        /// <summary>
        ///   Adds a new custom function that accepts two arguments.
        /// </summary>
        /// <param name="name">
        ///   Identifier of the function.
        /// </param>
        /// <param name="function">
        ///   Function to be invoked.
        /// </param>
        public void AddFunction2(string name, MathFunction2.Function function)
        {
            functionTokenMap.Add(name, (Operator)nextOperator);
            function2Map.Add((Operator)nextOperator, function);
            ++nextOperator;
        }

        /// <summary>
        ///   Operators supported.
        /// </summary>
        private enum Operator
        {
            Comma,
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Power,
            LeftParenthesis,
            LeftFunctionParenthesis,
            Minus,
            // functions with single argument
            Abs,
            Acos,
            Asin,
            Atan,
            Cos,
            Cosh,
            Exp,
            Log,
            Log10,
            Sin,
            Sinh,
            Sqrt,
            Tan,
            Tanh,
#if NET5_0_OR_GREATER
            Acosh,
            Asinh,
            Atanh,
            Cbrt,
            Log2,
#endif
            // functions with two arguments
            Atan2,
            Pow,
        }

        /// <summary>
        ///   Next available value for custom functions.
        /// </summary>
        private int nextOperator = Enum.GetValues(typeof(Operator)).Length;

        /// <summary>
        ///   Mapping of string token to <c>Operator</c> enumeration.
        /// </summary>
        private readonly Dictionary<string, Operator> functionTokenMap = new Dictionary<string, Operator>()
        {
            { "abs", Operator.Abs },
            { "acos", Operator.Acos },
            { "asin", Operator.Asin },
            { "atan", Operator.Atan },
            { "cos", Operator.Cos  },
            { "cosh", Operator.Cosh  },
            { "exp", Operator.Exp },
            { "ln", Operator.Log  },
            { "log", Operator.Log10 },
            { "log10", Operator.Log10 },
            { "sin", Operator.Sin },
            { "sinh", Operator.Sinh },
            { "sqrt", Operator.Sqrt },
            { "tan", Operator.Tan },
            { "tg", Operator.Tan },
            { "tanh", Operator.Tanh },
#if NET5_0_OR_GREATER
            { "acosh", Operator.Acosh },
            { "asinh", Operator.Asinh },
            { "atanh", Operator.Atanh },
            { "cbrt", Operator.Cbrt },
            { "log2", Operator.Log2 },
#endif       
            // functions with two arguments
            { "atan2", Operator.Atan2 },
            { "pow", Operator.Pow },
        };

        /// <summary>
        ///   Mapping of <c>Operator</c> enumeration to function delegates for functions with single argument.
        /// </summary>
        private readonly Dictionary<Operator, MathFunction.Function> functionMap = new Dictionary<Operator, MathFunction.Function>()
        {
            { Operator.Abs, Math.Abs },
            { Operator.Acos, Math.Acos },
            { Operator.Asin, Math.Asin },
            { Operator.Atan, Math.Atan },
            { Operator.Cos, Math.Cos },
            { Operator.Cosh, Math.Cosh },
            { Operator.Exp, Math.Exp},
            { Operator.Log, Math.Log },
            { Operator.Log10, Math.Log10 },
            { Operator.Sin, Math.Sin },
            { Operator.Sinh, Math.Sinh },
            { Operator.Sqrt, Math.Sqrt },
            { Operator.Tan, Math.Tan},
            { Operator.Tanh, Math.Tanh},
#if NET5_0_OR_GREATER
            { Operator.Acosh, Math.Acosh },
            { Operator.Asinh, Math.Asinh },
            { Operator.Atanh, Math.Atanh },
            { Operator.Cbrt, Math.Cbrt },
            { Operator.Log2, Math.Log2 },
#endif
        };

        /// <summary>
        ///   Mapping of <c>Operator</c> enumeration to function delegates for functions with two arguments.
        /// </summary>
        private readonly Dictionary<Operator, MathFunction2.Function> function2Map = new Dictionary<Operator, MathFunction2.Function>()
        {
            { Operator.Atan2, Math.Atan2 },
            { Operator.Pow, Math.Pow },
        };
    }
}
