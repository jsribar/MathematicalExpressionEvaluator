using JSribar.MathematicalExpressionEvaluator.Expressions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace JSribar.MathematicalExpressionEvaluator
{
    /// <summary>
    ///   Class that parses a string with mathematical expression and evaluates
    ///   the resulting <c>Expression</c> object.
    /// </summary>
    public partial class Parser
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
        ///   Class containing information on a function and number of 
        ///   arguments consumed.
        /// </summary>
        private class FunctionArguments
        {
            /// <summary>
            ///   Creates <c>FunctionArguments</c> object.
            /// </summary>
            /// <param name="function">
            ///   <c>Operator</c> representing the function.
            /// </param>
            public FunctionArguments(Operator function)
            {
                Function = function;
                ArgumentsConsumed = 0;
            }

            /// <summary>
            ///   Increments the number of arguments processed and returns 
            ///   incemented number.
            /// </summary>
            /// <returns>Current number of processed arguments.</returns>
            public int IncrementArgumentsConsumed()
            {
                return ++ArgumentsConsumed;
            }

            /// <summary>
            ///   <c>Operator</c> representing the function.
            /// </summary>
            public readonly Operator Function;

            /// <summary>
            ///   Number of function arguments consumed. 
            /// </summary>
            private int ArgumentsConsumed;
        }

        /// <summary>
        ///   Stack with functions and number of arguments. Used to check number
        ///   of consumend arguments and provide detail error description.
        /// </summary>
        private readonly Stack<FunctionArguments> functions = new Stack<FunctionArguments>();

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
            functions.Clear();
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
                                    case '−': // U+2212
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
                            case ',':
                                if (operators.Count == 0)
                                {
                                    throw new ParserException(UnexpectedComma, pos);
                                }
                                ProcessComma(pos);
                                state = ParserState.AfterOperator;
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
        private static void SkipWhiteSpaces(string text, ref int pos)
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
                case ',':
                    throw new ParserException(UnexpectedComma, pos);
                case '-':
                case '−': // U+2212
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
                    operators.Push(Operator.LeftFunctionParenthesis);
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
                case '−': // U+2212
                    PushOperator(Operator.Subtraction);
                    return;
                case '*':
                case '×': // U+00D7
                case '⋅': // U+22C5
                    PushOperator(Operator.Multiplication);
                    return;
                case '/':
                case '÷': // U+00F7
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
        ///   Checks if <c>Operator</c> provided is left parenthesis, either opening 
        ///   a list of function arguments (<c>Operator.LeftFunctionParenthesis</c>) 
        ///   or starting a subexpression (<c>Operator.LeftParenthesis</c>).
        /// </summary>
        /// <param name="operator">
        ///   <c>Operator</c> to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if operator is <c>Operator.LeftFunctionParenthesis</c> or
        ///   <c>Operator.LeftParenthesis</c>. Else returns <c>false</c>:
        /// </returns>
        private static bool IsLeftParenthesis(Operator @operator)
        {
            return @operator == Operator.LeftParenthesis || @operator == Operator.LeftFunctionParenthesis;
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
                if (IsLeftParenthesis(operators.Peek()))
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
        private static string GetIdentifier(string text, int pos)
        {
            if (char.IsLetter(text[pos]))
            {
                int i = pos;
                do
                {
                    ++i;
                }
                while (i < text.Length && char.IsLetterOrDigit(text[i]));
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
                functions.Push(new FunctionArguments(function));
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
            while (operators.Count > 0 && !IsLeftParenthesis(operators.Peek()) && GetPrecedence(operators.Peek()) > GetPrecedence(@operator))
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
            var parenthesis = operators.Pop();
            if (parenthesis == Operator.LeftFunctionParenthesis && functions.Peek().IncrementArgumentsConsumed() < NumberOfOperands(functions.Peek().Function))
            {
                throw new ParserException(FunctionHasToFewArguments, pos);
            }
        }

        /// <summary>
        ///   Evaluates expression left from comma up to previous comma or function 
        ///   left parenthesis.
        /// </summary>
        /// <param name="pos">
        ///   Position of the right parenthesis used for error reporting.
        /// </param>
        private void ProcessComma(int pos)
        {
            while (operators.Peek() != Operator.LeftFunctionParenthesis && operators.Peek() != Operator.Comma)
            {
                EvaluateExpressionFromTop();
            }
            if (functions.Peek().IncrementArgumentsConsumed() >= NumberOfOperands(functions.Peek().Function))
            {
                throw new ParserException(FunctionHasToManyArguments, pos);
            }
            operators.Push(Operator.Comma);
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
        private static int GetPrecedence(Operator @operator)
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
                case Operator.LeftFunctionParenthesis:
                case Operator.Minus:
                    return 4;
                default:
                    break;
            }
            return 5;
        }

        /// <summary>
        ///   Evaluates number of operands which <c>Operator</c> provided requires.
        /// </summary>
        /// <param name="operator">
        ///   <c>Operator</c> for which evaluation is done is done.
        /// </param>
        /// <returns>
        ///   Number of operands. 
        /// </returns>
        private int NumberOfOperands(Operator @operator)
        {
            switch (@operator)
            {
                case Operator.Addition:
                case Operator.Subtraction:
                case Operator.Multiplication:
                case Operator.Division:
                case Operator.Power:
                    return 2;
            }
            if (function2Map.ContainsKey(@operator))
            {
                return 2;
            }
            return 1;
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
                if (topOperator == Operator.Comma)
                {
                    break;
                }
                topOperators.Push(topOperator);
                // If additional operands are required, push them to output.
                int n = NumberOfOperands(topOperator);
                while (--n > 0)
                {
                    topExpressions.Push(output.Pop());
                }
            } while (operators.Count > 0 && GetPrecedence(operators.Peek()) == precedence && !IsLeftParenthesis(operators.Peek()));

            Debug.Assert(topExpressions.Count != 0);
            // Evaluates operations from the local stack and pushes the resulting expression to the output stack.
            var lhs = topExpressions.Pop();
            while (topOperators.Count > 0)
            {
                var @operator = topOperators.Pop();
                if (functionMap.TryGetValue(@operator, out var function))
                {
                    lhs = new MathFunction(function, lhs);
                    functions.Pop();
                }
                else if (function2Map.TryGetValue(@operator, out var function2))
                {
                    var rhs = topExpressions.Pop();
                    lhs = new MathFunction2(function2, lhs, rhs);
                    functions.Pop();
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
            return !IsLeftParenthesis(operators.Peek());
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
        private static IExpression EvaluateBinaryOperation(Operator @operator, IExpression lhs, IExpression rhs)
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
