using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class MathFunction
    {
        [TestMethod]
        public void InterpretMethodForMathFunctionOfSinReturns1ForPiHalfConstant()
        {
            var piHalf = new MathematicalExpressionEvaluation.Expressions.Constant(Math.PI / 2.0);
            var mathFun = new MathematicalExpressionEvaluation.Expressions.MathFunction(Math.Sin, piHalf);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(3);
            Assert.AreEqual(1.0, mathFun.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionOfSinReturns0ForPiConstant()
        {
            var x = new MathematicalExpressionEvaluation.Expressions.Variable();
            var mathFun = new MathematicalExpressionEvaluation.Expressions.MathFunction(Math.Sin, x);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(Math.PI);
            Assert.AreEqual(0, mathFun.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionOfSqrtReturnsCorrectValueForXEqualTo2()
        {
            var argument = new MathematicalExpressionEvaluation.Expressions.Variable();
            var mathFun = new MathematicalExpressionEvaluation.Expressions.MathFunction(Math.Sqrt, argument);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(2);
            Assert.AreEqual(Math.Sqrt(2.0), mathFun.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionSinOfSqrtOfXPlus2ReturnsCorrectValueForXEqualTo2()
        {
            var x = new MathematicalExpressionEvaluation.Expressions.Variable();
            var const2 = new MathematicalExpressionEvaluation.Expressions.Constant(2);
            var xPlus2 = new MathematicalExpressionEvaluation.Expressions.SumExpression(x, const2);
            var sqrtFun = new MathematicalExpressionEvaluation.Expressions.MathFunction(Math.Sqrt, xPlus2);
            var sinFun = new MathematicalExpressionEvaluation.Expressions.MathFunction(Math.Sin, sqrtFun);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(2);
            Assert.AreEqual(Math.Sin(Math.Sqrt(2 + 2)), sinFun.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionOfCosReturnsMinus1ForPiConstant()
        {
            var pi = new MathematicalExpressionEvaluation.Expressions.Constant(Math.PI);
            var mathFun = new MathematicalExpressionEvaluation.Expressions.MathFunction(Math.Cos, pi);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(Math.PI);
            Assert.AreEqual(-1.0, mathFun.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionOfCosReturns0ForPiHalfConstant()
        {
            var piHalf = new MathematicalExpressionEvaluation.Expressions.Constant(Math.PI / 2);
            var mathFun = new MathematicalExpressionEvaluation.Expressions.MathFunction(Math.Cos, piHalf);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(Math.PI);
            Assert.AreEqual(0, mathFun.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionOfCosReturns1ForZeroConstant()
        {
            var zero = new MathematicalExpressionEvaluation.Expressions.Constant(0);
            var mathFun = new MathematicalExpressionEvaluation.Expressions.MathFunction(Math.Cos, zero);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(0);
            Assert.AreEqual(1, mathFun.Interpret(context), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionCosOfSqrtReturnsCorrectValueForXEqualTo2()
        {
            var x = new MathematicalExpressionEvaluation.Expressions.Variable();
            var const2 = new MathematicalExpressionEvaluation.Expressions.Constant(2);
            var xPlus2 = new MathematicalExpressionEvaluation.Expressions.SumExpression(x, const2);
            var sqrtFun = new MathematicalExpressionEvaluation.Expressions.MathFunction(Math.Sqrt, xPlus2);
            var cosFun = new MathematicalExpressionEvaluation.Expressions.MathFunction(Math.Cos, sqrtFun);

            var context = new MathematicalExpressionEvaluation.Expressions.Context(2);
            Assert.AreEqual(Math.Cos(Math.Sqrt(2 + 2)), cosFun.Interpret(context), 1e-10);
        }
    }
}
