using System;

namespace JSribar.MathematicalExpressionEvaluator.Expressions
{
    public class EvaluationException : Exception
    {
        public EvaluationException(string message, string variableName, Exception innerException) : base(message, innerException)
        {
            VariableName = variableName;
        }

        public readonly string VariableName;
    }
}
