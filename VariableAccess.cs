using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class VariableAccess : Expression
    {
        public VariableAccess()
            : this(null)
        {
        }

        public VariableAccess(Variable variable)
        {
            _processVariableChangedDelegate = new EventHandler(ProcessVariableChanged);

            Variable = variable;
        }

        public override Expression Clone()
        {
            return new VariableAccess(Variable);
        }

        private Variable _variable;

        public Variable Variable
        {
            get { return _variable; }
            set
            {
                if (_variable != value)
                {
                    if (_variable != null)
                    {
                        _variable.ValueChanged -= _processVariableChangedDelegate;
                    }

                    _variable = value;

                    if (_variable != null)
                    {
                        _variable.ValueChanged += _processVariableChangedDelegate;
                    }
                }
            }
        }

        public override Literal Eval(VariableTable varTable)
        {
            if (varTable.ContainsKey(Variable))
            {
                if (varTable[Variable] is Literal)
                {
                    return (Literal)varTable[Variable];
                }

                return varTable[Variable].Eval(varTable);
            }
            else
            {
                //return new Literal(0);
                throw new InvalidOperationException("Variable not found in variable table: " + Variable.Name);
            }
        }

        EventHandler _processVariableChangedDelegate;
        protected void ProcessVariableChanged(object sender, EventArgs e)
        {
        }

        public override Expression PreliminaryEval(VariableTable varTable)
        {
            if (varTable.ContainsKey(Variable))
            {
                //this will cause an infinite recursion if a variable 
                //is defined in terms of itself or in terms of another 
                //variable.
                //we could add (if (varTable[Variable] as VariableAccess).Variable == Variable) { throw }
                //but we can't look for cyclical definitions involving other variables, at least, not in an efficient way, right now.
                return varTable[Variable].PreliminaryEval(varTable);
            }
            else
            {
                return base.PreliminaryEval(varTable);
            }
        }

        public override string ToString()
        {
            if (Variable != null)
            {
                return Variable.Name;
            }
            else
            {
                return "[unknown variable]";
            }
        }
    }
}
