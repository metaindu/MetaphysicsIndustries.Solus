
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

namespace MetaphysicsIndustries.Solus.Compiler.IlExpressions
{
    public class StoreElemIlExpression : IlExpression
    {
        public StoreElemIlExpression(IlExpression array_ = null,
            IlExpression index = null, IlExpression value = null)
        {
            Array = array_;
            Index = index;
            Value = value;
        }

        public IlExpression Array { get; }
        public IlExpression Index { get; }
        public IlExpression Value { get; }

        protected override void GetInstructionsInternal(NascentMethod nm)
        {
            if (Array != null) Array.GetInstructions(nm);
            if (Index != null) Index.GetInstructions(nm);
            if (Value != null) Value.GetInstructions(nm);
            nm.Instructions.Add(Instruction.StElem(typeof(float)));
        }
    }
}