﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class MultiplyExpression
    {
        [TestMethod]
        public void InterpretMethodForMultiplicationOfConstant5AndConstantMinus2EvaluatesToMinus10()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Constant(5);
            var right = new MathematicalExpressionEvaluator.Expressions.Constant(-2);

            var context = new MathematicalExpressionEvaluator.Expressions.Context(24);
            Assert.AreEqual(-10, new MathematicalExpressionEvaluator.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMultiplicationOfConstant3WithVariableEvaluatesTo15ForContext5()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Constant(3);
            var right = new MathematicalExpressionEvaluator.Expressions.Variable();

            var context = new MathematicalExpressionEvaluator.Expressions.Context(5);
            Assert.AreEqual(15, new MathematicalExpressionEvaluator.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMultiplicationOfVariableWithItselfEvaluatesToVariableSquared()
        {
            var left = new MathematicalExpressionEvaluator.Expressions.Variable();
            var right = new MathematicalExpressionEvaluator.Expressions.Variable();

            var context = new MathematicalExpressionEvaluator.Expressions.Context(5);
            Assert.AreEqual(25, new MathematicalExpressionEvaluator.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);

            context = new MathematicalExpressionEvaluator.Expressions.Context(8);
            Assert.AreEqual(64, new MathematicalExpressionEvaluator.Expressions.MultiplyExpression(left, right).Interpret(context), 1e-10);
        }
    }
}
