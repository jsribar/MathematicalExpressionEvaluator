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

            Assert.AreEqual(12, difference.Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForConstantsAndVariableReturnsTheirDifference()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Variable();
            var right = new MathematicalExpressionEvaluator.Expressions.Constant(5);
            var difference = new MathematicalExpressionEvaluator.Expressions.SubtractExpression(left, right);

            Assert.AreEqual(-2, difference.Evaluate(3), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForTwoVariablesReturnsTheirDifference()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Variable();
            var right = new MathematicalExpressionEvaluator.Expressions.Variable();
            var difference = new MathematicalExpressionEvaluator.Expressions.SubtractExpression(left, right);

            Assert.AreEqual(0, difference.Evaluate(4), 1e-10);
        }
    }
}
