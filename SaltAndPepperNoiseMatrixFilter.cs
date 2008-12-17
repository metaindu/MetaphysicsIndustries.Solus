using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SaltAndPepperNoiseMatrixFilter : MatrixFilter
    {
        public SaltAndPepperNoiseMatrixFilter(double probability)
            :  this(probability, 0, 1)
        {
        }

        public SaltAndPepperNoiseMatrixFilter(double probability, double lowValue, double highValue)
        {
            _probability = probability;
            _lowValue = lowValue;
            _highValue = highValue;
        }

        private double _probability;
        private double _lowValue;
        private double _highValue;

        public override Matrix Apply(Matrix input)
        {
            int i;
            int j;
            double randomNumber;
            double isHighOrLow;
            Matrix ret = new Matrix(input.RowCount, input.ColumnCount);

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    randomNumber = _rand.NextDouble();

                    if (randomNumber < _probability)
                    {
                        isHighOrLow = _rand.NextDouble();

                        ret[i, j] = isHighOrLow > 0.5 ? _highValue : _lowValue;//  _lowValue + y * (_highValue - _lowValue);
                    }
                    else
                    {
                        ret[i, j] = input[i, j];
                    }
                }
            }

            return ret;
        }
    }
}
