using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JSribar.AlgebraicExpressionParser.UnitTests.Expressions
{
    [TestClass]
    public class PowerExpression
    {
        [TestMethod]
        public void InterpretMethodForPowerExpressionForTwoConstantsReturnsFirstConstantToThePowerOfSecond()
        {
            var @base = new AlgebraicExpressionParser.Expressions.Constant(2);
            var exponent = new AlgebraicExpressionParser.Expressions.Constant(3);
            var power = new AlgebraicExpressionParser.Expressions.PowerExpression(@base, exponent);

            var context = new AlgebraicExpressionParser.Expressions.Context(5);
            Assert.AreEqual(8, power.Interpret(context), 1e-10);

            @base = new AlgebraicExpressionParser.Expressions.Constant(-2);
            power = new AlgebraicExpressionParser.Expressions.PowerExpression(@base, exponent);
            Assert.AreEqual(-8, power.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForAConstantAndVariableReturnsConstantToThePowerOfVariable()
        {
            var @base = new AlgebraicExpressionParser.Expressions.Constant(3);
            var exponent = new AlgebraicExpressionParser.Expressions.Variable();
            var power = new AlgebraicExpressionParser.Expressions.PowerExpression(@base, exponent);

            var context = new AlgebraicExpressionParser.Expressions.Context(3);
            Assert.AreEqual(27, power.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForAVariableAndConstantReturnsVariableToThePowerOfConstant()
        {
            var @base = new AlgebraicExpressionParser.Expressions.Constant(2);
            var exponent = new AlgebraicExpressionParser.Expressions.Variable();
            var power = new AlgebraicExpressionParser.Expressions.PowerExpression(@base, exponent);

            var context = new AlgebraicExpressionParser.Expressions.Context(-3);
            Assert.AreEqual(0.125, power.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForTwoVariablesReturnsVariableToThePowerOfVariable()
        {
            var var = new AlgebraicExpressionParser.Expressions.Variable();
            var power = new AlgebraicExpressionParser.Expressions.PowerExpression(var, var);

            var context = new AlgebraicExpressionParser.Expressions.Context(-2);
            Assert.AreEqual(0.25, power.Interpret(context), 1e-10);
        }
    }
}
