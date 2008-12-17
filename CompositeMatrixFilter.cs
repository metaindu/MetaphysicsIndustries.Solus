using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ChainMatrixFilter : MatrixFilter
    {
        public ChainMatrixFilter(IEnumerable<MatrixFilter> initialContents)
            : this()
        {
            Filters.AddRange(initialContents);
        }

        public ChainMatrixFilter(params MatrixFilter[] initialContents)
            : this((IEnumerable<MatrixFilter>)initialContents)
        {
        }

        public ChainMatrixFilter()
        {
        }

        private List<MatrixFilter> _filters = new List<MatrixFilter>();
        public List<MatrixFilter> Filters
        {
            get { return _filters; }
        }


        public override Matrix Apply(Matrix input)
        {
            Matrix temp = input;
            foreach (MatrixFilter filter in Filters)
            {
                temp = filter.Apply(temp);
            }

            return temp;
        }
    }
}
