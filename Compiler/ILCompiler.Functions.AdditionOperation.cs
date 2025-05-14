
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(
            AdditionOperation func, NascentMethod nm,
            VariableIdentityMap variables,
            List<Expression> arguments)
        {
            IlExpression expr = new RawInstructions();

            bool first = true;
            foreach (var arg in arguments)
            {
                if (!first &&
                    arg is FunctionCall call &&
                    call.Function is Literal literal &&
                    literal.Value is Function f &&
                    f == NegationOperation.Value)
                {
                    expr = new SubIlExpression(expr,
                        ConvertToIlExpression(call.Arguments[0], nm,
                            variables));
                }
                else
                {
                    if (first)
                        expr = ConvertToIlExpression(arg, nm, variables);
                    else
                        expr = new AddIlExpression(expr,
                            ConvertToIlExpression(arg, nm, variables));
                    first = false;
                }
            }

            return expr;
        }
    }
}
