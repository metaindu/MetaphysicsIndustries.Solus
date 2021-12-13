
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

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    CallIlExpressionT
{
    [TestFixture]
    public class GetInstructionsTest
    {
        public static void DummyMethod(int i, float f, bool b)
        {
        }

        [Test]
        public void GetInstructionsAddsToList()
        {
            // given
            var method = new Action<int, float, bool>(DummyMethod);
            var args = new IlExpression[0];
            var expr = new CallIlExpression(method, args);
            var instructions = new List<Instruction>();
            // precondition
            Assert.AreEqual(0, instructions.Count);
            // when
            expr.GetInstructions(instructions);
            // then
            Assert.AreEqual(1, instructions.Count);
            Assert.AreEqual(Instruction.ArgumentType.Method,
                instructions[0].ArgType);
            Assert.AreEqual(method.Method, instructions[0].MethodArg);
            Assert.AreEqual(OpCodes.Call, instructions[0].OpCode);
        }

        [Test]
        public void ArgsYieldMoreInstructions()
        {
            // given
            var method = new Action<int, float, bool>(DummyMethod);
            var args = new IlExpression[]
            {
                new LoadConstantIlExpression(1),
                new LoadConstantIlExpression(2),
                new LoadConstantIlExpression(3)
            };
            var expr = new CallIlExpression(method, args);
            var instructions = new List<Instruction>();
            // precondition
            Assert.AreEqual(0, instructions.Count);
            // when
            expr.GetInstructions(instructions);
            // then
            Assert.AreEqual(4, instructions.Count);
            Assert.AreEqual(Instruction.LoadConstant(1), instructions[0]);
            Assert.AreEqual(Instruction.LoadConstant(2), instructions[1]);
            Assert.AreEqual(Instruction.LoadConstant(3), instructions[2]);
            Assert.AreEqual(Instruction.ArgumentType.Method,
                instructions[3].ArgType);
            Assert.AreEqual(method.Method, instructions[3].MethodArg);
            Assert.AreEqual(OpCodes.Call, instructions[3].OpCode);
        }
    }
}
