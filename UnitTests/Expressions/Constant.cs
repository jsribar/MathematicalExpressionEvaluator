namespace JSribar.AlgebraicExpressionParser.UnitTests.Expressions
{
    [TestClass]
    public class Constant
    {
        [TestMethod]
        public void InterpretMethodReturnsValurOfConstant()
        {
            var constant = new AlgebraicExpressionParser.Expressions.Constant(12);

            var context = new AlgebraicExpressionParser.Expressions.Context(3);
            Assert.AreEqual(12, constant.Interpret(context));
        }
    }
}
