
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
using System.Linq;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class CatmullRomSpline : Function
    {
        public CatmullRomSpline(IEnumerable<float> times,
            IEnumerable<float> values)
            : base(paramTypes: new Types[] {Solus.Values.Types.Scalar})
        {
            var times2 = times.ToList();
            var values2 = values.ToList();

            if (times2.Count > values2.Count)
            {
                times2 = times2.Take(values2.Count).ToList();
            }
            else if (times2.Count < values2.Count)
            {
                values2 = values2.Take(times2.Count).ToList();
            }

            var indexes = Enumerable.Range(0, times2.Count).ToList();
            indexes.Sort((x, y) => Comparer<float>.Default.Compare(times2[x], times2[y]));
            times2 = indexes.Select(i => times2[i]).ToList();
            values2 = indexes.Select(i => values2[i]).ToList();

            Times = times2.ToArray();
            Values = values2.ToArray();
        }

        readonly float[] Times;
        readonly float[] Values;
            
        protected override IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args)
        {
            return Evaluate(args[0].ToNumber().Value).ToNumber();
        }

        public float Evaluate(float time)
        {
            int i;
            for (i = 0; i < Times.Length; i++)
            {
                if (Times[i] > time) break;
            }

            if (i == Times.Length) return Values[Values.Length - 1];
            if (i == 0) return Values[0];

            float t0, t1, t2, t3;
            float p0, p1, p2, p3;

            p1 = Values[i - 1];
            t1 = Times[i - 1];
            p2 = Values[i - 0];
            t2 = Times[i - 0];

            if (i == 1)
            {
                p0 = p1 - (p2-p1);
                t0 = t1 - (t2 - t1);
            }
            else
            {
                p0 = Values[i - 2];
                t0 = Times[i - 2];
            }

            if (i == Times.Length - 1)
            {
                p3 = p2 + (p2 - p1);
                t3 = t2 + (t2 - t1);
            }
            else
            {
                p3 = Values[i + 1];
                t3 = Times[i + 1];
            }

            var tlen = t2 - t1;
            var s0 = (t0 - t1) / tlen;
            var s1 = (t1 - t1) / tlen;
            var s2 = (t2 - t1) / tlen;
            var s3 = (t3 - t1) / tlen;

            var s = (time - t1) / tlen;

            var m1 = (p2 - p0) / (s2 - s0);
            var m2 = (p3 - p1) / (s3 - s1);

            var ss = s * s;
            var sss = ss * s;
            var v =
                (2 * sss - 3 * ss + 1) * p1 +
                (sss - 2 * ss + s) * m1 +
                (-2 * sss + 3 * ss) * p2 +
                (sss - ss) * m2;

            return v;
        }
    }
}

