
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

using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class DivisionOperation : BinaryOperation
    {
        public static readonly DivisionOperation Value = new DivisionOperation();

        protected DivisionOperation()
        {
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Division; }
        }

        public override bool IsCommutative
        {
            get
            {
                return false;
            }
        }

        //protected override Literal InternalCall(SolusEnvironment env,
        //    Literal[] args)
        //{
        //    return new Literal(args[0].Value / args[1].Value);
        //}

        protected override float InternalBinaryCall(float x, float y)
        {
            return x / y;
        }

        public override IMathObject GetResult(IEnumerable<IMathObject> args)
        {
            // TODO: vector divided by scalar
            // TODO: matrix divided by scalar
            return ScalarMathObject.Value;
        }
    }
}
