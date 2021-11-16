
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

using System.Diagnostics;

namespace MetaphysicsIndustries.Solus
{
    //public partial class Evaluator
    //{
    //    public static float Convert24gToFloat(float value)
    //    {
    //        value = Math.Max(0, Math.Min(0xFFFFFF, value));
    //        float r = ((int)(value) & 0x000000FF) / 255.0;
    //        float g = (((int)(value) & 0x0000FF00) >> 8) / 255.0;
    //        float b = (((int)(value) & 0x00FF0000) >> 16) / 255.0;
    //        return (r + g + b) / 3;
    //    }

    //    public static float ConvertFloatTo24g(float value)
    //    {
    //        value = Math.Max(0, Math.Min(1, value));

    //        int b = (int)(value * 255) & 0xFF;
    //        int g = b << 8;
    //        int r = g << 8;

    //        return r | g | b;
    //    }

    //    public static float Convert24cTo24g(float value)
    //    {
    //        return ConvertFloatTo24g(Convert24gToFloat(value));
    //    }

    //    public static float ConvertRgbTo24cTriModulator(float r, float g, float b)
    //    {
    //        r = Math.Max(0, Math.Min(1, r));
    //        g = Math.Max(0, Math.Min(1, g));
    //        b = Math.Max(0, Math.Min(1, b));

    //        int rr = ((int)(r * 255) & 0xFF)<<16;
    //        int gg = ((int)(g * 255) & 0xFF)<<8;
    //        int bb = (int)(b * 255) & 0xFF;

    //        return rr | gg | bb;
    //    }

    //    public class MultiplyModulator
    //    {
    //        public MultiplyModulator(float factor) { _factor = factor; }
    //        private float _factor;
    //        public float Modulate(float value) { return value * _factor; }
    //    }

    //    public class AdditionModulator
    //    {
    //        public AdditionModulator(float factor) { _factor = factor; }
    //        private float _factor;
    //        public float Modulate(float value) { return value + _factor; }
    //    }

    //    public class MaximumModulator
    //    {
    //        public MaximumModulator(float factor) { _factor = factor; }
    //        private float _factor;
    //        public float Modulate(float value) { return Math.Max(value, _factor); }
    //    }

    //    public class MinimumModulator
    //    {
    //        public MinimumModulator(float factor) { _factor = factor; }
    //        private float _factor;
    //        public float Modulate(float value) { return Math.Min(value, _factor); }
    //    }

    //    public static float AdditionBiMod(float x, float y)
    //    {
    //        return x + y;
    //    }

    //    public static float MultiplicationBiMod(float x, float y)
    //    {
    //        return x * y;
    //    }

    //    public static float ConvertNegOneOneToZeroOne(float x)
    //    {
    //        //convert a number on the interval of [-1,1] to [0,1]
    //        return (x + 1) / 2;
    //    }

    //    public static float ConvertZeroOneToNegOneOne(float x)
    //    {
    //        //convert a number on the interval of [0,1] to [-1,1]
    //        return x * 2 - 1;
    //    }

    //    public static Pair<float> ConvertNegOneOneToZeroOne(float x, float y)
    //    {
    //        return new Pair<float>(
    //            ConvertNegOneOneToZeroOne(x),
    //            ConvertNegOneOneToZeroOne(y));
    //    }

    //    public static Pair<float> ConvertZeroOneToNegOneOne(float x, float y)
    //    {
    //        return new Pair<float>(
    //            ConvertZeroOneToNegOneOne(x),
    //            ConvertZeroOneToNegOneOne(y));
    //    }

    //    public static Pair<float> ConvertEuclideanToPolar(float x, float y)
    //    {
    //        Pair<float> pair = new Pair<float>();
    //        pair.First = Math.Sqrt(x * x + y * y);
    //        pair.Second = Math.Atan2(y, x);
    //        return pair;
    //    }

    //    public static Pair<float> ConvertPolarToEuclidean(float r, float theta)
    //    {
    //        Pair<float> pair = new Pair<float>();
    //        pair.First = r * Math.Cos(theta);
    //        pair.Second = r * Math.Sin(theta);
    //        return pair;
    //    }

    //    public static float ConvertDegreesToRadians(float degrees)
    //    {
    //        return Math.PI * degrees / 180.0;
    //    }

    //    public static float ConvertRadiansToDegrees(float radians)
    //    {
    //        return 180.0 * radians / Math.PI;
    //    }

    //    public static float ComplexMagnitude(float real, float imaginary)
    //    {
    //        return Math.Sqrt(real * real + imaginary * imaginary);
    //    }

    //    public static float ComplexPhase(float real, float imaginary)
    //    {
    //        return Math.Atan2(imaginary, real);
    //    }

    //    public static float IntervalFit(float value, float min, float max)
    //    {
    //        return (value - min) / (max - min);
    //    }

    //    public static float MeanSquareError(Matrix a, Matrix b)
    //    {
    //        if (a == null) { throw new ArgumentNullException("a"); }
    //        if (b == null) { throw new ArgumentNullException("b"); }
    //        if (a.RowCount != b.RowCount ||
    //            a.ColumnCount != b.ColumnCount)
    //        {
    //            throw new ArgumentException("Matrix sizes do not match", "matrixToCompare");
    //        }

    //        int i;
    //        int j;

    //        float sum = 0;
    //        float rect;

    //        for (i = 0; i < a.RowCount; i++)
    //        {
    //            for (j = 0; j < a.ColumnCount; j++)
    //            {
    //                rect = a[i, j] - b[i, j];
    //                sum += rect * rect;
    //            }
    //        }

    //        sum /= a.RowCount;
    //        sum /= a.ColumnCount;

    //        return sum;
    //    }

    //    public static float MaxError(Matrix a, Matrix b)
    //    {
    //        if (a == null) { throw new ArgumentNullException("a"); }
    //        if (b == null) { throw new ArgumentNullException("b"); }
    //        if (a.RowCount != b.RowCount ||
    //            a.ColumnCount != b.ColumnCount)
    //        {
    //            throw new ArgumentException("Matrix sizes do not match", "matrixToCompare");
    //        }

    //        int i;
    //        int j;

    //        float max = 0;

    //        for (i = 0; i < a.RowCount; i++)
    //        {
    //            for (j = 0; j < a.ColumnCount; j++)
    //            {
    //                max = Math.Max(Math.Abs(a[i, j] - b[i, j]), max);
    //            }
    //        }

    //        return max;
    //    }

    //    public static Triple<float> ConvertRgbToHsl(Triple<float> rgb)
    //    {
    //        float r = rgb.First;
    //        float g = rgb.Second;
    //        float b = rgb.Third;

    //        float max = Math.Max(r, Math.Max(g, b));
    //        float min = Math.Min(r, Math.Min(g, b));

    //        float h;
    //        float s;
    //        float l;

    //        if (max == min) { h = 0; }
    //        else if (max == r)
    //        {
    //            h = (g - b) / 6.0;
    //            if (g < b) { h += 1; }
    //        }
    //        else if (max == g) { h = (b - r + 2) / 6.0; }
    //        else { h = (r - g + 4) / 6.0; }

    //        l = (max+min)/2;

    //        if (max == min) { s = 0; }
    //        else if (l <= 0.5) { s = (max - min) / (2 * l); }
    //        else { s = (max - min) / (2 - 2 * l); }

    //        return new Triple<float>(h, s, l);
    //    }

    //    public static Triple<float> ConvertHslToRgb(Triple<float> hsl)
    //    {
    //        float h = hsl.First;
    //        float s = hsl.Second;
    //        float l = hsl.Third;

    //        float q;
    //        float p;
    //        float tr = h + 1 / 3.0;
    //        float tg = h;
    //        float tb = h - 1 / 3.0;

    //        if (tr > 1) { tr -= 1; }
    //        if (tb < 1) { tb += 1; }

    //        if (l < 0.5) { q = l * (1 + s); }
    //        else { q = l + s - l * s; }

    //        p = 2 * l - q;

    //        float r;
    //        float g;
    //        float b;

    //        r = CalcHslToRgbConversion(q, p, tr);
    //        g = CalcHslToRgbConversion(q, p, tg);
    //        b = CalcHslToRgbConversion(q, p, tb);

    //        return new Triple<float>(r, g, b);
    //    }

    //    public static float CalcHslToRgbConversion(float q, float p, float t)
    //    {

    //        float c;
    //        if (t < 1 / 6.0) { c = q + ((q - p) * 6 * t); }
    //        else if (t < 0.5) { c = q; }
    //        else if (t < 2 / 3.0) { c = p + ((q - p) * 6 * ((2 / 3.0) - t)); }
    //        else { c = p; }
    //        return c;
    //    }
    //}
}
