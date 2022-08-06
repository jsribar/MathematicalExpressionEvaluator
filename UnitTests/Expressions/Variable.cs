namespace JSribar.AlgebraicExpressionParser.UnitTests.Expressions
{
    [TestClass]
    public class Variable
    {
        [TestMethod]
        public void InterpretMethodReturnsValueOfASingleVariableForContextProvided()
        {
            var variable = new AlgebraicExpressionParser.Expressions.Variable();

            Assert.AreEqual(3, variable.Interpret(new AlgebraicExpressionParser.Expressions.Context(3)));
        }

        [TestMethod]
        public void InterpretMethodReturnsValuesOfMultipleVariablesForContextProvided()
        {
            var variableX = new AlgebraicExpressionParser.Expressions.Variable("x");
            var variableY = new AlgebraicExpressionParser.Expressions.Variable("y");

            var context = new Dictionary<string, double> { { "x", 5 }, { "y", 12 } };
            Assert.AreEqual(5, variableX.Interpret(new AlgebraicExpressionParser.Expressions.Context(context)));
            Assert.AreEqual(12, variableY.Interpret(new AlgebraicExpressionParser.Expressions.Context(context)));
        }
    }
}