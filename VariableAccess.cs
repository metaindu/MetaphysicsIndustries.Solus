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
            Variable = variable;
        }

        public override Expression Clone()
        {
            return new VariableAccess(Variable);
        }

        public Variable Variable;

        public override Literal Eval(VariableTable varTable)
        {
            Variable var = Variable;

            if (varTable.ContainsKey(var))
            {
                if (varTable[var] is Literal)
                {
                    return (Literal)varTable[var];
                }

                return varTable[var].Eval(varTable);
            }
            else
            {
                //return new Literal(0);
                throw new InvalidOperationException("Variable not found in variable table: " + Variable.Name);
            }
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
