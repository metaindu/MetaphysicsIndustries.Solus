
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

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class VariableAccess : Expression
    {
        public VariableAccess()
            : this(null)
        {
        }

        public VariableAccess(string variableName)
        {
            if (string.IsNullOrEmpty(variableName)) throw new ArgumentNullException("variableName");

            VariableName = variableName;
        }

        public override Expression Clone()
        {
            return new VariableAccess(VariableName);
        }

        public string VariableName;

        public override Literal Eval(SolusEnvironment env)
        {
            var var = VariableName;

            if (env.Variables.ContainsKey(var))
            {
                if (env.Variables[var] is Literal)
                {
                    return (Literal)env.Variables[var];
                }

                return env.Variables[var].Eval(env);
            }
            else
            {
                throw new InvalidOperationException("Variable not found in variable table: " + VariableName);
            }
        }

        public override Expression PreliminaryEval(SolusEnvironment env)
        {
            if (env.Variables.ContainsKey(VariableName))
            {
                //this will cause an infinite recursion if a variable 
                //is defined in terms of itself or in terms of another 
                //variable.
                //we could add (if (env.Variables[Variable] as VariableAccess).Variable == Variable) { throw }
                //but we can't look for cyclical definitions involving other variables, at least, not in an efficient way, right now.
                var var = VariableName;
                return env.Variables[var].PreliminaryEval(env);
            }
            else
            {
                return base.PreliminaryEval(env);
            }
        }

        public override string ToString()
        {
            return VariableName;
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap)
        {
            return new [] { Instruction.LoadLocalVariable(varmap[VariableName]) };
        }
    }
}
