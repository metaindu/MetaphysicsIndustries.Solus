using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class BiModulatorMatrixFilter : MatrixFilter
    {
        public BiModulatorMatrixFilter(BiModulator modulator)
        {
            _modulator = modulator;
        }

        BiModulator _modulator;

        public override Matrix Apply(Matrix input)
        {
            return Apply2(new Pair<Matrix>(input, input.CloneSize()));
        }

        public Matrix Apply2(Pair<Matrix> input)
        {
            int i;
            int j;

            Matrix output = input.First.CloneSize();

            for (i = 0; i < input.First.RowCount; i++)
            {
                for (j = 0; j < input.First.ColumnCount; j++)
                {
                    output[i, j] =
                        _modulator(
                            input.First[i, j],
                            input.Second[i, j]);
                }
            }

            return output;
        }
    }
}
