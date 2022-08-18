using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class Comma
    {
        [TestMethod]
        public void ParseMethodThrowsExceptionForCommaOnExpressionStart()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse(",");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(0, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedComma, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForCommaAsDecimalSeparator()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("1,3");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(1, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedComma, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForCommaBeforeOperator()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("1 + ,3");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(4, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedComma, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForCommaImmediatelyAfterFunctionLeftParenthesis()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("sin(,)");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(4, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedComma, e.Message);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("pow(,)");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(4, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedComma, e.Message);
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
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForCommaAfterFunctionLastArgument()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("sin(3,)");
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
                parser.Parse("sin(3,");
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
                parser.Parse("pow(1,3,)");
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
                parser.Parse("pow(1,3,");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(7, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.FunctionHasToManyArguments, e.Message);
            }
        }
    }
}
