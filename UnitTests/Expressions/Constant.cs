namespace Expressions
{
    [TestClass]
    public class Constant
    {
        [TestMethod]
        public void InterpretMethodReturnsValurOfConstant()
        {
            var constant = new MathematicalExpressionEvaluation.Expressions.Constant(12);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(3);
            Assert.AreEqual(12, constant.Interpret(context));
        }
    }
}
