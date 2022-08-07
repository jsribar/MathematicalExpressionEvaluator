using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JSribar.AlgebraicExpressionParser.UnitTests.Expressions
{
    [TestClass]
    public class MultiplyExpression
    {
        [TestMethod]
        public void InterpretMethodForMultiplicationOfConstant5AndConstantMinus2EvaluatesToMinus10()
        {
            var left = new AlgebraicExpressionParser.Expressions.Constant(5);
            var right = new AlgebraicExpressionParser.Expressions.Constant(-2);

            var context = new AlgebraicExpressionParser.Expressions.Context(24);
            Assert.AreEqual(-10, new AlgebraicExpressionParser.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMultiplicationOfConstant3WithVariableEvaluatesTo15ForContext5()
        {
            var left = new AlgebraicExpressionParser.Expressions.Constant(3);
            var right = new AlgebraicExpressionParser.Expressions.Variable();

            var context = new AlgebraicExpressionParser.Expressions.Context(5);
            Assert.AreEqual(15, new AlgebraicExpressionParser.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMultiplicationOfVariableWithItselfEvaluatesToVariableSquared()
        {
            var left = new AlgebraicExpressionParser.Expressions.Variable();
            var right = new AlgebraicExpressionParser.Expressions.Variable();

            var context = new AlgebraicExpressionParser.Expressions.Context(5);
            Assert.AreEqual(25, new AlgebraicExpressionParser.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);

            context = new AlgebraicExpressionParser.Expressions.Context(8);
            Assert.AreEqual(64, new AlgebraicExpressionParser.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);
        }
    }
}