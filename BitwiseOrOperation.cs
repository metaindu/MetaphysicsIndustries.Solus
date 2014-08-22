using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class BitwiseOrOperation : AssociativeCommutativeOperation
    {
        public static readonly BitwiseOrOperation Value = new BitwiseOrOperation();

        protected BitwiseOrOperation()
        {
            Name = "|";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Bitwise; }
        }

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            long value = 0;

            foreach (Literal arg in args)
            {
                value |= (long)(arg.Value);
            }

            return new Literal(value);
        }

        public override bool HasIdentityValue
        {
            get { return false; }
        }

        public override bool Culls
        {
            get { return true; }
        }

        public override float CullValue
        {
            get { return 0; }
        }
    }
}
