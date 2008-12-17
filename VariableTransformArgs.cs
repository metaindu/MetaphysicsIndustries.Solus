using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class VariableTransformArgs : TransformArgs
    {
        public VariableTransformArgs(Variable variable)
        {
            if (variable == null) { throw new ArgumentNullException("variable"); }

            _variable = variable;	
        }

        private Variable _variable;
        public Variable Variable
        {
            get { return _variable; }
        }
    }
}
