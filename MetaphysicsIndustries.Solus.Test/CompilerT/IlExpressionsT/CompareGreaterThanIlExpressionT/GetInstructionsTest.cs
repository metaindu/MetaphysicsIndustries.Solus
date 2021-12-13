
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    CompareGreaterThanIlExpressionT
{
    [TestFixture]
    public class GetInstructionsTest
    {
        [Test]
        public void GetInstructionsAddsToList()
        {
            // given
            var i1 = Instruction.LoadConstant(1);
            var i2 = Instruction.LoadConstant(2);
            var expr = new CompareGreaterThanIlExpression(
                new MockIlExpression(il => il.Add(i1)),
                new MockIlExpression(il => il.Add(i2)));
            var instructions = new List<Instruction>();
            // precondition
            Assert.AreEqual(0, instructions.Count);
            // when
            expr.GetInstructions(instructions);
            // then
            Assert.AreEqual(3, instructions.Count);
            Assert.AreEqual(i1, instructions[0]);
            Assert.AreEqual(i2, instructions[1]);
            Assert.AreEqual(Instruction.CompareGreaterThan(),
                instructions[2]);
        }
    }
}
