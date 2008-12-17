
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
        public static double CalculateVariance(IEnumerable<double> measures)
        {
            double mean = CalculateMean(measures);
            return CalculateVariance(measures, mean);
        }

        public static double CalculateVariance(IEnumerable<double> measures, double mean)
        {

            double variance = 0;
            int count = -1;

            foreach (double measure in measures)
            {
                double value = measure - mean;
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

        public static double CalculateVariance(IList<double> measures, double mean, int startIndex, int count)
        {
            int i;
            double sum = 0;
            for (i = startIndex; i < startIndex + count; i++)
            {
                double value = measures[i] - mean;
                sum += value * value;
            }
            double variance = sum / (count - 1);
            return variance;
        }

        public static double CalculateMean(IEnumerable<double> measures)
        {
            double mean = 0;
            int count = 0;
            foreach (double measure in measures)
            {
                mean += measure;
                count++;
            }
            mean /= count;
            return mean;
        }

        public static double CalculateMean(IList<double> measures, int startIndex, int count)
        {
            int i;
            double sum = 0;
            for (i = startIndex; i < startIndex + count; i++)
            {
                sum += measures[i];
            }
            double mean = sum / count;
            return mean;
        }

        public static double CalculateNormalDistributionOverlap(double mean1, double variance1, double mean2, double variance2)
        {
            double a = variance1 + variance2;
            double b = -2 * (variance1*mean2 +variance2*mean1);
            double c = variance1 * mean2 * mean2 + 
                        variance2 * mean1 * mean1 - 
                        variance1 * variance2 * Math.Log(Math.Sqrt(variance1 / variance2));

            Pair<double> x = QuadraticEquation(a, b, c);

            return 0;
        }

        public static Pair<double> QuadraticEquation(double a, double b, double c)
        {
            double d = Math.Sqrt(b * b - 4 * a * c);
            double x1 = (-b + d) / (2 * a);
            double x2 = (-b - d) / (2 * a);

            return new Pair<double>(x1, x2);
        }
    }
}
