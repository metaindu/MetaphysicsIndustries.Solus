using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class DerivativeOfVariable : Variable
    {
        public DerivativeOfVariable(Variable variable, Variable lowerVariable)
            : base("d")
        {
            if (variable is DerivativeOfVariable && (variable as DerivativeOfVariable).LowerVariable == lowerVariable)
            {
                _variable = (variable as DerivativeOfVariable).Variable;
                _order = (variable as DerivativeOfVariable).Order + 1;
            }
            else
            {
                _variable = variable;
                _order = 1;
            }
            _lowerVariable = lowerVariable;
        }

        private Variable _variable;

        public Variable Variable
        {
            get { return _variable; }
        }

        private int _order;

        public int Order
        {
            get { return _order; }
        }

        private Variable _lowerVariable;
        public Variable LowerVariable
        {
            get { return _lowerVariable; }
        }


        public override string Name
        {
            get
            {
                return "d" + (Order > 1 ? Order.ToString() : "") + Variable.Name + "/d" + LowerVariable.Name + (Order > 1 ? Order.ToString() : "");
            }
            //set
            //{
            //}
        }

    }
}
