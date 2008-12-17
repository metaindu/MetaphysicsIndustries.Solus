using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public struct Pair<T>
        //where T : Tensor
    {
        public Pair(T first, T second)
        {
            _first = first;
            _second = second;
        }

        private T _first;
        public T First
        {
            get { return _first; }
            set { _first = value; }
        }

        private T _second;
        public T Second
        {
            get { return _second; }
            set { _second = value; }
        }
    }
}
