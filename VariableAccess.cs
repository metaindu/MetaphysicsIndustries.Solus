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

        public VariableAccess(string variableName)
        {
            if (string.IsNullOrEmpty(variableName)) throw new ArgumentNullException("variableName");

            VariableName = variableName;
        }

        public override Expression Clone()
        {
            return new VariableAccess(VariableName);
        }

        public string VariableName;

        public override Literal Eval(VariableTable varTable)
        {
            Variable var;
            if (varTable.ContainsKey(VariableName))
            {
                var = varTable[VariableName];
            }
            else
            {
                throw new InvalidOperationException("Variable not found in variable table: " + VariableName);
            }


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
                throw new InvalidOperationException("Variable not found in variable table: " + VariableName);
            }
        }

        public override Expression PreliminaryEval(VariableTable varTable)
        {
            if (varTable.ContainsKey(VariableName))
            {
                //this will cause an infinite recursion if a variable 
                //is defined in terms of itself or in terms of another 
                //variable.
                //we could add (if (varTable[Variable] as VariableAccess).Variable == Variable) { throw }
                //but we can't look for cyclical definitions involving other variables, at least, not in an efficient way, right now.
                var var = varTable[VariableName];
                return varTable[var].PreliminaryEval(varTable);
            }
            else
            {
                return base.PreliminaryEval(varTable);
            }
        }

        public override string ToString()
        {
            return VariableName;
        }
    }
}
