using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class SumExpression
    {
        [TestMethod]
        public void InterpretMethodForTwoConstantsReturnsTheirSum()
        {
            var lhs = new MathematicalExpressionEvaluator.Expressions.Constant(23);
            var rhs = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            var sum = new MathematicalExpressionEvaluator.Expressions.SumExpression(lhs, rhs);

            Assert.AreEqual(25, sum.Evaluate(3));
        }

        [TestMethod]
        public void InterpretMethodForConstantsAndVariableReturnsTheirSum()
        {
            var lhs = new MathematicalExpressionEvaluator.Expressions.Variable();
            var rhs = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            var sum = new MathematicalExpressionEvaluator.Expressions.SumExpression(lhs, rhs);

            Assert.AreEqual(5, sum.Evaluate(3));
            
            Assert.AreEqual(7, sum.Evaluate(5));
        }

        [TestMethod]
        public void InterpretMethodForTwoVariablesReturnsTheirSum()
        {
            var lhs = new MathematicalExpressionEvaluator.Expressions.Variable();
            var rhs = new MathematicalExpressionEvaluator.Expressions.Variable();
            var sum = new MathematicalExpressionEvaluator.Expressions.SumExpression(lhs, rhs);

            Assert.AreEqual(8, sum.Evaluate(4), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForThreeVariablesReturnsTheirSum()
        {
            var first = new MathematicalExpressionEvaluator.Expressions.Variable();
            var second = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            var third = new MathematicalExpressionEvaluator.Expressions.Constant(5);

            var sum1 = new MathematicalExpressionEvaluator.Expressions.SumExpression(first, second);
            var sum2 = new MathematicalExpressionEvaluator.Expressions.SumExpression(sum1, third);

            Assert.AreEqual(6, sum1.Evaluate(4), 1e-10);
            Assert.AreEqual(11, sum2.Evaluate(4), 1e-10);
        }
    }
}
