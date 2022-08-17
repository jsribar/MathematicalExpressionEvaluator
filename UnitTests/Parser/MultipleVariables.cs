using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluator;
using EvaluationException = JSribar.MathematicalExpressionEvaluator.Expressions.EvaluationException;
using Messages = JSribar.MathematicalExpressionEvaluator.Expressions.Messages;

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
        public void ParseMethodThrowsEvaluationExceptionIfContextDoesNotContainAllVariableValues()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser(new string[] { "x", "y" });
                parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(2));
                Assert.Fail();
            }
            catch (EvaluationException e)
            {
                Assert.AreEqual(Messages.ValueOfVariableNotProvided, e.Message);
                Assert.AreEqual("y", e.VariableName);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser(new string[] { "x", "y" });
                parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluation.Expressions.Context((new Dictionary<string, double> { { "x", 2 } })));
                Assert.Fail();
            }
            catch (EvaluationException e)
            {
                Assert.AreEqual(Messages.ValueOfVariableNotProvided, e.Message);
                Assert.AreEqual("y", e.VariableName);
            }
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser(new string[] { "x", "y" });
                parser.Parse("x + y").Interpret(new MathematicalExpressionEvaluation.Expressions.Context((new Dictionary<string, double> { { "x", 2 }, { "t", 3 } })));
                Assert.Fail();
            }
            catch (EvaluationException e)
            {
                Assert.AreEqual(Messages.ValueOfVariableNotProvided, e.Message);
                Assert.AreEqual("y", e.VariableName);
            }
        }
    }
}
