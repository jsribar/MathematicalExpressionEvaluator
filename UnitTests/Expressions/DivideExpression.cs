using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class DivideExpression
    {
        [TestMethod]
        public void InterpretMethodForDivisionOfCOnstant3WithConstantEvaluatesTo1Point5()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Constant(3);
            var right = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            Assert.AreEqual(1.5, new MathematicalExpressionEvaluator.Expressions.DivideExpression(left, right).Evaluate(24), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForDivisionOfConstant4WithVariableEvaluatesTo0Point5ForContext8()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Constant(4);
            var right = new MathematicalExpressionEvaluator.Expressions.Variable();
            Assert.AreEqual(0.5, new MathematicalExpressionEvaluator.Expressions.DivideExpression(left, right).Evaluate(8), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForDivisionOfVariableWithItselfEvaluatesTo1ForAnyContext()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Variable();
            var right = new MathematicalExpressionEvaluator.Expressions.Variable();
            Assert.AreEqual(1, new MathematicalExpressionEvaluator.Expressions.DivideExpression(left, right).Evaluate(8), 1e-10);
            Assert.AreEqual(1, new MathematicalExpressionEvaluator.Expressions.DivideExpression(left, right).Evaluate(4), 1e-10);
        }
    }
}
