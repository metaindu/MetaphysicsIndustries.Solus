using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class BitwiseAndOperation : BinaryOperation
    {
        public BitwiseAndOperation()
        {
            Name = "&";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Bitwise; }
        }

        //protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        //{
        //    ulong value = 0xffffffffffffffff;

        //    foreach (Literal arg in args)
        //    {
        //        ulong argvalue = (ulong)(arg.Value);
        //        value &= argvalue;
        //    }

        //    return new Literal(value);
        //}
        protected override double InternalBinaryCall(double x, double y)
        {
            return ((long)x) & ((long)y);   
        }

        //public override double IdentityValue
        //{
        //    get { return 0xffffffffffffffff; }
        //}

        //public override bool Culls
        //{
        //    get { return false; }
        //}
    }
}
