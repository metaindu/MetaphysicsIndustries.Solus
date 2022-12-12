
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
    public class RawInstructions : IlExpression
    {
        public RawInstructions(params Instruction[] instructions)
            : this(null, instructions)
        {
        }
        public RawInstructions(Type resultType=null,
            params Instruction[] instructions)
        {
            ResultType = resultType;
            Instructions = instructions;
        }

        public Instruction[] Instructions { get; }

        protected override void GetInstructionsInternal(NascentMethod nm)
        {
            int i;
            for (i = 0; i < Instructions.Length; i++)
                nm.Instructions.Add(Instructions[i]);
        }

        public override Type ResultType { get; }
    }
}
