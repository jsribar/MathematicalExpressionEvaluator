namespace JSribar.AlgebraicExpressionParser.UnitTests.Expressions
{
    [TestClass]
    public class Variable
    {
        [TestMethod]
        public void InterpretMethodReturnsValueOfASingleVariableForContextProvided()
        {
            var variable = new AlgebraicExpressionParser.Expressions.Variable();

            var context = new AlgebraicExpressionParser.Expressions.Context(3);
            Assert.AreEqual(3, variable.Interpret(context));
        }

        [TestMethod]
        public void InterpretMethodReturnsValuesOfMultipleVariablesForContextProvided()
        {
            var variableX = new AlgebraicExpressionParser.Expressions.Variable("x");
            var variableY = new AlgebraicExpressionParser.Expressions.Variable("y");

            var context = new AlgebraicExpressionParser.Expressions.Context(new Dictionary<string, double> { { "x", 5 }, { "y", 12 } });
            Assert.AreEqual(5, variableX.Interpret(context));
            Assert.AreEqual(12, variableY.Interpret(context));
        }
    }
}