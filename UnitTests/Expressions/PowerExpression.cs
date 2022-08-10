namespace Expressions
{
    [TestClass]
    public class PowerExpression
    {
        [TestMethod]
        public void InterpretMethodForPowerExpressionForTwoConstantsReturnsFirstConstantToThePowerOfSecond()
        {
            var @base = new MathematicalExpressionEvaluation.Expressions.Constant(2);
            var exponent = new MathematicalExpressionEvaluation.Expressions.Constant(3);
            var power = new MathematicalExpressionEvaluation.Expressions.PowerExpression(@base, exponent);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(5);
            Assert.AreEqual(8, power.Interpret(context), 1e-10);

            @base = new MathematicalExpressionEvaluation.Expressions.Constant(-2);
            power = new MathematicalExpressionEvaluation.Expressions.PowerExpression(@base, exponent);
            Assert.AreEqual(-8, power.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForAConstantAndVariableReturnsConstantToThePowerOfVariable()
        {
            var @base = new MathematicalExpressionEvaluation.Expressions.Constant(3);
            var exponent = new MathematicalExpressionEvaluation.Expressions.Variable();
            var power = new MathematicalExpressionEvaluation.Expressions.PowerExpression(@base, exponent);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(3);
            Assert.AreEqual(27, power.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForAVariableAndConstantReturnsVariableToThePowerOfConstant()
        {
            var @base = new MathematicalExpressionEvaluation.Expressions.Constant(2);
            var exponent = new MathematicalExpressionEvaluation.Expressions.Variable();
            var power = new MathematicalExpressionEvaluation.Expressions.PowerExpression(@base, exponent);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(-3);
            Assert.AreEqual(0.125, power.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForPowerExpressionForTwoVariablesReturnsVariableToThePowerOfVariable()
        {
            var var = new MathematicalExpressionEvaluation.Expressions.Variable();
            var power = new MathematicalExpressionEvaluation.Expressions.PowerExpression(var, var);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(-2);
            Assert.AreEqual(0.25, power.Interpret(context), 1e-10);
        }
    }
}
