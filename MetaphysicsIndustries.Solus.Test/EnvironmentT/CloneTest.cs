using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EnvironmentT
{
    [TestFixture]
    public class CloneTest
    {
        [Test]
        public void CloneCreatesDuplicate()
        {
            // given
            var original = new SolusEnvironment(useDefaults: false);
            original.SetVariable("a", new Literal(1));
            // precondition
            Assert.AreEqual(1, original.CountVariables());
            Assert.Contains("a", original.GetVariableNames().ToList());
            Assert.AreEqual(0, original.CountFunctions());
            Assert.AreEqual(0, original.CountMacros());
            // when
            var clone = original.Clone();
            // then
            Assert.AreEqual(1, original.CountVariables());
            Assert.Contains("a", original.GetVariableNames().ToList());
            Assert.AreEqual(0, original.CountFunctions());
            Assert.AreEqual(0, original.CountMacros());
            
            Assert.AreEqual(1, clone.CountVariables());
            Assert.Contains("a", clone.GetVariableNames().ToList());
            Assert.AreEqual(0, clone.CountFunctions());
            Assert.AreEqual(0, clone.CountMacros());
        }

        [Test]
        public void CloneIsShallow()
        {
            // given
            var original = new SolusEnvironment(useDefaults: false);
            original.SetVariable("a", new Literal(1));
            var clone = original.Clone();
            // precondition
            Assert.AreEqual(1, 
                ((Literal)original.GetVariable("a")).Value.ToFloat());
            Assert.AreEqual(1, 
                ((Literal)clone.GetVariable("a")).Value.ToFloat());
            // when
            ((Literal) original.GetVariable("a")).Value = 2.ToNumber();
            // then
            Assert.AreEqual(2, 
                ((Literal)original.GetVariable("a")).Value.ToFloat());
            Assert.AreEqual(2, 
                ((Literal)clone.GetVariable("a")).Value.ToFloat());
        }

        [Test]
        public void AddInCloneDoesNotAddInOriginal()
        {
            // given
            var original = new SolusEnvironment(useDefaults: false);
            original.SetVariable("a", new Literal(1));
            var clone = original.Clone();
            // precondition
            Assert.AreEqual(1, original.CountVariables());
            Assert.AreEqual(1, clone.CountVariables());
            Assert.IsNotNull(original.GetVariable("a"));
            Assert.IsNotNull(clone.GetVariable("a"));
            Assert.IsNull(original.GetVariable("b"));
            Assert.IsNull(clone.GetVariable("b"));
            // when
            clone.SetVariable("b", new Literal(2));
            // then
            Assert.AreEqual(1, original.CountVariables());
            Assert.AreEqual(2, clone.CountVariables());
            Assert.IsNotNull(original.GetVariable("a"));
            Assert.IsNotNull(clone.GetVariable("a"));
            Assert.IsNull(original.GetVariable("b"));
            Assert.IsNotNull(clone.GetVariable("b"));
        }

        [Test]
        public void RemoveInCloneDoesNotRemoveInOriginal()
        {
            // given
            var original = new SolusEnvironment(useDefaults: false);
            original.SetVariable("a", new Literal(1));
            var clone = original.Clone();
            // precondition
            Assert.IsNotNull(original.GetVariable("a"));
            Assert.IsNotNull(clone.GetVariable("a"));
            // when
            clone.RemoveVariable("a");
            Assert.IsNotNull(original.GetVariable("a"));
            Assert.IsNull(clone.GetVariable("a"));
        }

    }
}