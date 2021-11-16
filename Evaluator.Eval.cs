
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus
{
    public partial class Evaluator
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
            if (env.ContainsVariable(x))
            {
                previousValue = env.GetVariable(x);
                env.RemoveVariable(x);
            }
            Expression preeval = expr.PreliminaryEval(env);

            i = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                env.SetVariable(x, new Literal(xx));
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
            if (env.ContainsVariable(x))
            {
                hasPreviousValue = true;
                previousValue = env.GetVariable(x);
                env.RemoveVariable(x);
            }
            Expression preeval = expr.PreliminaryEval(env);
            //check that all variables in the expression are already in the variable table


            float[] values = new float[i];

            i = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                env.SetVariable(x, new Literal(xx));
                values[i] = preeval.Eval(env).ToNumber().Value;
                i++;
            }

            if (hasPreviousValue)
            {
                env.SetVariable(x, previousValue);
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
            if (env.ContainsVariable(x))
            {
                hasPreviousValueX = true;
                previousValueX = env.GetVariable(x);
                env.RemoveVariable(x);
            }

            Expression previousValueY = null;
            bool hasPreviousValueY = false;
            if (env.ContainsVariable(y))
            {
                hasPreviousValueY = true;
                previousValueY = env.GetVariable(y);
                env.RemoveVariable(y);
            }

            Expression preeval = expr;//.PreliminaryEval(vars);
            //check that all variables in the expression are already in the variable table


            float[,] values = new float[nx, ny];

            int ix = 0;
            for (xx = xStart; xx <= xEnd; xx += xStep)
            {
                env.SetVariable(x, xValues[ix]);

                int iy = 0;
                for (yy = yStart; yy <= yEnd; yy += yStep)
                {
                    env.SetVariable(y, yValues[iy]);
                    values[ix, iy] = preeval.Eval(env).ToNumber().Value;
                    iy++;
                }

                ix++;
            }

            if (hasPreviousValueX)
            {
                env.SetVariable(x, previousValueX);
            }
            if (hasPreviousValueY)
            {
                env.SetVariable(y, previousValueY);
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
            if (env.ContainsVariable(x))
            {
                hasPreviousValueX = true;
                previousValueX = env.GetVariable(x);
                env.RemoveVariable(x);
            }

            Expression previousValueY = null;
            bool hasPreviousValueY = false;
            if (env.ContainsVariable(y))
            {
                hasPreviousValueY = true;
                previousValueY = env.GetVariable(y);
                env.RemoveVariable(y);
            }

            Expression previousValueZ = null;
            bool hasPreviousValueZ = false;
            if (env.ContainsVariable(z))
            {
                hasPreviousValueZ = true;
                previousValueZ = env.GetVariable(z);
                env.RemoveVariable(z);
            }

            Expression preeval = expr;//.PreliminaryEval(vars);
            //check that all variables in the expression are already in the variable table


            float[, ,] values = new float[nx, ny, nz];

            int ix;
            int iy;
            int iz;
            for (ix = 0; ix < nx; ix++)
            {
                env.SetVariable(x, xValues[ix]);

                for (iy = 0; iy < ny; iy++)
                {
                    env.SetVariable(y, yValues[iy]);

                    for (iz = 0; iz < nz; iz++)
                    {
                        env.SetVariable(z, zValues[iz]);
                        values[ix, iy, iz] = preeval.Eval(env).ToNumber().Value;
                    }

                }

            }

            if (hasPreviousValueX)
            {
                env.SetVariable(x, previousValueX);
            }
            if (hasPreviousValueY)
            {
                env.SetVariable(y, previousValueY);
            }
            if (hasPreviousValueZ)
            {
                env.SetVariable(z, previousValueZ);
            }

            return values;
        }

        public float[,] EvalMathPaint(Expression expr, SolusEnvironment env, int width, int height)
        {
            //previous values?
            SolusParser parser = new SolusParser();
            env.SetVariable("width", new Literal(width));
            env.SetVariable("width", new Literal(width));
            env.SetVariable("theta", parser.GetExpression("atan2(y,x)", env));
            env.SetVariable("radius", parser.GetExpression("sqrt(x^2+y^2)", env));
            env.SetVariable("i", new VariableAccess("x"));
            env.SetVariable("j", new VariableAccess("y"));

            return EvalInterval(expr, env, "x", 0, width - 1, 1, "y", 0, height - 1, 1);
        }


        public float[, ,] EvalMathPaint3D(Expression expr, SolusEnvironment env, int width, int height, int numframes)
        {

            //previous values?
            SolusParser parser = new SolusParser();
            env.SetVariable("width", new Literal(width));
            env.SetVariable("height", new Literal(height));
            env.SetVariable("theta", parser.GetExpression("atan2(y,x)", env));
            env.SetVariable("radius", parser.GetExpression("sqrt(x^2+y^2)", env));
            env.SetVariable("i", new VariableAccess("x"));
            env.SetVariable("j", new VariableAccess("y"));
            env.SetVariable("numframes", new Literal(numframes));
            env.SetVariable("k", new VariableAccess("z"));
            env.SetVariable("t", new VariableAccess("z"));

            return EvalInterval(expr, env, "x", 0, width - 1, 1, "y", 0, height - 1, 1, "z", 0, numframes - 1, 1);
        }
    }
}
