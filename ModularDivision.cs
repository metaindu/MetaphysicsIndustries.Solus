using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ModularDivision : BinaryOperation
    {
        public ModularDivision()
        {
            Name = "%";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Multiplication; }
        }

        protected override double InternalBinaryCall(double x, double y)
        {
            return ((long)x) % ((long)y);   
        }
    }
}
