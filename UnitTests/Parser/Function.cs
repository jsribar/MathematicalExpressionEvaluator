﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluation;
using ParserException = JSribar.MathematicalExpressionEvaluation.ParserException;

namespace Parser
{
    [TestClass]
    public class Function
    {
        [TestMethod]
        public void ParserReturnsExpressionForSinFunctionWithConstantArgument()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(1, parser.Parse("sin(1.5707963267948966192313216916398)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForSinFunctionWithVariableArgument()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(1, parser.Parse("sin(x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(1.5707963267948966192313216916398)), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForSinFunctionWithExpressionAsArgument()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(1, parser.Parse("sin(x + 1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(0.5707963267948966192313216916398)), 1e-10);
            Assert.AreEqual(3, parser.Parse("sqrt(11 - x * 2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(1)), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForExpressionWithMultipleFunctions()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(-4, parser.Parse("sqrt(4) - sqrt(9) * 2").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(2)), 1e-10);
            Assert.AreEqual(Math.Sqrt(2), parser.Parse("3 * sqrt(x) - sqrt(x) * 2").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(2)), 1e-10);
            Assert.AreEqual(2, parser.Parse("sin(x / 2) + cos(2 * x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3.1415926535897932384626433832795)), 1e-10);
            Assert.AreEqual(2, parser.Parse("sin((2 * x) / (2 + 2)) - cos(x - 2 * x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3.1415926535897932384626433832795)), 1e-10);
            Assert.AreEqual(-1, parser.Parse("sin((2 * x) / (2 + 2)) * cos(x - 2 * x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3.1415926535897932384626433832795)), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForACompositionOfFunctions()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(Math.Sqrt(2), parser.Parse("sqrt(sin(x / 2) + cos(2 * x))").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3.1415926535897932384626433832795)), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForFunctionsWithPrecedingSign()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(-Math.Sqrt(2), parser.Parse("-sqrt(2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3.1415926535897932384626433832795)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForAFunctionWithEmptyParentheses()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse(" sin() ");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedRigthParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForASignBetweenFunctionNameAndParentheses()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse(" sin-(2) ");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(4, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.FunctionNotFollowedByLeftParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForMissingRightParenthesesAfterFunction()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse(" sin(2 ");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(7, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.MissingRightParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForOnlyLeftParenthesisAfterFunction()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse(" sin( ");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(6, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.ExpressionTerminatedUnexpectedly, e.Message);
            }
        }
    }
}