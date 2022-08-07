using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JSribar.AlgebraicExpressionParser.UnitTests.Parser
{
    [TestClass]
    public class MathematicalConstant
    {
        [TestMethod]
        public void ParserReturnsValueOfPi()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(Math.PI, parser.Parse("PI").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.PI, parser.Parse(" PI ").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParserEvaluatesExpressionWithPi()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(-1, parser.Parse("cos(PI)").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsValueOfE()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(Math.E, parser.Parse("E").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.E, parser.Parse(" E ").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParserEvaluatesExpressionWithE()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(Math.Exp(2), parser.Parse("E ^ 2").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForUndefinedMathematicalConstant()
        {
            try
            {
                var parser = new AlgebraicExpressionParser.Parser();
                parser.Parse(" sin(F) ");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(AlgebraicExpressionParser.Parser.UnknownIdentifier, e.Message);
            }

        }
    }
}
