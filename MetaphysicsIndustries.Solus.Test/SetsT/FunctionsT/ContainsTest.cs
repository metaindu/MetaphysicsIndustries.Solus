
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

using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.SetsT.FunctionsT
{

    [TestFixture]
    public class ContainsTest
    {
        [Test]
        public void OneParameterFunctionMatches()
        {
            // given
            var fs = Sets.Functions.Get(Reals.Value, Reals.Value);
            // expect
            Assert.That(fs.Contains(SineFunction.Value));
            Assert.That(fs.Contains(CosineFunction.Value));
            Assert.That(fs.Contains(NaturalLogarithmFunction.Value));
            Assert.That(fs.Contains(Log2Function.Value));
            Assert.That(fs.Contains(Log10Function.Value));

            Assert.IsFalse(fs.Contains(AdditionOperation.Value));
            Assert.IsFalse(fs.Contains(DivisionOperation.Value));
            Assert.IsFalse(fs.Contains(LogarithmFunction.Value));
            Assert.IsFalse(fs.Contains(SizeFunction.Value));
            Assert.IsFalse(fs.Contains(MaximumFiniteFunction.Value));
            Assert.IsFalse(fs.Contains(MaximumFunction.Value));
            Assert.IsFalse(fs.Contains(MinimumFiniteFunction.Value));
            Assert.IsFalse(fs.Contains(MinimumFunction.Value));
            Assert.IsFalse(fs.Contains(NegationOperation.Value));
        }

        [Test]
        public void TwoParameterFunctionMatches()
        {
            // given
            var fs = Sets.Functions.Get(Reals.Value,
                Reals.Value, Reals.Value);
            // expect
            Assert.That(fs.Contains(DivisionOperation.Value));
            Assert.That(fs.Contains(LogarithmFunction.Value));
            Assert.That(!fs.Contains(SineFunction.Value));
            Assert.That(!fs.Contains(CosineFunction.Value));
            Assert.That(!fs.Contains(NaturalLogarithmFunction.Value));
            Assert.That(!fs.Contains(Log2Function.Value));
            Assert.That(!fs.Contains(Log10Function.Value));
            Assert.That(!fs.Contains(AdditionOperation.Value));
            Assert.That(!fs.Contains(SizeFunction.Value));
            Assert.That(!fs.Contains(MaximumFiniteFunction.Value));
            Assert.That(!fs.Contains(MaximumFunction.Value));
            Assert.That(!fs.Contains(MinimumFiniteFunction.Value));
            Assert.That(!fs.Contains(MinimumFunction.Value));
            Assert.That(!fs.Contains(NegationOperation.Value));
        }

        [Test]
        public void DifferentReturnTypeDoesNotMatch()
        {
            // given
            var fs = Sets.Functions.Get(Strings.Value, Reals.Value);
            // expect
            Assert.That(!fs.Contains(SineFunction.Value));
            Assert.That(!fs.Contains(CosineFunction.Value));
            Assert.That(!fs.Contains(NaturalLogarithmFunction.Value));
            Assert.That(!fs.Contains(Log2Function.Value));
            Assert.That(!fs.Contains(Log10Function.Value));
        }

        [Test]
        public void DifferentParameterTypeDoesNotMatch()
        {
            // given
            var fs = Sets.Functions.Get(Reals.Value, Strings.Value);
            // expect
            Assert.That(!fs.Contains(SineFunction.Value));
            Assert.That(!fs.Contains(CosineFunction.Value));
            Assert.That(!fs.Contains(NaturalLogarithmFunction.Value));
            Assert.That(!fs.Contains(Log2Function.Value));
            Assert.That(!fs.Contains(Log10Function.Value));
        }

        [Test]
        public void AllFunctionsMatchesAllFunctions()
        {
            Assert.That(AllFunctions.Value.Contains(
                SineFunction.Value));
            Assert.That(AllFunctions.Value.Contains(
                CosineFunction.Value));
            Assert.That(AllFunctions.Value.Contains(
                NaturalLogarithmFunction.Value));
            Assert.That(AllFunctions.Value.Contains(
                Log2Function.Value));
            Assert.That(AllFunctions.Value.Contains(
                Log10Function.Value));
            Assert.That(AllFunctions.Value.Contains(
                AdditionOperation.Value));
            Assert.That(AllFunctions.Value.Contains(
                DivisionOperation.Value));
            Assert.That(AllFunctions.Value.Contains(
                LogarithmFunction.Value));
            Assert.That(AllFunctions.Value.Contains(
                SizeFunction.Value));
            Assert.That(AllFunctions.Value.Contains(
                MaximumFiniteFunction.Value));
            Assert.That(AllFunctions.Value.Contains(
                MaximumFunction.Value));
            Assert.That(AllFunctions.Value.Contains(
                MinimumFiniteFunction.Value));
            Assert.That(AllFunctions.Value.Contains(
                MinimumFunction.Value));
            Assert.That(AllFunctions.Value.Contains(
                NegationOperation.Value));
        }

        [Test]
        public void NonFunctionDoesNotMatch1()
        {
            // given
            var fs = Sets.Functions.Get(Reals.Value, Reals.Value);
            // expect
            Assert.That(!fs.Contains(1.ToNumber()));
        }

        [Test]
        public void NonFunctionDoesNotMatch2()
        {
            Assert.That(!AllFunctions.Value.Contains(1.ToNumber()));
        }
    }
}
