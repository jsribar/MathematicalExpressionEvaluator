using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class Variable
    {
        [TestMethod]
        public void InterpretMethodReturnsValueOfASingleVariableForContextProvided()
        {
            var variable = new MathematicalExpressionEvaluator.Expressions.Variable();

            Assert.AreEqual(3, variable.Evaluate(3));
        }

        [TestMethod]
        public void InterpretMethodReturnsValuesOfMultipleVariablesForContextProvided()
        {
            var variableX = new MathematicalExpressionEvaluator.Expressions.Variable("x");
            var variableY = new MathematicalExpressionEvaluator.Expressions.Variable("y");

            Assert.AreEqual(5, variableX.Evaluate(("x", 5), ("y", 12)));
            Assert.AreEqual(12, variableY.Evaluate(("x", 5), ("y", 12)));
        }
    }
}
