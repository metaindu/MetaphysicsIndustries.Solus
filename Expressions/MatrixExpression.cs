
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class MatrixExpression : TensorExpression, IMatrix
    {
        private static Evaluator _evaluator = new Evaluator();

        public static MatrixExpression FromUniform(float value, int rows,
            int columns)
        {
            return FromUniform(new Literal(value), rows, columns);
        }

        public static MatrixExpression FromUniform(Expression value, int rows,
            int columns)
        {
            var ret = new MatrixExpression(rows, columns);

            int i;
            int j;
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    ret[i, j] = value;
                }
            }

            return ret;
        }

        public MatrixExpression(int rows, int columns)
        {
            _rowCount = rows;
            _columnCount = columns;
            _array = new Expression[_rowCount, _columnCount];

            int i;
            int j;

            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    _array[i, j] = Literal.Zero;
                }
            }

            Result = new ResultC(this);
        }

        public MatrixExpression(int rows, int columns,
            params float[] initialContents)
            : this(rows, columns)
        {
            int i;
            int j;
            int x = 0;
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    if (x >= initialContents.Length) { break; }
                    this[i, j] = new Literal(initialContents[x]);
                    x++;
                }
            }
        }

        public MatrixExpression(int rows, int columns,
            params Expression[] initialContents)
            : this(rows, columns)
        {
            int i;
            int j;
            int x = 0;
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    if (x >= initialContents.Length) { break; }
                    this[i, j] = initialContents[x];
                    x++;
                }
            }
        }

        public MatrixExpression(Expression[,] initialContents)
            : this(initialContents.GetLength(0),
                initialContents.GetLength(1))
        {
            int r, c;
            for(r=0;r<initialContents.GetLength(0);r++)
            for (c = 0; c < initialContents.GetLength(1); c++)
                this[r, c] = initialContents[r, c];
        }

        public override int TensorRank => 2;

        private int _rowCount;
        public int RowCount
        {
            get { return _rowCount; }
        }
        private int _columnCount;
        public int ColumnCount
        {
            get { return _columnCount; }
        }

        public IMathObject GetComponent(int row, int column) =>
            this[row, column];

        public override Expression GetComponent(int[] indexes)
        {
            if (indexes == null)
                throw new ArgumentNullException(nameof(indexes));
            if (indexes.Length != 2)
                throw new ArgumentOutOfRangeException(
                    nameof(indexes), "Wrong number of indexes");
            if (indexes[0] < 0 || indexes[0] >= RowCount ||
                indexes[1] < 0 || indexes[1] >= ColumnCount)
                throw new IndexOutOfRangeException();
            return this[indexes[0], indexes[1]];
        }

        private Expression[,] _array;

        public int Count { get { return RowCount * ColumnCount; } }

        public VectorExpression GetRow(int row)
        {
            var ret = new VectorExpression(ColumnCount);
            int i;

            for (i = 0; i < ColumnCount; i++)
            {
                Expression expr = this[row, i];
                ret[i] = expr;
            }

            return ret;
        }

        public VectorExpression GetColumn(int column)
        {
            VectorExpression ret = new VectorExpression(RowCount);
            int i;

            for (i = 0; i < RowCount; i++)
            {
                ret[i] = this[i, column];
            }

            return ret;
        }

        public MatrixExpression GetSlice(int startRow, int startColumn,
            int numberOfRows, int numberOfColumns)
        {
            int i;
            int j;

            var mat = new MatrixExpression(numberOfRows, numberOfColumns);

            for (i = 0; i < numberOfRows; i++)
            {
                for (j = 0; j < numberOfColumns; j++)
                {
                    mat[i, j] = this[i + startRow, j + startColumn];
                }
            }

            return mat;
        }

        //RowCollection Rows
        //ColumnCollection Columns

        //public Matrix Multiply(Expression scaleFactor)
        //{
        //    //check dimensions
        //}
        ////public Vector Multiply(Vector vector)
        ////{
        //    //check dimensions
        ////}
        //public Matrix Multiply(Matrix matrix)
        //{
        //    //check dimensions
        //}

        //public Matrix Add(Matrix matrix)
        //{
        //    //check dimensions
        //}

        //operator overloads

        //public Expression GetDeterminant()
        //{
        //}

        public MatrixExpression Convolution(MatrixExpression convolvee)
        {
            //return AdvancedConvolution(convolvee, MultiplicationOperation.Value, AdditionOperation.Value);
            // return AdvancedConvolution(convolvee,
            //     Evaluator.MultiplicationBiMod, Evaluator.AdditionBiMod);
            throw new NotImplementedException();
        }

        public MatrixExpression AdvancedConvolution(MatrixExpression convolvee,
            BiModulator firstOp, BiModulator secondOp)
        //Operation firstOp, AssociativeCommutativeOperation secondOp)
        {



            int r = RowCount + convolvee.RowCount - 1;
            int c = ColumnCount + convolvee.ColumnCount - 1;

            var ret = new MatrixExpression(r, c);

            //int iiend;
            //int jjend;

            //List<Expression> group = new List<Expression>();
            float term;

            float[,] values1 = new float[RowCount, ColumnCount];
            float[,] values2 = new float[convolvee.RowCount, convolvee.ColumnCount];

            int i;
            int j;
            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    values1[i, j] = (((Literal)this[i, j]).Value.ToFloat());
                }
            }
            for (i = 0; i < convolvee.RowCount; i++)
            {
                for (j = 0; j < convolvee.ColumnCount; j++)
                {
                    values2[i, j] = (((Literal)convolvee[i, j]).Value.ToFloat());
                }
            }

            int n1;
            int n2;
            int k1;
            int k2;

            int[] times = new int[16];
            int lasttime = System.Environment.TickCount;
            int time;

            time = System.Environment.TickCount; times[0] += time - lasttime; lasttime = time;
            for (n1 = 0; n1 < r; n1++)
            {
                ////time = Environment.TickCount; times[1] += time - lasttime; lasttime = time;
                for (n2 = 0; n2 < c; n2++)
                {
                    //group.Clear();
                    term = 0;//Literal.Zero;

                    for (k1 = 0; k1 < RowCount; k1++)
                    {
                        ////time = Environment.TickCount; times[2] += time - lasttime; lasttime = time;
                        if (n1 - k1 < 0) { break; }
                        if (n1 - k1 >= convolvee.RowCount) { continue; }
                        ////time = Environment.TickCount; times[3] += time - lasttime; lasttime = time;

                        ////time = Environment.TickCount; times[4] += time - lasttime; lasttime = time;

                        ////time = Environment.TickCount; times[5] += time - lasttime; lasttime = time;
                        for (k2 = 0; k2 < ColumnCount; k2++)
                        {
                            ////time = Environment.TickCount; times[6] += time - lasttime; lasttime = time;
                            if (n2 - k2 < 0) { break; }
                            if (n2 - k2 >= convolvee.ColumnCount) { continue; }

                            ////time = Environment.TickCount; times[7] += time - lasttime; lasttime = time;

                            //Expression expr = new FunctionCall(
                            //    firstOp,
                            //    this[k1, k2],
                            //    convolvee[n1 - k1, n2 - k2]);

                            term = secondOp(term, firstOp(values1[k1, k2], values2[n1 - k1, n2 - k2]));
                            ////time = Environment.TickCount; times[11] += time - lasttime; lasttime = time;
                            //////expr = expr.CleanUp();
                            ////time = Environment.TickCount; times[10] += time - lasttime; lasttime = time;
                            //////expr = new FunctionCall(
                            //////    secondOp,
                            //////    term,
                            //////    expr);

                            //group.Add(
                            //    (expr));

                            ////time = Environment.TickCount; times[9] += time - lasttime; lasttime = time;
                            //////term = expr.CleanUp();
                            ////time = Environment.TickCount; times[8] += time - lasttime; lasttime = time;
                        }

                    }

                    //Expression[] terms = group.ToArray();
                    //FunctionCall fc = new FunctionCall(secondOp, terms);
                    //Expression expr2 = fc.CleanUp();
                    //time = Environment.TickCount; times[12] += time - lasttime; lasttime = time;
                    ret[n1, n2] = new Literal(term);// fc.CleanUp();
                    ////time = Environment.TickCount; times[13] += time - lasttime; lasttime = time;
                }
                ////time = Environment.TickCount; times[14] += time - lasttime; lasttime = time;
            }
            time = System.Environment.TickCount; times[15] += time - lasttime; lasttime = time;




            ////////int i;
            ////////int j;
            ////////int ii;
            ////////int jj;
            //////int r = RowCount + convolvee.RowCount - 1;
            //////int c = ColumnCount + convolvee.ColumnCount - 1;

            //////Matrix ret = new Matrix(r, c);

            ////////int iiend;
            ////////int jjend;

            //////List<Expression> group = new List<Expression>();
            //////Expression term;

            //////int n1;
            //////int n2;
            //////int k1;
            //////int k2;

            //////int[] times = new int[16];
            //////int lasttime = Environment.TickCount;
            //////int time;

            //////time = Environment.TickCount; times[0] += time - lasttime; lasttime = time;
            //////for (n1 = 0; n1 < r; n1++)
            //////{
            //////    time = Environment.TickCount; times[1] += time - lasttime; lasttime = time;
            //////    for (k1 = 0; k1 < RowCount; k1++)
            //////    {
            //////        time = Environment.TickCount; times[2] += time - lasttime; lasttime = time;
            //////        if (n1 - k1 < 0) { break; }
            //////        if (n1 - k1 >= convolvee.RowCount) { continue; }
            //////        time = Environment.TickCount; times[3] += time - lasttime; lasttime = time;

            //////        //group.Clear();
            //////        term = Literal.Zero;

            //////        time = Environment.TickCount; times[4] += time - lasttime; lasttime = time;
            //////        for (n2 = 0; n2 < c; n2++)
            //////        {
            //////            time = Environment.TickCount; times[5] += time - lasttime; lasttime = time;
            //////            for (k2 = 0; k2 < ColumnCount; k2++)
            //////            {
            //////                time = Environment.TickCount; times[6] += time - lasttime; lasttime = time;
            //////                if (n2 - k2 < 0) { break; }
            //////                if (n2 - k2 >= convolvee.ColumnCount) { continue; }

            //////                time = Environment.TickCount; times[7] += time - lasttime; lasttime = time;

            //////                Expression expr = new FunctionCall(
            //////                    firstOp,
            //////                    this[k1, k2],
            //////                    convolvee[n1 - k1, n2 - k2]);
            //////            time = Environment.TickCount; times[11] += time - lasttime; lasttime = time;
            //////                expr = expr.CleanUp();
            //////            time = Environment.TickCount; times[10] += time - lasttime; lasttime = time;
            //////                expr = new FunctionCall(
            //////                    secondOp,
            //////                    term,
            //////                    expr);

            //////                //group.Add(
            //////                //    (expr));

            //////            time = Environment.TickCount; times[9] += time - lasttime; lasttime = time;
            //////                term = expr.CleanUp();
            //////                time = Environment.TickCount; times[8] += time - lasttime; lasttime = time;
            //////            }

            //////            Expression[] terms = group.ToArray();
            //////            FunctionCall fc = new FunctionCall(secondOp, terms);
            //////            ret[n1, n2] = term;// fc.CleanUp();
            //////            time = Environment.TickCount; times[12] += time - lasttime; lasttime = time;
            //////        }
            //////        time = Environment.TickCount; times[13] += time - lasttime; lasttime = time;
            //////    }
            //////    time = Environment.TickCount; times[14] += time - lasttime; lasttime = time;
            //////}
            //////time = Environment.TickCount; times[15] += time - lasttime; lasttime = time;

            //////for (i = 0; i < r; i++)
            //////{
            //////    for (j = 0; j < c; j++)
            //////    {
            //////        iiend = Math.Min(i, convolvee.RowCount);
            //////        jjend = Math.Min(j, convolvee.ColumnCount);
            //////
            //////        group.Clear();
            //////        term = this[i - convolvee.RowCount + 1, j - convolvee.ColumnCount + 1];
            //////
            //////        for (ii = Math.Max(0, i - r); ii < iiend; ii++)
            //////        {
            //////            for (jj = Math.Min(0, j - c); jj < jjend; jj++)
            //////            {
            //////                group.Add(
            //////                    new FunctionCall(
            //////                    firstOp,
            //////                    term,
            //////                    convolvee[ii, jj]));
            //////            }
            //////        }
            //////
            //////        ret[i, j] = _evaluator.CleanUp(
            //////            new FunctionCall(secondOp, group.ToArray()));
            //////    }
            //////}

            return ret;
        }

        public MatrixExpression PerPixelOperation(IPerPixelOperator oper)
        {
            //int r = RowCount + oper.GetExtraWidth(this);
            //int c = ColumnCount + oper.GetExtraHeight(this);

            //var ret = new MatrixExpression(r, c);

            //float[,] values = new float[RowCount, ColumnCount];
            ////float[,] values2 = new float[convolvee.RowCount, convolvee.ColumnCount];

            //int i;
            //int j;
            //for (i = 0; i < RowCount; i++)
            //{
            //    for (j = 0; j < ColumnCount; j++)
            //    {
            //        values[i, j] = ((Literal)this[i, j]).Value;
            //    }
            //}
            ////for (i = 0; i < convolvee.RowCount; i++)
            ////{
            ////    for (j = 0; j < convolvee.ColumnCount; j++)
            ////    {
            ////        values2[i, j] = ((Literal)convolvee[i, j]).Value;
            ////    }
            ////}

            //oper.SetValues(values);

            //int row;
            //int column;

            //for (row = 0; row < r; row++)
            //{
            //    for (column = 0; column < c; column++)
            //    {
            //        ret[row, column] = new Literal(oper.Operate(row, column));
            //    }
            //}

            //return ret;
            throw new NotImplementedException();
        }

        public Expression this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= RowCount) { throw new IndexOutOfRangeException("row"); }
                if (column < 0 || column >= ColumnCount) { throw new IndexOutOfRangeException("column"); }

                Expression expr = _array[row, column];
                return expr;
            }
            set
            {
                if (row < 0 || row >= RowCount) { throw new IndexOutOfRangeException("row"); }
                if (column < 0 || column >= ColumnCount) { throw new IndexOutOfRangeException("column"); }

                _array[row, column] = value;
            }
        }

        protected class MatrixEnumerator : IEnumerator<Expression>
        {
            public MatrixEnumerator(MatrixExpression matrix)
            {
                if (matrix == null) { throw new ArgumentNullException("matrix"); }

                //attach collection change notification

                _matrix = matrix;
            }

            private readonly MatrixExpression _matrix;
            int _row = -1;
            int _column = 0;

            #region IEnumerator<Expression> Members

            public Expression Current
            {
                get
                {
                    if (_row < 0)
                    {
                        //before first element
                        return null;
                    }
                    else if (_column < 0)
                    {
                        //after last element
                        return null;
                    }
                    else
                    {
                        //normal operation
                        return _matrix[_row, _column];
                    }
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                //detach collection change notification
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (_row < 0)
                {
                    //before first element

                    _row = 0;
                    _column = 0;

                    return true;
                }
                else if (_column < 0)
                {
                    //after last element
                    return false;
                }
                else
                {
                    //normal operation
                    _column++;
                    if (_column >= _matrix.ColumnCount)
                    {
                        _row++;
                        if (_row >= _matrix.RowCount)
                        {
                            _column = -1;
                            return false;
                        }
                        _column = 0;
                    }

                    return true;
                }
            }

            public void Reset()
            {
                _row = -1;
                _column = 0;
            }

            #endregion
        }

        #region IEnumerable<Expression> Members

        public override IEnumerator<Expression> GetEnumerator()
        {
            return new MatrixEnumerator(this);
        }

        #endregion

        public override IMathObject Eval(SolusEnvironment env)
        {
            var values = new IMathObject[RowCount, ColumnCount];
            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColumnCount; c++)
                    values[r, c] = this[r, c].Eval(env);
            return new Matrix(values);
        }

        public override Expression Clone()
        {
            var ret = new MatrixExpression(RowCount, ColumnCount);

            int i;
            int j;

            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    ret[i, j] = this[i, j];//.Clone();
                }
            }

            return ret;
        }

        //public override Expression CleanUp()
        //{
        //    var ret = new MatrixExpression(RowCount, ColumnCount);

        //    int i;
        //    int j;

        //    for (i = 0; i < RowCount; i++)
        //    {
        //        for (j = 0; j < ColumnCount; j++)
        //        {
        //            ret[i, j] = this[i, j].CleanUp();
        //        }
        //    }

        //    return ret;
        //}

        protected override void InternalApplyToExpressionTree(SolusAction action, bool applyToChildrenBeforeParent)
        {
            foreach (Expression expr in this)
            {
                expr.ApplyToExpressionTree(action, applyToChildrenBeforeParent);
            }
        }
        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);

            foreach (Expression expr in this)
            {
                expr.AcceptVisitor(visitor);
            }
        }

        public override Expression Simplify(SolusEnvironment env)
        {
            int r;
            int c;
            var values = new Expression[RowCount, ColumnCount];
            var allLiteral = true;
            var allSame = true;
            for (r = 0; r < RowCount; r++)
            for (c = 0; c < ColumnCount; c++)
            {
                var value = this[r, c].Simplify(env);
                allLiteral &= value is Literal;
                allSame &= value == this[r, c];
                values[r, c] = value;
            }

            if (allSame)
                return this;
            if (!allLiteral)
                return new MatrixExpression(values);

            var values2 = new IMathObject[RowCount, ColumnCount];
            for (r = 0; r < RowCount; r++)
            for (c = 0; c < ColumnCount; c++)
                values2[r, c] = ((Literal) values[r, c]).Value;
            var matrix = new Matrix(values2);
            return new Literal(matrix);
        }

        public override void ApplyToAll(Modulator mod)
        {
            int i;
            int j;
            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    this[i, j] = new Literal(
                        mod(((Literal) this[i, j]).Value.ToFloat()));
                }
            }
        }

        public float MeanSquareError(MatrixExpression mat)
        {
            int i;
            int j;

            float sum = 0;
            float v;

            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    v = ((Literal)this[i, j]).Value.ToFloat() -
                        ((Literal)mat[i, j]).Value.ToFloat();
                    sum += v * v;
                }
            }

            sum /= RowCount;
            sum /= ColumnCount;

            return sum;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[ ");
            for (var r = 0; r < RowCount; r++)
            {
                if (r > 0) sb.Append("; ");
                for (var c = 0; c < ColumnCount; c++)
                {
                    if (c > 0) sb.Append(", ");
                    sb.Append(this[r, c]);
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        public override IMathObject Result { get; }

        private class ResultC : IMathObject
        {
            public ResultC(MatrixExpression me) => _me = me;
            private readonly MatrixExpression _me;
            public bool? IsScalar(SolusEnvironment env) => false;
            public bool? IsVector(SolusEnvironment env) => false;
            public bool? IsMatrix(SolusEnvironment env) => true;
            public int? GetTensorRank(SolusEnvironment env) => _me.TensorRank;
            public bool? IsString(SolusEnvironment env) => false;

            public int? GetDimension(SolusEnvironment env, int index)
            {
                if (index == 0) return _me.RowCount;
                if (index == 1) return _me.ColumnCount;
                return null;
            }

            private int[] __GetDimensions;

            public int[] GetDimensions(SolusEnvironment env)
            {
                if (__GetDimensions == null)
                    __GetDimensions = new[] { _me.RowCount, _me.ColumnCount };
                return __GetDimensions;
            }

            public int? GetVectorLength(SolusEnvironment env) => null;
            public bool? IsInterval(SolusEnvironment env) => false;
            public bool? IsFunction(SolusEnvironment env) => false;
            public bool? IsExpression(SolusEnvironment env) => false;

            public bool IsConcrete => false;
            public string DocString => "";
        }
    }
}
