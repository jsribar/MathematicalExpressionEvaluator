using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class CustomConstants
    {
        [TestMethod]
        public void AddConstantIncludesAdditionalConstant()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            parser.AddConstant("two", 2);
            parser.AddConstant("five", 5);

            Assert.AreEqual(Math.Sqrt(17), parser.Parse("sqrt(two + five * x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3)), 1e-10);
        }
    }
}
