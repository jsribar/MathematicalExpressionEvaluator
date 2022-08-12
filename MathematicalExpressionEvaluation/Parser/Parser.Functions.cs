using JSribar.MathematicalExpressionEvaluation.Expressions;
using System;
using System.Collections.Generic;

namespace JSribar.MathematicalExpressionEvaluation
{
    public partial class Parser
    {
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
#if NET6_0
            Acosh,
            Asinh,
            Atanh,
            Log2,
#endif
            // functions with two arguments
            Atan2,
            Pow,
        }

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
#if NET6_0
            { "acosh", Operator.Acosh },
            { "asinh", Operator.Asinh },
            { "atanh", Operator.Atanh },
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
#if NET6_0
            { Operator.Acosh, Math.Acosh },
            { Operator.Asinh, Math.Asinh },
            { Operator.Atanh, Math.Atanh },
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
