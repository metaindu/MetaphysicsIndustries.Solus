using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class VariableTransformArgs : TransformArgs
    {
        public VariableTransformArgs(string variable)
        {
            if (variable == null) { throw new ArgumentNullException("variable"); }

            _variable = variable;	
        }

        private string _variable;
        public string Variable
        {
            get { return _variable; }
        }
    }
}
