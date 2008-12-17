using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class Tensor : IEnumerable<double>
    {
        public abstract IEnumerator<double> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract void ApplyToAll(Modulator mod);
    }
}
