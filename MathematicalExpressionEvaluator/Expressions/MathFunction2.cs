namespace JSribar.MathematicalExpressionEvaluator.Expressions
{
    /// <summary>
    ///   Class representing a call of mathematical function.
    /// </summary>
    public class MathFunction2 : Expression
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
        public delegate double Function(double argument1, double argument2);

        /// <summary>
        ///   Creates <c>MathFunctio</c> object representing 
        ///   a call of a function.
        /// </summary>
        /// <param name="argument">
        ///   <c>IExpression</c> passed as an argument to function.
        /// </param>
        public MathFunction2(Function function, IExpression argument1, IExpression argument2)
        {
            this.function += function;
            this.argument1 = argument1;
            this.argument2 = argument2;
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
            return function(argument1.Interpret(context), argument2.Interpret(context));
        }

        /// <summary>
        ///   Function assigned to the object.
        /// </summary>
        private readonly Function function;

        /// <summary>
        ///   Argument passed as an argument to the function.
        /// </summary>
        private readonly IExpression argument1;
        private readonly IExpression argument2;
    }
}
