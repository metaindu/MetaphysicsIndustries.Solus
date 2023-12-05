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

using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class MultiplicationOperation : Function, IAssociativeCommutativeOperation
    {
        public static readonly MultiplicationOperation Value = new MultiplicationOperation();

        private MultiplicationOperation()
        {
        }

        public override string Name => "*";

        public override bool IsVariadic => true;
        public override IReadOnlyList<Parameter> Parameters { get; } = new List<Parameter>();

        public OperationPrecedence Precedence => OperationPrecedence.Multiplication;
        public override bool IsCommutative => true;
        public override bool IsAssociative => true;
        public bool HasIdentityValue => true;
        public float IdentityValue => 1;
        public bool Collapses => true;
        public float CollapseValue => 0;
        public bool Culls => true;
        public float CullValue => 1;


        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes)
        {
            // TODO: matrix multiplication
            // TODO: matrix times scalar and vice-versa
            // TODO: matrix times vector and vice-versa
            // TODO: vector times scalar
            return argTypes.First();
        }

        public override IFunctionType FunctionType =>
            VariadicFunctions.Get(Reals.Value, Reals.Value, 2);

        public override string ToString(List<Expression> arguments) =>
            Operation.ToString(this, arguments);
    }
}
