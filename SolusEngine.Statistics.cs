
/*****************************************************************************
 *                                                                           *
 *  SolusEngine.Statistics.cs                                                *
 *  16 April 2008                                                            *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
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
        public static float CalculateVariance(IEnumerable<float> measures)
        {
            float mean = CalculateMean(measures);
            return CalculateVariance(measures, mean);
        }

        public static float CalculateVariance(IEnumerable<float> measures, float mean)
        {

            float variance = 0;
            int count = -1;

            foreach (float measure in measures)
            {
                float value = measure - mean;
                variance += value * value;
                count++;
            }

            if (count < 1)
            {
                return 0;
            }
            else
            {
                return variance / count;
            }
        }

        public static float CalculateVariance(IList<float> measures, float mean, int startIndex, int count)
        {
            int i;
            float sum = 0;
            for (i = startIndex; i < startIndex + count; i++)
            {
                float value = measures[i] - mean;
                sum += value * value;
            }
            float variance = sum / (count - 1);
            return variance;
        }

        public static float CalculateMean(IEnumerable<float> measures)
        {
            float mean = 0;
            int count = 0;
            foreach (float measure in measures)
            {
                mean += measure;
                count++;
            }
            mean /= count;
            return mean;
        }

        public static float CalculateMean(IList<float> measures, int startIndex, int count)
        {
            int i;
            float sum = 0;
            for (i = startIndex; i < startIndex + count; i++)
            {
                sum += measures[i];
            }
            float mean = sum / count;
            return mean;
        }

        public static float CalculateNormalDistributionOverlap(float mean1, float variance1, float mean2, float variance2)
        {
            float a = variance1 + variance2;
            float b = -2 * (variance1*mean2 +variance2*mean1);
            float c = (float)(variance1 * mean2 * mean2 + 
                        variance2 * mean1 * mean1 - 
                        variance1 * variance2 * System.Math.Log(System.Math.Sqrt(variance1 / variance2)));

            Pair<float> x = QuadraticEquation(a, b, c);

            return 0;
        }

        public static Pair<float> QuadraticEquation(float a, float b, float c)
        {
            float d = (float)System.Math.Sqrt(b * b - 4 * a * c);
            float x1 = (-b + d) / (2 * a);
            float x2 = (-b - d) / (2 * a);

            return new Pair<float>(x1, x2);
        }
    }
}
