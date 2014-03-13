using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class SolusTensor : Expression, IEnumerable<Expression>
    {
        public abstract IEnumerator<Expression> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public delegate float Modulator(float x);
        public delegate float BiModulator(float x, float y);
        public abstract void ApplyToAll(Modulator mod);
    }
}
