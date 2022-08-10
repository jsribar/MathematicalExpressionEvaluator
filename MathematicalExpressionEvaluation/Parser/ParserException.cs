using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSribar.MathematicalExpressionEvaluation
{
    /// <summary>
    ///   Exception thrown by <c>Parser</c>.
    /// </summary>
    public class ParserException : Exception
    {
        public ParserException(string message, int position)
            : base(message)
        {
            Position = position;
        }

        /// <summary>
        ///   Position in input string where exception was thrown.
        /// </summary>
        public readonly int Position;
    }
}
