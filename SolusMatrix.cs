using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MetaphysicsIndustries.Solus
{
    public class SolusMatrix : SolusTensor//, IEnumerable<Expression>
                                    //, IEnumerable<RowVector>
                                    //, IEnumerable<ColumnVector>
    {
        private static SolusEngine _engine = new SolusEngine();

        public static SolusMatrix FromUniform(double value, int rows, int columns)
        {
            return FromUniform(new Literal(value), rows, columns);
        }

        public static SolusMatrix FromUniform(Expression value, int rows, int columns)
        {
            SolusMatrix ret = new SolusMatrix(rows, columns);

            int i;
            int j;
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    ret[i,j] = value;
                }
            }

            return ret;
        }

        public SolusMatrix(int rows, int columns)
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
        }

        public SolusMatrix(int rows, int columns, params double[] initialContents)
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

        public SolusMatrix(int rows, int columns, params Expression[] initialContents)
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
                }
            }
        }

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
        private Expression[,] _array;

        public int Count { get { return RowCount * ColumnCount; } }

        public SolusVector GetRow(int row)
        {
            SolusVector ret = new SolusVector(ColumnCount);
            int i;

            for (i = 0; i < ColumnCount; i++)
            {
                Expression expr = this[row, i];
                ret[i] = expr;
            }

            return ret;
        }

        public SolusVector GetColumn(int column)
        {
            SolusVector ret = new SolusVector(RowCount);
            int i;

            for (i = 0; i < RowCount; i++)
            {
                ret[i] = this[i, column];
            }

            return ret;
        }

        public SolusMatrix GetSlice(int startRow, int startColumn, int numberOfRows, int numberOfColumns)
        {
            int i;
            int j;

            SolusMatrix mat = new SolusMatrix(numberOfRows, numberOfColumns);

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

        public SolusMatrix Convolution(SolusMatrix convolvee)
        {
            //return AdvancedConvolution(convolvee, AssociativeCommutativeOperation.Multiplication, AssociativeCommutativeOperation.Addition);
            return AdvancedConvolution(convolvee, SolusEngine.MultiplicationBiMod, SolusEngine.AdditionBiMod);
        }

        public SolusMatrix AdvancedConvolution(SolusMatrix convolvee, 
            BiModulator firstOp, BiModulator secondOp)
            //Operation firstOp, AssociativeCommutativeOperation secondOp)
        {



            int r = RowCount + convolvee.RowCount - 1;
            int c = ColumnCount + convolvee.ColumnCount - 1;

            SolusMatrix ret = new SolusMatrix(r, c);

            //int iiend;
            //int jjend;

            //List<Expression> group = new List<Expression>();
            double term;

            double[,] values1 = new double[RowCount, ColumnCount];
            double[,] values2 = new double[convolvee.RowCount, convolvee.ColumnCount];

            int i;
            int j;
            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    values1[i, j] = (((Literal)this[i, j]).Value);
                }
            }
            for (i = 0; i < convolvee.RowCount; i++)
            {
                for (j = 0; j < convolvee.ColumnCount; j++)
                {
                    values2[i, j] = (((Literal)convolvee[i, j]).Value);
                }
            }

            int n1;
            int n2;
            int k1;
            int k2;

            int[] times = new int[16];
            int lasttime = Environment.TickCount;
            int time;

            time = Environment.TickCount; times[0] += time - lasttime; lasttime = time;
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
            time = Environment.TickCount; times[15] += time - lasttime; lasttime = time;




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
            //////        ret[i, j] = _engine.CleanUp(new FunctionCall(secondOp, group.ToArray()));
            //////    }
            //////}

            return ret;
        }

        public SolusMatrix PerPixelOperation(IPerPixelOperator oper)
        {
            //int r = RowCount + oper.GetExtraWidth(this);
            //int c = ColumnCount + oper.GetExtraHeight(this);

            //SolusMatrix ret = new SolusMatrix(r, c);

            //double[,] values = new double[RowCount, ColumnCount];
            ////double[,] values2 = new double[convolvee.RowCount, convolvee.ColumnCount];

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
            public MatrixEnumerator(SolusMatrix matrix)  
            {
                if (matrix == null) { throw new ArgumentNullException("matrix"); }
                 
                //attach collection change notification

                _matrix = matrix;
            }

            SolusMatrix _matrix;
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

        public override Literal Eval(VariableTable varTable)
        {
            return new Literal(0);
        }

        public override Expression Clone()
        {
            SolusMatrix ret = new SolusMatrix(RowCount, ColumnCount);

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
        //    SolusMatrix ret = new SolusMatrix(RowCount, ColumnCount);

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

        public override Expression PreliminaryEval(VariableTable varTable)
        {
            SolusMatrix ret = new SolusMatrix(RowCount, ColumnCount);

            int i;
            int j;
            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    ret[i, j] = this[i, j].PreliminaryEval(varTable);
                }
            }

            return ret;
        }

        public override void ApplyToAll(Modulator mod)
        {
            int i;
            int j;
            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    this[i, j] = new Literal(mod((float)((Literal)this[i, j]).Value));
                }
            }
        }

        public double MeanSquareError(SolusMatrix mat)
        {
            int i;
            int j;

            double sum = 0;
            double v;

            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    v = ((Literal)this[i, j]).Value - ((Literal)mat[i, j]).Value;
                    sum += v * v;
                }
            }

            sum /= RowCount;
            sum /= ColumnCount;

            return sum;
        }
    }
}
