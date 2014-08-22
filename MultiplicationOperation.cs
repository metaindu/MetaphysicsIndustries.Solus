using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class MultiplicationOperation : AssociativeCommutativeOperation
    {
        public static readonly MultiplicationOperation Value = new MultiplicationOperation();

        protected MultiplicationOperation()
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

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            float value = 1;
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

        public override float CollapseValue
        {
            get
            {
                return 0;
            }
        }
    }
}
