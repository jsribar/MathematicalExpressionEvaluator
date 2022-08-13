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
		public const string InvalidCharacter = "Invalid character";
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
