
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

namespace MetaphysicsIndustries.Solus.Extra
{
    public static class PascalTriangle
    {
        public static float BinomialCoefficient(int n, int k)
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

        public static float[] PascalsTriangle(int row)
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
