namespace JSribar.MathematicalExpressionEvaluation.Expressions
{
    /// <summary>
    ///   Interfece common to all expression objects.
    /// </summary>
    public interface IExpression
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
        double Interpret(Context context);

        /// <summary>
        ///   Changes the sign of the expression.
        /// </summary>
        void ToggleSign();
    }
}
