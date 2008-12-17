using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class Matrix : Tensor
    {
        private static SolusEngine _engine = new SolusEngine();

        public static Matrix FromUniform(double value, int rows, int columns)
        {
            Matrix ret = new Matrix(rows, columns);

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

        public Matrix(int rows, int columns)
        {
            _rowCount = rows;
            _columnCount = columns;
            _array = new double[_rowCount, _columnCount];

            int i;
            int j;

            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    _array[i, j] = 0;
                }
            }
        }

        public Matrix(int rows, int columns, params double[] initialContents)
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

        public Matrix Clone()
        {
            List<double> values = new List<double>(RowCount * ColumnCount);
            foreach (double value in this)
            {
                values.Add(value);
            }
            return new Matrix(RowCount, ColumnCount, values.ToArray());
        }

        public Matrix CloneSize()
        {
            return new Matrix(RowCount, ColumnCount);
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
        private double[,] _array;

        public int Count { get { return RowCount * ColumnCount; } }

        public Vector GetRow(int row)
        {
            Vector ret = new Vector(ColumnCount);
            int i;

            for (i = 0; i < ColumnCount; i++)
            {
                double expr = this[row, i];
                ret[i] = expr;
            }

            return ret;
        }

        public Vector GetColumn(int column)
        {
            Vector ret = new Vector(RowCount);
            int i;

            for (i = 0; i < RowCount; i++)
            {
                ret[i] = this[i, column];
            }

            return ret;
        }

        public Matrix GetSlice(int startRow, int startColumn, int numberOfRows, int numberOfColumns)
        {
            int i;
            int j;

            Matrix mat = new Matrix(numberOfRows, numberOfColumns);

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

        //public Matrix2 Multiply(double scaleFactor)
        //{
        //    //check dimensions
        //}
        ////public Vector2 Multiply(Vector2 vector)
        ////{
        //    //check dimensions
        ////}
        //public Matrix2 Multiply(Matrix2 matrix)
        //{
        //    //check dimensions
        //}

        //public Matrix2 Add(Matrix2 matrix)
        //{
        //    //check dimensions
        //}

        //operator overloads

        //public double GetDeterminant()
        //{
        //}

        public Matrix Convolution(Matrix convolvee)
        {
            //return AdvancedConvolution(convolvee, AssociativeCommutativeOperation.Multiplication, AssociativeCommutativeOperation.Addition);
            return AdvancedConvolution(convolvee, SolusEngine.MultiplicationBiMod, SolusEngine.AdditionBiMod);
        }

        public Matrix AdvancedConvolution(Matrix convolvee, 
            BiModulator firstOp, BiModulator secondOp)
            //Operation firstOp, AssociativeCommutativeOperation secondOp)
        {



            int r = RowCount + convolvee.RowCount - 1;
            int c = ColumnCount + convolvee.ColumnCount - 1;

            Matrix ret = new Matrix(r, c);

            //int iiend;
            //int jjend;

            //List<double> group = new List<double>();
            double term;



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

                            //double expr = new FunctionCall(
                            //    firstOp,
                            //    this[k1, k2],
                            //    convolvee[n1 - k1, n2 - k2]);

                            term = secondOp(term, firstOp(this[k1, k2], convolvee[n1 - k1, n2 - k2]));
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

                    //double[] terms = group.ToArray();
                    //FunctionCall fc = new FunctionCall(secondOp, terms);
                    //double expr2 = fc.CleanUp();
                    //time = Environment.TickCount; times[12] += time - lasttime; lasttime = time;
                    ret[n1, n2] = term;// fc.CleanUp();
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

            //////Matrix2 ret = new Matrix2(r, c);

            ////////int iiend;
            ////////int jjend;

            //////List<double> group = new List<double>();
            //////double term;

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

            //////                double expr = new FunctionCall(
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

            //////            double[] terms = group.ToArray();
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

        public Matrix PerPixelOperation(IPerPixelOperator oper)
        {
            int r = RowCount + oper.GetExtraWidth(this);
            int c = ColumnCount + oper.GetExtraHeight(this);

            Matrix ret = new Matrix(r, c);

            double[,] values = new double[RowCount, ColumnCount];
            //double[,] values2 = new double[convolvee.RowCount, convolvee.ColumnCount];

            int i;
            int j;
            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    values[i, j] = this[i, j];
                }
            }

            oper.SetValues(values);

            int row;
            int column;

            for (row = 0; row < r; row++)
            {
                for (column = 0; column < c; column++)
                {
                    ret[row, column] = oper.Operate(row, column);
                }
            }

            return ret;
        }

        public double this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= RowCount) { throw new IndexOutOfRangeException("row"); }
                if (column < 0 || column >= ColumnCount) { throw new IndexOutOfRangeException("column"); }

                return _array[row, column];
            }
            set
            {
                if (row < 0 || row >= RowCount) { throw new IndexOutOfRangeException("row"); }
                if (column < 0 || column >= ColumnCount) { throw new IndexOutOfRangeException("column"); }

                _array[row, column] = value;
            }
        }

        protected class MatrixEnumerator : IEnumerator<double>
        {
            public MatrixEnumerator(Matrix matrix)  
            {
                if (matrix == null) { throw new ArgumentNullException("matrix"); }
                 
                //attach collection change notification

                _matrix = matrix;
            }

            Matrix _matrix;
            int _row = -1;
            int _column = 0;

            #region IEnumerator<double> Members

            public double Current
            {
                get
                {
                    if (_row < 0)
                    {
                        //before first element
                        return double.NaN;
                    }
                    else if (_column < 0)
                    {
                        //after last element
                        return double.NaN;
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

        #region IEnumerable<double> Members

        public override IEnumerator<double> GetEnumerator()
        {
            return new MatrixEnumerator(this);
        }

        #endregion


        public override void ApplyToAll(Modulator mod)
        {
            int i;
            int j;
            for (i = 0; i < RowCount; i++)
            {
                for (j = 0; j < ColumnCount; j++)
                {
                    this[i, j] = mod(this[i, j]);
                }
            }
        }



        internal double GetValueNoCheck(int row, int column)
        {
            return _array[row, column];
        }
    }
}
