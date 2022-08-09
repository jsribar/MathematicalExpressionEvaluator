using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSribar.AlgebraicExpressionParser.UnitTests.Parser
{
    [TestClass]
    public class MultiFunction
    {
        [TestMethod]
        public void ParserReturnsExpressionForPowFunctionWithTwoConstantArguments()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(8, parser.Parse("pow(2, 3)").Interpret(new AlgebraicExpressionParser.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForPowFunctionWithTwoVariableArguments()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(27, parser.Parse("pow(x, x)").Interpret(new AlgebraicExpressionParser.Expressions.Context(3)), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForPowFunctionWithTwoExpressionArguments()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(16, parser.Parse("pow(x + 1, x - 1)").Interpret(new AlgebraicExpressionParser.Expressions.Context(3)), 1e-10);
        }

        [TestMethod]
        public void ParserReturnsExpressionForPowFunctionWithTwoExpressionArgumentsWithParentheses()
        {
            var parser = new AlgebraicExpressionParser.Parser();
            Assert.AreEqual(65536, parser.Parse("pow(2 * (x - 1), (x - 1) * (x + 1))").Interpret(new AlgebraicExpressionParser.Expressions.Context(3)), 1e-10);
        }
    }
}
