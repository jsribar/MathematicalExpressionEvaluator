using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class CustomConstants
    {
        [TestMethod]
        public void AddConstantIncludesAdditionalConstant()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            parser.AddConstant("two", 2);
            parser.AddConstant("five", 5);

            Assert.AreEqual(Math.Sqrt(17), parser.Parse("sqrt(two + five * x)").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(3)), 1e-10);
        }

        [TestMethod]
        public void AddConstantThrowsIdentifierExceptionForAnExistingFunctionName()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.AddConstant("PI", 2);
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.IdentifierException e)
            {
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.IdentifierAlreadyUsed, e.Message);
                Assert.AreEqual("PI", e.Identifier);
            }
        }
    }
}
