namespace JSribar.AlgebraicExpressionParser.Expressions
{
    /// <summary>
    ///   Expression corresponding to arbitrary named variable.
    /// </summary>
    public class Variable : Expression
    {
        /// <summary>
        ///   Creates a single variable 'x'.
        /// </summary>
        /// <param name="name">
        ///   Name of the variable.
        /// </param>
        public Variable() : this("x")
        {
        }

        /// <summary>
        ///   Creates a variable with an identifier provided.
        /// </summary>
        /// <param name="name">
        ///   Name of the variable.
        /// </param>
        public Variable(string name)
        {
            this.name = name;
        }

        /// <summary>
        ///   Evaluates value of the current <c>Variable</c> object 
        ///   for the <c>Context</c> provided.
        /// </summary>
        /// <param name="context">
        ///   <c>Context</c> object with current values of variables.
        /// </param>
        /// <returns>
        ///   Value of the current <c>Variable</c> object.
        /// </returns>
        protected override double DoInterpret(Context context)
        {
            return context.GetValue(name);
        }

        /// <summary>
        ///   Variable identifier.
        /// </summary>
        private readonly string name;
    }
}
