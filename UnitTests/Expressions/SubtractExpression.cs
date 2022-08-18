using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class SubtractExpression
    {
        [TestMethod]
        public void InterpretMethodForTwoConstantsReturnsTheirDifference()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Constant(16);
            var right = new MathematicalExpressionEvaluator.Expressions.Constant(4);
            var difference = new MathematicalExpressionEvaluator.Expressions.SubtractExpression(left, right);

            var context = new MathematicalExpressionEvaluator.Expressions.Context(5);
            Assert.AreEqual(12, difference.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForConstantsAndVariableReturnsTheirDifference()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Variable();
            var right = new MathematicalExpressionEvaluator.Expressions.Constant(5);
            var difference = new MathematicalExpressionEvaluator.Expressions.SubtractExpression(left, right);

            var context = new MathematicalExpressionEvaluator.Expressions.Context(3);
            Assert.AreEqual(-2, difference.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForTwoVariablesReturnsTheirDifference()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Variable();
            var right = new MathematicalExpressionEvaluator.Expressions.Variable();
            var difference = new MathematicalExpressionEvaluator.Expressions.SubtractExpression(left, right);

            var context = new MathematicalExpressionEvaluator.Expressions.Context(4);
            Assert.AreEqual(0, difference.Interpret(context), 1e-10);
        }
    }
}
