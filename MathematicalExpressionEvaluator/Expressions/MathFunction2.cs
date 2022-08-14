namespace JSribar.MathematicalExpressionEvaluator.Expressions
{
    /// <summary>
    ///   Class representing a call of mathematical function that accepts two 
    ///   arguments.
    /// </summary>
    public class MathFunction2 : Expression
    {
        /// <summary>
        ///   Declaration of function delegate.
        /// </summary>
        /// <param name="argument1">
        ///   First argument to function.
        /// </param>
        /// <param name="argument2">
        ///   Second argument to function.
        /// </param>
        /// <returns>
        ///   Evaluated value.
        /// </returns>
        public delegate double Function(double argument1, double argument2);

        /// <summary>
        ///   Creates <c>MathFunction2</c> object representing a call of a 
        ///   function.
        /// </summary>
        /// <param name="argument1">
        ///   <c>IExpression</c> passed as the first argument to function.
        /// </param>
        /// <param name="argument2">
        ///   <c>IExpression</c> passed as the second argument to function.
        /// </param>
        public MathFunction2(Function function, IExpression argument1, IExpression argument2)
        {
            this.function += function;
            this.argument1 = argument1;
            this.argument2 = argument2;
        }

        /// <summary>
        ///   Evaluates the value of function assigned for the <c>Context</c> 
        ///   provided.
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
        ///   <c>IExpression</c> passed as the first argument to the function.
        /// </summary>
        private readonly IExpression argument1;

        /// <summary>
        ///   <c>IExpression</c> passed as the second argument to the function.
        /// </summary>
        private readonly IExpression argument2;
    }
}
