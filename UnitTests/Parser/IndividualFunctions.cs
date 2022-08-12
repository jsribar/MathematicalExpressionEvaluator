using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluation;
using System;

namespace Parser
{
    [TestClass]
    public class IndividualFunctions
    {
        [TestMethod]
        public void AbsFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(2, parser.Parse("abs(-2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(2, parser.Parse("abs(2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void AcosFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("acos(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.PI / 2, parser.Parse("acos(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void AcoshFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("acosh(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.Acosh(2), parser.Parse("acosh(2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void AsinFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("asin(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.PI / 2, parser.Parse("asin(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void AsinhFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("asinh(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.Asinh(1), parser.Parse("asinh(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void AtanFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("atan(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.PI / 4, parser.Parse("atan(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void Atan2Function()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(3 * Math.PI / 4, parser.Parse("atan2(1, -1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(-Math.PI / 4, parser.Parse("atan2(-1, 1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void AtanhFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("atanh(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.Atanh(0.5), parser.Parse("atanh(0.5)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void CosFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(1, parser.Parse("cos(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0, parser.Parse("cos(PI / 2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void CoshFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(1, parser.Parse("cosh(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.Cosh(1), parser.Parse("cosh(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void ExpFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(1, parser.Parse("exp(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1 / Math.E, parser.Parse("exp(-1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void LogFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("ln(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1, parser.Parse("ln(E)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void Log10Function()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("log(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1, parser.Parse("log(10)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);

            Assert.AreEqual(0, parser.Parse("log10(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1, parser.Parse("log10(10)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void Log2Function()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("log2(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1, parser.Parse("log2(2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void SinFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(1, parser.Parse("sin(PI / 2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0, parser.Parse("sin(PI)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void PowFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(8, parser.Parse("pow(2, 3)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1.0 / 3, parser.Parse("pow(3, -1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void SinhFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(Math.Sinh(1), parser.Parse("sinh(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(0, parser.Parse("sinh(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void SqrtFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(Math.Sqrt(2), parser.Parse("sqrt(2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.Sqrt(3), parser.Parse("sqrt(3)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void TanFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("tan(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1, parser.Parse("tan(PI / 4)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);

            Assert.AreEqual(0, parser.Parse("tg(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(1, parser.Parse("tg(PI / 4)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }

        [TestMethod]
        public void TanhFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            Assert.AreEqual(0, parser.Parse("tanh(0)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
            Assert.AreEqual(Math.Tanh(1), parser.Parse("tanh(1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(5)), 1e-10);
        }
    }
}
