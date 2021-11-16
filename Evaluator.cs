
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

/*****************************************************************************
 *                                                                           *
 *  Evaluator.cs                                                           *
 *                                                                           *
 *  The central core of processing in Solus. Does some rudimentary parsing   *
 *    and evaluation and stuff.                                              *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;

using System.Drawing;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Transformers;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus
{
    public partial class Evaluator
	{
		public Evaluator()
		{
			
		}

        //public  void Dispose()
        //{
			
        //}

        //public  Expression Parse(string s)
        //{
        //    string[]	tokens;
        //    FunctionCall	fc;
        //    int				i;
        //    int				j;
        //    fc = new FunctionCall();
        //    tokens = s.Split(' ');
        //    j = tokens.Length;
        //    for (i = 1; i < j; i++)
        //    {
        //        float		fl;
        //        Expression	e;
        //        fl = float.Parse(tokens[i]);
        //        e = new Literal(fl);
        //        fc.Arguments.Add(e);
        //    }
        //    fc.Function = GetFunctionByName(tokens[0]);
        //    return fc;
        //}

        //public  Literal Eval(Expression e, VariableTable env)
        //{
        //    return e.Eval(env);
        //}

        public Expression PreliminaryEval(Expression expr, SolusEnvironment env)
        {
            CleanUpTransformer cleanup = new CleanUpTransformer();
            return cleanup.CleanUp(expr.PreliminaryEval(env));
        }

        //protected Expression InternalPreliminaryEval(Expression expr, VariableTable env)
        //{
        //    if (expr is VariableAccess)
        //    {
        //        VariableAccess varAccess = expr as VariableAccess;
        //        if (env.Variables.ContainsKey(varAccess.Variable))
        //        {
        //            return new Literal(env.Variables[varAccess.Variable]);
        //        }
        //    }
        //    else if (expr is FunctionCall)
        //    {
        //        FunctionCall call = expr as FunctionCall;
        //        List<Expression> args = new List<Expression>(call.Arguments.Count);

        //        bool allLiterals = true;
        //        foreach (Expression arg in call.Arguments)
        //        {
        //            Expression arg2 = InternalPreliminaryEval(arg, env);
        //            if (!(arg2 is Literal))
        //            {
        //                allLiterals = false;
        //            }
        //            args.Add(arg2);
        //        }

        //        if (allLiterals)
        //        {
        //            return call.Function.Call(env, args.ToArray());
        //            //return call.Call(env);
        //        }
        //        else
        //        {
        //            return new FunctionCall(call.Function, args.ToArray());
        //        }
        //    }
        //    else if (expr is Vector)
        //    {
        //        Vector vector = (Vector)expr;
        //        Vector ret = new Vector(vector.Length);

        //        int i;
        //        for (i =0; i < vector.Length; i++)
        //        {
        //            ret[i] = InternalPreliminaryEval(vector[i], env);
        //        }

        //        return ret;
        //    }
        //    else if (expr is Matrix)
        //    {
        //        Matrix matrix = (Matrix)expr;
        //        Matrix ret = new Matrix(matrix.RowCount, matrix.ColumnCount);

        //        int i;
        //        int j;
        //        for (i = 0; i < matrix.RowCount; i++)
        //        {
        //            for (j = 0; j < matrix.ColumnCount; j++)
        //            {
        //                ret[i, j] = InternalPreliminaryEval(matrix[i, j], env);
        //            }
        //        }

        //        return ret;
        //    }

        //    return expr.Clone();
        //}

        //protected  Function GetFunctionByName(string s)
        //{
			
			
			
			
        //    Function[]	functions = {
        //                                        CosineFunction.Value,
        //                                        SineFunction.Value,
        //                                        TangentFunction.Value,
        //                                        Function.Cotangent,
        //                                        Function.Secant,
        //                                        Function.Cosecant,
        //                                        Function.Arccosine,
        //                                        Function.Arcsine,
        //                                        Function.Arctangent,
        //                                        Function.Arccotangent,
        //                                        Function.Arcsecant,
        //                                        Function.Arccosecant,
        //                                        Function.Floor,
        //                                        Function.Ceiling,
        //                                    };
        //    foreach (Function f in functions)
        //    {
        //        if (s == f.Name)
        //        {
        //            return f;
        //        }
        //    }
        //    foreach (Function f in functions)
        //    {
        //        if (s == f.DisplayName)
        //        {
        //            return f;
        //        }
        //    }
        //    return null;
        //}

        public static Matrix LoadImage(string filename,
            Func<string, Image> _loader = null)
        {
            if (_loader == null)
                _loader = Image.FromFile;
            var fileImage = _loader(filename);
            if (!(fileImage is Bitmap bitmap))
                throw new InvalidOperationException(
                    "The file is not in the correct format");
            var image = new MemoryImage(bitmap);
            image.CopyBitmapToPixels();

            var values = new float[image.Height, image.Width];
            for (var c = 0; c < image.Width; c++)
            for (var r = 0; r < image.Height; r++)
                values[r, c] = image[r, c].ToArgb() & 0x00FFFFFF;
            return new Matrix(values);
        }

        public float BinomialCoefficient(int n, int k)
        {
            if (k > n) return 0;
            if (k > n / 2) k = n - k;

            float prod = 1;
            int i;

            for (i = 1; i <= k; i++)
            {
                prod *= (n - k + i) / (float)i;
            }

            return prod;
        }

        public float[] PascalsTriangle(int row)
        {
            float[] res = new float[row + 1];

            int i;

            for (i = 0; i < row; i++)
            {
                res[i] = BinomialCoefficient(row, i);
            }

            return res;
        }
    }
}
