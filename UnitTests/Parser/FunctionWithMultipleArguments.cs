using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class FunctionWithMultipleArguments
    {
        [TestMethod]
        public void ParserReturnsExpressionForPowFunctionWithTwoConstantArguments()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(8, parser.Parse("pow(2, 3)").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForPowFunctionWithTwoVariableArguments()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(27, parser.Parse("pow(x, x)").Evaluate(3), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForPowFunctionWithTwoExpressionArguments()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(16, parser.Parse("pow(x + 1, x - 1)").Evaluate(3), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForPowFunctionWithTwoExpressionArgumentsWithParentheses()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(65536, parser.Parse("pow(2 * (x - 1), (x - 1) * (x + 1))").Evaluate(3), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForCompositionOfPowFunctions()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(16, parser.Parse("pow(pow(x, 1), pow(x, x))").Evaluate(2), 1e-10);
            Assert.AreEqual(64, parser.Parse("pow(pow(x, 4 - x), pow(x + 1, x - 1))").Evaluate(2), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForAtan2FunctionWithTwoConstantArguments()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(Math.Atan2(1, -1), parser.Parse("atan2(1, -1)").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParserThrowsExceptionIfCommaIsNotFollowedByOperand()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("atan2(1,)");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(8, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedRigthParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParserThrowsExceptionIfFunctionHasMoreArgumentsThenRequired()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("sin(1,2)");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.FunctionHasToManyArguments, e.Message);
            }

            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("pow(1,2,3)");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(7, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.FunctionHasToManyArguments, e.Message);
            }

            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("pow(pow(1,2,3),3)");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(11, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.FunctionHasToManyArguments, e.Message);
            }

            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("pow(pow(1,2),sin(3),cos(5))");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(19, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.FunctionHasToManyArguments, e.Message);
            }
        }

        [TestMethod]
        public void ParserThrowsExceptionIfFunctionHasLessArgumentsThenRequired()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("sin()");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(4, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedRigthParenthesis, e.Message);
            }

            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("pow(1)");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.FunctionHasToFewArguments, e.Message);
            }

            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("pow(1,)");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(6, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedRigthParenthesis, e.Message);
            }

            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("pow(pow(1,2),)");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(13, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedRigthParenthesis, e.Message);
            }

            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("pow(pow(1,),2)");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(10, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedRigthParenthesis, e.Message);
            }

            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("pow(pow(1,2),pow(2,))");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(19, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedRigthParenthesis, e.Message);
            }
        }
    }
}
