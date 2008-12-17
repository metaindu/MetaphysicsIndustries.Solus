using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public struct Triple<T>
    {
        public Triple(T first, T second, T third)
        {
            _first = first;
            _second = second;
            _third = third;
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

        private T _third;
        public T Third
        {
            get { return _third; }
            set { _third = value; }
        }
    }
}
