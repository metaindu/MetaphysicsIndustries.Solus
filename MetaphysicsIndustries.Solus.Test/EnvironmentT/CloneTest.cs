
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
    public class CloneTest
    {
        [Test]
        public void CloneCreatesDuplicate()
        {
            // given
            var original = new SolusEnvironment(useDefaults: false);
            original.SetVariable("a", new Literal(1));
            // precondition
            Assert.That(original.CountVariables(), Is.EqualTo(1));
            Assert.Contains("a", original.GetVariableNames().ToList());
            // when
            var clone = original.Clone();
            // then
            Assert.That(original.CountVariables(), Is.EqualTo(1));
            Assert.Contains("a", original.GetVariableNames().ToList());

            Assert.That(clone.CountVariables(), Is.EqualTo(1));
            Assert.Contains("a", clone.GetVariableNames().ToList());
        }

        [Test]
        public void CloneIsShallow()
        {
            // given
            var original = new SolusEnvironment(useDefaults: false);
            original.SetVariable("a", new Literal(1));
            var clone = original.Clone();
            // precondition
            Assert.That(((Literal)original.GetVariable("a")).Value.ToFloat(),
                Is.EqualTo(1));
            Assert.That(((Literal)clone.GetVariable("a")).Value.ToFloat(),
                Is.EqualTo(1));
            // when
            ((Literal) original.GetVariable("a")).Value = 2.ToNumber();
            // then
            Assert.That(((Literal)original.GetVariable("a")).Value.ToFloat(),
                Is.EqualTo(2));
            Assert.That(((Literal)clone.GetVariable("a")).Value.ToFloat(),
                Is.EqualTo(2));
        }

        [Test]
        public void AddInCloneDoesNotAddInOriginal()
        {
            // given
            var original = new SolusEnvironment(useDefaults: false);
            original.SetVariable("a", new Literal(1));
            var clone = original.Clone();
            // precondition
            Assert.That(original.CountVariables(), Is.EqualTo(1));
            Assert.That(clone.CountVariables(), Is.EqualTo(1));
            Assert.IsNotNull(original.GetVariable("a"));
            Assert.IsNotNull(clone.GetVariable("a"));
            Assert.IsNull(original.GetVariable("b"));
            Assert.IsNull(clone.GetVariable("b"));
            // when
            clone.SetVariable("b", new Literal(2));
            // then
            Assert.That(original.CountVariables(), Is.EqualTo(1));
            Assert.That(clone.CountVariables(), Is.EqualTo(2));
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