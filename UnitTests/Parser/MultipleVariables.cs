using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
    }
}