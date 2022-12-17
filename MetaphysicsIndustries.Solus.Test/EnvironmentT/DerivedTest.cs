
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

using NUnit.Framework;
using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus.Test.EnvironmentT
{
    [TestFixture]
    public class DerivedTest
    {
        class DerivedEnvironment : SolusEnvironment
        {
            public DerivedEnvironment(bool useDefaults = false,
                SolusEnvironment parent = null)
                : base(useDefaults, parent)
            {
            }

            public List<string> Items { get; } = new List<string>();

            protected override SolusEnvironment Instantiate(
                bool useDefaults = false, SolusEnvironment parent = null)
            {
                return new DerivedEnvironment(useDefaults, parent);
            }

            protected override void PopulateClone(SolusEnvironment clone)
            {
                ((DerivedEnvironment) clone).Items.AddRange(this.Items);
                base.PopulateClone(clone);
            }
        }

        [Test]
        public void DerivedCloneYieldsDerivedClass()
        {
            // given
            var original = new DerivedEnvironment();
            // when
            var clone = original.Clone();
            // then
            Assert.IsInstanceOf<DerivedEnvironment>(clone);
        }

        [Test]
        public void DerivedCloneAlsoIncludesDerivedItems()
        {
            // given
            var original = new DerivedEnvironment();
            original.Items.Add("one");
            original.Items.Add("two");
            original.Items.Add("three");
            // when
            var clone = original.Clone();
            // then
            var derived = (DerivedEnvironment) clone;
            Assert.That(derived.Items.Count, Is.EqualTo(3));
            Assert.That(derived.Items[0], Is.EqualTo("one"));
            Assert.That(derived.Items[1], Is.EqualTo("two"));
            Assert.That(derived.Items[2], Is.EqualTo("three"));
        }

        [Test]
        public void DerivedChildYieldsDerivedClass()
        {
            // given
            var parent = new DerivedEnvironment();
            // when
            var child = parent.CreateChildEnvironment();
            // then
            Assert.IsInstanceOf<DerivedEnvironment>(child);
        }
    }
}
