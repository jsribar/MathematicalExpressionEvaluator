/******************************************************************************
Copyright(c) 2022, Julijan Šribar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
******************************************************************************/

using System.Collections.Generic;

namespace JSribar.MathematicalExpressionEvaluator.Expressions
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
            try
            {
            return values[variableName];

            }
            catch (KeyNotFoundException)
            {
                throw new IdentifierException(Messages.ValueOfVariableNotProvided, variableName);
            }
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
