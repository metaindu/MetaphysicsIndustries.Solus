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
using MetaphysicsIndustries.Solus.Exceptions;

namespace MetaphysicsIndustries.Solus.Compiler.IlExpressions
{
    public class BranchIlExpression : IlExpression
    {
        public BranchIlExpression(IlExpression target)
        {
            Target = target ??
                     throw ValueException.Null(nameof(target));
        }

        public IlExpression Target { get; }

        protected override void GetInstructionsInternal(NascentMethod nm)
        {
            var label = nm.GetOrCreateExpressionLabel(Target);
            nm.Instructions.Add(Instruction.Branch(label));
        }

        public override Type ResultType =>
            throw new NotImplementedException();
    }
}
