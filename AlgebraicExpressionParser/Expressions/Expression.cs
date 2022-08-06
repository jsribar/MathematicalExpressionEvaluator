namespace JSribar.AlgebraicExpressionParser.Expressions
{
    /// <summary>
    ///   Base abstract class for all expressions.
    /// </summary>
    public abstract class Expression : IExpression
    {
        /// <summary>
        ///   Evaluates value of the expression fro the context provided.
        /// </summary>
        /// <param name="context">
        ///   <c>Context</c> object with current values of variables.
        /// </param>
        /// <returns>
        ///   Evaluated value.
        /// </returns>
        public double Interpret(Context context)
        {
            if (isPositive)
                return DoInterpret(context);
            return -DoInterpret(context);
        }
        
        /// <summary>
        ///   Toggles expression sign.
        /// </summary>
        public void ToggleSign()
        {
            isPositive = !isPositive;
        }

        /// <summary>
        ///   Abtract method to be implemented in derived expression objects.
        /// </summary>
        /// <param name="context">
        ///   <c>Context</c> object with current values of variables.
        /// </param>
        /// <returns>
        ///   Evaluated value.
        /// </returns>
        protected abstract double DoInterpret(Context context);

        /// <summary>
        ///   Current sign of the expression.
        /// </summary>
        private bool isPositive = true;
    }
}
