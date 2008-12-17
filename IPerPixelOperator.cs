using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public interface IPerPixelOperator
    {
        int GetExtraWidth(Matrix input);
        int GetExtraHeight(Matrix input);
        void SetValues(double[,] values);

        double Operate(int row, int column);
    }
}
