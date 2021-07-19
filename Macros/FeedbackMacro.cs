
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
using System.Linq;
using System.Text;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Macros;

namespace MetaphysicsIndustries.Solus
{
    public class FeedbackMacro : Macro
    {
        public static readonly FeedbackMacro Value = new FeedbackMacro();

        protected FeedbackMacro()
        {
            Name = "feedback";
            NumArguments = 2;
        }

        public override Expression InternalCall(IEnumerable<Expression> args, SolusEnvironment env)
        {
            Expression g = args.ElementAt(0);
            Expression h = args.ElementAt(1);

            return new FunctionCall(
                        DivisionOperation.Value,
                        g,
                        new FunctionCall(
                            AdditionOperation.Value,
                            new Literal(1),
                            new FunctionCall(
                                MultiplicationOperation.Value,
                                g,
                                h)));
        }
    }
}
