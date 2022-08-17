using System;

namespace JSribar.MathematicalExpressionEvaluator
{
    /// <summary>
    ///   Exception thrown for invalid identifier.
    /// </summary>
    public class IdentifierException : Exception
    {
        /// <summary>
        ///   Creates <c>IdentifierException</c> object with message and 
        ///   identifier provided.
        /// </summary>
        /// <param name="message">
        ///   Message.
        /// </param>
        /// <param name="identifier">
        ///   Invalid identifier.
        /// </param>
        public IdentifierException(string message, string identifier) : base(message)
        {
            Identifier = identifier;
        }

        /// <summary>
        ///   Name of the invalid identifier.
        /// </summary>
        public readonly string Identifier;

    }
}
