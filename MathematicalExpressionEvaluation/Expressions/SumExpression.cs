namespace JSribar.MathematicalExpressionEvaluation.Expressions
{
    /// <summary>
    ///   Class representing sum of two expressions.
    /// </summary>
    public class SumExpression : Expression
    {
        /// <summary>
        ///   Creates <c>SumExpression</c> representing a sum of two 
        ///   <c>IExpression</c> objects.
        /// </summary>
        /// <param name="lhs">
        ///   Left-hand side expression.
        /// </param>
        /// <param name="rhs">
        ///   Right-hand side expression.
        /// </param>
        public SumExpression(IExpression lhs, IExpression rhs)
        {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        /// <summary>
        ///   Evaluates the sum of left-hand and right-hand side 
        ///   expressions for the <c>Context</c> provided.
        /// </summary>
        /// <param name="context">
        ///   <c>Context</c> object with current values of variables.
        /// </param>
        /// <returns>
        ///   Evaluated sum.
        /// </returns>
        protected override double DoInterpret(Context context)
        {
            return lhs.Interpret(context) + rhs.Interpret(context);
        }

        /// <summary>
        ///   <c>IExpression</c> on the left-hand side. 
        /// </summary>
        private readonly IExpression lhs;

        /// <summary>
        ///   <c>IExpression</c> on the right-hand side. 
        /// </summary>
        private readonly IExpression rhs;
    }
}
