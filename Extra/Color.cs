
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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

namespace MetaphysicsIndustries.Solus.Extra
{
    public class Color
    {
        public static float Convert24gToFloat(float value)
        {
            value = Math.Max(0, Math.Min(0xFFFFFF, value));
            double r = ((int)(value) & 0x000000FF) / 255.0;
            double g = (((int)(value) & 0x0000FF00) >> 8) / 255.0;
            double b = (((int)(value) & 0x00FF0000) >> 16) / 255.0;
            return (float)((r + g + b) / 3);
        }

        public static float ConvertFloatTo24g(float value)
        {
            value = Math.Max(0, Math.Min(1, value));

            int b = (int)(value * 255) & 0xFF;
            int g = b << 8;
            int r = g << 8;

            return r | g | b;
        }

        public static float Convert24cTo24g(float value)
        {
            return ConvertFloatTo24g(Convert24gToFloat(value));
        }

        public static float ConvertRgbTo24cTriModulator(float r, float g,
            float b)
        {
            r = Math.Max(0, Math.Min(1, r));
            g = Math.Max(0, Math.Min(1, g));
            b = Math.Max(0, Math.Min(1, b));

            int rr = ((int)(r * 255) & 0xFF) << 16;
            int gg = ((int)(g * 255) & 0xFF) << 8;
            int bb = (int)(b * 255) & 0xFF;

            return rr | gg | bb;
        }

        public static STuple<float, float, float> ConvertRgbToHsl(
            STuple<float, float, float> rgb)
        {
            float r = rgb.Value1;
            float g = rgb.Value2;
            float b = rgb.Value3;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));

            float h;
            float s;
            float l;

            if (max == min)
            {
                h = 0;
            }
            else if (max == r)
            {
                h = (g - b) / 6.0f;
                if (g < b)
                {
                    h += 1;
                }
            }
            else if (max == g)
            {
                h = (b - r + 2) / 6.0f;
            }
            else
            {
                h = (r - g + 4) / 6.0f;
            }

            l = (max + min) / 2;

            if (max == min)
            {
                s = 0;
            }
            else if (l <= 0.5)
            {
                s = (max - min) / (2 * l);
            }
            else
            {
                s = (max - min) / (2 - 2 * l);
            }

            return new STuple<float, float, float>(h, s, l);
        }

        public static STuple<float, float, float> ConvertHslToRgb(
            STuple<float, float, float> hsl)
        {
            float h = hsl.Value1;
            float s = hsl.Value2;
            float l = hsl.Value3;

            float q;
            float p;
            float tr = h + 1 / 3.0f;
            float tg = h;
            float tb = h - 1 / 3.0f;

            if (tr > 1)
            {
                tr -= 1;
            }

            if (tb < 1)
            {
                tb += 1;
            }

            if (l < 0.5)
            {
                q = l * (1 + s);
            }
            else
            {
                q = l + s - l * s;
            }

            p = 2 * l - q;

            float r;
            float g;
            float b;

            r = CalcHslToRgbConversion(q, p, tr);
            g = CalcHslToRgbConversion(q, p, tg);
            b = CalcHslToRgbConversion(q, p, tb);

            return new STuple<float, float, float>(r, g, b);
        }

        public static float CalcHslToRgbConversion(float q, float p, float t)
        {

            float c;
            if (t < 1 / 6.0)
            {
                c = q + ((q - p) * 6 * t);
            }
            else if (t < 0.5)
            {
                c = q;
            }
            else if (t < 2 / 3.0)
            {
                c = p + ((q - p) * 6 * ((2 / 3.0f) - t));
            }
            else
            {
                c = p;
            }

            return c;
        }
    }
}
