using System;

namespace JSribar.MathematicalExpressionEvaluator
{
    public class VariableValueNotDefinedException : Exception
    {
        public VariableValueNotDefinedException(string message, string variableName, Exception innerException) : base(message, innerException)
        {
            VariableName = variableName;
        }

        public readonly string VariableName;
    }
}
