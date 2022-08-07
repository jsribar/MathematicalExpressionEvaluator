using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JSribar.AlgebraicExpressionParser.UnitTests.Parser
{
    [TestClass]
    public class MultipleVariables
    {
        [TestMethod]
        public void ParseMethodReturnsExpressionForMultipleVariables()
        {
            var parser = new AlgebraicExpressionParser.Parser(new string[] { "x", "y" });
            Assert.AreEqual(5, parser.Parse("x + y").Interpret(new AlgebraicExpressionParser.Expressions.Context(new Dictionary<string, double> { { "x", 2 }, { "y", 3 } })));
            Assert.AreEqual(6, parser.Parse("x * y").Interpret(new AlgebraicExpressionParser.Expressions.Context(new Dictionary<string, double> { { "x", 2 }, { "y", 3 } })));
        }
    }
}