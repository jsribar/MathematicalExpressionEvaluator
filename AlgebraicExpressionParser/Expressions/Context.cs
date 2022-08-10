namespace JSribar.MathematicalExpressionEvaluation.Expressions
{
    /// <summary>
    ///   Class representing evaluation context;
    /// </summary>
    public class Context
    {
        /// <summary>
        ///   Dictionary with variable names as keys and corresponding values.
        /// </summary>
        private readonly Dictionary<string, double> values = new Dictionary<string, double>();

        /// <summary>
        ///   Constructor for multiple variables.
        /// </summary>
        /// <param name="values">
        ///   Dictionary with variables and their current values.
        /// </param>
        public Context(Dictionary<string, double> values)
        {
            this.values = values;
        }

        /// <summary>
        ///   Constructor for a single variable x.
        /// </summary>
        /// <param name="x">
        ///   Current value of the single variable.
        /// </param>
        public Context(double x)
        {
            values["x"] = x;
        }

        /// <summary>
        ///   Get value for a variable with name provided.
        /// </summary>
        /// <param name="variableName">
        ///   Name of the varible for which value is requested.
        /// </param>
        /// <returns>
        ///   Value of the variable provided.
        /// </returns>
        public double GetValue(string variableName)
        {
            return values[variableName];
        }

        /// <summary>
        ///   Get value for a single variable x.
        /// </summary>
        /// <returns>
        ///   Value of the single varibale 'x'.
        /// </returns>
        public double GetValue()
        {
            return values["x"];
        }
    }
}
