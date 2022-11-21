
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
using System.Reflection;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public class CompiledExpression
    {
        public Delegate Method;
        public Type DelegateType;
        public string[] VariableNames;
        public Type[] ParameterTypes;

        // diagnostics
        public NascentMethod nm;
        public IlExpression ilexpr;
        public List<Instruction> setup;
        public List<Instruction> shutdown;

        /// <summary>
        /// Evaluate a compiled expression, optionally with some values to
        /// use for the expression's unbound variables.
        /// </summary>
        /// <param name="varValuesInOrder">
        /// An optional array of values to use for unbound variables. The
        /// order, number, and type of elements must match the ParameterType
        /// field. The VariableNames field gives the names of the unbound
        /// variables from when the expression was compiled.
        /// </param>
        /// <returns>
        /// The results of the evaluation, as an IMathObject
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The type of the value returned from the compiled method is not
        /// supported.
        /// </exception>
        public IMathObject Evaluate(object[] varValuesInOrder=null)
        {
            object result;
            try
            {
                result = Method.Method.Invoke(null, varValuesInOrder);
            }
            catch (Exception ex)
            {
                if (ex is TargetInvocationException tie)
                    throw tie.InnerException;
                throw;
            }
            if (result is float f)
                return f.ToNumber();
            if (result is float[] v)
                return v.ToVector();
            if (result is float[,] m)
                return m.ToMatrix();
            if (result is string s)
                return s.ToStringValue();

            throw new InvalidOperationException(
                $"Unsupported result type: {result.GetType()}");
        }
        /// <summary>
        /// Evaluate a compiled expression, using the given environment to
        /// provide values for the expression's unbound variables.
        /// </summary>
        /// <param name="env">
        /// An environment containing values for the variables. If a variable
        /// name is not found in the environment, null will be used.
        /// </param>
        /// <returns>
        /// The results of the evaluation, as an IMathObject
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The type of the value returned from the compiled method is not
        /// supported.
        /// </exception>
        public IMathObject Evaluate(SolusEnvironment env)
        {
            var varValuesInOrder = CompileEnvironment(env);
            return Evaluate(varValuesInOrder);
        }

        public object[] CompileEnvironment(SolusEnvironment env)
        {
            object[] varValuesInOrder = null;
            CompileEnvironment(env, ref varValuesInOrder);
            return varValuesInOrder;
        }
        public void CompileEnvironment(SolusEnvironment env,
            ref object[] varValuesInOrder)
        {
            if (VariableNames.Length < 1)
            {
                varValuesInOrder = null;
                return;
            }

            if (varValuesInOrder == null ||
                VariableNames.Length > varValuesInOrder.Length)
                varValuesInOrder = new object[VariableNames.Length];

            int i;
            for (i = 0; i < VariableNames.Length; i++)
            {
                var varName = VariableNames[i];
                var mo = env.GetVariable(varName);
                if (mo == null)
                    continue;
                if (mo is Literal literal)
                    mo = literal.Value;
                else if (mo.IsIsExpression(env))
                    throw new OperandException(
                        $"Variable \"{varName}\" is not defined " +
                        "as a literal");
                var value = ResolveValue(mo);

                // TODO: check type
                varValuesInOrder[i] = value;
            }
        }

        public static object ResolveValue(IMathObject value)
        {
            if (value.IsIsString(null))
                return value.ToStringValue().Value;
            if (value.IsIsScalar(null))
                return value.ToNumber().Value;

            if (value.IsIsVector(null))
            {
                var v = value.ToVector();
                if (v is Vector2 vv)
                    return new float[] { vv.X, vv.Y };
                if (v is Vector3 vvv)
                    return new float[] { vvv.X, vvv.Y, vvv.Z };
                var rv = new float[v.Length];
                int i;
                for (i = 0; i < v.Length; i++)
                    rv[i] = v[i].ToFloat();
                return rv;
            }

            if (value.IsIsMatrix(null))
            {
                var m = value.ToMatrix();

                var rv = new float[m.RowCount, m.ColumnCount];
                int r, c;
                for (r = 0; r < m.RowCount; r++)
                for (c = 0; c < m.ColumnCount; c++)
                    rv[r, c] = m[r, c].ToFloat();
                return rv;
            }

            throw new NotImplementedException(
                $"Unrecognized type, {value.GetType()}");
        }
    }
}
