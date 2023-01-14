
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
using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class MultiplicationOperation : AssociativeCommutativeOperation
    {
        public static readonly MultiplicationOperation Value = new MultiplicationOperation();

        protected MultiplicationOperation()
        {
        }

        public override string Name => "*";

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Multiplication; }
        }

        //public override bool IsCommutative
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        //public override bool IsAssociative
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        public override bool Collapses
        {
            get
            {
                return true;
            }
        }

        public override float CollapseValue
        {
            get
            {
                return 0;
            }
        }

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
            throw new NotImplementedException();
    }
}
