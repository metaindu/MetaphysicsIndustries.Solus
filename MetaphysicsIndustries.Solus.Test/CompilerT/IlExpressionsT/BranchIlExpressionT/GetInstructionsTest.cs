
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

using System.Linq;
using System.Reflection.Emit;
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    BranchIlExpressionT
{
    [TestFixture]
    public class GetInstructionsTest
    {
        [Test]
        public void GetInstructionsAddsToList()
        {
            // given
            var i1 = Instruction.LoadConstant(1);
            var expr = new BranchIlExpression(
                new MockIlExpression(il => il.Add(i1)));
            var nm = new NascentMethod();
            // precondition
            Assert.That(nm.Instructions.Count, Is.EqualTo(0));
            Assert.That(nm.GetAllLabels().Count(), Is.EqualTo(0));
            // when
            expr.GetInstructions(nm);
            // then
            Assert.That(nm.Instructions.Count, Is.EqualTo(1));
            Assert.That(nm.Instructions[0].OpCode, Is.EqualTo(OpCodes.Br));
            Assert.That(nm.Instructions[0].ArgType,
                Is.EqualTo(Instruction.ArgumentType.Label));
            Assert.IsNotNull(nm.Instructions[0].LabelArg);
            Assert.That(nm.GetAllLabels().Count(), Is.EqualTo(1));
            Assert.That(nm.Instructions[0].LabelArg,
                Is.SameAs(nm.GetAllLabels().First()));
        }
    }
}
