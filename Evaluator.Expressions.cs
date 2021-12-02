using System;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus
{
    public partial class Evaluator
    {
        public IMathObject Eval(ColorExpression expr,
            SolusEnvironment env)
        {
            return new Number(0xFFFFFF & expr.Color.ToArgb());
        }

        private int[] _ComponentAccessIndexesCache;
        public IMathObject Eval(ComponentAccess expr, SolusEnvironment env)
        {
            var value = expr.Expr.Eval(env);
            // TODO: there are some situations where we could work with a
            //       result of Expr.Eval that is not a concrete value. for
            //       example, "[a,2][1]" should evaluate to "2", even though
            //       "[a,2]" with an unbound variable would not evaluate to a
            //       concrete value.
            switch (value)
            {
                case IVector v:
                case StringValue s:
                    if (expr.Indexes.Count != 1)
                        throw new OperandException(
                            "Wrong number of indexes for the expression");
                    break;
                case IMatrix m:
                    if (expr.Indexes.Count != 2)
                        throw new OperandException(
                            "Wrong number of indexes for the expression");
                    break;
                default:
                    throw new OperandException(
                        "Unable to get components from expression, " +
                        "or the expression does not have components");
            }

            if (_ComponentAccessIndexesCache == null ||
                _ComponentAccessIndexesCache.Length < expr.Indexes.Count)
                _ComponentAccessIndexesCache = new int[expr.Indexes.Count];
            int i;
            for (i = 0; i < expr.Indexes.Count; i++)
            {
                var si = expr.Indexes[i].Eval(env);
                if (!(si is Number))
                    throw new IndexException(
                        "Indexes must be scalar");
                var vi = si.ToNumber().Value;
                if (!vi.IsInteger())
                    throw new IndexException(
                        "Indexes must be integers");
                if (vi < 0)
                    throw new IndexException(
                        "Indexes must not be negative");
                _ComponentAccessIndexesCache[i] = (int)vi;
            }

            switch (value)
            {
                case IVector v:
                    if (_ComponentAccessIndexesCache[0] >= v.Length)
                        throw new IndexException(
                            "Index exceeds the size of the vector");
                    return v.GetComponent(_ComponentAccessIndexesCache[0]);
                case StringValue s:
                    if (_ComponentAccessIndexesCache[0] >= s.Length)
                        throw new IndexException(
                            "Index exceeds the size of the string");
                    return s.Value[_ComponentAccessIndexesCache[0]].ToStringValue();
                case IMatrix m:
                    if (_ComponentAccessIndexesCache[0] >= m.RowCount)
                        throw new IndexException(
                            "Index exceeds number of rows of the matrix");
                    if (_ComponentAccessIndexesCache[1] >= m.ColumnCount)
                        throw new IndexException(
                            "Index exceeds number of columns of the matrix");
                    return m.GetComponent(_ComponentAccessIndexesCache[0],
                        _ComponentAccessIndexesCache[1]);
            }

            throw new OperandException("Unknown");
        }

        public IMathObject Eval(DerivativeOfVariable expr,
            SolusEnvironment env)
        {
            throw new NotImplementedException();
        }
    }
}