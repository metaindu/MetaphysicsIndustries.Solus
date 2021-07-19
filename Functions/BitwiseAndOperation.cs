
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

namespace MetaphysicsIndustries.Solus.Functions
{
    public class BitwiseAndOperation : BinaryOperation
    {
        public static readonly BitwiseAndOperation Value = new BitwiseAndOperation();

        protected BitwiseAndOperation()
        {
            Name = "&";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Bitwise; }
        }

        //protected override Literal InternalCall(VariableTable env, Literal[] args)
        //{
        //    ulong value = 0xffffffffffffffff;

        //    foreach (Literal arg in args)
        //    {
        //        ulong argvalue = (ulong)(arg.Value);
        //        value &= argvalue;
        //    }

        //    return new Literal(value);
        //}
        protected override float InternalBinaryCall(float x, float y)
        {
            return ((long)x) & ((long)y);   
        }

        //public override float IdentityValue
        //{
        //    get { return 0xffffffffffffffff; }
        //}

        //public override bool Culls
        //{
        //    get { return false; }
        //}
    }
}
