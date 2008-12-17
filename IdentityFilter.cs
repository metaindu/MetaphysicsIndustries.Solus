using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class IdentityFilter : MatrixFilter
    {
        public override Matrix Apply(Matrix input)
        {
            return input.Clone();
        }
    }
}
