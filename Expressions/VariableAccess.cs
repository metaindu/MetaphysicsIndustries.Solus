
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
                var target = env.GetVariable(var);
                if (target.IsIsExpression(env))
                    target = ((Expression)target).Eval(env);
                return target;
            }

            throw new NameException(
                $"Variable not found: {VariableName}");
        }

        public override string ToString()
        {
            return VariableName;
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override IMathObject Result { get; }

        private class ResultC : IMathObject
        {
            public ResultC(VariableAccess va) => _va = va;
            private readonly VariableAccess _va;

            public bool? IsScalar(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return varexpr.IsScalar(env);
            }

            public bool? IsVector(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return varexpr.IsVector(env);
            }

            public bool? IsMatrix(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return varexpr.IsMatrix(env);
            }

            public int? GetTensorRank(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return varexpr.GetTensorRank(env);
            }

            public bool? IsString(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return varexpr.IsString(env);
            }

            public int? GetDimension(SolusEnvironment env, int index)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return varexpr.GetDimension(env, index);
            }

            public int[] GetDimensions(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return varexpr.GetDimensions(env);
            }

            public int? GetVectorLength(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return varexpr.GetVectorLength(env);
            }

            public bool? IsInterval(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return varexpr.IsInterval(env);
            }

            public bool? IsFunction(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return false;
            }

            public bool? IsExpression(SolusEnvironment env)
            {
                if (!env.ContainsVariable(_va.VariableName)) return null;
                var varexpr = env.GetVariable(_va.VariableName);
                if (varexpr.IsIsExpression(env))
                    varexpr = ((Expression)varexpr).Result;
                return true;
            }

            public bool IsConcrete => false;
        }
    }
}
