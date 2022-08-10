namespace JSribar.MathematicalExpressionEvaluation.Expressions
{
    /// <summary>
    ///   Class representing a call of mathematical function.
    /// </summary>
    public class MathFunction : Expression
    {
        /// <summary>
        ///   Declaration of function delegate.
        /// </summary>
        /// <param name="argument">
        ///   Argument passed to function.
        /// </param>
        /// <returns>
        ///   Evaluated value.
        /// </returns>
        public delegate double Function(double argument);

        /// <summary>
        ///   Creates <c>MathFunctio</c> object representing 
        ///   a call of a function.
        /// </summary>
        /// <param name="argument">
        ///   <c>IExpression</c> passed as an argument to function.
        /// </param>
        public MathFunction(Function function, IExpression argument)
        {
            this.function += function;
            this.argument = argument;
        }

        /// <summary>
        ///   Evaluates the value of function assigned for the 
        ///   <c>Context</c> provided.
        /// </summary>
        /// <param name="context">
        ///   <c>Context</c> object with current values of variables.
        /// </param>
        /// <returns>
        ///   Evaluated function value.
        /// </returns>
        protected override double DoInterpret(Context context)
        {
            return function(argument.Interpret(context));
        }

        /// <summary>
        ///   Function assigned to the object.
        /// </summary>
        private readonly Function function;

        /// <summary>
        ///   Argument passed as an argument to the function.
        /// </summary>
        private readonly IExpression argument;
    }
}

