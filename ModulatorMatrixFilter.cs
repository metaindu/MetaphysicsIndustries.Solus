using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class ModulatorMatrixFilter : MatrixFilter
    {
        public override Matrix Apply(Matrix input)
        {
            Matrix output = input.CloneSize();
            int i;
            int j;

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    output[i, j] = Modulate(input[i, j]);
                }
            }

            return output;
        }

        public abstract double Modulate(double x);
    }
}
