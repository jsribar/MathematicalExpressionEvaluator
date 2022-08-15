using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluator;
using ParserException = JSribar.MathematicalExpressionEvaluator.ParserException;


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
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse(",");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(0, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedComma, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForCommaAsDecimalSeparator()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("1,3");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(1, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedComma, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForCommaBeforeOperator()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("1 + ,3");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(4, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedComma, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForCommaImmediatelyAfterFunctionLeftParenthesis()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("sin(,)");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(4, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedComma, e.Message);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("pow(,)");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(4, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedComma, e.Message);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("pow(1,)");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(6, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedRigthParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForCommaAfterFunctionLastArgument()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("sin(3,)");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.FunctionHasToManyArguments, e.Message);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("sin(3,");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.FunctionHasToManyArguments, e.Message);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("pow(1,3,)");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(7, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.FunctionHasToManyArguments, e.Message);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("pow(1,3,");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(7, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.FunctionHasToManyArguments, e.Message);
            }
        }
    }
}
