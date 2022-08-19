using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Expressions
{
    [TestClass]
    public class MathFunction
    {
        [TestMethod]
        public void InterpretMethodForMathFunctionOfSinReturns1ForPiHalfConstant()
        {
            var piHalf = new MathematicalExpressionEvaluator.Expressions.Constant(Math.PI / 2.0);
            var mathFun = new MathematicalExpressionEvaluator.Expressions.MathFunction(Math.Sin, piHalf);

            Assert.AreEqual(1.0, mathFun.Evaluate(3), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionOfSinReturns0ForPiConstant()
        {
            var x = new MathematicalExpressionEvaluator.Expressions.Variable();
            var mathFun = new MathematicalExpressionEvaluator.Expressions.MathFunction(Math.Sin, x);

            Assert.AreEqual(0, mathFun.Evaluate(Math.PI), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionOfSqrtReturnsCorrectValueForXEqualTo2()
        {
            var argument = new MathematicalExpressionEvaluator.Expressions.Variable();
            var mathFun = new MathematicalExpressionEvaluator.Expressions.MathFunction(Math.Sqrt, argument);

            Assert.AreEqual(Math.Sqrt(2.0), mathFun.Evaluate(2), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionSinOfSqrtOfXPlus2ReturnsCorrectValueForXEqualTo2()
        {
            var x = new MathematicalExpressionEvaluator.Expressions.Variable();
            var const2 = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            var xPlus2 = new MathematicalExpressionEvaluator.Expressions.SumExpression(x, const2);
            var sqrtFun = new MathematicalExpressionEvaluator.Expressions.MathFunction(Math.Sqrt, xPlus2);
            var sinFun = new MathematicalExpressionEvaluator.Expressions.MathFunction(Math.Sin, sqrtFun);

            Assert.AreEqual(Math.Sin(Math.Sqrt(2 + 2)), sinFun.Evaluate(2), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionOfCosReturnsMinus1ForPiConstant()
        {
            var pi = new MathematicalExpressionEvaluator.Expressions.Constant(Math.PI);
            var mathFun = new MathematicalExpressionEvaluator.Expressions.MathFunction(Math.Cos, pi);

            Assert.AreEqual(-1.0, mathFun.Evaluate(Math.PI), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionOfCosReturns0ForPiHalfConstant()
        {
            var piHalf = new MathematicalExpressionEvaluator.Expressions.Constant(Math.PI / 2);
            var mathFun = new MathematicalExpressionEvaluator.Expressions.MathFunction(Math.Cos, piHalf);

            Assert.AreEqual(0, mathFun.Evaluate(Math.PI), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionOfCosReturns1ForZeroConstant()
        {
            var zero = new MathematicalExpressionEvaluator.Expressions.Constant(0);
            var mathFun = new MathematicalExpressionEvaluator.Expressions.MathFunction(Math.Cos, zero);

            Assert.AreEqual(1, mathFun.Evaluate(0), 1e-10);
        }

        [TestMethod]
        public void InterpretMethodForMathFunctionCosOfSqrtReturnsCorrectValueForXEqualTo2()
        {
            var x = new MathematicalExpressionEvaluator.Expressions.Variable();
            var const2 = new MathematicalExpressionEvaluator.Expressions.Constant(2);
            var xPlus2 = new MathematicalExpressionEvaluator.Expressions.SumExpression(x, const2);
            var sqrtFun = new MathematicalExpressionEvaluator.Expressions.MathFunction(Math.Sqrt, xPlus2);
            var cosFun = new MathematicalExpressionEvaluator.Expressions.MathFunction(Math.Cos, sqrtFun);

            Assert.AreEqual(Math.Cos(Math.Sqrt(2 + 2)), cosFun.Evaluate(2), 1e-10);
        }
    }
}
