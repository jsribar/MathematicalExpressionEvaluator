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
    ///   Base abstract class for all expressions.
    /// </summary>
    public abstract class Expression : IExpression
    {
        /// <summary>
        ///   Evaluates value of the expression for the context provided.
        /// </summary>
        /// <param name="context">
        ///   <c>Context</c> object with current values of variables.
        /// </param>
        /// <returns>
        ///   Evaluated value.
        /// </returns>
        double IExpression.Evaluate(Context context)
        {
            if (isPositive)
                return Evaluate(context);
            return -Evaluate(context);
        }

        /// <summary>
        ///   Evaluates value of the expression for the single variable value 
        ///   provided.
        /// </summary>
        /// <param name="value">
        ///   Value of the variable.
        /// </param>
        /// <returns>
        ///   Evaluated value.
        /// </returns>
        public double Evaluate(double value)
        {
            return ((IExpression)this).Evaluate(new Context(value));
        }

        /// <summary>
        ///   Evaluates value of the expression for multiple variable values 
        ///   provided.
        /// </summary>
        /// <param name="values">
        ///   Array of <c>string</c> - <c>double</c> tuples representing 
        ///   variable identifiers and corresponding values.
        /// </param>
        /// <returns>
        ///   Evaluated value.
        /// </returns>
        public double Evaluate(params (string, double)[] values)
        {
            return ((IExpression)this).Evaluate(new Context(values));
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
        protected abstract double Evaluate(Context context);

        /// <summary>
        ///   Current sign of the expression.
        /// </summary>
        private bool isPositive = true;
    }
}
