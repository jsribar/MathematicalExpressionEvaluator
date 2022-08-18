using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class Constant
    {
        [TestMethod]
        public void ParseMethodReturnsExpressionForAnExpressionConsistingOfThatValueOnly()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(2, parser.Parse("2").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(2, parser.Parse("2.").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(2, parser.Parse("2.0").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0.2, parser.Parse(".2").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(10.3, parser.Parse("10.3").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForAnExpressionConsistingOfThatValueOnlyPrecededBySpace()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(5, parser.Parse(" 5").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForAnExpressionConsistingOfThatValueOnlyFollowedBySpace()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(7, parser.Parse("7 ").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForANumberInScientificFormat()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(530, parser.Parse("5.3e2").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(530, parser.Parse("5.3e+2").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(530, parser.Parse("5.3E2").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0.3, parser.Parse("3e-1").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0.3, parser.Parse("3.e-1").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0.3, parser.Parse("3.0e-1").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0.3, parser.Parse("3E-1").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0, parser.Parse("0.E0").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(3.14159, parser.Parse("314159E-5").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForAnExpressionConsistingOfThatValueOnlyEnclosedInParentheses()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(7, parser.Parse("(7)").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(7.3, parser.Parse("( 7.3 )").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(7.5, parser.Parse(" ( 7.5 ) ").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionIfMinusSignIsNotImmediatellyBeforeConstant()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("- 723");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(1, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedSpace, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionIfPlusSignIsNotImmediatellyBeforeConstant()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("+ 723");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(1, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnexpectedSpace, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForMultipleDecimalSeparators()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("7..23");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(2, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.DuplicateDecimalSeparator, e.Message);
            }
        }

        [TestMethod]
        public void ParseThrowsExceptionForConstantFollowedByCharacter()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("7.23a");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(4, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.InvalidOperator, e.Message);
            }
        }

        [TestMethod]
        public void ParseThrowsExceptionForIncomleteConstantInScientificFormat()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("7.23e");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(0, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.InvalidNumberFormat, e.Message);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("7.23e+");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(0, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.InvalidNumberFormat, e.Message);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("7.23e-");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(0, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.InvalidNumberFormat, e.Message);
            }
        }

        [TestMethod]
        public void ParseMethodEvaluatesToSumOfTwoConstants()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(5, parser.Parse("2+3").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(5, parser.Parse("2 +3").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(5, parser.Parse("2+ 3").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesToDifferenceOfTwoConstants()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(-1, parser.Parse("2-3").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1, parser.Parse("3 - 2").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesToProductOfTwoConstants()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(6, parser.Parse("2*3").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(6, parser.Parse("3 * 2").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesToQuotientOfTwoConstants()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(2 / 3.0, parser.Parse("2 / 3").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(2.5, parser.Parse("5 / 2").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesResultOfSeveralOperationsOfSamePrecedenceOnConstants()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(18, parser.Parse("2 + 3 + 13").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-7, parser.Parse("2 + 3 - 12").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(6 / 4.0, parser.Parse("2 * 3 / 4").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForAnExpressionConsistingOfOperationsOnConstantsEnclosedInParentheses()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(10, parser.Parse("(7 + 3)").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForIncompleteExpression()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse("5 +");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(3, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.ExpressionTerminatedUnexpectedly, e.Message);
            }
        }
    }
}
