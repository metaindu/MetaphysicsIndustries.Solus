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

        public override Literal Call(VariableTable varTable, params Expression[] args)
        {
            CheckArguments(args);

            float value = args[0].Eval(varTable).Value;

            if (value == 0 || float.IsNaN(value) || float.IsInfinity(value))
            {
                return args[2].Eval(varTable);
            }
            else
            {
                return args[1].Eval(varTable);
            }
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            throw new NotSupportedException();
            //float value = args[0].Value;
            //if (value == 0 || float.IsNaN(value) || float.IsInfinity(value))
            //{
            //    return args[2];
            //}
            //else
            //{
            //    return args[1];
            //}
        }

        public override string DisplayName
        {
            get { return "if"; }
        }
    }
}
