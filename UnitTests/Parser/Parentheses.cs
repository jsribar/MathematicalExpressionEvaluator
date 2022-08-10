using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Parser
{
    [TestClass]
    public class Parentheses
    {
        [TestMethod]
        public void ParseMethodReturnsValidExpressionForSingleParentheses()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(2, parser.Parse("(2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0.3, parser.Parse("(10.3 - x * 2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsValidExpressionForMultipleParentheses()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(2, parser.Parse("((2))").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(2, parser.Parse("(((2)))").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0.3, parser.Parse("((10.3 - x * 2))").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0.3, parser.Parse("(((10.3 - x * 2)))").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(5.3, parser.Parse("(((10.3 - x * 2))) + (x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(4.7, parser.Parse("((x) - (((10.3 - x * 2))))").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(4.7, parser.Parse("(((x) - (((10.3 - x * 2)))))").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForAnExpressionConsistingOfOperationsOnConstantsWithMultipleParentheses()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(-15, parser.Parse("(2 + 3) * (4 - 7)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)));
            Assert.AreEqual(20, parser.Parse("5 - (2 + 3) * (4 - 7)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)));
            Assert.AreEqual(-22, parser.Parse("5 - (2 + 3) * 6 - (4 - 7)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)));
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForAnExpressionWithParentheses()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(8 / -5.0, parser.Parse("(x + 3) / (x - 10)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)));
            Assert.AreEqual(24 / -5.0, parser.Parse("3 * (x + 3) / (x - 10)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(25 / -10.0, parser.Parse("(3 * (x + 3) + 1) / (2 * (x - 10))").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)));
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForExpressionStartingWithRightParenthesis()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse(")");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(0, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedRigthParenthesis, e.Message);
            }

        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForExpressionWithMissingRightParenthesis()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("((3)");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(4, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.MissingRightParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForExpressionWithExcesiveRightParenthesis()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("(3))");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(3, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.MissingLeftParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForRightParenthesisAfterConstant()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("1.23 )");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.MissingLeftParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForRightParenthesisPlacedAfterOperator()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("((3 +) 2)");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedRigthParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForLeftParenthesisPlacedBeforeOperator()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("(3 (+ 2))");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(3, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.InvalidOperator, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForEmptyParentheses()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse(" () ");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(2, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedRigthParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForEmptyParenthesesAfterOperator()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse("3 + ()");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedRigthParenthesis, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForEmptyParenthesesBetweenOperators()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse(" 3 + () - 2 ");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(6, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnexpectedRigthParenthesis, e.Message);
            }

        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForEmptyParenthesesInsideBinaryOperation()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse(" 3 () - 2 ");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(3, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.InvalidOperator, e.Message);
            }
        }
    }
}
