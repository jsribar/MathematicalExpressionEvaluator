using JSribar.AlgebraicExpressionParser.Expressions;
using System.Diagnostics;
using System.Globalization;

namespace JSribar.AlgebraicExpressionParser
{
    /// <summary>
    ///   Class that parses a string with mathematical expression and evaluates
    ///   the resulting <c>Expression</c> object.
    /// </summary>
    public class Parser
    {
        /// <summary>
        ///   Specifies current state of the parsing process.
        /// </summary>
        private enum ParserState
        {
            BeforeOperator,
            AfterOperator,
        }

        /// <summary>
        ///   Operators supported.
        /// </summary>
        private enum Operator
        {
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Power,
            LeftParenthesis,
            Minus,
            // functions
            Sin,
            Cos,
            Tan,
            Sqrt,
            Exp,
            Log,
            Log10,
            Asin,
            Acos,
            Atan,
            Abs
        }

        /// <summary>
        ///   Mapping of string token to <c>Operator</c> enumeration.
        /// </summary>
        private readonly Dictionary<string, Operator> functionTokenMap = new Dictionary<string, Operator> {
            { "sin", Operator.Sin },
            { "cos", Operator.Cos  },
            { "tan", Operator.Tan },
            { "sqrt", Operator.Sqrt },
            { "exp", Operator.Exp },
            { "ln", Operator.Log  },
            { "log", Operator.Log10 },
            { "asin", Operator.Asin },
            { "acos", Operator.Acos },
            { "atan", Operator.Atan },
            { "abs", Operator.Abs },
        };

        /// <summary>
        ///   Mapping of <c>Operator</c> enumeration to function delegates.
        /// </summary>
        private readonly Dictionary<Operator, MathFunction.Function> functionMap = new Dictionary<Operator, MathFunction.Function> {
            { Operator.Sin, Math.Sin },
            { Operator.Cos, Math.Cos },
            { Operator.Tan, Math.Tan},
            { Operator.Sqrt, Math.Sqrt },
            { Operator.Exp, Math.Exp},
            { Operator.Log, Math.Log },
            { Operator.Log10, Math.Log10 },
            { Operator.Asin, Math.Asin },
            { Operator.Acos, Math.Acos },
            { Operator.Atan, Math.Atan },
            { Operator.Abs, Math.Abs },
        };

        /// <summary>
        ///   Mapping of string token to mathematical constant values.
        /// </summary>
        private readonly Dictionary<string, double> mathematicalConstantsMap = new Dictionary<string, double> {
            { "PI", Math.PI },
            { "E", Math.E  },
        };

        /// <summary>
        ///   Set of supported variable identifiers.
        /// </summary>
        private readonly HashSet<string> variableNames = new HashSet<string>();

        /// <summary>
        ///   Stack with operators to be processed. On successful parsing this 
        ///   stack should be empty.
        /// </summary>
        private readonly Stack<Operator> operators = new Stack<Operator>();

        /// <summary>
        ///   Output stack where results are pushed. On successful parsing this 
        ///   stack should contain the final expression only.
        /// </summary>
        private readonly Stack<IExpression> output = new Stack<IExpression>();

        /// <summary>
        ///   Parser exception messages.
        /// </summary>
        public const string DuplicateDecimalSeparator = "Duplicate decimal separator";
        public const string ExpressionTerminatedUnexpectedly = "Expression terminated unexpectedly";
        public const string FunctionNotFollowedByLeftParenthesis = "Function name not followerd by left parenthesis";
        public const string InvalidCharacter = "Invalid character";
        public const string InvalidNumberFormat = "Invalid number format";
        public const string InvalidOperator = "Invalid operator";
        public const string MissingLeftParenthesis = "Missing or mismatched left parentesis";
        public const string MissingRightParenthesis = "Missing or mismatched right parentesis";
        public const string UnexpectedRigthParenthesis = "Unexpected right parenthesis";
        public const string UnexpectedSign = "Unexpected sign";
        public const string UnexpectedSpace = "Unexpected space";
        public const string UnknownIdentifier = "Unknown identifier";

        /// <summary>
        ///   Creates <c>Parser</c> for expressions with a single variable 'x'.
        /// </summary>
        public Parser()
        {
            variableNames = new HashSet<string> { "x" };
        }

        /// <summary>
        ///   Creates <c>Parser</c> for expressions with custom variables.
        /// </summary>
        /// <param name="variableNames">
        ///   Collection of variable identifiers.
        /// </param>
        public Parser(ICollection<string> variableNames)
        {
            this.variableNames = new HashSet<string>(variableNames);
        }

        /// <summary>
        ///   Parses the text of mathematical expression provided. Implements 
        ///   <see href="https://en.wikipedia.org/wiki/Shunting_yard_algorithm">
        ///   Dykstra's Shunting yard algorithm</see>, taking care of operator 
        ///   precedence and parentheses.
        /// </summary>
        /// <param name="text">
        ///   String with expression to parse.
        /// </param>
        /// <returns>
        ///   Final <c>Expression</c> object.
        /// </returns>
        public IExpression Parse(string text)
        {
            operators.Clear();
            output.Clear();
            ParserState state = ParserState.AfterOperator;

            for (int pos = 0; pos < text.Length;)
            {
                switch (state)
                {
                    case ParserState.AfterOperator:
                        SkipWhiteSpaces(text, ref pos);
                        if (pos == text.Length)
                        {
                            throw new ParserException(ExpressionTerminatedUnexpectedly, pos);
                        }
                        switch (text[pos])
                        {
                            case '(':
                                operators.Push(Operator.LeftParenthesis);
                                ++pos;
                                break;
                            case ')':
                                throw new ParserException(UnexpectedRigthParenthesis, pos);
                            default:
                                switch (text[pos])
                                {
                                    case '-':
                                        operators.Push(Operator.Minus);
                                        ++pos;
                                        break;
                                    case '+':
                                        ++pos;
                                        break;
                                }
                                state = ProcessTokenAfterOperator(text, ref pos);
                                break;
                        }
                        break;
                    case ParserState.BeforeOperator:
                        SkipWhiteSpaces(text, ref pos);
                        if (pos == text.Length)
                        {
                            break;
                        }
                        var token = text[pos];
                        switch (token)
                        {
                            case ')':
                                ProcessRightParenthesis(pos);
                                state = ParserState.BeforeOperator;
                                break;
                            default:
                                ProcessOperator(token, pos);
                                state = ParserState.AfterOperator;
                                break;
                        }
                        ++pos;
                        break;
                }
            }
            if (state != ParserState.BeforeOperator)
            {
                throw new ParserException(ExpressionTerminatedUnexpectedly, text.Length);
            }
            return FinalExpression(text);
        }

        /// <summary>
        ///   Increments <c>pos</c> to the first non-space character.
        /// </summary>
        /// <param name="text">
        ///   Text of the mathematical expression.
        /// </param>
        /// <param name="pos">
        ///   Position where space sequence starts.
        /// </param>
        private void SkipWhiteSpaces(string text, ref int pos)
        {
            while (pos < text.Length && text[pos] == ' ')
            {
                ++pos;
            }
        }

        /// <summary>
        ///   Processes token after operator.
        /// </summary>
        /// <param name="text">
        ///   Text of the mathematical expression.
        /// </param>
        /// <param name="pos">
        ///   Position where token starts.
        /// </param>
        /// <returns>
        ///   New <c>ParserState</c> value.
        /// </returns>
        private ParserState ProcessTokenAfterOperator(string text, ref int pos)
        {
            if (text.Length == pos)
            {
                throw new ParserException(ExpressionTerminatedUnexpectedly, pos);
            }
            switch (text[pos])
            {
                case ' ':
                    throw new ParserException(UnexpectedSpace, pos);
                case '-':
                case '+':
                    throw new ParserException(UnexpectedSign, pos);
                case '(':
                    operators.Push(Operator.LeftParenthesis);
                    ++pos;
                    return ParserState.AfterOperator;
                default:
                    if (char.IsDigit(text[pos]))
                    {
                        PushNumber(text, ref pos);
                        return ParserState.BeforeOperator;
                    }
                    break;
            }
            // Extract a sequence of letters and digits.
            string identifier = GetIdentifier(text, pos);
            if (identifier.Length > 0)
            {
                if (PushFunction(identifier, ref pos))
                {
                    // Function name must be followed by left parenthesis.
                    if (text[pos] != '(')
                    {
                        throw new ParserException(FunctionNotFollowedByLeftParenthesis, pos);
                    }
                    operators.Push(Operator.LeftParenthesis);
                    ++pos;
                    return ParserState.AfterOperator;
                }
                if (PushVariable(identifier, ref pos))
                {
                    return ParserState.BeforeOperator;
                }
                if (PushMathematicalConstant(identifier, ref pos))
                {
                    return ParserState.BeforeOperator;
                }
                throw new ParserException(UnknownIdentifier, pos);
            }
            throw new ParserException(InvalidCharacter, pos);
        }

        /// <summary>
        ///   Processes the operator token.
        /// </summary>
        /// <param name="token">
        ///   Character representing operator.
        /// </param>
        /// <param name="pos">
        ///   Position of the token.
        /// </param>
        private void ProcessOperator(char token, int pos)
        {
            switch (token)
            {
                case '+':
                    PushOperator(Operator.Addition);
                    return;
                case '-':
                    PushOperator(Operator.Subtraction);
                    return;
                case '*':
                    PushOperator(Operator.Multiplication);
                    return;
                case '/':
                    PushOperator(Operator.Division);
                    return;
                case '^':
                    PushOperator(Operator.Power);
                    return;
                default:
                    throw new ParserException(InvalidOperator, pos);
            }
        }

        /// <summary>
        ///   Evaluates remaining operators from the operator stack, 
        /// </summary>
        /// <returns>
        ///   Final <c>Expression</c> object.
        /// </returns>
        private IExpression FinalExpression(string text)
        {
            while (operators.Count > 0)
            {
                if (operators.Peek() == Operator.LeftParenthesis)
                {
                    // TODO: Provide exact position
                    throw new ParserException(MissingRightParenthesis, text.Length);
                }
                EvaluateExpressionFromTop();
            }
            return output.Pop();
        }

        /// <summary>
        ///   Extracts identifier, which starts with a letter and is followed 
        ///   by a sequence of letters and digits.
        /// </summary>
        /// <param name="text">
        ///   Text of the mathematical expression.
        /// </param>
        /// <param name="pos">
        ///   Position where identifier starts.
        /// </param>
        /// <returns>
        ///   String with identifier.
        /// </returns>
        private string GetIdentifier(string text, int pos)
        {
            if (char.IsLetter(text[pos]))
            {
                int i = pos;
                do
                {
                    ++i;
                }
                while (i < text.Length && Char.IsLetterOrDigit(text[i]));
                return text.Substring(pos, i - pos);
            }
            return string.Empty;
        }

        /// <summary>
        ///   Parses and pushes the numeric constant to the output stack.
        /// </summary>
        /// <param name="text">
        ///   Text of the mathematical expression.
        /// </param>
        /// <param name="pos">
        ///   Position where value of the constant starts.
        /// </param>
        private void PushNumber(string text, ref int pos)
        {
            int start = pos;
            int decimalSeparators = 0;
            while (pos < text.Length && (char.IsDigit(text[pos]) || text[pos] == '.'))
            {
                if (text[pos] == '.')
                {
                    ++decimalSeparators;
                    if (decimalSeparators > 1)
                    {
                        throw new ParserException(DuplicateDecimalSeparator, pos);
                    }
                }
                ++pos;
            }
            // Check if last digit is followed by 'e' or 'E' for scientific format
            if (pos < text.Length && (text[pos] == 'e' || text[pos] == 'E'))
            {
                ++pos;
                if (pos < text.Length && (text[pos] == '+' || text[pos] == '-'))
                {
                    ++pos;
                }
                while (pos < text.Length && char.IsDigit(text[pos]))
                {
                    ++pos;
                }
            }
            // Leading spaces should be eliminated already and trailing spaces will be handled separately, so we set number style accordingly.
            // Decimal separator must be a point so we set formatProvider to CultureInfo.InvariantCulture.
            if (double.TryParse(text.Substring(start, pos - start), NumberStyles.None | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out double value))
            {
                output.Push(new Constant(value));
                return;
            }
            throw new ParserException(InvalidNumberFormat, start);
        }

        /// <summary>
        ///   Resolves the function and pushes it onto operator stack.
        /// </summary>
        /// <param name="functionName">
        ///   Name of the function.
        /// </param>
        /// <param name="pos">
        ///   Position of the function name.
        /// </param>
        /// <returns>
        ///   <c>true</c> if function is found in <c>functionTokenMap</c>, 
        ///   else returns <c>false</c>.
        /// </returns>
        private bool PushFunction(string functionName, ref int pos)
        {
            if (functionTokenMap.TryGetValue(functionName, out Operator function))
            {
                operators.Push(function);
                pos += functionName.Length;
                return true;
            }
            return false;
        }

        /// <summary>
        ///   Resolves variable and pushes it onto output stack.
        /// </summary>
        /// <param name="variableName">
        ///   Variable identifer.
        /// </param>
        /// <param name="pos">
        ///   Position of the variable name.
        /// </param>
        /// <returns>
        ///   <c>true</c> if variable is defined in <c>variableNames</c>, 
        ///   else returns <c>false</c>.
        /// </returns>
        private bool PushVariable(string variableName, ref int pos)
        {
            if (variableNames.Contains(variableName))
            {
                output.Push(new Variable(variableName));
                pos += variableName.Length;
                return true;
            }
            return false;
        }

        /// <summary>
        ///   Parses and pushes mathematical constant to the output stack.
        /// </summary>
        /// <param name="identifier">
        ///   Mathematical constant identifier.
        /// </param>
        /// <param name="pos">
        ///   Position where value of the constant starts.
        /// </param>
        /// <returns>
        ///   <c>true</c> if constant is defined in <c>mathematicalConstantsMap</c>, 
        ///   else returns <c>false</c>.
        /// </returns>
        private bool PushMathematicalConstant(string identifier, ref int pos)
        {
            if (mathematicalConstantsMap.TryGetValue(identifier, out double value))
            {
                output.Push(new Constant(value));
                pos += identifier.Length;
                return true;
            }
            return false;
        }

        /// <summary>
        ///   Pushes operator passed onto operators stack. If operator on the 
        ///   top of the operator stack has higher precedence then operator 
        ///   provided, function evaluates operators from the top of the stack 
        ///   that have higher precedence first, pushes the result to the 
        ///   output stack and finally appends the operator provided.
        /// </summary>
        /// <param name="operator">
        ///   <c>Operator</c> to push onto operator stack.
        /// </param>
        private void PushOperator(Operator @operator)
        {
            while (operators.Count > 0 && operators.Peek() != Operator.LeftParenthesis && GetPrecedence(operators.Peek()) > GetPrecedence(@operator))
            {
                EvaluateExpressionFromTop();
            }
            operators.Push(@operator);
        }

        /// <summary>
        ///   Evaluates expression left from right parenthesis up to matching 
        ///   left parenthesis and then pops and discards the left parenthesis.
        /// </summary>
        /// <param name="pos">
        ///   Position of the right parenthesis used for error reporting.
        /// </param>
        private void ProcessRightParenthesis(int pos)
        {
            while (IsNotLeftParenthesis(pos))
            {
                EvaluateExpressionFromTop();
            }
            operators.Pop();
        }

        /// <summary>
        ///   Evaluates precedence of the operator provided.
        /// </summary>
        /// <param name="operator">
        ///   <c>Operator</c> for which precedence is requested.
        /// </param>
        /// <returns>
        ///   Integer represeting precedence level. Higher values represent 
        ///   higher precedence.
        /// </returns>
        private int GetPrecedence(Operator @operator)
        {
            switch (@operator)
            {
                case Operator.Addition:
                case Operator.Subtraction:
                    return 1;
                case Operator.Multiplication:
                case Operator.Division:
                    return 2;
                case Operator.Power:
                    return 3;
                case Operator.LeftParenthesis:
                case Operator.Minus:
                    return 4;
            }
            return 5;
        }

        /// <summary>
        ///   Evaluates expression for the top operators. First pops all 
        ///   operators with precedence equal to the precedence of top operator, 
        ///   together with corresponding operands and pushes them to a local 
        ///   stacks in order to evaluate operations from left to right, as 
        ///   they appear in the original text. Then processes local stacks 
        ///   and pushes the result to the output stack.
        /// </summary>
        private void EvaluateExpressionFromTop()
        {
            Debug.Assert(operators.Count > 0 && output.Count > 0);
            // Local stacks for evaluation from left to right.
            Stack<IExpression> topExpressions = new Stack<IExpression>();
            Stack<Operator> topOperators = new Stack<Operator>();

            int precedence = GetPrecedence(operators.Peek());
            topExpressions.Push(output.Pop());
            do
            {
                var topOperator = operators.Pop();
                topOperators.Push(topOperator);
                // For binary operation, additional operand is required.
                switch (topOperator)
                {
                    case Operator.Addition:
                    case Operator.Subtraction:
                    case Operator.Multiplication:
                    case Operator.Division:
                    case Operator.Power:
                        topExpressions.Push(output.Pop());
                        break;
                }
            } while (operators.Count > 0 && GetPrecedence(operators.Peek()) == precedence && operators.Peek() != Operator.LeftParenthesis);

            Debug.Assert(topExpressions.Count != 0);
            // Evaluates operations from the local stack and pushes the resulting expression to the output stack.
            var lhs = topExpressions.Pop();
            while (topOperators.Count > 0)
            {
                var @operator = topOperators.Pop();
                if (functionMap.TryGetValue(@operator, out MathFunction.Function function))
                {
                    lhs = new MathFunction(function, lhs);
                }
                else if (@operator == Operator.Minus)
                {
                    lhs.ToggleSign();
                }
                else
                {
                    var rhs = topExpressions.Pop();
                    lhs = EvaluateBinaryOperation(@operator, lhs, rhs);
                }
            }
            output.Push(lhs);
            Debug.Assert(topExpressions.Count == 0);
        }

        /// <summary>
        ///   Checks if operator on the top of operator stack is not left 
        ///   parenthesis.
        /// </summary>
        /// <param name="pos">
        ///   Position of the right parenthesis used for error reporting.
        /// </param>
        /// <returns>
        ///   Returns <c>true</c> if character is not left parenthesis. 
        ///   If character is '(' returns <c>false</c>.
        /// </returns>
        private bool IsNotLeftParenthesis(int pos)
        {
            if (operators.Count == 0)
            {
                throw new ParserException(MissingLeftParenthesis, pos);
            }
            return operators.Peek() != Operator.LeftParenthesis;
        }

        /// <summary>
        ///   Creates an expression for a binary operator, lefthand and 
        ///   righthand expressions provided.
        /// </summary>
        /// <param name="operator">
        ///   Binary operator for which expression should be evaluated.
        /// </param>
        /// <param name="lhs">
        ///   Expression on the lefthand side of the operator.
        /// </param>
        /// <param name="rhs">
        ///   Expression on the righthand side of the operator.
        /// </param>
        /// <returns>
        ///   Resulting <c>Expression</c> object.
        /// </returns>
        private IExpression EvaluateBinaryOperation(Operator @operator, IExpression lhs, IExpression rhs)
        {
            switch (@operator)
            {
                case Operator.Addition:
                    return new SumExpression(lhs, rhs);
                case Operator.Subtraction:
                    return new SubtractExpression(lhs, rhs);
                case Operator.Multiplication:
                    return new MultiplyExpression(lhs, rhs);
                case Operator.Division:
                    return new DivideExpression(lhs, rhs);
                case Operator.Power:
                    return new PowerExpression(lhs, rhs);
            }
            Debug.Assert(false);
            return null;
        }
    }
}
