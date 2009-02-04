using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class UnaryOperation : Operation
    {
        public UnaryOperation()
        {
            Types.Clear();
            Types.Add(typeof(Expression));
        }

        protected override void CheckArguments(Expression[] args)
        {
            if (Types.Count != 1)
            {
                throw new InvalidOperationException("Wrong number of types specified internally to UnaryOperation (given " + Types.Count.ToString() + ", require 1)");
            }
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Wrong number of arguments given to " + this.DisplayName + " (given " + args.Length.ToString() + ", require 1)");
            }

            Type e = typeof(Expression);

            if (!e.IsAssignableFrom(Types[0]))
            {
                throw new InvalidOperationException("Required argument type is invalid (given \"" + Types[0].Name + "\", require \"" + e.Name + "\")");
            }
            if (!Types[0].IsAssignableFrom(args[0].GetType()))
            {
                throw new InvalidOperationException("Argument of wrong type (given \"" + args[0].GetType().Name + "\", require \"" + Types[0].Name + "\")");
            }
        }
    }
}
