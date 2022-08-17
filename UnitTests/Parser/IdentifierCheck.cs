using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathematicalExpressionEvaluator = JSribar.MathematicalExpressionEvaluator;

namespace Parser
{
    [TestClass]
    public class IdentifierCheck
    {
        [TestMethod]
        public void IsValidIdentifierReturnsTrueForAnIdentifierConsistingOfOneLetterOnly()
        {
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("x"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("X"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("π"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("å"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("Я"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("汉"));
        }

        [TestMethod]
        public void IsValidIdentifierReturnsTrueForAnIdentifierStartingWithLetterFollowedByLettersAndDigits()
        {
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("xx"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("xy"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("x1"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("x12"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("xn1"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("xn1a"));
            Assert.IsTrue(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("πa"));
        }

        [TestMethod]
        public void IsValidIdentifierReturnsFalseForAnIdentifierStartingWithDigit()
        {
            Assert.IsFalse(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("1"));
            Assert.IsFalse(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("1x"));
        }

        [TestMethod]
        public void IsValidIdentifierReturnsFalseForAnIdentifierContainingCharactersThatAreNotLetterOrDigit()
        {
            Assert.IsFalse(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("x y"));
            Assert.IsFalse(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("x_x"));
            Assert.IsFalse(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("x.1"));
            Assert.IsFalse(MathematicalExpressionEvaluator.Parser.IsValidIdentifier("x!"));
        }
    }
}
