
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

using System.Reflection.Emit;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    LoadConstantIlExpressionT
{
    [TestFixture]
    public class LoadConstantIlExpressionTest
    {
        [Test]
        public void CreateWithDoubleSetsProperty()
        {
            // when
            var result = new LoadConstantIlExpression(1.0d);
            // then
            Assert.IsNotNull(result);
            Assert.That(result.Instruction.DoubleArg, Is.EqualTo(1.0d));
        }

        [Test]
        public void CreateWithFloatSetsProperty()
        {
            // when
            var result = new LoadConstantIlExpression(1.0f);
            // then
            Assert.IsNotNull(result);
            Assert.That(result.Instruction.FloatArg, Is.EqualTo(1.0f));
        }

        private static readonly OpCode[] OpCodeValues =
        {
            OpCodes.Ldc_I4_0,
            OpCodes.Ldc_I4_1,
            OpCodes.Ldc_I4_2,
            OpCodes.Ldc_I4_3,
            OpCodes.Ldc_I4_4,
            OpCodes.Ldc_I4_5,
            OpCodes.Ldc_I4_6,
            OpCodes.Ldc_I4_7,
            OpCodes.Ldc_I4_8,
            OpCodes.Ldc_I4_M1
        };
        
        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(-1, 9)]
        public void CreateWithIntValueSetsSpecialOpcode(long arg,
            int opcodeIndex)
        {
            // given
            var opcode = OpCodeValues[opcodeIndex];
            // when
            var result = new LoadConstantIlExpression(arg);
            // then
            Assert.IsNotNull(result);
            Assert.That(result.Instruction.IntArg, Is.EqualTo(0));
            Assert.That(result.Instruction.OpCode, Is.EqualTo(opcode));
        }

        [Test]
        [TestCase(9)]
        [TestCase(127)]
        [TestCase(-2)]
        [TestCase(-128)]
        public void CreateWithIntValueSetsShortOpcode(long arg)
        {
            // when
            var result = new LoadConstantIlExpression(arg);
            // then
            Assert.IsNotNull(result);
            Assert.That(result.Instruction.IntArg, Is.EqualTo(arg));
            Assert.That(result.Instruction.OpCode,
                Is.EqualTo(OpCodes.Ldc_I4_S));
        }

        [Test]
        [TestCase(128)]
        [TestCase(2147483647)]
        [TestCase(-129)]
        [TestCase(-2147483648)]
        public void CreateWithIntValueSetsNormalOpcode(long arg)
        {
            // when
            var result = new LoadConstantIlExpression(arg);
            // then
            Assert.IsNotNull(result);
            Assert.That(result.Instruction.IntArg, Is.EqualTo(arg));
            Assert.That(result.Instruction.OpCode,
                Is.EqualTo(OpCodes.Ldc_I4));
        }

        [Test]
        [TestCase(2147483648)]
        [TestCase(-2147483649)]
        public void CreateWithIntValueSetsLongOpcode(long arg)
        {
            // when
            var result = new LoadConstantIlExpression(arg);
            // then
            Assert.IsNotNull(result);
            Assert.That(result.Instruction.IntArg, Is.EqualTo(arg));
            Assert.That(result.Instruction.OpCode,
                Is.EqualTo(OpCodes.Ldc_I8));
        }
    }
}
