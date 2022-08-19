using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class MathematicalOperatorCharacters
    {
        [TestMethod]
        public void ParseMethodReturnsExpressionForConstantPrecededByMinusSign()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(-3, parser.Parse("−3").Evaluate(5), 1e-10);
            Assert.AreEqual(-6, parser.Parse("\u22126").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForSubtractionUsingMinusSign()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(-1, parser.Parse("2 − 3").Evaluate(5), 1e-10);
            Assert.AreEqual(1, parser.Parse("3 \u2212 2").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForMultiplicationUsinghMultiplicationSign()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(6, parser.Parse("2 × 3").Evaluate(5), 1e-10);
            Assert.AreEqual(6, parser.Parse("3 \u00D7 2").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForMultiplicationUsingDotOperator()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(6, parser.Parse("2 ⋅ 3").Evaluate(5), 1e-10);
            Assert.AreEqual(6, parser.Parse("3 \u22C5 2").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParseMethodReturnsExpressionForDivisionUsingDivisionSign()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(2.0 / 3, parser.Parse("2 ÷ 3").Evaluate(5), 1e-10);
            Assert.AreEqual(3.0 / 2, parser.Parse("3 \u00F7 2").Evaluate(5), 1e-10);
        }
    }
}
