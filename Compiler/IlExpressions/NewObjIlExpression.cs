
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
using System.Reflection;
using MetaphysicsIndustries.Solus.Exceptions;

namespace MetaphysicsIndustries.Solus.Compiler.IlExpressions
{
    public class NewObjIlExpression : IlExpression
    {
        public NewObjIlExpression(ConstructorInfo constructor,
            params IlExpression[] arguments)
        {
            Constructor = constructor ??
                          throw ValueException.Null(
                              nameof(constructor));
            Arguments = arguments ??
                        throw ValueException.Null(nameof(arguments));
        }

        public ConstructorInfo Constructor { get; }
        public IlExpression[] Arguments { get; }

        protected override void GetInstructionsInternal(NascentMethod nm)
        {
            int i;
            for (i = 0; i < Arguments.Length; i++)
                Arguments[i].GetInstructions(nm);
            nm.Instructions.Add(Instruction.NewObj(Constructor));
        }

        public override Type ResultType => Constructor.DeclaringType;
    }
}
