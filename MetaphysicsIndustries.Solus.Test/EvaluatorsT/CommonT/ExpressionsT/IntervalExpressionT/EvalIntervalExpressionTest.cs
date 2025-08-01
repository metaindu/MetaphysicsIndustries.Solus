
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

using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    ExpressionsT.IntervalExpressionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalIntervalExpressionTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        public void EvalYieldsConcreteInterval()
        {
            // given
            var i = new IntervalExpression(
                new Literal(1), true,
                new Literal(2), true);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(i, null);
            // then
            Assert.IsTrue(result.IsConcrete);
            Assert.IsTrue(result.IsInterval(null));
            Assert.IsInstanceOf<Interval>(result);
            var interval = (Interval)result;
            Assert.That(interval.LowerBound, Is.EqualTo(1));
            Assert.IsTrue(interval.OpenLowerBound);
            Assert.That(interval.UpperBound, Is.EqualTo(2));
            Assert.IsTrue(interval.OpenUpperBound);
        }
    }
}
