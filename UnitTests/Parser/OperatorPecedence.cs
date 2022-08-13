using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class OperatorPecedence
    {
        [TestMethod]
        public void ParseMethodEvaluatesResultOfExpressionWithConstantsThatHasLowerLevelOperandsAtTheEnd()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(2, parser.Parse("18 / 3 - 4").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(10, parser.Parse("4 * 3 - 2").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-15, parser.Parse("2 - 3 * 4 - 5").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-40, parser.Parse("2 - 3 * 4 - 5 * 6").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-60, parser.Parse("2 - 3 * 4 * 5 - 2 ").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);

            Assert.AreEqual(4, parser.Parse("2 ^ 3 - 4").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(7, parser.Parse("2 ^ 3 - 4 + 3").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(2 / 6.0, parser.Parse("2 ^ 3 / 4 / 6").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(4, parser.Parse("2 ^ 3 / 4 / 6 * 12").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(2, parser.Parse("2 ^ 3 ^ 4 / 1024 * 5 / 10").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);

            Assert.AreEqual(-8, parser.Parse("2 ^ 3 / 4 - 10").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-3, parser.Parse("2 ^ 3 / 4 - 10 + 5").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-4, parser.Parse("2 ^ 3 / 4 * 3 - 10").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(3, parser.Parse("2 ^ 3 ^ 4 / 1024 * 5 / 10 - 8 + 9").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesResultOfExpressionWithConstantsThatHasHigherLevelOperandsAtTheEnd()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(-10, parser.Parse("2 - 3 * 4").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-40, parser.Parse("2 - 3 * 4 - 5 * 6").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-58, parser.Parse("2 - 3 * 4 * 5").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);

            Assert.AreEqual(-4, parser.Parse("4 - 2 ^ 3").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-96, parser.Parse("4000 - 2 ^ 3 ^ 4").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0.5, parser.Parse("4 / 2 ^ 3").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(10 / 8.0, parser.Parse("8 / 4 * 5 / 2 ^ 3").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(10 / 64.0, parser.Parse("8 / 4 * 5 / 2 ^ 3 ^ 2").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodEvaluatesResultOfExpressionWithConstantsThatHasHigherLevelOperandsInTheMiddle()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(-15, parser.Parse("2 - 3 * 4 - 5").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-5, parser.Parse("2 - 3 * 4 / 6 - 5").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-11, parser.Parse("2 - 6 / 3 * 4 - 5").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-3, parser.Parse("2 - 6 / 3 * 4 - 5 + 8").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1, parser.Parse("8 - 2 - 6 / 3 * 4 - 5 + 8").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);

            Assert.AreEqual(-5, parser.Parse("8 - 2 ^ 3 - 5").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-6, parser.Parse("4000 - 2 ^ 3 ^ 4 + 90").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0.1, parser.Parse("4 / 2 ^ 3 / 5").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1, parser.Parse("4 / 2 ^ 3 / 5 * 10").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }
    }
}