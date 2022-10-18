
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
using System.Reflection.Emit;
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.LoadLocalIlExpressionT
{
    [TestFixture]
    public class GetInstructionsTest
    {
        private static readonly OpCode[] OpCodeValues =
        {
            OpCodes.Ldloc_0,
            OpCodes.Ldloc_1,
            OpCodes.Ldloc_2,
            OpCodes.Ldloc_3,
        };

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void SmallNumberYieldsSpecialOpCode(byte arg)
        {
            // given
            var expr = new LoadLocalIlExpression(arg);
            var nm = new NascentMethod();
            var opcode = OpCodeValues[arg];
            // precondition
            Assert.AreEqual(0, nm.Instructions.Count);
            // when
            expr.GetInstructions(nm);
            // then
            Assert.AreEqual(1, nm.Instructions.Count);
            Assert.AreEqual(Instruction.LoadLocalVariable(arg),
                nm.Instructions[0]);
            Assert.AreEqual(opcode, nm.Instructions[0].OpCode);
        }

        [Test]
        [TestCase((ushort)4)]
        [TestCase((ushort)255)]
        public void ShortNumberYieldsShortOpCode(ushort arg)
        {
            // given
            var expr = new LoadLocalIlExpression(arg);
            var nm = new NascentMethod();
            // precondition
            Assert.AreEqual(0, nm.Instructions.Count);
            // when
            expr.GetInstructions(nm);
            // then
            Assert.AreEqual(1, nm.Instructions.Count);
            Assert.AreEqual(Instruction.LoadLocalVariable(arg),
                nm.Instructions[0]);
            Assert.AreEqual(OpCodes.Ldloc_S, nm.Instructions[0].OpCode);
        }

        [Test]
        [TestCase((ushort)256)]
        [TestCase((ushort)65535)]
        public void NormalNumberYieldsNormalOpCode(ushort arg)
        {
            // given
            var expr = new LoadLocalIlExpression(arg);
            var nm = new NascentMethod();
            // precondition
            Assert.AreEqual(0, nm.Instructions.Count);
            // when
            expr.GetInstructions(nm);
            // then
            Assert.AreEqual(1, nm.Instructions.Count);
            Assert.AreEqual(Instruction.LoadLocalVariable(arg),
                nm.Instructions[0]);
            Assert.AreEqual(OpCodes.Ldloc, nm.Instructions[0].OpCode);
        }
    }
}
