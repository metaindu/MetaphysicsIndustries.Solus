using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class Arctangent2Function : Function
    {
        public Arctangent2Function()
            : base ("Arctangent 2")
        {
            Types.Clear();
            Types.Add(typeof(Expression));
            Types.Add(typeof(Expression));
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal(
                Math.Atan2(
                    args[0].Eval(varTable).Value,
                    args[1].Eval(varTable).Value));
        }

        public override string DisplayName
        {
            get
            {
                return "atan2";
            }
        }
    }
}
