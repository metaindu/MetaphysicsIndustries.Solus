using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    //public delegate double ErrorFunction<T>(T a, T b)
    //    where T : Tensor;

    public delegate double ErrorFunction(Matrix a, Matrix b);
    //public delegate double ErrorFunction(Vector a, Vector b);
}
