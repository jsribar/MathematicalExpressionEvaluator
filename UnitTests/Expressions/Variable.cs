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

            var context = new MathematicalExpressionEvaluator.Expressions.Context(3);
            Assert.AreEqual(3, variable.Interpret(context));
        }

        [TestMethod]
        public void InterpretMethodReturnsValuesOfMultipleVariablesForContextProvided()
        {
            var variableX = new MathematicalExpressionEvaluator.Expressions.Variable("x");
            var variableY = new MathematicalExpressionEvaluator.Expressions.Variable("y");

            var context = new MathematicalExpressionEvaluator.Expressions.Context(("x", 5), ("y", 12));
            Assert.AreEqual(5, variableX.Interpret(context));
            Assert.AreEqual(12, variableY.Interpret(context));
        }
    }
}
