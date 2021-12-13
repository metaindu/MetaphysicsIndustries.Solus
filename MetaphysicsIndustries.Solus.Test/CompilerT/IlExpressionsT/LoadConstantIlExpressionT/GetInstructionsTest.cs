
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

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.LoadConstantIlExpressionT
{
    [TestFixture]
    public class GetInstructionsTest
    {
        [Test]
        public void CreateWithDoubleYieldsInstruction()
        {
            // given
            var expr = new LoadConstantIlExpression(1.0d);
            var instructions = new List<Instruction>();
            // precondition
            Assert.AreEqual(0, instructions.Count);
            // when
            expr.GetInstructions(instructions);
            // then
            Assert.AreEqual(1, instructions.Count);
            Assert.AreEqual(Instruction.LoadConstant(1.0d),
                instructions[0]);
            Assert.AreEqual(expr.Instruction, instructions[0]);
            Assert.AreEqual(OpCodes.Ldc_R8, instructions[0].OpCode);
        }

        [Test]
        public void CreateWithFloatYieldsInstruction()
        {
            // given
            var expr = new LoadConstantIlExpression(1.0f);
            var instructions = new List<Instruction>();
            // precondition
            Assert.AreEqual(0, instructions.Count);
            // when
            expr.GetInstructions(instructions);
            // then
            Assert.AreEqual(1, instructions.Count);
            Assert.AreEqual(Instruction.LoadConstant(1.0f),
                instructions[0]);
            Assert.AreEqual(expr.Instruction, instructions[0]);
            Assert.AreEqual(OpCodes.Ldc_R4, instructions[0].OpCode);
        }

        [Test]
        public void CreateWithIntegerYieldsInstruction()
        {
            // given
            var expr = new LoadConstantIlExpression(129);
            var instructions = new List<Instruction>();
            // precondition
            Assert.AreEqual(0, instructions.Count);
            // when
            expr.GetInstructions(instructions);
            // then
            Assert.AreEqual(1, instructions.Count);
            Assert.AreEqual(Instruction.LoadConstant(129),
                instructions[0]);
            Assert.AreEqual(expr.Instruction, instructions[0]);
            Assert.AreEqual(OpCodes.Ldc_I4, instructions[0].OpCode);
        }

        [Test]
        public void CreateWithSpecialInstructionYieldsInstruction()
        {
            // given
            var expr = new LoadConstantIlExpression(0);
            var instructions = new List<Instruction>();
            // precondition
            Assert.AreEqual(0, instructions.Count);
            // when
            expr.GetInstructions(instructions);
            // then
            Assert.AreEqual(1, instructions.Count);
            Assert.AreEqual(Instruction.LoadConstant(0),
                instructions[0]);
            Assert.AreEqual(expr.Instruction, instructions[0]);
            Assert.AreEqual(OpCodes.Ldc_I4_0, instructions[0].OpCode);
        }
    }
}
