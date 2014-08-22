using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ExponentOperation : BinaryOperation
    {
        public static readonly ExponentOperation Value = new ExponentOperation();

        protected ExponentOperation()
        {
            Name = "^";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Exponent; }
        }

        //protected override Literal InternalCall(VariableTable env, Literal[] args)
        //{
        //    return new Literal(Math.Pow(args[0].Value, args[1].Value));
        //}

        protected override float InternalBinaryCall(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }
    }
}
