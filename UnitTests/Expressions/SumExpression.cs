namespace JSribar.AlgebraicExpressionParser.UnitTests.Expressions
{
    [TestClass]
    public class SumExpression
    {
        [TestMethod]
        public void InterpretMethodForTwoConstantsReturnsTheirSum()
        {
            var lhs = new AlgebraicExpressionParser.Expressions.Constant(23);
            var rhs = new AlgebraicExpressionParser.Expressions.Constant(2);
            var sum = new AlgebraicExpressionParser.Expressions.SumExpression(lhs, rhs);

            var context = new AlgebraicExpressionParser.Expressions.Context(3);
            Assert.AreEqual(25, sum.Interpret(context));
        }

        [TestMethod]
        public void InterpretMethodForConstantsAndVariableReturnsTheirSum()
        {
            var lhs = new AlgebraicExpressionParser.Expressions.Variable();
            var rhs = new AlgebraicExpressionParser.Expressions.Constant(2);
            var sum = new AlgebraicExpressionParser.Expressions.SumExpression(lhs, rhs);

            Assert.AreEqual(5, sum.Interpret(new AlgebraicExpressionParser.Expressions.Context(3)));
            Assert.AreEqual(7, sum.Interpret(new AlgebraicExpressionParser.Expressions.Context(5)));
        }

        [TestMethod]
        public void InterpretMethodForTwoVariablesReturnsTheirSum()
        {
            var lhs = new AlgebraicExpressionParser.Expressions.Variable();
            var rhs = new AlgebraicExpressionParser.Expressions.Variable();
            var sum = new AlgebraicExpressionParser.Expressions.SumExpression(lhs, rhs);

            Assert.AreEqual(8, sum.Interpret(new AlgebraicExpressionParser.Expressions.Context(4)));
        }

        [TestMethod]
        public void InterpretMethodForThreeVariablesReturnsTheirSum()
        {
            var first = new AlgebraicExpressionParser.Expressions.Variable();
            var second = new AlgebraicExpressionParser.Expressions.Constant(2);
            var third = new AlgebraicExpressionParser.Expressions.Constant(5);

            var sum1 = new AlgebraicExpressionParser.Expressions.SumExpression(first, second);
            var sum2 = new AlgebraicExpressionParser.Expressions.SumExpression(sum1, third);

            Assert.AreEqual(11, sum2.Interpret(new AlgebraicExpressionParser.Expressions.Context(4)));
        }
    }
}