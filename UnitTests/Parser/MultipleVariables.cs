using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluator;
using VariableValueNotDefinedException = JSribar.MathematicalExpressionEvaluator.VariableValueNotDefinedException;

namespace Parser
{
    [TestClass]
    public class MultipleVariables
    {
        [TestMethod]
        public void ParseMethodReturnsExpressionForMultipleVariables()
        {
            var parser = new MathematicalExpressionEvaluation.Parser(new string[] { "x", "y" });
            Assert.AreEqual(5, parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(new Dictionary<string, double> { { "x", 2 }, { "y", 3 } })));
            Assert.AreEqual(6, parser.Parse("x * y").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(new Dictionary<string, double> { { "x", 2 }, { "y", 3 } })));
        }

        [TestMethod]
        public void ParseMethodThrowsExpressionEvaluationExceptionIfContextDoesNotContainAllVariableValues()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser(new string[] { "x", "y" });
                parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(2));
                Assert.Fail();
            }
            catch (VariableValueNotDefinedException e)
            {
                Assert.AreEqual("Value of a variable not provided.", e.Message);
                Assert.AreEqual("y", e.VariableName);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser(new string[] { "x", "y" });
                parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluation.Expressions.Context((new Dictionary<string, double> { { "x", 2 } })));
                Assert.Fail();
            }
            catch (VariableValueNotDefinedException e)
            {
                Assert.AreEqual("Value of a variable not provided.", e.Message);
                Assert.AreEqual("y", e.VariableName);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser(new string[] { "x", "y" });
                parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluation.Expressions.Context((new Dictionary<string, double> { { "x", 2 }, { "t", 3 } })));
                Assert.Fail();
            }
            catch (VariableValueNotDefinedException e)
            {
                Assert.AreEqual("Value of a variable not provided.", e.Message);
                Assert.AreEqual("y", e.VariableName);
            }
        }
    }
}