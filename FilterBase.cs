using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class FilterBase<T> : FilterBase<T, T>
        where T : Tensor
    {
    }

    public abstract class FilterBase<TInput, TOutput>
        where TInput : Tensor
        where TOutput : Tensor
    {
        public abstract TOutput Apply(TInput input);
        //protected static SolusEngine _engine = new SolusEngine();
    }
}
