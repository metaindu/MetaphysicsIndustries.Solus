
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
using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus
{
    public class StringExpression : Expression
    {
        public StringExpression(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public readonly string Value;
        public override Literal Eval(SolusEnvironment env)
        {
            throw new System.NotImplementedException(
                "Strings can not be treated as numbers.");
        }

        public override Expression Clone()
        {
            return new StringExpression(Value);
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
