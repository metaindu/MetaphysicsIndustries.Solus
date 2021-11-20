using MetaphysicsIndustries.Solus.Commands;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CommandsT.HelpCommandT
{
    [TestFixture]
    public class ConstructTextTest
    {
        [Test]
        public void WithFunctionYieldsDocString()
        {
            // given
            var env = new SolusEnvironment();
            var f = new MockFunction(new[] { Types.Scalar }, "f");
            f.DocStringV = "asdf";
            env.AddFunction(f);
            // prcondition
            Assert.AreEqual("asdf", f.DocString);
            // when
            var result = HelpCommand.Value.ConstructText(env, "f");
            // then
            Assert.AreEqual("asdf", result);
        }

        [Test]
        public void WithFunctionAsVariableYieldsDocString()
        {
            // given
            var env = new SolusEnvironment();
            var f = new MockFunction(new Types[] { Types.Scalar }, "f");
            f.DocStringV = "asdf";
            env.SetVariable("f", f);
            // prcondition
            Assert.AreEqual("asdf", f.DocString);
            // when
            var result = HelpCommand.Value.ConstructText(env, "f");
            // then
            Assert.AreEqual("asdf", result);
        }
    }
}
