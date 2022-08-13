namespace JSribar.MathematicalExpressionEvaluator.Expressions
{
    /// <summary>
    ///   Class representing a constant.
    /// </summary>
    public class Constant : Expression
    {
        /// <summary>
        ///   Creates <c>Constant</c> object with the value provided.
        /// </summary>
        /// <param name="value">
        ///   Value of the constant object.
        /// </param>
        public Constant(double value)
        {
            this.value = value;
        }

        /// <summary>
        ///   Returns the value of the constant.
        /// </summary>
        /// <param name="context">
        ///   <c>Context</c> object with current values of variables.
        /// </param>
        /// <returns>
        ///   Value of the current <c>Constant</c> object.
        /// </returns>
        protected override double DoInterpret(Context context)
        {
            return value;
        }

        /// <summary>
        ///   Value of the <c>Constant</c> object.
        /// </summary>
        private readonly double value;
    }
}
