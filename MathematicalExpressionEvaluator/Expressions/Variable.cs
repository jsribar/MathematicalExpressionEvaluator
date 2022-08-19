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

namespace JSribar.MathematicalExpressionEvaluator.Expressions
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
        protected override double DoEvaluate(Context context)
        {
            return context.GetValue(name);
        }

        /// <summary>
        ///   Variable identifier.
        /// </summary>
        private readonly string name;
    }
}
