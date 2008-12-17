using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class CloneTransformer : ExpressionTransformer
    {
        public override bool CanTransform(Expression expr)
        {
            return (expr != null);
        }

        public override Expression Transform(Expression expr)
        {
            return expr.Clone();
        }
    }
}
