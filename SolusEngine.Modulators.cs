
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

namespace MetaphysicsIndustries.Solus
{
    public partial class SolusEngine
    {
        public static double Convert24gToFloat(double value)
        {
            value = Math.Max(0, Math.Min(0xFFFFFF, value));
            double r = ((int)(value) & 0x000000FF) / 255.0;
            double g = (((int)(value) & 0x0000FF00) >> 8) / 255.0;
            double b = (((int)(value) & 0x00FF0000) >> 16) / 255.0;
            return (r + g + b) / 3;
        }

        public static double ConvertFloatTo24g(double value)
        {
            value = Math.Max(0, Math.Min(1, value));

            int b = (int)(value * 255) & 0xFF;
            int g = b << 8;
            int r = g << 8;

            return r | g | b;
        }

        public static double Convert24cTo24g(double value)
        {
            return ConvertFloatTo24g(Convert24gToFloat(value));
        }

        public class MultiplyModulator
        {
            public MultiplyModulator(double factor) { _factor = factor; }
            private double _factor;
            public double Modulate(double value) { return value * _factor; }
        }

        public class AdditionModulator
        {
            public AdditionModulator(double factor) { _factor = factor; }
            private double _factor;
            public double Modulate(double value) { return value + _factor; }
        }

        public class MaximumModulator
        {
            public MaximumModulator(double factor) { _factor = factor; }
            private double _factor;
            public double Modulate(double value) { return Math.Max(value, _factor); }
        }

        public class MinimumModulator
        {
            public MinimumModulator(double factor) { _factor = factor; }
            private double _factor;
            public double Modulate(double value) { return Math.Min(value, _factor); }
        }

        public static double AdditionBiMod(double x, double y)
        {
            return x + y;
        }

        public static double MultiplicationBiMod(double x, double y)
        {
            return x * y;
        }

        public static double ConvertNegOneOneToZeroOne(double x)
        {
            //convert a number on the interval of [-1,1] to [0,1]
            return (x + 1) / 2;
        }

        public static double ConvertZeroOneToNegOneOne(double x)
        {
            //convert a number on the interval of [0,1] to [-1,1]
            return x * 2 - 1;
        }

        public static Pair<double> ConvertNegOneOneToZeroOne(double x, double y)
        {
            return new Pair<double>(
                ConvertNegOneOneToZeroOne(x),
                ConvertNegOneOneToZeroOne(y));
        }

        public static Pair<double> ConvertZeroOneToNegOneOne(double x, double y)
        {
            return new Pair<double>(
                ConvertZeroOneToNegOneOne(x),
                ConvertZeroOneToNegOneOne(y));
        }

        public static Pair<double> ConvertEuclideanToPolar(double x, double y)
        {
            Pair<double> pair = new Pair<double>();
            pair.First = Math.Sqrt(x * x + y * y);
            pair.Second = Math.Atan2(y, x);
            return pair;
        }

        public static Pair<double> ConvertPolarToEuclidean(double r, double theta)
        {
            Pair<double> pair = new Pair<double>();
            pair.First = r * Math.Cos(theta);
            pair.Second = r * Math.Sin(theta);
            return pair;
        }

        public static double ConvertDegreesToRadians(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            return 180.0 * radians / Math.PI;
        }

        public static double ComplexMagnitude(double real, double imaginary)
        {
            return Math.Sqrt(real * real + imaginary * imaginary);
        }

        public static double ComplexPhase(double real, double imaginary)
        {
            return Math.Atan2(imaginary, real);
        }

        public static double IntervalFit(double value, double min, double max)
        {
            return (value - min) / (max - min);
        }

        public static double MeanSquareError(Matrix a, Matrix b)
        {
            if (a == null) { throw new ArgumentNullException("a"); }
            if (b == null) { throw new ArgumentNullException("b"); }
            if (a.RowCount != b.RowCount ||
                a.ColumnCount != b.ColumnCount)
            {
                throw new ArgumentException("Matrix sizes do not match", "matrixToCompare");
            }

            int i;
            int j;

            double sum = 0;
            double v;

            for (i = 0; i < a.RowCount; i++)
            {
                for (j = 0; j < a.ColumnCount; j++)
                {
                    v = a[i, j] - b[i, j];
                    sum += v * v;
                }
            }

            sum /= a.RowCount;
            sum /= a.ColumnCount;

            return sum;
        }

        public static double MaxError(Matrix a, Matrix b)
        {
            if (a == null) { throw new ArgumentNullException("a"); }
            if (b == null) { throw new ArgumentNullException("b"); }
            if (a.RowCount != b.RowCount ||
                a.ColumnCount != b.ColumnCount)
            {
                throw new ArgumentException("Matrix sizes do not match", "matrixToCompare");
            }

            int i;
            int j;

            double max = 0;

            for (i = 0; i < a.RowCount; i++)
            {
                for (j = 0; j < a.ColumnCount; j++)
                {
                    max = Math.Max(Math.Abs(a[i, j] - b[i, j]), max);
                }
            }

            return max;
        }

        public static Triple<double> ConvertRgbToHsl(Triple<double> rgb)
        {
            double r = rgb.First;
            double g = rgb.Second;
            double b = rgb.Third;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));

            double h;
            double s;
            double l;

            if (max == min) { h = 0; }
            else if (max == r)
            {
                h = (g - b) / 6.0;
                if (g < b) { h += 1; }
            }
            else if (max == g) { h = (b - r + 2) / 6.0; }
            else { h = (r - g + 4) / 6.0; }

            l = (max+min)/2;

            if (max == min) { s = 0; }
            else if (l <= 0.5) { s = (max - min) / (2 * l); }
            else { s = (max - min) / (2 - 2 * l); }

            return new Triple<double>(h, s, l);
        }

        public static Triple<double> ConvertHslToRgb(Triple<double> hsl)
        {
            double h = hsl.First;
            double s = hsl.Second;
            double l = hsl.Third;

            double q;
            double p;
            double tr = h + 1 / 3.0;
            double tg = h;
            double tb = h - 1 / 3.0;

            if (tr > 1) { tr -= 1; }
            if (tb < 1) { tb += 1; }

            if (l < 0.5) { q = l * (1 + s); }
            else { q = l + s - l * s; }

            p = 2 * l - q;

            double r;
            double g;
            double b;

            r = CalcHslToRgbConversion(q, p, tr);
            g = CalcHslToRgbConversion(q, p, tg);
            b = CalcHslToRgbConversion(q, p, tb);

            return new Triple<double>(r, g, b);
        }

        public static double CalcHslToRgbConversion(double q, double p, double t)
        {

            double c;
            if (t < 1 / 6.0) { c = q + ((q - p) * 6 * t); }
            else if (t < 0.5) { c = q; }
            else if (t < 2 / 3.0) { c = p + ((q - p) * 6 * ((2 / 3.0) - t)); }
            else { c = p; }
            return c;
        }
    }
}
