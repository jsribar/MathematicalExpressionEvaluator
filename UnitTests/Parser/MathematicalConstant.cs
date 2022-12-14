using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class MathematicalConstant
    {
        [TestMethod]
        public void ParserReturnsValueOfPi()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(Math.PI, parser.Parse("PI").Evaluate(5), 1e-10);
            Assert.AreEqual(Math.PI, parser.Parse(" PI ").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParserEvaluatesExpressionWithPi()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(-1, parser.Parse("cos(PI)").Evaluate(5), 1e-10);

            Assert.AreEqual(-1, parser.Parse("cos(π)").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsValueOfE()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(Math.E, parser.Parse("E").Evaluate(5), 1e-10);
            Assert.AreEqual(Math.E, parser.Parse(" E ").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParserEvaluatesExpressionWithE()
        {
            var parser = new MathematicalExpressionEvaluator.Parser();
            Assert.AreEqual(Math.Exp(2), parser.Parse("E ^ 2").Evaluate(5), 1e-10);
        }

        [TestMethod]
        public void ParseMethodThrowsExceptionForUndefinedMathematicalConstant()
        {
            try
            {
                var parser = new MathematicalExpressionEvaluator.Parser();
                parser.Parse(" sin(F) ");
                Assert.Fail();
            }
            catch (MathematicalExpressionEvaluator.ParserException e)
            {
                Assert.AreEqual(5, e.Position);
                Assert.AreEqual(MathematicalExpressionEvaluator.Messages.UnknownIdentifier, e.Message);
            }

        }
    }
}
