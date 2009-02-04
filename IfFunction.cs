using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class IfFunction : Function
    {
        public IfFunction()
            : base("If")
        {
            Types.Clear();
            Types.Add(typeof(Expression));
            Types.Add(typeof(Expression));
            Types.Add(typeof(Expression));
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            double value = args[0].Value;
            if (value == 0 || double.IsNaN(value) || double.IsInfinity(value))
            {
                return args[2];
            }
            else
            {
                return args[1];
            }
        }

        public override string DisplayName
        {
            get { return "if"; }
        }
    }
}
