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
                return new Literal(varTable[Variable].Eval(varTable).Value);
            }
            else
            {
                return base.PreliminaryEval(varTable);
            }
        }
    }
}
