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

            var context = new MathematicalExpressionEvaluator.Expressions.Context(3);
            Assert.AreEqual(25, sum.Interpret(context));
        }

        [TestMethod]
        public void InterpretMethodForConstantsAndVariableReturnsTheirSum()
        {
            var lhs = new MathematicalExpressionEvaluator.Expressions.Variable();
            var rhs = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            var sum = new MathematicalExpressionEvaluator.Expressions.SumExpression(lhs, rhs);

            var context = new MathematicalExpressionEvaluator.Expressions.Context(3);
            Assert.AreEqual(5, sum.Interpret(context));
            
            context = new MathematicalExpressionEvaluator.Expressions.Context(5);
            Assert.AreEqual(7, sum.Interpret(context));
        }

        [TestMethod]
        public void InterpretMethodForTwoVariablesReturnsTheirSum()
        {
            var lhs = new MathematicalExpressionEvaluator.Expressions.Variable();
            var rhs = new MathematicalExpressionEvaluator.Expressions.Variable();
            var sum = new MathematicalExpressionEvaluator.Expressions.SumExpression(lhs, rhs);

            var context = new MathematicalExpressionEvaluator.Expressions.Context(4);
            Assert.AreEqual(8, sum.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForThreeVariablesReturnsTheirSum()
        {
            var first = new MathematicalExpressionEvaluator.Expressions.Variable();
            var second = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            var third = new MathematicalExpressionEvaluator.Expressions.Constant(5);

            var sum1 = new MathematicalExpressionEvaluator.Expressions.SumExpression(first, second);
            var sum2 = new MathematicalExpressionEvaluator.Expressions.SumExpression(sum1, third);

            var context = new MathematicalExpressionEvaluator.Expressions.Context(4);
            Assert.AreEqual(6, sum1.Interpret(context), 1e-10);
            Assert.AreEqual(11, sum2.Interpret(context), 1e-10);
        }
    }
}
