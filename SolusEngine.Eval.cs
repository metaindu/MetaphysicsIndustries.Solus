
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
        public Expression[] PreliminaryEvalInterval(Expression expr, VariableTable vars,
                                                    Variable x, float xStart, float xEnd, float xStep)
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
            bool hasPreviousValue = false;
            if (vars.Contains(x))
            {
                hasPreviousValue = true;
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

        public float[] EvalInterval(Expression expr, VariableTable vars,
                                        Variable x, float xStart, float xEnd, float xStep)
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
            if (vars.Contains(x))
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

        public float[,] EvalInterval(Expression expr, VariableTable vars,
                                        Variable x, float xStart, float xEnd, float xStep,
                                        Variable y, float yStart, float yEnd, float yStep)
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
            if (vars.Contains(x))
            {
                hasPreviousValueX = true;
                previousValueX = vars[x];
                vars.Remove(x);
            }

            Expression previousValueY = null;
            bool hasPreviousValueY = false;
            if (vars.Contains(y))
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

        public float[, ,] EvalInterval(Expression expr, VariableTable vars,
                                        Variable x, float xStart, float xEnd, float xStep,
                                        Variable y, float yStart, float yEnd, float yStep,
                                        Variable z, float zStart, float zEnd, float zStep)
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
            if (vars.Contains(x))
            {
                hasPreviousValueX = true;
                previousValueX = vars[x];
                vars.Remove(x);
            }

            Expression previousValueY = null;
            bool hasPreviousValueY = false;
            if (vars.Contains(y))
            {
                hasPreviousValueY = true;
                previousValueY = vars[y];
                vars.Remove(y);
            }

            Expression previousValueZ = null;
            bool hasPreviousValueZ = false;
            if (vars.Contains(z))
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

        public float[,] EvalMathPaint(Expression expr, VariableTable vars, int width, int height)
        {
            if (!vars.ContainsKey("x"))
            {
                vars.Add("x", new Variable("x"));
            }
            if (!vars.ContainsKey("y"))
            {
                vars.Add("y", new Variable("y"));
            }

            if (!vars.ContainsKey("width")) { vars.Add("width", new Variable("width")); }
            if (!vars.ContainsKey("height")) { vars.Add("height", new Variable("height")); }
            if (!vars.ContainsKey("theta")) { vars.Add("theta", new Variable("theta")); }
            if (!vars.ContainsKey("radius")) { vars.Add("radius", new Variable("radius")); }
            if (!vars.ContainsKey("i")) { vars.Add("i", new Variable("i")); }
            if (!vars.ContainsKey("j")) { vars.Add("j", new Variable("j")); }

            //previous values?
            vars[vars["width"]] = new Literal(width);
            vars[vars["width"]] = new Literal(width);
            vars[vars["theta"]] = SolusParser.Compile("atan2(y,x)", vars);
            vars[vars["radius"]] = SolusParser.Compile("sqrt(x^2+y^2)", vars);
            vars[vars["i"]] = new VariableAccess(vars["x"]);
            vars[vars["j"]] = new VariableAccess(vars["y"]);

            return EvalInterval(expr, vars, vars["x"], 0, width - 1, 1, vars["y"], 0, height - 1, 1);
        }


        public float[, ,] EvalMathPaint3D(Expression expr, VariableTable vars, int width, int height, int numframes)
        {
            if (!vars.ContainsKey("x"))
            {
                vars.Add("x", new Variable("x"));
            }
            if (!vars.ContainsKey("y"))
            {
                vars.Add("y", new Variable("y"));
            }
            if (!vars.ContainsKey("z"))
            {
                vars.Add("z", new Variable("z"));
            }

            if (!vars.ContainsKey("width")) { vars.Add("width", new Variable("width")); }
            if (!vars.ContainsKey("height")) { vars.Add("height", new Variable("height")); }
            if (!vars.ContainsKey("theta")) { vars.Add("theta", new Variable("theta")); }
            if (!vars.ContainsKey("radius")) { vars.Add("radius", new Variable("radius")); }
            if (!vars.ContainsKey("i")) { vars.Add("i", new Variable("i")); }
            if (!vars.ContainsKey("j")) { vars.Add("j", new Variable("j")); }
            if (!vars.ContainsKey("numframes")) { vars.Add("numframes", new Variable("numframes")); }
            if (!vars.ContainsKey("k")) { vars.Add("k", new Variable("k")); }
            if (!vars.ContainsKey("t")) { vars.Add("t", new Variable("t")); }

            //previous values?
            vars[vars["width"]] = new Literal(width);
            vars[vars["height"]] = new Literal(height);
            vars[vars["theta"]] = SolusParser.Compile("atan2(y,x)", vars);
            vars[vars["radius"]] = SolusParser.Compile("sqrt(x^2+y^2)", vars);
            vars[vars["i"]] = new VariableAccess(vars["x"]);
            vars[vars["j"]] = new VariableAccess(vars["y"]);
            vars[vars["numframes"]] = new Literal(numframes);
            vars[vars["k"]] = new VariableAccess(vars["z"]);
            vars[vars["t"]] = new VariableAccess(vars["z"]);

            return EvalInterval(expr, vars, vars["x"], 0, width - 1, 1, vars["y"], 0, height - 1, 1, vars["z"], 0, numframes - 1, 1);
        }
    }
}
