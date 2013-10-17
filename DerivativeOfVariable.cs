using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class DerivativeOfVariable : Expression
    {
        public DerivativeOfVariable(string variable, string lowerVariable)
        {
            _variable = variable;
            _order = 1;
            _lowerVariable = lowerVariable;
        }
        public DerivativeOfVariable(DerivativeOfVariable variable, string lowerVariable)
        {
            if (variable.LowerVariable == lowerVariable)
            {
                _variable = variable.Variable;
                _order = variable.Order + 1;
            }
            else
            {
                _variable = variable.Name;
                _order = 1;
            }
            _lowerVariable = lowerVariable;
        }

        private string _variable;

        public string Variable
        {
            get { return _variable; }
        }

        private int _order;

        public int Order
        {
            get { return _order; }
        }

        private string _lowerVariable;
        public string LowerVariable
        {
            get { return _lowerVariable; }
        }


        public string Name
        {
            get
            {
                return "d" + (Order > 1 ? Order.ToString() : "") + Variable + "/d" + LowerVariable + (Order > 1 ? Order.ToString() : "");
            }
        }

        public override Literal Eval(SolusEnvironment env)
        {
            throw new InvalidOperationException();
        }
        public override Expression Clone()
        {
            throw new NotImplementedException();
        }
    }
}
