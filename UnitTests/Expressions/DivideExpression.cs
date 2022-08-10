namespace Expressions
{
    [TestClass]
    public class DivideExpression
    {
        [TestMethod]
        public void InterpretMethodForDivisionOfCOnstant3WithConstantEvaluatesTo1Point5()
        {
            var left = new MathematicalExpressionEvaluation.Expressions.Constant(3);
            var right = new MathematicalExpressionEvaluation.Expressions.Constant(2);
            Assert.AreEqual(1.5, new MathematicalExpressionEvaluation.Expressions.DivideExpression(left, right).Interpret(new MathematicalExpressionEvaluation.Expressions.Context(24)), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForDivisionOfConstant4WithVariableEvaluatesTo0Point5ForContext8()
        {
            var left = new MathematicalExpressionEvaluation.Expressions.Constant(4);
            var right = new MathematicalExpressionEvaluation.Expressions.Variable();
            Assert.AreEqual(0.5, new MathematicalExpressionEvaluation.Expressions.DivideExpression(left, right).Interpret(new MathematicalExpressionEvaluation.Expressions.Context(8)), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForDivisionOfVariableWithItselfEvaluatesTo1ForAnyContext()
        {
            var left = new MathematicalExpressionEvaluation.Expressions.Variable();
            var right = new MathematicalExpressionEvaluation.Expressions.Variable();
            Assert.AreEqual(1, new MathematicalExpressionEvaluation.Expressions.DivideExpression(left, right).Interpret(new MathematicalExpressionEvaluation.Expressions.Context(8)), 1e-10);
            Assert.AreEqual(1, new MathematicalExpressionEvaluation.Expressions.DivideExpression(left, right).Interpret(new MathematicalExpressionEvaluation.Expressions.Context(4)), 1e-10);
        }
    }
}
