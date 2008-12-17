using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class VariableRotateCoordinatesMatrixFilter : RotateCoordinatesMatrixFilter
    {
        public VariableRotateCoordinatesMatrixFilter(VariableTable varTable, Variable variable)
            : base(0)
        {
            if (varTable == null) { throw new ArgumentNullException("varTable"); }
            if (variable == null) { throw new ArgumentNullException("variable"); }

            _varTable = varTable;
            _variable = variable;
        }

        private VariableTable _varTable;
        private Variable _variable;

        public override double Angle
        {
            get
            {
                return _varTable[_variable].Eval(_varTable).Value;
            }
        }
    }
}
