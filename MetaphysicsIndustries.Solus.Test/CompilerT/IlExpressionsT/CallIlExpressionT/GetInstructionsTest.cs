
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
            var nm = new NascentMethod();
            // precondition
            Assert.That(nm.Instructions.Count, Is.EqualTo(0));
            // when
            expr.GetInstructions(nm);
            // then
            Assert.That(nm.Instructions.Count, Is.EqualTo(1));
            Assert.That(nm.Instructions[0].ArgType,
                Is.EqualTo(Instruction.ArgumentType.Method));
            Assert.That(nm.Instructions[0].MethodArg,
                Is.EqualTo(method.Method));
            Assert.That(nm.Instructions[0].OpCode, Is.EqualTo(OpCodes.Call));
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
            var nm = new NascentMethod();
            // precondition
            Assert.That(nm.Instructions.Count, Is.EqualTo(0));
            // when
            expr.GetInstructions(nm);
            // then
            Assert.That(nm.Instructions.Count, Is.EqualTo(4));
            Assert.That(nm.Instructions[0],
                Is.EqualTo(Instruction.LoadConstant(1)));
            Assert.That(nm.Instructions[1],
                Is.EqualTo(Instruction.LoadConstant(2)));
            Assert.That(nm.Instructions[2],
                Is.EqualTo(Instruction.LoadConstant(3)));
            Assert.That(nm.Instructions[3].ArgType,
                Is.EqualTo(Instruction.ArgumentType.Method));
            Assert.That(nm.Instructions[3].MethodArg,
                Is.EqualTo(method.Method));
            Assert.That(nm.Instructions[3].OpCode, Is.EqualTo(OpCodes.Call));
        }
    }
}
