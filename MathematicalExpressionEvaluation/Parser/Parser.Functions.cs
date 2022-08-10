using JSribar.MathematicalExpressionEvaluation.Expressions;

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
            Sin,
            Cos,
            Tan,
            Sqrt,
            Exp,
            Log,
            Log10,
            Asin,
            Acos,
            Atan,
            Abs,
            // functions with two arguments
            Pow,
            Atan2,
        }

        /// <summary>
        ///   Mapping of string token to <c>Operator</c> enumeration.
        /// </summary>
        private readonly Dictionary<string, Operator> functionTokenMap = new()
        {
            { "sin", Operator.Sin },
            { "cos", Operator.Cos  },
            { "tan", Operator.Tan },
            { "sqrt", Operator.Sqrt },
            { "exp", Operator.Exp },
            { "ln", Operator.Log  },
            { "log", Operator.Log10 },
            { "asin", Operator.Asin },
            { "acos", Operator.Acos },
            { "atan", Operator.Atan },
            { "abs", Operator.Abs },
            { "pow", Operator.Pow },
            { "atan2", Operator.Atan2 },
        };

        /// <summary>
        ///   Mapping of <c>Operator</c> enumeration to function delegates for functions with single argument.
        /// </summary>
        private readonly Dictionary<Operator, MathFunction.Function> functionMap = new()
        {
            { Operator.Sin, Math.Sin },
            { Operator.Cos, Math.Cos },
            { Operator.Tan, Math.Tan},
            { Operator.Sqrt, Math.Sqrt },
            { Operator.Exp, Math.Exp},
            { Operator.Log, Math.Log },
            { Operator.Log10, Math.Log10 },
            { Operator.Asin, Math.Asin },
            { Operator.Acos, Math.Acos },
            { Operator.Atan, Math.Atan },
            { Operator.Abs, Math.Abs },
        };

        /// <summary>
        ///   Mapping of <c>Operator</c> enumeration to function delegates for functions with two arguments.
        /// </summary>
        private readonly Dictionary<Operator, MathFunction2.Function> function2Map = new()
        {
            { Operator.Pow, Math.Pow },
            { Operator.Atan2, Math.Atan2 },
        };
    }
}
