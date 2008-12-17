using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class ExpressionTransformer<T>
        where T : TransformArgs
    {
        public abstract bool CanTransform(Expression expr, T args);
        public abstract Expression Transform(Expression expr, T args);
    }

    public abstract class ExpressionTransformer
    {
        public abstract bool CanTransform(Expression expr);
        public abstract Expression Transform(Expression expr);
    }
}
