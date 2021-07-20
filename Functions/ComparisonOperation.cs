
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
    public abstract class ComparisonOperation : BinaryOperation
    {
        protected ComparisonOperation(string name)
        {
            Name = name;
        }

        protected override sealed float InternalBinaryCall(float x, float y)
        {
            return Compare(x, y) ? 1 : 0;
        }

        protected abstract bool Compare(float x, float y);

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Comparison; }
        }

        public override bool HasIdentityValue
        {
            get { return false; }
        }
    }
}