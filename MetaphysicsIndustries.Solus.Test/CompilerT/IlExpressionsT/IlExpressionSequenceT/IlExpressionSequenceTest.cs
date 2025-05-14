
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

using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    IlExpressionSequenceT
{
    [TestFixture]
    public class IlExpressionSequenceTest
    {
        [Test]
        public void ConstructorCreatesInstance()
        {
            // when
            var result = new IlExpressionSequence();
            // then
            Assert.NotNull(result);
            Assert.NotNull(result.Expressions);
            Assert.That(result.Expressions.Length, Is.EqualTo(0));
        }

        [Test]
        public void ConstructorWithInstructionsSetsProperty()
        {
            // given
            var expr1 = new LoadConstantIlExpression(1);
            var expr2 = new LoadConstantIlExpression(2);
            // when
            var result = new IlExpressionSequence(
                expr1,
                expr2);
            // then
            Assert.NotNull(result.Expressions);
            Assert.That(result.Expressions.Length, Is.EqualTo(2));
            Assert.That(result.Expressions[0], Is.SameAs(expr1));
            Assert.That(result.Expressions[1], Is.SameAs(expr2));
        }
    }
}
