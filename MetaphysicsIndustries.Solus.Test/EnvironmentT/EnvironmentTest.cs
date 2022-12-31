
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
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
            Assert.That(result.CountVariables(), Is.EqualTo(0));
        }

        [Test]
        public void SetVariableIncrementsTheCount()
        {
            // given
            var env = new SolusEnvironment(useDefaults: false);
            // precondition
            Assert.That(env.CountVariables(), Is.EqualTo(0));
            // when
            env.SetVariable("a", new Literal(1));
            // then
            Assert.That(env.CountVariables(), Is.EqualTo(1));
            Assert.Contains("a", env.GetVariableNames().ToList());
        }

        [Test]
        public void UseDefaultsTrueAddsDefaultItems()
        {
            // when
            var result = new SolusEnvironment(useDefaults: true);
            // then
            Assert.That(result.CountVariables(), Is.EqualTo(50));
            // and
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
            // and
            Assert.Contains("Real", vars);
            Assert.Contains("Boolean", vars);
            Assert.Contains("Interval", vars);
            Assert.Contains("String", vars);
            Assert.Contains("Set", vars);
            Assert.Contains("Vector", vars);
            Assert.Contains("VectorR2", vars);
            Assert.Contains("VectorR3", vars);
            Assert.Contains("Matrix", vars);
            Assert.Contains("MatrixM2x2", vars);
            Assert.Contains("MatrixM2x3", vars);
            Assert.Contains("MatrixM2x4", vars);
            Assert.Contains("MatrixM3x2", vars);
            Assert.Contains("MatrixM3x3", vars);
            Assert.Contains("MatrixM3x4", vars);
            Assert.Contains("MatrixM4x2", vars);
            Assert.Contains("MatrixM4x3", vars);
            Assert.Contains("MatrixM4x4", vars);
            // and
            Assert.Contains("true", vars);
            Assert.Contains("false", vars);
        }
    }
}
