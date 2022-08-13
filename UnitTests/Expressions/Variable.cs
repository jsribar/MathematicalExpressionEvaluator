using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class Variable
    {
        [TestMethod]
        public void InterpretMethodReturnsValueOfASingleVariableForContextProvided()
        {
            var variable = new MathematicalExpressionEvaluation.Expressions.Variable();

            var context = new MathematicalExpressionEvaluation.Expressions.Context(3);
            Assert.AreEqual(3, variable.Interpret(context));
        }

        [TestMethod]
        public void InterpretMethodReturnsValuesOfMultipleVariablesForContextProvided()
        {
            var variableX = new MathematicalExpressionEvaluation.Expressions.Variable("x");
            var variableY = new MathematicalExpressionEvaluation.Expressions.Variable("y");

            var context = new MathematicalExpressionEvaluation.Expressions.Context(new Dictionary<string, double> { { "x", 5 }, { "y", 12 } });
            Assert.AreEqual(5, variableX.Interpret(context));
            Assert.AreEqual(12, variableY.Interpret(context));
        }
    }
}