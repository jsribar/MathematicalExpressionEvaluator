using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class Constant
    {
        [TestMethod]
        public void InterpretMethodReturnsValurOfConstant()
        {
            var constant = new MathematicalExpressionEvaluator.Expressions.Constant(12);

            Assert.AreEqual(12, constant.Evaluate(3));
        }
    }
}
