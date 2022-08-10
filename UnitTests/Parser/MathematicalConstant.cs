using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Parser
{
    [TestClass]
    public class MathematicalConstant
    {
        [TestMethod]
        public void ParserReturnsValueOfPi()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(Math.PI, parser.Parse("PI").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.PI, parser.Parse(" PI ").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParserEvaluatesExpressionWithPi()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(-1, parser.Parse("cos(PI)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsValueOfE()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(Math.E, parser.Parse("E").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.E, parser.Parse(" E ").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParserEvaluatesExpressionWithE()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(Math.Exp(2), parser.Parse("E ^ 2").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForUndefinedMathematicalConstant()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluation.Parser();
                parser.Parse(" sin(F) ");
                Assert.Fail();
            }
            catch (ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluation.Parser.UnknownIdentifier, e.Message);
            }

        }
    }
}
