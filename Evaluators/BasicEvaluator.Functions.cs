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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Evaluators
{
    public partial class BasicEvaluator
    {
        public IMathObject CallFunction(AbsoluteValueFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return Math.Abs(args[0].ToNumber().Value).ToNumber();
        }

        public IMathObject CallFunction(AdditionOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            // TODO: vector
            // TODO: matrix
            // TODO: string?

            if (args[0].IsIsScalar(env))
            {
                float sum = 0;
                foreach (var arg in args)
                {
                    sum += arg.ToNumber().Value;
                }
                return sum.ToNumber();
            }

            if (args[0].IsIsMatrix(env))
            {
                var m = args[0].ToMatrix();
                var nr = m.RowCount;
                var nc = m.ColumnCount;
                var elements = new float[nr, nc];
                int r, c;
                foreach (var arg in args)
                {
                    var v = arg.ToMatrix();
                    for (r = 0; r < nr; r++)
                    for (c = 0; c < nr; c++)
                        elements[r,c] += v[r,c].ToNumber().Value;
                }
                return new Matrix(elements);
            }

            if (args[0].IsIsVector(env))
            {
                int n = args[0].ToVector().Length;
                var elements = new float[n];
                int i;
                foreach (var arg in args)
                {
                    var v = arg.ToVector();
                    for (i = 0; i < n; i++)
                        elements[i] += v[i].ToNumber().Value;
                }

                if (n == 2) return new Vector2(elements[0], elements[1]);
                if (n == 3)
                    return new Vector3(
                        elements[0], elements[1], elements[2]);
                return new Vector(elements);
            }

            throw new OperandException("Unsupported type");
        }

        public IMathObject CallFunction(ArccosecantFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return
                ((float)Math.Asin(1 / args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(ArccosineFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            if (arg0 < -1)
                throw new OperandException(
                    "Argument less than -1");
            if (arg0 > 1)
                throw new OperandException(
                    "Argument greater than 1");
            return ((float)Math.Acos(arg0)).ToNumber();
        }

        public IMathObject CallFunction(ArccotangentFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            return ((float)(Math.Atan2(1, x))).ToNumber();
        }

        public IMathObject CallFunction(ArcsecantFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return ((float)Math.Acos(1 / args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(ArcsineFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            if (arg0 < -1)
                throw new OperandException(
                    "Argument less than -1");
            if (arg0 > 1)
                throw new OperandException(
                    "Argument greater than 1");
            return ((float)Math.Asin(arg0)).ToNumber();
        }

        public IMathObject CallFunction(Arctangent2Function f,
            IMathObject[] args, SolusEnvironment env)
        {
            return ((float)Math.Atan2(args[0].ToNumber().Value,
                args[1].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(ArctangentFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return ((float)Math.Atan(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(BitwiseAndOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            // TODO: BitwiseAndOperation is a BinaryOperation, but
            //       BitwiseOrOperation is an AssociativeCommutativeOperation.
            //       probably make this work on more than two arguments.
            return new Number((long)args[0].ToNumber().Value &
                              (long)args[1].ToNumber().Value);
        }

        public IMathObject CallFunction(BitwiseOrOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            long value = 0;

            foreach (var arg in args)
            {
                value |= (long)arg.ToNumber().Value;
            }

            return value.ToNumber();
        }

        public IMathObject CallFunction(CeilingFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return ((float)Math.Ceiling(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(CosecantFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return
                ((float)(1 / Math.Sin(args[0].ToNumber().Value))).ToNumber();
        }

        public IMathObject CallFunction(CosineFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            return ((float)Math.Cos(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(CotangentFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return
                ((float)(1 / Math.Tan(args[0].ToNumber().Value))).ToNumber();
        }

        public IMathObject CallFunction(DistFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            var arg1 = args[1].ToNumber().Value;
            return ((float)Math.Sqrt(arg0 * arg0 + arg1 * arg1)).ToNumber();
        }

        public IMathObject CallFunction(DistSqFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            var arg1 = args[1].ToNumber().Value;
            return (arg0 * arg0 + arg1 * arg1).ToNumber();
        }

        public IMathObject CallFunction(DivisionOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            if (y == 0)
                throw new OperandException("Division by zero");
            return (x / y).ToNumber();
        }

        public IMathObject CallFunction(EqualComparisonOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return (x == y ? 1 : 0).ToNumber();
        }

        public IMathObject CallFunction(ExponentOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return ((float)Math.Pow(x, y)).ToNumber();
        }

        public IMathObject CallFunction(FactorialFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            // TODO: check args
            var n = args[0].ToNumber().Value;
            if (n <= 1) return 1.ToNumber();
            var product = 1f;
            while (n > 0)
            {
                product *= n;
                n--;
            }

            return product.ToNumber();
        }

        public IMathObject CallFunction(FloorFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            return ((float)Math.Floor(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(GreaterThanComparisonOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return (x > y ? 1 : 0).ToNumber();
        }

        public IMathObject CallFunction(
            GreaterThanOrEqualComparisonOperation f, IMathObject[] args,
            SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return (x >= y ? 1 : 0).ToNumber();
        }

        public IMathObject CallFunction(LessThanComparisonOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return (x < y ? 1 : 0).ToNumber();
        }

        public IMathObject CallFunction(LessThanOrEqualComparisonOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return (x <= y ? 1 : 0).ToNumber();
        }

        public IMathObject CallFunction(LoadImageFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public IMathObject CallFunction(Log10Function f, IMathObject[] args,
            SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            if (arg0 <= 0)
                throw new OperandException(
                    "Argument must be positive");
            return ((float)Math.Log10(arg0)).ToNumber();
        }

        public IMathObject CallFunction(Log2Function f, IMathObject[] args,
            SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            if (arg0 <= 0)
                throw new OperandException(
                    "Argument must be positive");
            return ((float)Math.Log(arg0, 2)).ToNumber();
        }

        public IMathObject CallFunction(LogarithmFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            var arg1 = args[1].ToNumber().Value;
            if (arg0 <= 0)
                throw new OperandException("Argument must be positive");
            if (arg1 <= 0)
                throw new OperandException("Base must be positive");
            if (arg1 <= 1 && arg1 >= 1)
                throw new OperandException("Base must not be one");

            var rv = (float)Math.Log(arg0, arg1);
            return rv.ToNumber();
        }

        public IMathObject CallFunction(LogicalAndOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return ((long)x != 0 &&
                    (long)y != 0
                ? 1
                : 0).ToNumber();
        }

        public IMathObject CallFunction(LogicalOrOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return ((long)x != 0 ||
                    (long)y != 0
                ? 1
                : 0).ToNumber();
        }

        public IMathObject CallFunction(MaximumFiniteFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            // TODO: CheckArguments
            int i;
            float current = float.NaN;
            bool first = true;
            for (i = 0; i < args.Length; i++)
            {
                var value = args[i].ToNumber().Value;
                if (!float.IsInfinity(value) &&
                    !float.IsNaN(value))
                {
                    if (first)
                    {
                        current = value;
                        first = false;
                    }
                    else
                        current = Math.Max(current, value);
                }
            }

            return current.ToNumber();
        }

        public IMathObject CallFunction(MaximumFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            // TODO: CheckArguments
            int i;
            var current = args[0].ToNumber().Value;
            for (i = 1; i < args.Length; i++)
                current = Math.Max(
                    current,
                    args[i].ToNumber().Value);
            return current.ToNumber();
        }

        public IMathObject CallFunction(MinimumFiniteFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            // TODO: CheckArguments
            int i;
            float current = float.NaN;
            bool first = true;
            for (i = 0; i < args.Length; i++)
            {
                var value = args[i].ToNumber().Value;
                if (!float.IsInfinity(value) &&
                    !float.IsNaN(value))
                {
                    if (first)
                    {
                        current = value;
                        first = false;
                    }
                    else
                        current = Math.Min(current, value);
                }
            }

            return current.ToNumber();
        }

        public IMathObject CallFunction(MinimumFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            // TODO: CheckArguments
            int i;
            var current = args[0].ToNumber().Value;
            for (i = 1; i < args.Length; i++)
                current = Math.Min(
                    current,
                    args[i].ToNumber().Value);
            return current.ToNumber();
        }

        public IMathObject CallFunction(ModularDivision f, IMathObject[] args,
            SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            if (y == 0)
                throw new OperandException("Division by zero");
            return ((long)x % (long)y).ToNumber();
        }

        public IMathObject CallFunction(MultiplicationOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            // s*s=s
            // s*v=v
            // v*s=v
            // v*v=X
            // s*m=m
            // m*s=m
            // v*m=v?
            // m*V=v?
            // m*m=m

            IMathObject current = args[0];
            IMathObject next = null;
            for (int i = 1; i < args.Length; i++)
            {
                var cs = current.IsIsScalar(env);
                var cv = current.IsIsVector(env);
                var cm = current.IsIsMatrix(env);
                if (!cs && !cv && !cm)
                    throw new OperandException(
                        "Unsupported type, " + $"{current.GetMathType()}");
                next = args[i];
                var ns = next.IsIsScalar(env);
                var nv = next.IsIsVector(env);
                var nm = next.IsIsMatrix(env);
                if (!ns && !nv && !nm)
                    throw new OperandException(
                        "Unsupported type, " + $"{next.GetMathType()}");

                if (cs)
                {
                    if (ns)
                    {
                        current = new Number(current.ToNumber().Value *
                                             next.ToNumber().Value);
                    }
                    else if (nv)
                    {
                        var csv = current.ToNumber().Value;
                        var nvv = next.ToVector();
                        var fs = new float[nvv.Length];
                        for (int j = 0; j < nvv.Length; j++)
                            fs[j] = nvv[j].ToNumber().Value * csv;
                        current = new Vector(fs);
                    }
                    else // if (nm)
                    {
                        var csv = current.ToNumber().Value;
                        var nmv = next.ToMatrix();
                        var fs = new float[nmv.RowCount, nmv.ColumnCount];
                        for (int r = 0; r < nmv.RowCount; r++)
                        for (int c = 0; c < nmv.ColumnCount; c++)
                            fs[r, c] = nmv[r, c].ToNumber().Value * csv;
                        current = new Matrix(fs);
                    }
                }
                else if (cv)
                {
                    if (ns)
                    {
                        var cvv = current.ToVector();
                        var nsv = next.ToNumber().Value;
                        var fs = new float[cvv.Length];
                        for (int j = 0; j < cvv.Length; j++)
                            fs[j] = cvv[j].ToNumber().Value * nsv;
                        current = new Vector(fs);
                    }
                    else if (nv)
                    {
                        throw new NotImplementedException();
                    }
                    else // if (nm)
                    {
                        throw new NotImplementedException();
                    }
                }
                else // if (cm)
                {
                    if (ns)
                    {
                        var cmv = current.ToMatrix();
                        var nsv = next.ToNumber().Value;
                        var fs = new float[cmv.RowCount, cmv.ColumnCount];
                        for (int r = 0; r < cmv.RowCount; r++)
                        for (int c = 0; c < cmv.ColumnCount; c++)
                            fs[r, c] = cmv[r, c].ToNumber().Value * nsv;
                        current = new Matrix(fs);
                    }
                    else if (nv)
                    {
                        throw new NotImplementedException();
                    }
                    else // if (nm)
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            return current;
        }

        public IMathObject CallFunction(NaturalLogarithmFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            if (arg0 <= 0)
                throw new OperandException(
                    "Argument must be positive");
            return ((float)Math.Log(arg0)).ToNumber();
        }

        public IMathObject CallFunction(NegationOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            return (-args[0].ToNumber().Value).ToNumber();
        }

        public IMathObject CallFunction(NotEqualComparisonOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return (x != y ? 1 : 0).ToNumber();
        }

        public IMathObject CallFunction(SecantFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            return
                ((float)(1 / Math.Cos(args[0].ToNumber().Value))).ToNumber();
        }

        public IMathObject CallFunction(SineFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            return ((float)Math.Sin(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(SizeFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            // TODO: this function should be able to operate on things that
            // aren't IMathObject, namely TensorExpression
            var arg = args[0];
            if (arg.IsIsString(env) || arg.IsIsVector(env))
            {
                var dim = arg.GetDimension(env, 0);
                if (dim.HasValue)
                    return dim.Value.ToNumber();
            }

            return arg.GetDimensions(env).ToVector();
        }

        public IMathObject CallFunction(TangentFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return ((float)Math.Tan(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(UnitStepFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            if (args[0].ToNumber().Value >= 0)
                return new Number(1);
            return new Number(0);
        }

        private SolusEnvironment _udfParentEnvCache;
        private SolusEnvironment _udfChildCache;

        public IMathObject CallFunction(UserDefinedFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            if (_udfChildCache == null ||
                env != _udfParentEnvCache)
            {
                _udfChildCache = env.CreateChildEnvironment();
                _udfParentEnvCache = env;
            }

            var env2 = _udfChildCache;

            int i;
            for (i = 0; i < f.Argnames.Length; i++)
            {
                env2.SetVariable(f.Argnames[i], args[i]);
            }

            return Eval(f.Expression, env2);
        }
    }
}
