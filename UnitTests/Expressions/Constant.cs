namespace JSribar.AlgebraicExpressionParser.UnitTests.Expressions
{
    [TestClass]
    public class Constant
    {
        [TestMethod]
        public void InterpretMethodReturnsValurOfConstant()
        {
            var constant = new AlgebraicExpressionParser.Expressions.Constant(12);

            Assert.AreEqual(12, constant.Interpret(new AlgebraicExpressionParser.Expressions.Context(3)));
        }
    }
}
