using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class MultiplicationOperation : AssociativeCommutativeOperation
    {
        public MultiplicationOperation()
        {
            Name = "*";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Multiplication; }
        }

        //public override bool IsCommutative
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        //public override bool IsAssociative
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            double value = 1;
            int i;
            for (i = 0; i < args.Length; i++)
            {
                value *= args[i].Value;
            }
            return new Literal(value);
        }

        public override bool Collapses
        {
            get
            {
                return true;
            }
        }

        public override double CollapseValue
        {
            get
            {
                return 0;
            }
        }
    }
}
