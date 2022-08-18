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

            var context = new MathematicalExpressionEvaluator.Expressions.Context(5);
            Assert.AreEqual(8, power.Interpret(context), 1e-10);

            @base = new MathematicalExpressionEvaluator.Expressions.Constant(-2);
            power = new MathematicalExpressionEvaluator.Expressions.PowerExpression(@base, exponent);
            Assert.AreEqual(-8, power.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForAConstantAndVariableReturnsConstantToThePowerOfVariable()
        {
            var @base = new MathematicalExpressionEvaluator.Expressions.Constant(3);
            var exponent = new MathematicalExpressionEvaluator.Expressions.Variable();
            var power = new MathematicalExpressionEvaluator.Expressions.PowerExpression(@base, exponent);

            var context = new MathematicalExpressionEvaluator.Expressions.Context(3);
            Assert.AreEqual(27, power.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForAVariableAndConstantReturnsVariableToThePowerOfConstant()
        {
            var @base = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            var exponent = new MathematicalExpressionEvaluator.Expressions.Variable();
            var power = new MathematicalExpressionEvaluator.Expressions.PowerExpression(@base, exponent);

            var context = new MathematicalExpressionEvaluator.Expressions.Context(-3);
            Assert.AreEqual(0.125, power.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForTwoVariablesReturnsVariableToThePowerOfVariable()
        {
            var var = new MathematicalExpressionEvaluator.Expressions.Variable();
            var power = new MathematicalExpressionEvaluator.Expressions.PowerExpression(var, var);

            var context = new MathematicalExpressionEvaluator.Expressions.Context(-2);
            Assert.AreEqual(0.25, power.Interpret(context), 1e-10);
        }
    }
}
