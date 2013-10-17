using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class AssignExpression : Expression
    {
        public AssignExpression(string variable, Literal value)
        {
            _variable = variable;
            _value = value;
        }

        private string _variable;
        public string Variable
        {
            get { return _variable; }
            set { _variable = value; }
        }

        private Literal _value;
        public Literal Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public override Literal Eval(SolusEnvironment env)
        {
            return Value;
        }

        public override Expression Clone()
        {
            return new AssignExpression(Variable, Value);
        }
    }
}
