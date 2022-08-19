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
    ///   Class representing a call of mathematical function that accepts two 
    ///   arguments.
    /// </summary>
    public class MathFunction2 : Expression
    {
        /// <summary>
        ///   Declaration of function delegate.
        /// </summary>
        /// <param name="argument1">
        ///   First argument to function.
        /// </param>
        /// <param name="argument2">
        ///   Second argument to function.
        /// </param>
        /// <returns>
        ///   Evaluated value.
        /// </returns>
        public delegate double Function(double argument1, double argument2);

        /// <summary>
        ///   Creates <c>MathFunction2</c> object representing a call of a 
        ///   function.
        /// </summary>
        /// <param name="argument1">
        ///   <c>IExpression</c> passed as the first argument to function.
        /// </param>
        /// <param name="argument2">
        ///   <c>IExpression</c> passed as the second argument to function.
        /// </param>
        public MathFunction2(Function function, IExpression argument1, IExpression argument2)
        {
            this.function += function;
            this.argument1 = argument1;
            this.argument2 = argument2;
        }

        /// <summary>
        ///   Evaluates the value of function assigned for the <c>Context</c> 
        ///   provided.
        /// </summary>
        /// <param name="context">
        ///   <c>Context</c> object with current values of variables.
        /// </param>
        /// <returns>
        ///   Evaluated function value.
        /// </returns>
        protected override double DoEvaluate(Context context)
        {
            return function(argument1.Evaluate(context), argument2.Evaluate(context));
        }

        /// <summary>
        ///   Function assigned to the object.
        /// </summary>
        private readonly Function function;

        /// <summary>
        ///   <c>IExpression</c> passed as the first argument to the function.
        /// </summary>
        private readonly IExpression argument1;

        /// <summary>
        ///   <c>IExpression</c> passed as the second argument to the function.
        /// </summary>
        private readonly IExpression argument2;
    }
}
