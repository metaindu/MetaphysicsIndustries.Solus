
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

using MetaphysicsIndustries.Solus.Commands;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CommandsT.HelpCommandT
{
    [TestFixture]
    public class ConstructTextTest
    {
        [Test]
        public void WithFunctionYieldsDocString()
        {
            // given
            var env = new SolusEnvironment();
            var f = new MockFunction(new[] { Types.Scalar }, "f");
            f.DocStringV = "asdf";
            env.AddFunction(f);
            // prcondition
            Assert.AreEqual("asdf", f.DocString);
            // when
            var result = HelpCommand.Value.ConstructText(env, "f");
            // then
            Assert.AreEqual("asdf", result);
        }

        [Test]
        public void WithFunctionAsVariableYieldsDocString()
        {
            // given
            var env = new SolusEnvironment();
            var f = new MockFunction(new Types[] { Types.Scalar }, "f");
            f.DocStringV = "asdf";
            env.SetVariable("f", f);
            // prcondition
            Assert.AreEqual("asdf", f.DocString);
            // when
            var result = HelpCommand.Value.ConstructText(env, "f");
            // then
            Assert.AreEqual("asdf", result);
        }
    }
}
