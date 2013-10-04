
/*****************************************************************************
 *                                                                           *
 *  SolusEngine.Eval.cs                                                      *
 *  16 February 2010                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2010 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Some helpful methods for evaluating expressions across intervals.        *
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
        public Expression[] PreliminaryEvalInterval(Expression expr, Dictionary<string, Expression> vars,
                                                    string x, float xStart, float xEnd, float xStep)
        {
            int i;
            float xx;
            i = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                i++;
            }

            Expression[] exprs = new Expression[i];

            Expression previousValue = null;
            if (vars.ContainsKey(x))
            {
                previousValue = vars[x];
                vars.Remove(x);
            }
            Expression preeval = expr.PreliminaryEval(vars);

            i = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                vars[x] = new Literal(xx);
                exprs[i] = preeval.PreliminaryEval(vars);
                i++;
            }

            return exprs;
        }

        public float[] EvalInterval(Expression expr, Dictionary<string, Expression> vars,
                                        string x, float xStart, float xEnd, float xStep)
        {
            int i;
            float xx;
            i = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                i++;
            }

            Expression previousValue = null;
            bool hasPreviousValue = false;
            if (vars.ContainsKey(x))
            {
                hasPreviousValue = true;
                previousValue = vars[x];
                vars.Remove(x);
            }
            Expression preeval = expr.PreliminaryEval(vars);
            //check that all variables in the expression are already in the variable table


            float[] values = new float[i];

            i = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                vars[x] = new Literal(xx);
                values[i] = preeval.Eval(vars).Value;
                i++;
            }

            if (hasPreviousValue)
            {
                vars[x] = previousValue;
            }

            return values;
        }

        public float[,] EvalInterval(Expression expr, Dictionary<string, Expression> vars,
                                        string x, float xStart, float xEnd, float xStep,
                                        string y, float yStart, float yEnd, float yStep)
        {
            int nx = 0;
            int ny = 0;

            float xx;
            float yy;

            List<Literal> xValues = new List<Literal>();
            List<Literal> yValues = new List<Literal>();

            nx = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                xValues.Add(new Literal(xx));
                nx++;
            }
            ny = 0;
            for (yy = yStart; yy <= yEnd; yy += yStep)
            {
                yValues.Add(new Literal(yy));
                ny++;
            }

            Expression previousValueX = null;
            bool hasPreviousValueX = false;
            if (vars.ContainsKey(x))
            {
                hasPreviousValueX = true;
                previousValueX = vars[x];
                vars.Remove(x);
            }

            Expression previousValueY = null;
            bool hasPreviousValueY = false;
            if (vars.ContainsKey(y))
            {
                hasPreviousValueY = true;
                previousValueY = vars[y];
                vars.Remove(y);
            }

            Expression preeval = expr;//.PreliminaryEval(vars);
            //check that all variables in the expression are already in the variable table


            float[,] values = new float[nx, ny];

            int ix = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                vars[x] = xValues[ix];

                int iy = 0;
                for (yy = yStart; yy <= yEnd; yy += yStep)
                {
                    vars[y] = yValues[iy];
                    values[ix, iy] = preeval.Eval(vars).Value;
                    iy++;
                }

                ix++;
            }

            if (hasPreviousValueX)
            {
                vars[x] = previousValueX;
            }
            if (hasPreviousValueY)
            {
                vars[y] = previousValueY;
            }

            return values;
        }

        public float[, ,] EvalInterval(Expression expr, Dictionary<string, Expression> vars,
                                        string x, float xStart, float xEnd, float xStep,
                                        string y, float yStart, float yEnd, float yStep,
                                        string z, float zStart, float zEnd, float zStep)
        {
            int nx = 0;
            int ny = 0;
            int nz = 0;

            float xx;
            float yy;
            float zz;

            List<Literal> xValues = new List<Literal>();
            List<Literal> yValues = new List<Literal>();
            List<Literal> zValues = new List<Literal>();

            nx = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                xValues.Add(new Literal(xx));
                nx++;
            }
            ny = 0;
            for (yy = yStart; yy <= yEnd; yy += yStep)
            {
                yValues.Add(new Literal(yy));
                ny++;
            }
            nz = 0;
            for (zz = zStart; zz <= zEnd; zz += zStep)
            {
                zValues.Add(new Literal(zz));
                nz++;
            }

            Expression previousValueX = null;
            bool hasPreviousValueX = false;
            if (vars.ContainsKey(x))
            {
                hasPreviousValueX = true;
                previousValueX = vars[x];
                vars.Remove(x);
            }

            Expression previousValueY = null;
            bool hasPreviousValueY = false;
            if (vars.ContainsKey(y))
            {
                hasPreviousValueY = true;
                previousValueY = vars[y];
                vars.Remove(y);
            }

            Expression previousValueZ = null;
            bool hasPreviousValueZ = false;
            if (vars.ContainsKey(z))
            {
                hasPreviousValueZ = true;
                previousValueZ = vars[z];
                vars.Remove(z);
            }

            Expression preeval = expr;//.PreliminaryEval(vars);
            //check that all variables in the expression are already in the variable table


            float[, ,] values = new float[nx, ny, nz];

            int ix;
            int iy;
            int iz;
            for (ix = 0; ix < nx; ix++)
            {
                vars[x] = xValues[ix];

                for (iy = 0; iy < ny; iy++)
                {
                    vars[y] = yValues[iy];

                    for (iz = 0; iz < nz; iz++)
                    {
                        vars[z] = zValues[iz];
                        values[ix, iy, iz] = preeval.Eval(vars).Value;
                    }

                }

            }

            if (hasPreviousValueX)
            {
                vars[x] = previousValueX;
            }
            if (hasPreviousValueY)
            {
                vars[y] = previousValueY;
            }
            if (hasPreviousValueZ)
            {
                vars[z] = previousValueZ;
            }

            return values;
        }

        public float[,] EvalMathPaint(Expression expr, Dictionary<string, Expression> vars, int width, int height)
        {
            //previous values?
            SolusParser parser = new SolusParser();
            vars["width"] = new Literal(width);
            vars["width"] = new Literal(width);
            vars["theta"] = parser.Compile("atan2(y,x)", vars);
            vars["radius"] = parser.Compile("sqrt(x^2+y^2)", vars);
            vars["i"] = new VariableAccess("x");
            vars["j"] = new VariableAccess("y");

            return EvalInterval(expr, vars, "x", 0, width - 1, 1, "y", 0, height - 1, 1);
        }


        public float[, ,] EvalMathPaint3D(Expression expr, Dictionary<string, Expression> vars, int width, int height, int numframes)
        {

            //previous values?
            SolusParser parser = new SolusParser();
            vars["width"] = new Literal(width);
            vars["height"] = new Literal(height);
            vars["theta"] = parser.Compile("atan2(y,x)", vars);
            vars["radius"] = parser.Compile("sqrt(x^2+y^2)", vars);
            vars["i"] = new VariableAccess("x");
            vars["j"] = new VariableAccess("y");
            vars["numframes"] = new Literal(numframes);
            vars["k"] = new VariableAccess("z");
            vars["t"] = new VariableAccess("z");

            return EvalInterval(expr, vars, "x", 0, width - 1, 1, "y", 0, height - 1, 1, "z", 0, numframes - 1, 1);
        }
    }
}
