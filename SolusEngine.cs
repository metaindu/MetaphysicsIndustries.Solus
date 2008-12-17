
/*****************************************************************************
 *                                                                           *
 *  SolusEngine.cs                                                           *
 *  17 November 2006                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The central core of processing in Solus. Does some rudimentary parsing   *
 *    and evaluation and stuff.                                              *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;
using System.Diagnostics;
using System.Drawing;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Solus
{
    public partial class SolusEngine
	{
		public SolusEngine()
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

        //public  Literal Eval(Expression e, VariableTable varTable)
        //{
        //    return e.Eval(varTable);
        //}

        public Expression PreliminaryEval(Expression expr, VariableTable varTable)
        {
            return CleanUp(expr.PreliminaryEval(varTable));
        }

        //protected Expression InternalPreliminaryEval(Expression expr, VariableTable varTable)
        //{
        //    if (expr is VariableAccess)
        //    {
        //        VariableAccess varAccess = expr as VariableAccess;
        //        if (varTable.ContainsKey(varAccess.Variable))
        //        {
        //            return new Literal(varTable[varAccess.Variable]);
        //        }
        //    }
        //    else if (expr is FunctionCall)
        //    {
        //        FunctionCall call = expr as FunctionCall;
        //        List<Expression> args = new List<Expression>(call.Arguments.Count);

        //        bool allLiterals = true;
        //        foreach (Expression arg in call.Arguments)
        //        {
        //            Expression arg2 = InternalPreliminaryEval(arg, varTable);
        //            if (!(arg2 is Literal))
        //            {
        //                allLiterals = false;
        //            }
        //            args.Add(arg2);
        //        }

        //        if (allLiterals)
        //        {
        //            return call.Function.Call(varTable, args.ToArray());
        //            //return call.Call(varTable);
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
        //            ret[i] = InternalPreliminaryEval(vector[i], varTable);
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
        //                ret[i, j] = InternalPreliminaryEval(matrix[i, j], varTable);
        //            }
        //        }

        //        return ret;
        //    }

        //    return expr.Clone();
        //}

        //protected  Function GetFunctionByName(string s)
        //{
			
			
			
			
        //    Function[]	functions = {
        //                                        Function.Cosine,
        //                                        Function.Sine,
        //                                        Function.Tangent,
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

        public Brush GetBrushFromExpression(Expression expression, VariableTable varTable)
        {
            if (expression is ColorExpression)
            {
                return ((ColorExpression)expression).Brush;
            }
            else //if (arg is Literal)
            {
                double value = expression.Eval(varTable).Value;// ((Literal)arg).Value;
                int iValue = (int)(value);
                Color color = Color.FromArgb(255, Color.FromArgb(iValue));
                return new SolidBrush(color);
            }
        }

        public Pen GetPenFromExpression(Expression arg, VariableTable varTable)
        {
            if (arg is ColorExpression)
            {
                return ((ColorExpression)arg).Pen;
            }
            else //if (arg is Literal)
            {
                double value = arg.Eval(varTable).Value;// ((Literal)arg).Value;
                int iValue = (int)(value);
                Color color = Color.FromArgb(255, Color.FromArgb(iValue));
                return new Pen(color);
            }
        }

        public static SolusMatrix LoadImage(string filename)
        {
            Image fileImage = Image.FromFile(filename);
            if (!(fileImage is Bitmap))
            {
                throw new InvalidOperationException("The file is not in the correct format");
            }

            Bitmap bitmap = (Bitmap)fileImage;
            MemoryImage image = new MemoryImage(bitmap);

            int i;
            int j;

            image.CopyBitmapToPixels();

            SolusMatrix matrix = new SolusMatrix(image.Height, image.Width);

            for (i = 0; i < image.Width; i++)
            {
                for (j = 0; j < image.Height; j++)
                {
                    matrix[i, j] = new Literal(image[i, j].ToArgb() & 0x00FFFFFF);
                }
            }

            return matrix;
        }

        public static Matrix LoadImage2(string filename)
        {
            Image fileImage = Image.FromFile(filename);
            if (!(fileImage is Bitmap))
            {
                throw new InvalidOperationException("The file is not in the correct format");
            }

            Bitmap bitmap = (Bitmap)fileImage;
            MemoryImage image = new MemoryImage(bitmap);

            int i;
            int j;

            image.CopyBitmapToPixels();

            Matrix matrix = new Matrix(image.Height, image.Width);

            for (i = 0; i < image.Height; i++)
            {
                for (j = 0; j < image.Width; j++)
                {
                    matrix[i, j] = (image[i, j].ToArgb() & 0x00FFFFFF);
                }
            }

            return matrix;
        }

        public static void SaveImage(string filename, Matrix image)
        {
            MemoryImage mem = new MemoryImage();
            mem.Size = new Size(image.ColumnCount, image.RowCount);

            int i;
            int j;

            for (i = 0; i < mem.Width; i++)
            {
                for (j = 0; j < mem.Height; j++)
                {
                    mem[i, j] = Color.FromArgb((int)(((uint)image[i, j]) | 0xFF000000));
                }
            }

            mem.CopyPixelsToBitmap();

            mem.Bitmap.Save(filename);
        }

        public static void SaveImage2(string filename, Matrix image)
        {
            Matrix image2 = image.Clone();
            image2.ApplyToAll(SolusEngine.ConvertFloatTo24g);
            SaveImage(filename, image2);
        }

        public double BinomialCoefficient(int n, int k)
        {
            if (k > n) return 0;
            if (k > n / 2) k = n - k;

            double prod = 1;
            int i;

            for (i = 1; i <= k; i++)
            {
                prod *= (n - k + i) / (double)i;
            }

            return prod;
        }

        public double[] PascalsTriangle(int row)
        {
            double[] res = new double[row + 1];

            int i;

            for (i = 0; i < row; i++)
            {
                res[i] = BinomialCoefficient(row, i);
            }

            return res;
        }
    }
}
