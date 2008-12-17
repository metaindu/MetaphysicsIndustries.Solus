using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SolusVector : SolusTensor//, IEnumerable<Expression>
    {
        private static SolusEngine _engine = new SolusEngine();

        public static SolusVector FromUniformSequence(double value, int length)
        {
            return FromUniformSequence(new Literal(value), length);
        }

        public static SolusVector FromUniformSequence(Expression value, int length)
        {
            SolusVector ret = new SolusVector(length);

            int i;
            for (i = 0; i < length; i++)
            {
                ret[i] = value;
            }

            return ret;
        }

        public SolusVector(int length)
        {
            _length = length;
            _array = new Expression[_length];

            int i;

            for (i = 0; i < length; i++)
            {
                _array[i] = Literal.Zero;
            }
        }

        public SolusVector(int length, params double[] initialContents)
            : this(length)
        {
            int i;
            int j = Math.Min(length, initialContents.Length);
            for (i = 0; i < j; i++)
            {
                _array[i] = new Literal(initialContents[i]);
            }
        }

        public SolusVector(int length, params Expression[] initialContents)
            : this(length)
        {
            int i;
            int j = Math.Min(length, initialContents.Length);
            for (i = 0; i < j; i++)
            {
                _array[i] = initialContents[i];
            }
        }

        private Expression[] _array;
        private int _length;
        public int Length
        {
            get { return _length; }
        }

        public override Literal Eval(VariableTable varTable)
        {
            return new Literal(0);
        }

        public override Expression Clone()
        {
            SolusVector ret = new SolusVector(Length);

            int i;
            for (i = 0; i < Length; i++)
            {
                ret[i] = this[i];//.Clone();
            }

            return ret;
        }

        //public override Expression CleanUp()
        //{
        //    SolusVector ret = new SolusVector(Length);

        //    int i;
        //    for (i = 0; i < Length; i++)
        //    {
        //        ret[i] = this[i].CleanUp();
        //    }

        //    return ret;
        //}

        public Expression this[int index]
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

        #region IEnumerable<Expression> Members

        public override IEnumerator<Expression> GetEnumerator()
        {
            IList<Expression> list = _array;
            return list.GetEnumerator();
        }

        #endregion

        //methods and overloaded operators

        public SolusVector Convolution(SolusVector convolvee)
        {
            return AdvancedConvolution(convolvee, AssociativeCommutativeOperation.Multiplication, AssociativeCommutativeOperation.Addition);
        }

        public SolusVector AdvancedConvolution(SolusVector convolvee, Operation firstOp, AssociativeCommutativeOperation secondOp)
        {
            int r = Length + convolvee.Length - 1;

            SolusVector ret = new SolusVector(r);

            List<Expression> group = new List<Expression>();

            int n;
            int k;
            for (n = 0; n < r; n++)
            {
                group.Clear();

                for (k = 0; k < Length; k++)
                {
                    if (n - k < 0) { break; }
                    if (n - k >= convolvee.Length) { continue; }

                    group.Add(
                        new FunctionCall(
                        firstOp,
                        this[k],
                        convolvee[n - k]));
                }

                ret[n] = _engine.CleanUp(new FunctionCall(secondOp, group.ToArray()));
            }

            return ret;
        }

        protected override void InternalApplyToExpressionTree(SolusAction action, bool applyToChildrenBeforeParent)
        {
            foreach (Expression expr in this)
            {
                expr.ApplyToExpressionTree(action, applyToChildrenBeforeParent);
            }
        }

        public override Expression PreliminaryEval(VariableTable varTable)
        {
            SolusVector ret = new SolusVector(Length);

            int i;
            for (i = 0; i < Length; i++)
            {
                ret[i] = this[i].PreliminaryEval(varTable);
            }

            return ret;
        }

        public override void ApplyToAll(Modulator mod)
        {
            int i;
            for (i = 0; i < Length; i++)
            {
                this[i] = new Literal(mod(((Literal)this[i]).Value));
            }
        }

        public SolusVector GetSlice(int startIndex, int length)
        {
            SolusVector ret = new SolusVector(length);

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
