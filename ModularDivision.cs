using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ModularDivision : BinaryOperation
    {
        public static readonly ModularDivision Value = new ModularDivision();

        protected ModularDivision()
        {
            Name = "%";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Multiplication; }
        }

        protected override float InternalBinaryCall(float x, float y)
        {
            return ((long)x) % ((long)y);   
        }
    }
}
