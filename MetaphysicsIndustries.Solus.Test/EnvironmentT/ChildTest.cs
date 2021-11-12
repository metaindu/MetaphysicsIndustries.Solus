
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