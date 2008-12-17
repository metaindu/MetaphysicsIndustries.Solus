using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class PlotVectorExpression : Expression
    {
        public PlotVectorExpression(SolusVector vector)
        {
            _vector = vector;
        }

        private SolusVector _vector;
        public SolusVector Vector
        {
            get { return _vector; }
        }


        public override Literal Eval(VariableTable varTable)
        {
            return Vector.Eval(varTable);
        }

        public override Expression Clone()
        {
            return new PlotVectorExpression(Vector);
        }
    }
}
