using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class Vector : Tensor
    {
        public static Vector FromUniformSequence(float value, int length)
        {
            Vector ret = new Vector(length);

            int i;
            for (i = 0; i < length; i++)
            {
                ret[i] = value;
            }

            return ret;
        }

        public Vector(int length)
        {
            _length = length;
            _array = new double[_length];

            int i;

            for (i = 0; i < length; i++)
            {
                _array[i] = 0;
            }
        }

        public Vector(int length, params double[] initialContents)
            : this(length)
        {
            int i;
            int j = Math.Min(length, initialContents.Length);
            for (i = 0; i < j; i++)
            {
                _array[i] = initialContents[i];
            }
        }

        public Vector Clone()
        {
            return new Vector(Length, _array);
        }

        private double[] _array;
        private int _length;
        public int Length
        {
            get { return _length; }
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) { throw new IndexOutOfRangeException("index"); }

                return _array[index];
            }
            set
            {
                if (index < 0 || index >= Length) { throw new IndexOutOfRangeException("index"); }

                _array[index] = value;
            }
        }

        #region IEnumerable<double> Members

        public override IEnumerator<double> GetEnumerator()
        {
            IList<double> list = _array;
            return list.GetEnumerator();
        }

        #endregion

        //methods and overloaded operators

        public Vector Convolution(Vector convolvee)
        {
            return AdvancedConvolution(convolvee, AssociativeCommutativeOperation.Multiplication, AssociativeCommutativeOperation.Addition);
        }

        public Vector AdvancedConvolution(Vector convolvee, Operation firstOp, AssociativeCommutativeOperation secondOp)
        {
            int r = Length + convolvee.Length - 1;

            Vector ret = new Vector(r);

            List<double> group = new List<double>();

            int n;
            int k;
            double term;
            for (n = 0; n < r; n++)
            {
                term = 0;

                for (k = 0; k < Length; k++)
                {
                    if (n - k < 0) { break; }
                    if (n - k >= convolvee.Length) { continue; }

                    term += this[k] * convolvee[n - k];
                }

                ret[n] = term;
            }

            return ret;
        }

        public override void ApplyToAll(Modulator mod)
        {
            int i;
            for (i = 0; i < Length; i++)
            {
                this[i] = mod(this[i]);
            }
        }

        public Vector GetSlice(int startIndex, int length)
        {
            Vector ret = new Vector(length);

            int i;
            int j = Math.Min(length, Length - startIndex);
            for (i = 0; i < j; i++)
            {
                ret[i] = this[i + startIndex];
            }

            return ret;
        }
    }
}
