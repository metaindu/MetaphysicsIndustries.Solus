
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
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class DeriveOperator : Function
    {
        public static readonly DeriveOperator Value = new DeriveOperator();

        private DeriveOperator()
        {
        }

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes) => Sets.Expressions.Value;

        public override string Name => "derive";

        public override IReadOnlyList<Parameter> Parameters { get; } =
            new List<Parameter>
            {
                new Parameter("e", Sets.Expressions.Value),
                new Parameter("v", Sets.VariableAccesses.Value),
            };

        public override IFunctionType FunctionType { get; } =
            Sets.Functions.Get(
                Sets.Expressions.Value,
                Sets.Expressions.Value,
                VariableAccesses.Value);

        public override string DocString
        {
            get
            {
                return "The derive operator\n  derive(f(x), x)\n\nReturns the derivative of f(x) with respect to x.";
            }
        }
    }
}
