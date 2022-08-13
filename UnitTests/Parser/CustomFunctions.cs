using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluation = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class AdditionalFunction
    {
        static double Increment(double val)
        {
            return val + 1;
        }

        static double Double(double val)
        {
            return 2 * val;
        }

        static double Hypotenuse(double a, double b)
        {
            return Math.Sqrt(a * a + b * b);
        }

        static double Mid(double a, double b)
        {
            return (a + b) / 2;
        }

        [TestMethod]
        public void AddFunctionMethodIncludesNewFunction()
        {
            var parser = new MathematicalExpressionEvaluation.Parser();
            parser.AddFunction("increment", Increment);

            Assert.AreEqual(5, parser.Parse("increment(x + 1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3)), 1e-10);

            parser.AddFunction("double", Double);
            Assert.AreEqual(5, parser.Parse("increment(x + 1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3)), 1e-10);
            Assert.AreEqual(12, parser.Parse("double(x + x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3)), 1e-10);

            parser.AddFunction2("hypotenuse", Hypotenuse);
            Assert.AreEqual(5, parser.Parse("increment(x + 1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3)), 1e-10);
            Assert.AreEqual(12, parser.Parse("double(x + x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3)), 1e-10);
            Assert.AreEqual(5, parser.Parse("hypotenuse(3, 2 * x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(2)), 1e-10);

            parser.AddFunction2("mid", Mid);
            Assert.AreEqual(5, parser.Parse("increment(x + 1)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3)), 1e-10);
            Assert.AreEqual(12, parser.Parse("double(x + x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(3)), 1e-10);
            Assert.AreEqual(5, parser.Parse("hypotenuse(3, 2 * x)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(2)), 1e-10);
            Assert.AreEqual(2.5, parser.Parse("mid(3, 2)").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(2)), 1e-10);

            Assert.AreEqual(8.0907413371995e-5, parser.Parse("exp(2 * mid(increment(x), 2) - hypotenuse(8, 3 * double(x)))").Interpret(new MathematicalExpressionEvaluation.Expressions.Context(2)), 1e-10);
        }
    }
}
