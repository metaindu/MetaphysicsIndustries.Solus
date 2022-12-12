
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
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class NegationOperation : UnaryOperation
    {
        public static readonly NegationOperation Value = new NegationOperation();

        protected NegationOperation()
        {
            Name = "-";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Negation; }
        }

        public override string ToString(List<Expression> arguments)
        {
            var arg = arguments[0];

            if (arg == null)
                return $"{DisplayName}({Expression.ToString(null)})";

            if (arg is FunctionCall call &&
                call.Function is Literal literal &&
                literal.Value is Operation oper &&
                oper.Precedence < Precedence)
                return $"{DisplayName}({arg})";

            return $"{DisplayName}{arg}";
        }

        public override float IdentityValue
        {
            get { return 0; }
        }

        public override IMathObject GetResultType(SolusEnvironment env,
            IEnumerable<IMathObject> argTypes)
        {
            return argTypes.First();
        }
    }
}
