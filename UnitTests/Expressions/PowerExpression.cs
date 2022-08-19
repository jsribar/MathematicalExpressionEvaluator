using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class PowerExpression
    {
        [TestMethod]
        public void InterpretMethodForPowerExpressionForTwoConstantsReturnsFirstConstantToThePowerOfSecond()
        {
            var @base = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            var exponent = new MathematicalExpressionEvaluator.Expressions.Constant(3);
            var power = new MathematicalExpressionEvaluator.Expressions.PowerExpression(@base, exponent);

            Assert.AreEqual(8, power.Evaluate(5), 1e-10);

            @base = new MathematicalExpressionEvaluator.Expressions.Constant(-2);
            power = new MathematicalExpressionEvaluator.Expressions.PowerExpression(@base, exponent);
            Assert.AreEqual(-8, power.Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForAConstantAndVariableReturnsConstantToThePowerOfVariable()
        {
            var @base = new MathematicalExpressionEvaluator.Expressions.Constant(3);
            var exponent = new MathematicalExpressionEvaluator.Expressions.Variable();
            var power = new MathematicalExpressionEvaluator.Expressions.PowerExpression(@base, exponent);

            Assert.AreEqual(27, power.Evaluate(3), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForAVariableAndConstantReturnsVariableToThePowerOfConstant()
        {
            var @base = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            var exponent = new MathematicalExpressionEvaluator.Expressions.Variable();
            var power = new MathematicalExpressionEvaluator.Expressions.PowerExpression(@base, exponent);

            Assert.AreEqual(0.125, power.Evaluate(-3), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForTwoVariablesReturnsVariableToThePowerOfVariable()
        {
            var var = new MathematicalExpressionEvaluator.Expressions.Variable();
            var power = new MathematicalExpressionEvaluator.Expressions.PowerExpression(var, var);

            Assert.AreEqual(0.25, power.Evaluate(-2), 1e-10);
        }
    }
}
