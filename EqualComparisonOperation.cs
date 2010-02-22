using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class EqualComparisonOperation : ComparisonOperation
    {
        public EqualComparisonOperation()
            : base("==")
        {
        }

        protected override bool Compare(double x, double y)
        {
            return x == y;
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Equality; }
        }

        public override bool IsCommutative
        {
            get
            {
                return true;
            }
        }
    }
}
