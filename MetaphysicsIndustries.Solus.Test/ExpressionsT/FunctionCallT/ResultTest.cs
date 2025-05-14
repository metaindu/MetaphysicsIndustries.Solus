
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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

using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.FunctionCallT
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void ResultMatchesFunction1()
        {
            // given
            var parameters = new[] { new Parameter("", Reals.Value) };
            var f = new MockFunction(parameters, "f");
            var ms = new MockSet();
            f.GetResultF = args => ms;
            var expr = new FunctionCall(f);
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.That(result, Is.SameAs(ms));
        }

        [Test]
        public void ResultMatchesFunction2()
        {
            // given
            var parameters = new[] { new Parameter("", Reals.Value) };
            var f = new MockFunction(parameters, "f");
            f.GetResultF = args => Matrices.M3x4;
            var expr = new FunctionCall(f);
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.That(result, Is.SameAs(Matrices.M3x4));
        }
    }
}
