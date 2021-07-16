
/*****************************************************************************
 *                                                                           *
 *  SolusEngine.Eval.cs                                                      *
 *  16 February 2010                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright (c) 2010-2021 Metaphysics Industries, Inc.                     *
 *                                                                           *
 *  Some helpful methods for evaluating expressions across intervals.        *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus
{
    public partial class SolusEngine
    {
        public Expression[] PreliminaryEvalInterval(Expression expr, SolusEnvironment env,
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
            if (env.Variables.ContainsKey(x))
            {
                previousValue = env.Variables[x];
                env.Variables.Remove(x);
            }
            Expression preeval = expr.PreliminaryEval(env);

            i = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                env.Variables[x] = new Literal(xx);
                exprs[i] = preeval.PreliminaryEval(env);
                i++;
            }

            return exprs;
        }

        public float[] EvalInterval(Expression expr, SolusEnvironment env,
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
            if (env.Variables.ContainsKey(x))
            {
                hasPreviousValue = true;
                previousValue = env.Variables[x];
                env.Variables.Remove(x);
            }
            Expression preeval = expr.PreliminaryEval(env);
            //check that all variables in the expression are already in the variable table


            float[] values = new float[i];

            i = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                env.Variables[x] = new Literal(xx);
                values[i] = preeval.Eval(env).Value;
                i++;
            }

            if (hasPreviousValue)
            {
                env.Variables[x] = previousValue;
            }

            return values;
        }

        public float[,] EvalInterval(Expression expr, SolusEnvironment env,
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
            if (env.Variables.ContainsKey(x))
            {
                hasPreviousValueX = true;
                previousValueX = env.Variables[x];
                env.Variables.Remove(x);
            }

            Expression previousValueY = null;
            bool hasPreviousValueY = false;
            if (env.Variables.ContainsKey(y))
            {
                hasPreviousValueY = true;
                previousValueY = env.Variables[y];
                env.Variables.Remove(y);
            }

            Expression preeval = expr;//.PreliminaryEval(vars);
            //check that all variables in the expression are already in the variable table


            float[,] values = new float[nx, ny];

            int ix = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                env.Variables[x] = xValues[ix];

                int iy = 0;
                for (yy = yStart; yy <= yEnd; yy += yStep)
                {
                    env.Variables[y] = yValues[iy];
                    values[ix, iy] = preeval.Eval(env).Value;
                    iy++;
                }

                ix++;
            }

            if (hasPreviousValueX)
            {
                env.Variables[x] = previousValueX;
            }
            if (hasPreviousValueY)
            {
                env.Variables[y] = previousValueY;
            }

            return values;
        }

        public float[, ,] EvalInterval(Expression expr, SolusEnvironment env,
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
            if (env.Variables.ContainsKey(x))
            {
                hasPreviousValueX = true;
                previousValueX = env.Variables[x];
                env.Variables.Remove(x);
            }

            Expression previousValueY = null;
            bool hasPreviousValueY = false;
            if (env.Variables.ContainsKey(y))
            {
                hasPreviousValueY = true;
                previousValueY = env.Variables[y];
                env.Variables.Remove(y);
            }

            Expression previousValueZ = null;
            bool hasPreviousValueZ = false;
            if (env.Variables.ContainsKey(z))
            {
                hasPreviousValueZ = true;
                previousValueZ = env.Variables[z];
                env.Variables.Remove(z);
            }

            Expression preeval = expr;//.PreliminaryEval(vars);
            //check that all variables in the expression are already in the variable table


            float[, ,] values = new float[nx, ny, nz];

            int ix;
            int iy;
            int iz;
            for (ix = 0; ix < nx; ix++)
            {
                env.Variables[x] = xValues[ix];

                for (iy = 0; iy < ny; iy++)
                {
                    env.Variables[y] = yValues[iy];

                    for (iz = 0; iz < nz; iz++)
                    {
                        env.Variables[z] = zValues[iz];
                        values[ix, iy, iz] = preeval.Eval(env).Value;
                    }

                }

            }

            if (hasPreviousValueX)
            {
                env.Variables[x] = previousValueX;
            }
            if (hasPreviousValueY)
            {
                env.Variables[y] = previousValueY;
            }
            if (hasPreviousValueZ)
            {
                env.Variables[z] = previousValueZ;
            }

            return values;
        }

        public float[,] EvalMathPaint(Expression expr, SolusEnvironment env, int width, int height)
        {
            //previous values?
            SolusParser parser = new SolusParser();
            env.Variables["width"] = new Literal(width);
            env.Variables["width"] = new Literal(width);
            env.Variables["theta"] = parser.GetExpression("atan2(y,x)", env);
            env.Variables["radius"] = parser.GetExpression("sqrt(x^2+y^2)", env);
            env.Variables["i"] = new VariableAccess("x");
            env.Variables["j"] = new VariableAccess("y");

            return EvalInterval(expr, env, "x", 0, width - 1, 1, "y", 0, height - 1, 1);
        }


        public float[, ,] EvalMathPaint3D(Expression expr, SolusEnvironment env, int width, int height, int numframes)
        {

            //previous values?
            SolusParser parser = new SolusParser();
            env.Variables["width"] = new Literal(width);
            env.Variables["height"] = new Literal(height);
            env.Variables["theta"] = parser.GetExpression("atan2(y,x)", env);
            env.Variables["radius"] = parser.GetExpression("sqrt(x^2+y^2)", env);
            env.Variables["i"] = new VariableAccess("x");
            env.Variables["j"] = new VariableAccess("y");
            env.Variables["numframes"] = new Literal(numframes);
            env.Variables["k"] = new VariableAccess("z");
            env.Variables["t"] = new VariableAccess("z");

            return EvalInterval(expr, env, "x", 0, width - 1, 1, "y", 0, height - 1, 1, "z", 0, numframes - 1, 1);
        }
    }
}
