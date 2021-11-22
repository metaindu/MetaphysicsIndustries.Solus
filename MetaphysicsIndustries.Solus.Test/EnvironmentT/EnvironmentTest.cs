
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

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
            Assert.AreEqual(0, result.CountMacros());
        }

        [Test]
        public void SetVariableIncrementsTheCount()
        {
            // given
            var env = new SolusEnvironment(useDefaults: false);
            // precondition
            Assert.AreEqual(0, env.CountVariables());
            Assert.AreEqual(0, env.CountMacros());
            // when
            env.SetVariable("a", new Literal(1));
            // then
            Assert.AreEqual(1, env.CountVariables());
            Assert.Contains("a", env.GetVariableNames().ToList());
            Assert.AreEqual(0, env.CountMacros());
        }

        [Test]
        public void UseDefaultsTrueAddsDefaultItems()
        {
            // when
            var result = new SolusEnvironment(useDefaults: true);
            // then
            Assert.AreEqual(30, result.CountVariables());
            var vars = result.GetVariableNames().ToList();
            Assert.Contains("sin", vars);
            Assert.Contains("cos", vars);
            Assert.Contains("tan", vars);
            Assert.Contains("ln", vars);
            Assert.Contains("log2", vars);
            Assert.Contains("log10", vars);
            Assert.Contains("abs", vars);
            Assert.Contains("sec", vars);
            Assert.Contains("csc", vars);
            Assert.Contains("cot", vars);
            Assert.Contains("acos", vars);
            Assert.Contains("asin", vars);
            Assert.Contains("atan", vars);
            Assert.Contains("asec", vars);
            Assert.Contains("acsc", vars);
            Assert.Contains("acot", vars);
            Assert.Contains("ceil", vars);
            Assert.Contains("floor", vars);
            Assert.Contains("unitstep", vars);
            Assert.Contains("atan2", vars);
            Assert.Contains("log", vars);
            Assert.Contains("dist", vars);
            Assert.Contains("distsq", vars);
            Assert.Contains("load_image", vars);
            Assert.Contains("size", vars);
            Assert.Contains("sqrt", vars);
            Assert.Contains("rand", vars);
            Assert.Contains("derive", vars);
            Assert.Contains("subst", vars);
            Assert.Contains("if", vars);

            Assert.AreEqual(0, result.CountMacros());
        }
    }
}
