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
    ///   Class representing a call of mathematical function.
    /// </summary>
    public class MathFunction : Expression
    {
        /// <summary>
        ///   Declaration of function delegate.
        /// </summary>
        /// <param name="argument">
        ///   Argument passed to function.
        /// </param>
        /// <returns>
        ///   Evaluated value.
        /// </returns>
        public delegate double Function(double argument);

        /// <summary>
        ///   Creates <c>MathFunction</c> object representing a call of a 
        ///   function.
        /// </summary>
        /// <param name="argument">
        ///   <c>IExpression</c> passed as an argument to function.
        /// </param>
        public MathFunction(Function function, IExpression argument)
        {
            this.function += function;
            this.argument = argument;
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
            return function(argument.Evaluate(context));
        }

        /// <summary>
        ///   Function assigned to the object.
        /// </summary>
        private readonly Function function;

        /// <summary>
        ///   <c>IExpression</c> passed as an argument to the function.
        /// </summary>
        private readonly IExpression argument;
    }
}

