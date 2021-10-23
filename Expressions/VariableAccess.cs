
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

using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Values;
using ArgumentNullException = System.ArgumentNullException;

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
            Result = new ResultC(this);
        }

        public override Expression Clone()
        {
            return new VariableAccess(VariableName);
        }

        public string VariableName;

        public override IMathObject Eval(SolusEnvironment env)
        {
            var var = VariableName;

            if (env.ContainsVariable(var))
            {
                if (env.GetVariable(var) is Literal literal)
                    return literal.Value.ToNumber();

                return env.GetVariable(var).Eval(env);
            }
            else
            {
                throw new NameException(
                    "Variable not found in variable table: " +
                    VariableName);
            }
        }

        public override Expression PreliminaryEval(SolusEnvironment env)
        {
            if (env.ContainsVariable(VariableName))
            {
                //this will cause an infinite recursion if a variable 
                //is defined in terms of itself or in terms of another 
                //variable.
                //we could add (if (env.Variables[Variable] as VariableAccess).Variable == Variable) { throw }
                //but we can't look for cyclical definitions involving other variables, at least, not in an efficient way, right now.
                var var = VariableName;
                return env.GetVariable(var).PreliminaryEval(env);
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

        public override IEnvMathObject Result { get; }

        private class ResultC : IEnvMathObject
        {
            public ResultC(VariableAccess va) => _va = va;
            private readonly VariableAccess _va;

            private void Check(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName))
                    throw new NameException(
                        $"Variable not found in the environment: " +
                        _va.VariableName);
            }

            public bool IsScalar(SolusEnvironment env)
            {
                Check(env);
                var varexpr = env.GetVariable(_va.VariableName);
                return varexpr.Result.IsScalar(env);
            }

            public bool IsVector(SolusEnvironment env)
            {
                Check(env);
                var varexpr = env.GetVariable(_va.VariableName);
                return varexpr.Result.IsVector(env);
            }

            public bool IsMatrix(SolusEnvironment env)
            {
                Check(env);
                var varexpr = env.GetVariable(_va.VariableName);
                return varexpr.Result.IsMatrix(env);
            }

            public int GetTensorRank(SolusEnvironment env)
            {
                Check(env);
                var varexpr = env.GetVariable(_va.VariableName);
                return varexpr.Result.GetTensorRank(env);
            }

            public bool IsString(SolusEnvironment env)
            {
                Check(env);
                var varexpr = env.GetVariable(_va.VariableName);
                return varexpr.Result.IsString(env);
            }

            public int GetDimension(SolusEnvironment env, int index)
            {
                Check(env);
                var varexpr = env.GetVariable(_va.VariableName);
                return varexpr.Result.GetDimension(env, index);
            }

            public int[] GetDimensions(SolusEnvironment env)
            {
                Check(env);
                var varexpr = env.GetVariable(_va.VariableName);
                return varexpr.Result.GetDimensions(env);
            }

            public int GetVectorLength(SolusEnvironment env)
            {
                Check(env);
                var varexpr = env.GetVariable(_va.VariableName);
                return varexpr.Result.GetVectorLength(env);
            }

            public int GetStringLength(SolusEnvironment env)
            {
                Check(env);
                var varexpr = env.GetVariable(_va.VariableName);
                return varexpr.Result.GetStringLength(env);
            }

            public bool IsConcrete => false;
        }
    }
}
