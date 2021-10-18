using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EnvironmentT
{
    [TestFixture]
    public class ChildTest
    {
        
        [Test]
        public void CreateChildEnvironmentCreatesChildEnvironment()
        {
            // given
            var parent = new SolusEnvironment(useDefaults: false);
            parent.SetVariable("a", new Literal(1));
            // precondition
            Assert.AreEqual(1, parent.CountVariables());
            Assert.Contains("a", parent.GetVariableNames().ToList());
            Assert.AreEqual(0, parent.CountFunctions());
            Assert.AreEqual(0, parent.CountMacros());
            // when
            var child = parent.CreateChildEnvironment();
            // then
            Assert.AreEqual(1, parent.CountVariables());
            Assert.Contains("a", parent.GetVariableNames().ToList());
            Assert.AreEqual(0, parent.CountFunctions());
            Assert.AreEqual(0, parent.CountMacros());
            
            Assert.AreEqual(1, child.CountVariables());
            Assert.Contains("a", child.GetVariableNames().ToList());
            Assert.AreEqual(0, child.CountFunctions());
            Assert.AreEqual(0, child.CountMacros());
        }

        [Test]
        public void AddInParentAddsInChild()
        {
            // given
            var parent = new SolusEnvironment(useDefaults: false);
            parent.SetVariable("a", new Literal(1));
            var child = parent.CreateChildEnvironment();
            // precondition
            Assert.AreEqual(1, parent.CountVariables());
            Assert.AreEqual(1, child.CountVariables());
            Assert.IsNotNull(parent.GetVariable("a"));
            Assert.IsNotNull(child.GetVariable("a"));
            Assert.IsNull(parent.GetVariable("b"));
            Assert.IsNull(child.GetVariable("b"));
            // when
            parent.SetVariable("b", new Literal(2));
            // then
            Assert.AreEqual(2, parent.CountVariables());
            Assert.AreEqual(2, child.CountVariables());
            Assert.IsNotNull(parent.GetVariable("a"));
            Assert.IsNotNull(child.GetVariable("a"));
            Assert.IsNotNull(parent.GetVariable("b"));
            Assert.IsNotNull(child.GetVariable("b"));
        }

        [Test]
        public void RemoveInParentRemovesInChild()
        {
            // given
            var parent = new SolusEnvironment(useDefaults: false);
            parent.SetVariable("a", new Literal(1));
            var child = parent.CreateChildEnvironment();
            // precondition
            Assert.IsNotNull(parent.GetVariable("a"));
            Assert.IsNotNull(child.GetVariable("a"));
            // when
            parent.RemoveVariable("a");
            // then
            Assert.IsNull(parent.GetVariable("a"));
            Assert.IsNull(child.GetVariable("a"));
        }

        [Test]
        public void AddInChildDoesNotAddInParent()
        {
            // given
            var parent = new SolusEnvironment(useDefaults: false);
            parent.SetVariable("a", new Literal(1));
            var child = parent.CreateChildEnvironment();
            // precondition
            Assert.AreEqual(1, parent.CountVariables());
            Assert.AreEqual(1, child.CountVariables());
            Assert.IsNotNull(parent.GetVariable("a"));
            Assert.IsNotNull(child.GetVariable("a"));
            Assert.IsNull(parent.GetVariable("b"));
            Assert.IsNull(child.GetVariable("b"));
            // when
            child.SetVariable("b", new Literal(2));
            // then
            Assert.AreEqual(1, parent.CountVariables());
            Assert.AreEqual(2, child.CountVariables());
            Assert.IsNotNull(parent.GetVariable("a"));
            Assert.IsNotNull(child.GetVariable("a"));
            Assert.IsNull(parent.GetVariable("b"));
            Assert.IsNotNull(child.GetVariable("b"));
        }

        [Test]
        public void RemoveInChildDoesNotRemoveInParent()
        {
            // given
            var parent = new SolusEnvironment(useDefaults: false);
            parent.SetVariable("a", new Literal(1));
            var child = parent.CreateChildEnvironment();
            // precondition
            Assert.IsNotNull(parent.GetVariable("a"));
            Assert.IsNotNull(child.GetVariable("a"));
            // when
            child.RemoveVariable("a");
            // then
            Assert.IsNotNull(parent.GetVariable("a"));
            Assert.IsNull(child.GetVariable("a"));
        }

        [Test]
        public void RemoveAndAddDoesNotRestoreConnection()
        {
            // given
            var parent = new SolusEnvironment(useDefaults: false);
            parent.SetVariable("a", new Literal(1));
            var child = parent.CreateChildEnvironment();
            // precondition
            Assert.AreEqual(1,
                ((Literal)parent.GetVariable("a")).Value.ToFloat());
            Assert.AreEqual(1,
                ((Literal)child.GetVariable("a")).Value.ToFloat());
            Assert.AreSame(parent.GetVariable("a"),
                child.GetVariable("a"));
            // when
            child.RemoveVariable("a");
            child.SetVariable("a", new Literal(2));
            // then
            Assert.AreEqual(1,
                ((Literal)parent.GetVariable("a")).Value.ToFloat());
            Assert.AreEqual(2,
                ((Literal)child.GetVariable("a")).Value.ToFloat());
            Assert.AreNotSame(parent.GetVariable("a"),
                child.GetVariable("a"));
        }
    }
}