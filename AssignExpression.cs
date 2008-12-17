using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class AssignExpression : Expression
    {
        public AssignExpression(Variable variable, Literal value)
        {
            _variable = variable;
            _value = value;
        }

        private Variable _variable;
        public Variable Variable
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

        public override Literal Eval(VariableTable varTable)
        {
            return Value;
        }

        public override Expression Clone()
        {
            return new AssignExpression(Variable, Value);
        }
    }
}
