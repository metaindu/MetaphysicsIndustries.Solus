
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

using System;

namespace MetaphysicsIndustries.Solus.Compiler.IlExpressions
{
    public class IfThenElseConstruct : IlExpression
    {
        public IfThenElseConstruct(IlExpression condition=null,
            IlExpression thenBlock=null, IlExpression elseBlock=null)
        {
            Condition = condition;
            ThenBlock = thenBlock;
            ElseBlock = elseBlock;
        }

        public IlExpression Condition { get; }
        public IlExpression ThenBlock { get; }
        public IlExpression ElseBlock { get; }

        protected override void GetInstructionsInternal(NascentMethod nm)
        {
            if (Condition != null)
                Condition.GetInstructions(nm);
            if (ThenBlock != null && ElseBlock != null)
            {
                var nop = new NopIlExpression();
                var br1 = new BrFalseIlExpression(ElseBlock);
                br1.GetInstructions(nm);
                ThenBlock.GetInstructions(nm);
                var br2 = new BranchIlExpression(nop);
                br2.GetInstructions(nm);
                ElseBlock.GetInstructions(nm);
                nop.GetInstructions(nm);
            }
            else if (ThenBlock != null)
            {
                var nop = new NopIlExpression();
                var br = new BrFalseIlExpression(nop);
                br.GetInstructions(nm);
                ThenBlock.GetInstructions(nm);
                nop.GetInstructions(nm);
            }
            else if (ElseBlock != null)
            {
                var nop = new NopIlExpression();
                var br = new BrTrueIlExpression(nop);
                br.GetInstructions(nm);
                ElseBlock.GetInstructions(nm);
                nop.GetInstructions(nm);
            }
            else
            {
                nm.Instructions.Add(Instruction.Pop());
            }
        }

        public override Type ResultType =>
            throw new NotImplementedException();
    }
}