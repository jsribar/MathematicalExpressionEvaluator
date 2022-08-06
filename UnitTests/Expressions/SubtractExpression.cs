﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JSribar.AlgebraicExpressionParser.UnitTests.Expressions
{
    [TestClass]
    public class SubtractExpression
    {
        [TestMethod]
        public void InterpretMethodForTwoConstantsReturnsTheirDifference()
        {
            var left = new AlgebraicExpressionParser.Expressions.Constant(16);
            var right = new AlgebraicExpressionParser.Expressions.Constant(4);
            var difference = new AlgebraicExpressionParser.Expressions.SubtractExpression(left, right);

            var context = new AlgebraicExpressionParser.Expressions.Context(5);
            Assert.AreEqual(12, difference.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForConstantsAndVariableReturnsTheirDifference()
        {
            var left = new AlgebraicExpressionParser.Expressions.Variable();
            var right = new AlgebraicExpressionParser.Expressions.Constant(5);
            var difference = new AlgebraicExpressionParser.Expressions.SubtractExpression(left, right);

            var context = new AlgebraicExpressionParser.Expressions.Context(3);
            Assert.AreEqual(-2, difference.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForTwoVariablesReturnsTheirDifference()
        {
            var left = new AlgebraicExpressionParser.Expressions.Variable();
            var right = new AlgebraicExpressionParser.Expressions.Variable();
            var difference = new AlgebraicExpressionParser.Expressions.SubtractExpression(left, right);

            var context = new AlgebraicExpressionParser.Expressions.Context(4);
            Assert.AreEqual(0, difference.Interpret(context), 1e-10);
        }
    }
}