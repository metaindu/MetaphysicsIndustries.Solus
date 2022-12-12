
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
    public class StoreLocalIlExpression : IlExpression
    {
        public StoreLocalIlExpression(IlLocal local, IlExpression target=null)
        {
            Local = local;
            Target = target;
        }

        public IlLocal Local { get; }
        public IlExpression Target { get; }

        protected override void GetInstructionsInternal(NascentMethod nm)
        {
            if (Target != null)
                Target.GetInstructions(nm);
            var index = nm.GetIndexOfLocal(Local);
            nm.Instructions.Add(Instruction.StoreLocalVariable(index));
        }

        public override Type ResultType =>
            throw new NotImplementedException();
    }
}
