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

namespace JSribar.MathematicalExpressionEvaluator
{
	public partial class Parser
	{
		/// <summary>
		///   Parser exception messages.
		/// </summary>
		public const string DuplicateDecimalSeparator = "Duplicate decimal separator";
		public const string ExpressionTerminatedUnexpectedly = "Expression terminated unexpectedly";
		public const string FunctionHasToFewArguments = "Function has too few arguments";
		public const string FunctionHasToManyArguments = "Function has too many arguments";
		public const string FunctionNotFollowedByLeftParenthesis = "Function name not followerd by left parenthesis";
		public const string IdentifierAlreadyUsed = "Identifier is already used";
		public const string InvalidCharacter = "Invalid character";
		public const string InvalidIdentifier = "Invalid identifier";
		public const string InvalidNumberFormat = "Invalid number format";
		public const string InvalidOperator = "Invalid operator";
		public const string MissingLeftParenthesis = "Missing or mismatched left parentesis";
		public const string MissingRightParenthesis = "Missing or mismatched right parentesis";
		public const string UnexpectedComma = "Unexpected comma";
		public const string UnexpectedRigthParenthesis = "Unexpected right parenthesis";
		public const string UnexpectedSign = "Unexpected sign";
		public const string UnexpectedSpace = "Unexpected space";
		public const string UnknownIdentifier = "Unknown identifier";
	}
}
