namespace Expressions
{
    [TestClass]
    public class MultiplyExpression
    {
        [TestMethod]
        public void InterpretMethodForMultiplicationOfConstant5AndConstantMinus2EvaluatesToMinus10()
        {
            var left = new MathematicalExpressionEvaluation.Expressions.Constant(5);
            var right = new MathematicalExpressionEvaluation.Expressions.Constant(-2);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(24);
            Assert.AreEqual(-10, new MathematicalExpressionEvaluation.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMultiplicationOfConstant3WithVariableEvaluatesTo15ForContext5()
        {
            var left = new MathematicalExpressionEvaluation.Expressions.Constant(3);
            var right = new MathematicalExpressionEvaluation.Expressions.Variable();

            var context = new MathematicalExpressionEvaluation.Expressions.Context(5);
            Assert.AreEqual(15, new MathematicalExpressionEvaluation.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMultiplicationOfVariableWithItselfEvaluatesToVariableSquared()
        {
            var left = new MathematicalExpressionEvaluation.Expressions.Variable();
            var right = new MathematicalExpressionEvaluation.Expressions.Variable();

            var context = new MathematicalExpressionEvaluation.Expressions.Context(5);
            Assert.AreEqual(25, new MathematicalExpressionEvaluation.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);

            context = new MathematicalExpressionEvaluation.Expressions.Context(8);
            Assert.AreEqual(64, new MathematicalExpressionEvaluation.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);
        }
    }
}