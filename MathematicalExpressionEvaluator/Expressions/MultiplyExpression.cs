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
    ///   Class representing a product of two expressions.
    /// </summary>
    public class MultiplyExpression : Expression
    {
        /// <summary>
        ///   Creates <c>MultiplyExpression</c> object representing 
        ///   a product of two <c>IExpression</c> objects.
        /// </summary>
        /// <param name="lhs">
        ///   Left-hand side expression.
        /// </param>
        /// <param name="rhs">
        ///   Right-hand side expression.
        /// </param>
        public MultiplyExpression(IExpression lhs, IExpression rhs)
        {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        /// <summary>
        ///   Evaluates the product of left-hand and right-hand side 
        ///   expressions for the <c>Context</c> provided.
        /// </summary>
        /// <param name="context">
        ///   <c>Context</c> object with current values of variables.
        /// </param>
        /// <returns>
        ///   Evaluated product.
        /// </returns>
        protected override double DoEvaluate(Context context)
        {
            return lhs.Evaluate(context) * rhs.Evaluate(context);
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
