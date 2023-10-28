using System.Linq;
using BinkyRailways.Core.Services;
using NUnit.Framework;

namespace BinkyRailways.Test.Core.Services
{
    [TestFixture]
    public class ExpressionTests
    {
        [Test]
        public void TestHelloWorld()
        {
            var tokens = new Expression("Hello world").Tokens.ToList();
            Assert.AreEqual(1, tokens.Count, "Expected 1 token");
            Assert.IsInstanceOf(typeof(Expression.LiteralText), tokens[0], "Expected literal text");
        }

        [Test]
        public void TestVars1()
        {
            var tokens = new Expression("{address}").Tokens.ToList();
            Assert.AreEqual(1, tokens.Count, "Expected 1 token");
            Assert.IsInstanceOf(typeof(Expression.Variable), tokens[0], "Expected variable");
        }

        [Test]
        public void TestVars2()
        {
            var tokens = new Expression("pre{address}").Tokens.ToList();
            Assert.AreEqual(2, tokens.Count, "Expected 2 tokens");
            Assert.IsInstanceOf(typeof(Expression.LiteralText), tokens[0], "Expected text");
            Assert.IsInstanceOf(typeof(Expression.Variable), tokens[1], "Expected variable");
        }

        [Test]
        public void TestVars3()
        {
            var tokens = new Expression("pre{address}post").Tokens.ToList();
            Assert.AreEqual(3, tokens.Count, "Expected 3 tokens");
            Assert.IsInstanceOf(typeof(Expression.LiteralText), tokens[0], "Expected text");
            Assert.IsInstanceOf(typeof(Expression.Variable), tokens[1], "Expected variable");
            Assert.IsInstanceOf(typeof(Expression.LiteralText), tokens[2], "Expected text");
        }

        [Test]
        public void TestVars4()
        {
            var tokens = new Expression("{address}{address-value}").Tokens.ToList();
            Assert.AreEqual(2, tokens.Count, "Expected 2 tokens");
            Assert.IsInstanceOf(typeof(Expression.Variable), tokens[0], "Expected variable");
            Assert.IsInstanceOf(typeof(Expression.Variable), tokens[1], "Expected variable");
        }

        [Test]
        public void TestVars5()
        {
            var tokens = new Expression("{address").Tokens.ToList();
            Assert.AreEqual(1, tokens.Count, "Expected 1 token");
            Assert.IsInstanceOf(typeof(Expression.LiteralText), tokens[0], "Expected text");
        }
    }
}
