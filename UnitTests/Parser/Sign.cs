﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JSribar.AlgebraicExpressionParser.UnitTests.Parser
{
    [TestClass]
    public class Sign
    {
        [TestMethod]
        public void ParseMethodEvaluatesToNegativeValueIfConstantIsPrecededWithMinusSign()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(-10.3, parser.Parse("-10.3").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesToValueIfConstantIsPrecededWithPlusSign()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(21.32, parser.Parse("+21.32").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesToNegativeValueIfVariableIsPrecededWithMinusSign()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(-5, parser.Parse("-x").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesToValueIfVariableIsPrecededWithPlusSign()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(5, parser.Parse("+x").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesFunctionPrecededWithMinusSign()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(-5, parser.Parse("-sqrt(x)").Interpret(new AlgebraicExpressionParser.Expressions.Context(25)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesExpressionInParenthesesPrecededWithMinusSign()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(1, parser.Parse("-(3 - x)").Interpret(new AlgebraicExpressionParser.Expressions.Context(4)), 1e-10);
            Assert.AreEqual(-1, parser.Parse("+(x - 3)").Interpret(new AlgebraicExpressionParser.Expressions.Context(2)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesExpressionConsistingOfMultipleEntriesWithSign()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(-1, parser.Parse("-3 - -x - +2").Interpret(new AlgebraicExpressionParser.Expressions.Context(4)), 1e-10);
            Assert.AreEqual(-1, parser.Parse("-3--x-+2").Interpret(new AlgebraicExpressionParser.Expressions.Context(4)), 1e-10);
            Assert.AreEqual(-21, parser.Parse("-15 - -3 * -x").Interpret(new AlgebraicExpressionParser.Expressions.Context(2)), 1e-10);
            Assert.AreEqual(-21, parser.Parse("-15--3*-x").Interpret(new AlgebraicExpressionParser.Expressions.Context(2)), 1e-10);
            Assert.AreEqual(-7, parser.Parse("-(-x--3)*-(-4 + x)-5").Interpret(new AlgebraicExpressionParser.Expressions.Context(2)), 1e-10);
            Assert.AreEqual(7, parser.Parse("-(-(-x--3)*-(-4 + x)-5)").Interpret(new AlgebraicExpressionParser.Expressions.Context(2)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesEvaluatesFunctionPrecededWithPlusSign()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(-3, parser.Parse("-sqrt(x)").Interpret(new AlgebraicExpressionParser.Expressions.Context(9)), 1e-10);
            Assert.AreEqual(-3, parser.Parse("-sqrt(-x)").Interpret(new AlgebraicExpressionParser.Expressions.Context(-9)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForMultipleMinusSigns()
        {
            try
            {
                var parser = new AlgebraicExpressionParser.Parser();
                parser.Parse("--723");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(1, e.Position);
                Assert.AreEqual(AlgebraicExpressionParser.Parser.UnexpectedSign, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForMultiplePlusSigns()
        {
            try
            {
                var parser = new AlgebraicExpressionParser.Parser();
                parser.Parse("++723");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(1, e.Position);
                Assert.AreEqual(AlgebraicExpressionParser.Parser.UnexpectedSign, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForPlusAndMinusSignsPecedingConstant()
        {
            try
            {
                var parser = new AlgebraicExpressionParser.Parser();
                parser.Parse("+-723");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(1, e.Position);
                Assert.AreEqual(AlgebraicExpressionParser.Parser.UnexpectedSign, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForMinusAndPlusSignsPecedingConstant()
        {
            try
            {
                var parser = new AlgebraicExpressionParser.Parser();
                parser.Parse("-+723");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(1, e.Position);
                Assert.AreEqual(AlgebraicExpressionParser.Parser.UnexpectedSign, e.Message);
            }
        }
    }
}