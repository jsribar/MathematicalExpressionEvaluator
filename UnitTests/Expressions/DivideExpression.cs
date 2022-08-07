namespace JSribar.AlgebraicExpressionParser.UnitTests.Expressions
{
    [TestClass]
    public class DivideExpression
    {
        [TestMethod]
        public void InterpretMethodForDivisionOfCOnstant3WithConstantEvaluatesTo1Point5()
        {
            var left = new AlgebraicExpressionParser.Expressions.Constant(3);
            var right = new AlgebraicExpressionParser.Expressions.Constant(2);
            Assert.AreEqual(1.5, new AlgebraicExpressionParser.Expressions.DivideExpression(left, right).Interpret(new AlgebraicExpressionParser.Expressions.Context(24)), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForDivisionOfConstant4WithVariableEvaluatesTo0Point5ForContext8()
        {
            var left = new AlgebraicExpressionParser.Expressions.Constant(4);
            var right = new AlgebraicExpressionParser.Expressions.Variable();
            Assert.AreEqual(0.5, new AlgebraicExpressionParser.Expressions.DivideExpression(left, right).Interpret(new AlgebraicExpressionParser.Expressions.Context(8)), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForDivisionOfVariableWithItselfEvaluatesTo1ForAnyContext()
        {
            var left = new AlgebraicExpressionParser.Expressions.Variable();
            var right = new AlgebraicExpressionParser.Expressions.Variable();
            Assert.AreEqual(1, new AlgebraicExpressionParser.Expressions.DivideExpression(left, right).Interpret(new AlgebraicExpressionParser.Expressions.Context(8)), 1e-10);
            Assert.AreEqual(1, new AlgebraicExpressionParser.Expressions.DivideExpression(left, right).Interpret(new AlgebraicExpressionParser.Expressions.Context(4)), 1e-10);
        }
    }
}
