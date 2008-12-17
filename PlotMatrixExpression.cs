using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class PlotMatrixExpression : Expression
    {
        public PlotMatrixExpression(SolusMatrix matrix)
        {
            _matrix = matrix;
        }

        private SolusMatrix _matrix;
        public SolusMatrix Matrix
        {
            get { return _matrix; }
        }


        public override Literal Eval(VariableTable varTable)
        {
            return Matrix.Eval(varTable);
        }

        public override Expression Clone()
        {
            return new PlotMatrixExpression(Matrix);
        }
    }
}
