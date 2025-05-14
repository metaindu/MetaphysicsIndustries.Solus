
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

namespace MetaphysicsIndustries.Solus.Compiler.IlExpressions
{
    public class WhileLoopConstruct : IlExpression
    {
        public WhileLoopConstruct(IlExpression condition = null,
            IlExpression body = null)
        {
            Condition = condition;
            Body = body;
        }

        public IlExpression Condition { get; }
        public IlExpression Body { get; }


        protected override void GetInstructionsInternal(NascentMethod nm)
        {
            var nop1 = new NopIlExpression();
            var nop2 = new NopIlExpression();
            var exprs = new List<IlExpression>();
            exprs.Add(nop1);
            if (Condition != null)
            {
                exprs.Add(Condition);
                exprs.Add(new BrFalseIlExpression(nop2));
            }

            if (Body != null)
                exprs.Add(Body);
            exprs.Add(new BranchIlExpression(nop1));
            exprs.Add(nop2);
            var seq = new IlExpressionSequence(exprs);
            seq.GetInstructions(nm);
        }

        public override Type ResultType =>
            throw new NotImplementedException();
    }
}
