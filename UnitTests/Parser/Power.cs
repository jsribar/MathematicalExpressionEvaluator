using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class Power
    {
        [TestMethod]
        public void ParserReturnsExpressionForPowerWithConstantBaseAndConstantExponent()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(125, parser.Parse("5.0 ^ 3.0").Evaluate(5), 1e-10);
            Assert.AreEqual(0.125, parser.Parse("0.5 ^ 3").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForPowerWithVariableBaseAndVariableExponent()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(27, parser.Parse("x ^ x").Evaluate(3), 1e-10);
            Assert.AreEqual(Math.Sqrt(Math.Sqrt(0.25)), parser.Parse("x ^ x").Evaluate(0.25), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForPowerWithBaseAsAComplexExpression()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(125, parser.Parse("(2 + 3) ^ x").Evaluate(3), 1e-10);
            Assert.AreEqual(Math.Sqrt(4), parser.Parse("(x + 3.5) ^ x").Evaluate(0.5), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForPowerWithExponentAsAComplexExpression()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(32, parser.Parse("2 ^ (2 + 3)").Evaluate(3), 1e-10);
            Assert.AreEqual(Math.Pow(3, 1.5), parser.Parse("3 ^ (2 - x)").Evaluate(0.5), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForAComplexOperationsContainingPower()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(-16, parser.Parse("2 - 2 ^ x - 10").Evaluate(3), 1e-10);
            Assert.AreEqual(121.5, parser.Parse("3 * x ^ 4 / 2").Evaluate(3), 1e-10);
            Assert.AreEqual(4, parser.Parse("2 ^ 0.5 ^ 4").Evaluate(3), 1e-10); // 3 * 2 ^ -5 ^ 2 * 3 
            Assert.AreEqual(240, parser.Parse("3 * (x - 1) ^ (8 - x * 2) ^ 2 * 5").Evaluate(3), 1e-10); // 3 * 2 ^ 2 ^ 2 * 3 
            Assert.AreEqual(0.015625, parser.Parse("2 ^ (x - 5) ^ 3").Evaluate(3), 1e-10); // 2 ^ -2 ^ 3
        }
    }
}