using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class MultipleVariables
    {
        [TestMethod]
        public void ParseMethodReturnsExpressionForMultipleVariables()
        {
            var parser = new MathematicalExpressionEvaluator.Parser(new string[] { "x", "y" });
            Assert.AreEqual(5, parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(new Dictionary<string, double> { { "x", 2 }, { "y", 3 } })));
            Assert.AreEqual(6, parser.Parse("x * y").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(new Dictionary<string, double> { { "x", 2 }, { "y", 3 } })));
        }

        [TestMethod]
        public void ParseMethodThrowsIdentifierExceptionIfContextDoesNotContainAllVariableValues()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser(new string[] { "x", "y" });
                parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluator.Expressions.Context(2));
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.IdentifierException e)
            {
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.ValueOfVariableNotProvided, e.Message);
                Assert.AreEqual("y", e.Identifier);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser(new string[] { "x", "y" });
                parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluator.Expressions.Context((new Dictionary<string, double> { { "x", 2 } })));
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.IdentifierException e)
            {
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.ValueOfVariableNotProvided, e.Message);
                Assert.AreEqual("y", e.Identifier);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser(new string[] { "x", "y" });
                parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluator.Expressions.Context((new Dictionary<string, double> { { "x", 2 }, { "t", 3 } })));
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.IdentifierException e)
            {
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.ValueOfVariableNotProvided, e.Message);
                Assert.AreEqual("y", e.Identifier);
            }
        }
    }
}
