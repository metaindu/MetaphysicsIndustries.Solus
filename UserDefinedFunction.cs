using System;

namespace MetaphysicsIndustries.Solus
{
    public class UserDefinedFunction : Function
    {
        public UserDefinedFunction(string name, string[] argnames, Expression expr)
        {
            Name = name;
            Argnames = argnames;
            Expression = expr;

            Types.Clear();
            foreach (var argname in argnames)
            {
                Types.Add(typeof(Expression));
            }
        }

        public string[] Argnames;
        public Expression Expression;

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            SolusEnvironment env2 = env.CreateChildEnvironment();

            int i;
            for (i = 0; i < Argnames.Length; i++)
            {
                env2.Variables[Argnames[i]] = args[i];
            }

            return Expression.Eval(env2);
        }
    }
}

