
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

using System.Reflection.Emit;
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    LoadLocalIlExpressionT
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
            var nm = new NascentMethod();
            int i;
            IlLocal local = null;
            for (i = 0; i <= arg; i++)
                local = nm.CreateLocal();
            var expr = new LoadLocalIlExpression(local);
            var opcode = OpCodeValues[arg];
            // precondition
            Assert.That(nm.Instructions.Count, Is.EqualTo(0));
            // when
            expr.GetInstructions(nm);
            // then
            Assert.That(nm.Instructions.Count, Is.EqualTo(1));
            Assert.That(nm.Instructions[0],
                Is.EqualTo(Instruction.LoadLocalVariable(arg)));
            Assert.That(nm.Instructions[0].OpCode, Is.EqualTo(opcode));
        }

        [Test]
        [TestCase((ushort)4)]
        [TestCase((ushort)255)]
        public void ShortNumberYieldsShortOpCode(ushort arg)
        {
            // given
            var nm = new NascentMethod();
            int i;
            IlLocal local = null;
            for (i = 0; i <= arg; i++)
                local = nm.CreateLocal();
            var expr = new LoadLocalIlExpression(local);
            // precondition
            Assert.That(nm.Instructions.Count, Is.EqualTo(0));
            // when
            expr.GetInstructions(nm);
            // then
            Assert.That(nm.Instructions.Count, Is.EqualTo(1));
            Assert.That(nm.Instructions[0],
                Is.EqualTo(Instruction.LoadLocalVariable(arg)));
            Assert.That(nm.Instructions[0].OpCode,
                Is.EqualTo(OpCodes.Ldloc_S));
        }

        [Test]
        [TestCase((ushort)256)]
        [TestCase((ushort)65535)]
        public void NormalNumberYieldsNormalOpCode(ushort arg)
        {
            // given
            var nm = new NascentMethod();
            int i;
            IlLocal local = null;
            for (i = 0; i <= arg; i++)
                local = nm.CreateLocal();
            var expr = new LoadLocalIlExpression(local);
            // precondition
            Assert.That(nm.Instructions.Count, Is.EqualTo(0));
            // when
            expr.GetInstructions(nm);
            // then
            Assert.That(nm.Instructions.Count, Is.EqualTo(1));
            Assert.That(nm.Instructions[0],
                Is.EqualTo(Instruction.LoadLocalVariable(arg)));
            Assert.That(nm.Instructions[0].OpCode, Is.EqualTo(OpCodes.Ldloc));
        }
    }
}
