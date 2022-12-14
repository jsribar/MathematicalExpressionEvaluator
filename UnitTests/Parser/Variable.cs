using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class Variable
    {
        [TestMethod]
        public void ParseMethodReturnsExpressionForASimpleVariable()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(5, parser.Parse("x").Evaluate(5), 1e-10);
            Assert.AreEqual(-3, parser.Parse("-x").Evaluate(3), 1e-10);
            Assert.AreEqual(-3, parser.Parse("x").Evaluate(-3), 1e-10);

            Assert.AreEqual(-3, parser.Parse(" -x").Evaluate(3), 1e-10);
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionIfVariableNamehasAdditionalCharacters()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("xx");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(0, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnknownIdentifier, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionIfMinusSignIsNotImmediatellyBeforeVariable()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("- x");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(1, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedSpace, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionIfPlusSignIsNotImmediatellyBeforeVariable()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("+ x");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(1, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedSpace, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForAdditionOfVariableAndConstant()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(8, parser.Parse("x+3").Evaluate(5), 1e-10);
            Assert.AreEqual(6, parser.Parse("3+x").Evaluate(3), 1e-10);
            Assert.AreEqual(7, parser.Parse("-x+4").Evaluate(-3), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForSubtractionOfVariableAndConstant()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(2, parser.Parse("x-3").Evaluate(5), 1e-10);
            Assert.AreEqual(0, parser.Parse("3-x").Evaluate(3), 1e-10);
            Assert.AreEqual(1, parser.Parse("4--x").Evaluate(-3), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForMultiplicationOfVariableAndConstant()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(15, parser.Parse("x*3").Evaluate(5), 1e-10);
            Assert.AreEqual(9, parser.Parse("3*x").Evaluate(3), 1e-10);
            Assert.AreEqual(-9, parser.Parse("-3*x").Evaluate(3), 1e-10);
            Assert.AreEqual(12, parser.Parse("4*-x").Evaluate(-3), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForDivisionOfVariableAndConstant()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(5 / 3.0, parser.Parse("x / 3").Evaluate(5), 1e-10);
            Assert.AreEqual(1, parser.Parse("3 / x").Evaluate(3), 1e-10);
            Assert.AreEqual(-1, parser.Parse("-3 / x").Evaluate(3), 1e-10);
            Assert.AreEqual(4 / 3.0, parser.Parse("4 / -x").Evaluate(-3), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForAdditionOfMultipleVariablesAndConstants()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(10, parser.Parse("x + x").Evaluate(5), 1e-10);
            Assert.AreEqual(13, parser.Parse("x + 3 + x ").Evaluate(5), 1e-10);
            Assert.AreEqual(11, parser.Parse("3 + x + x").Evaluate(4), 1e-10);
            Assert.AreEqual(10, parser.Parse("-x + -x + 4").Evaluate(-3), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForSubtractionOfMultipleVariablesAndConstants()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(3, parser.Parse("x - 3 - x + 6").Evaluate(5), 1e-10);
            Assert.AreEqual(-10, parser.Parse("-x - 5 - x - 2 + x").Evaluate(3), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForMultiplicationOfMultipleVariablesAndConstants()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(25, parser.Parse("x * x").Evaluate(5), 1e-10);
            Assert.AreEqual(-27, parser.Parse("x * x * x").Evaluate(-3), 1e-10);
            Assert.AreEqual(3, parser.Parse("3 * x - 2 * x").Evaluate(3), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForDivisionOfMultipleVariablesAndConstants()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(1, parser.Parse("x / x").Evaluate(5), 1e-10);
            Assert.AreEqual(3, parser.Parse("3 * x / x").Evaluate(3), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForExpressionWithDifferentOperationsOnVariablesAndConstants()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(-70, parser.Parse("x - x * 3 * x").Evaluate(5), 1e-10);
        }
    }
}