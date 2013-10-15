using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class IfFunction : Function
    {
        public static readonly IfFunction Value = new IfFunction();

        protected IfFunction()
            : base("If")
        {
            Types.Clear();
            Types.Add(typeof(Expression));
            Types.Add(typeof(Expression));
            Types.Add(typeof(Expression));
        }

        public override Literal Call(Environment env, params Expression[] args)
        {
            CheckArguments(args);

            float value = args[0].Eval(env).Value;

            if (value == 0 || float.IsNaN(value) || float.IsInfinity(value))
            {
                return args[2].Eval(env);
            }
            else
            {
                return args[1].Eval(env);
            }
        }

        protected override Literal InternalCall(Environment env, Literal[] args)
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
