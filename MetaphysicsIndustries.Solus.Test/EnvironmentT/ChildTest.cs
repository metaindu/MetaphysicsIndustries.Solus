
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
            Assert.That(parent.CountVariables(), Is.EqualTo(1));
            Assert.Contains("a", parent.GetVariableNames().ToList());
            // when
            var child = parent.CreateChildEnvironment();
            // then
            Assert.That(parent.CountVariables(), Is.EqualTo(1));
            Assert.Contains("a", parent.GetVariableNames().ToList());

            Assert.That(child.CountVariables(), Is.EqualTo(1));
            Assert.Contains("a", child.GetVariableNames().ToList());
        }

        [Test]
        public void AddInParentAddsInChild()
        {
            // given
            var parent = new SolusEnvironment(useDefaults: false);
            parent.SetVariable("a", new Literal(1));
            var child = parent.CreateChildEnvironment();
            // precondition
            Assert.That(parent.CountVariables(), Is.EqualTo(1));
            Assert.That(child.CountVariables(), Is.EqualTo(1));
            Assert.IsNotNull(parent.GetVariable("a"));
            Assert.IsNotNull(child.GetVariable("a"));
            Assert.IsNull(parent.GetVariable("b"));
            Assert.IsNull(child.GetVariable("b"));
            // when
            parent.SetVariable("b", new Literal(2));
            // then
            Assert.That(parent.CountVariables(), Is.EqualTo(2));
            Assert.That(child.CountVariables(), Is.EqualTo(2));
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
            Assert.That(parent.CountVariables(), Is.EqualTo(1));
            Assert.That(child.CountVariables(), Is.EqualTo(1));
            Assert.IsNotNull(parent.GetVariable("a"));
            Assert.IsNotNull(child.GetVariable("a"));
            Assert.IsNull(parent.GetVariable("b"));
            Assert.IsNull(child.GetVariable("b"));
            // when
            child.SetVariable("b", new Literal(2));
            // then
            Assert.That(parent.CountVariables(), Is.EqualTo(1));
            Assert.That(child.CountVariables(), Is.EqualTo(2));
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
            Assert.That(((Literal)parent.GetVariable("a")).Value.ToFloat(),
                Is.EqualTo(1));
            Assert.That(((Literal)child.GetVariable("a")).Value.ToFloat(),
                Is.EqualTo(1));
            Assert.That(child.GetVariable("a"),
                Is.SameAs(parent.GetVariable("a")));
            // when
            child.RemoveVariable("a");
            child.SetVariable("a", new Literal(2));
            // then
            Assert.That(((Literal)parent.GetVariable("a")).Value.ToFloat(),
                Is.EqualTo(1));
            Assert.That(((Literal)child.GetVariable("a")).Value.ToFloat(),
                Is.EqualTo(2));
            Assert.That(parent.GetVariable("a"),
                Is.Not.SameAs(child.GetVariable("a")));
        }
    }
}