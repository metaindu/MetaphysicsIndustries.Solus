using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EnvironmentT
{
    [TestFixture]
    public class EnvironmentTest
    {
        [Test]
        public void InstantiateCreatesObject()
        {
            // when
            var result = new SolusEnvironment();
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<SolusEnvironment>(result);
        }

        [Test]
        public void NoDefaultsYieldsNoItems()
        {
            // when
            var result = new SolusEnvironment(useDefaults: false);
            // then
            Assert.AreEqual(0, result.CountVariables());
            Assert.AreEqual(0, result.CountFunctions());
            Assert.AreEqual(0, result.CountMacros());
            Assert.AreEqual(0, result.CountCommands());
        }

        [Test]
        public void SetVariableIncrementsTheCount()
        {
            // given
            var env = new SolusEnvironment(useDefaults: false);
            // precondition
            Assert.AreEqual(0, env.CountVariables());
            Assert.AreEqual(0, env.CountFunctions());
            Assert.AreEqual(0, env.CountMacros());
            Assert.AreEqual(0, env.CountCommands());
            // when
            env.SetVariable("a", new Literal(1));
            // then
            Assert.AreEqual(1, env.CountVariables());
            Assert.Contains("a", env.GetVariableNames().ToList());
            Assert.AreEqual(0, env.CountFunctions());
            Assert.AreEqual(0, env.CountMacros());
            Assert.AreEqual(0, env.CountCommands());
        }

        [Test]
        public void UseDefaultsTrueAddsDefaultItems()
        {
            // when 
            var result = new SolusEnvironment(useDefaults: true);
            // then
            Assert.AreEqual(0, result.CountVariables());

            Assert.AreEqual(25, result.CountFunctions());
            var functions = result.GetFunctionNames().ToList();
            Assert.Contains("sin", functions);
            Assert.Contains("cos", functions);
            Assert.Contains("tan", functions);
            Assert.Contains("ln", functions);
            Assert.Contains("log2", functions);
            Assert.Contains("log10", functions);
            Assert.Contains("abs", functions);
            Assert.Contains("sec", functions);
            Assert.Contains("csc", functions);
            Assert.Contains("cot", functions);
            Assert.Contains("acos", functions);
            Assert.Contains("asin", functions);
            Assert.Contains("atan", functions);
            Assert.Contains("asec", functions);
            Assert.Contains("acsc", functions);
            Assert.Contains("acot", functions);
            Assert.Contains("ceil", functions);
            Assert.Contains("floor", functions);
            Assert.Contains("unitstep", functions);
            Assert.Contains("atan2", functions);
            Assert.Contains("log", functions);
            Assert.Contains("dist", functions);
            Assert.Contains("distsq", functions);
            Assert.Contains("load_image", functions);
            Assert.Contains("size", functions);

            Assert.AreEqual(8, result.CountMacros());
            var macros = result.GetMacroNames().ToList();
            Assert.Contains("sqrt", macros);
            Assert.Contains("rand", macros);
            Assert.Contains("derive", macros);
            Assert.Contains("feedback", macros);
            Assert.Contains("subst", macros);
            Assert.Contains("assign", macros);
            Assert.Contains("delete", macros);
            Assert.Contains("if", macros);

            Assert.AreEqual(5, result.CountCommands());
            var commands = result.GetCommandNames().ToList();
            Assert.Contains("delete", commands);
            Assert.Contains("func_assign", commands);
            Assert.Contains("var_assign", commands);
            Assert.Contains("vars", commands);
            Assert.Contains("help", commands);
        }
    }
}