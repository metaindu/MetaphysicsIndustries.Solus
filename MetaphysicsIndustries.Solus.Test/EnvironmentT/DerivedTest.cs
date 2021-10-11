using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
            var dclone = (DerivedEnvironment) clone;
            Assert.AreEqual(3, dclone.Items.Count);
            Assert.AreEqual("one", dclone.Items[0]);
            Assert.AreEqual("two", dclone.Items[1]);
            Assert.AreEqual("three", dclone.Items[2]);
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
