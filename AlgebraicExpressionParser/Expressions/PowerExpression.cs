using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSribar.AlgebraicExpressionParser.Expressions
{
    /// <summary>
    ///   Class representing a power of base onto an exponent.
    /// </summary>
    public class PowerExpression : Expression
    {
        /// <summary>
        ///   Creates <c>PowerExpression</c> object representing 
        ///   a power of base <c>IExpression</c> onto exponent <c>IExpression</c>.
        /// </summary>
        /// <param name="base">
        ///   Base of the power expression.
        /// </param>
        /// <param name="exponent">
        ///   Exponent of the power expression
        /// </param>
        public PowerExpression(IExpression @base, IExpression exponent)
        {
            this.@base = @base;
            this.exponent = exponent;
        }

        /// <summary>
        ///   Evaluates the power for the <c>Context</c> provided.
        /// </summary>
        /// <param name="context">
        ///   <c>Context</c> object with current values of variables.
        /// </param>
        /// <returns>
        ///   Evaluated power.
        /// </returns>
        protected override double DoInterpret(Context context)
        {
            return Math.Pow(@base.Interpret(context), exponent.Interpret(context));
        }

        /// <summary>
        ///   Base of the power.
        /// </summary>
        private readonly IExpression @base;

        /// <summary>
        ///   Exponent of the expression.
        /// </summary>
        private readonly IExpression exponent;
    }
}
