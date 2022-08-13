using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class SumExpression
    {
        [TestMethod]
        public void InterpretMethodForTwoConstantsReturnsTheirSum()
        {
            var lhs = new MathematicalExpressionEvaluation.Expressions.Constant(23);
            var rhs = new MathematicalExpressionEvaluation.Expressions.Constant(2);
            var sum = new MathematicalExpressionEvaluation.Expressions.SumExpression(lhs, rhs);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(3);
            Assert.AreEqual(25, sum.Interpret(context));
        }

        [TestMethod]
        public void InterpretMethodForConstantsAndVariableReturnsTheirSum()
        {
            var lhs = new MathematicalExpressionEvaluation.Expressions.Variable();
            var rhs = new MathematicalExpressionEvaluation.Expressions.Constant(2);
            var sum = new MathematicalExpressionEvaluation.Expressions.SumExpression(lhs, rhs);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(3);
            Assert.AreEqual(5, sum.Interpret(context));
            
            context = new MathematicalExpressionEvaluation.Expressions.Context(5);
            Assert.AreEqual(7, sum.Interpret(context));
        }

        [TestMethod]
        public void InterpretMethodForTwoVariablesReturnsTheirSum()
        {
            var lhs = new MathematicalExpressionEvaluation.Expressions.Variable();
            var rhs = new MathematicalExpressionEvaluation.Expressions.Variable();
            var sum = new MathematicalExpressionEvaluation.Expressions.SumExpression(lhs, rhs);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(4);
            Assert.AreEqual(8, sum.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForThreeVariablesReturnsTheirSum()
        {
            var first = new MathematicalExpressionEvaluation.Expressions.Variable();
            var second = new MathematicalExpressionEvaluation.Expressions.Constant(2);
            var third = new MathematicalExpressionEvaluation.Expressions.Constant(5);

            var sum1 = new MathematicalExpressionEvaluation.Expressions.SumExpression(first, second);
            var sum2 = new MathematicalExpressionEvaluation.Expressions.SumExpression(sum1, third);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(4);
            Assert.AreEqual(6, sum1.Interpret(context), 1e-10);
            Assert.AreEqual(11, sum2.Interpret(context), 1e-10);
        }
    }
}