using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class FourierTransformMatrixFilter : MatrixFilter
    {
        //public struct Pair
        //{
        //    public Pair(Matrix real, Matrix imaginary)
        //    {
        //        _real = real;
        //        _imaginary = imaginary;
        //    }

        //    private Matrix _real;
        //    public Matrix Real
        //    {
        //        get { return _real; }
        //    }

        //    private Matrix _imaginary;
        //    public Matrix Imaginary
        //    {
        //        get { return _imaginary; }
        //    }

        //}

        public override Matrix Apply(Matrix input)
        {
            //Xk = sigma(n=0,N-1,xn*e^(-2pi*i*n*k/N)

            //e^(i*theta) = cos(theta) + i*sin(theta)

            return Apply2(input).First;











            //Matrix x = new Matrix(input.RowCount, input.ColumnCount);
            //Matrix phase = new Matrix(input.RowCount, input.ColumnCount);

            //int i;
            //int j;

            //Vector row;
            //Vector column;

            //FourierTransformVectorFilter vFilter = new FourierTransformVectorFilter();
            ////InverseFourierTransformVectorFilter vFilter2 = new InverseFourierTransformVectorFilter();

            //for (i = 0; i < input.RowCount; i++)
            //{
            //    row = input.GetRow(i);
            //    row = vFilter.Apply(row);

            //    for (j = 0; j < input.ColumnCount; j++)
            //    {
            //        x[i, j] = row[j];
            //    }
            //}

            //for (j = 0; j < input.ColumnCount; j++)
            //{
            //    column = input.GetColumn(j);
            //    column = vFilter.Apply(column);

            //    for (i = 0; i < input.RowCount; i++)
            //    {
            //        x[i, j] += column[i];
            //    }
            //}

            //return x;


            ////    for (kc = 0; kc < input.ColumnCount; kc++)
            ////    {
            ////        columnAmount = kc / (double)input.ColumnCount;

            ////        real = 0;
            ////        imag = 0;
            ////        for (nr = 0; nr < input.RowCount; nr++)
            ////        {
            ////            for (nc = 0; nc < input.ColumnCount; nc++)
            ////            {
            ////                z = twoPi * (nr * rowAmount);

            ////                value = input.GetValueNoCheck(nr, nc);

            ////                real += value * Math.Cos(z);
            ////                imag += value * Math.Sin(z);
            ////            }
            ////        }
            ////        x[kr,kc] = Math.Sqrt(real * real + imag * imag);
            ////        //phase[kr,kc] = Math.Atan2(imag, real);
            ////    }
            ////}

            //return x;
        }

        protected virtual FourierTransformVectorFilter GetVectorFilter()
        {
            return new FourierTransformVectorFilter();
        }

        public  Pair<Matrix> Apply2(Matrix input)
        {
            return Apply2(new Pair<Matrix>(input, null));
        }

        public virtual Pair<Matrix> Apply2(Pair<Matrix> input)
        {
            Matrix inputReal = input.First;
            Matrix inputImag = input.Second;

            int width = inputReal.RowCount;

            if (inputImag == null) { inputImag = new Matrix(inputReal.RowCount, inputReal.ColumnCount); }

            //output
            Matrix outputReal = new Matrix(width, width);
            Matrix outputImaginary = new Matrix(width, width);

            //temp
            Matrix tempReal = new Matrix(width, width);
            Matrix tempImaginary = new Matrix(width, width);

            int i;
            int j;
            int k;

            Vector rowReal;
            Vector rowImag;
            Vector colReal;
            Vector colImag;

            Pair<Vector> tempVectors;

            FourierTransformVectorFilter vFilter = GetVectorFilter();

            for (i = 0; i < inputReal.RowCount; i++)
            {
                rowReal = inputReal.GetRow(i);
                rowImag = inputImag.GetRow(i);

                tempVectors = vFilter.Apply2(new Pair<Vector>(rowReal, rowImag));

                for (k = 0; k < inputReal.ColumnCount; k++)
                {
                    tempReal[i, k] = tempVectors.First[k];
                    tempImaginary[i, k] = tempVectors.Second[k];
                }
            }

            for (j = 0; j < tempReal.ColumnCount; j++)
            {
                colReal = tempReal.GetColumn(j);
                colImag = tempImaginary.GetColumn(j);

                tempVectors = vFilter.Apply2(new Pair<Vector>(colReal, colImag));

                for (k = 0; k < inputReal.RowCount; k++)
                {
                    outputReal[k, j] = tempVectors.First[k];
                    outputImaginary[k, j] = tempVectors.Second[k];
                }
            }

            for (i = 0; i < outputReal.RowCount; i++)
            {
                for (j = 0; j < outputReal.ColumnCount; j++)
                {
                    outputReal[i, j] = ScaleForInverse(outputReal[i, j], outputReal.RowCount, outputReal.ColumnCount);
                    outputImaginary[i, j] = ScaleForInverse(outputImaginary[i, j], outputImaginary.RowCount, outputImaginary.ColumnCount);
                }
            }



            ////calculate the fourier transform of the columns
            //for (i = 0; i < width; i++)
            //{
            //    //This is the 1D DFT:
            //    for (j = 0; j < width; j++)
            //    {
            //        double scale = -2 * (Math.PI * j) / width;

            //        for (k = 0; k < width; k++)
            //        {
            //            double a = k * scale;
            //            double ca = Math.Cos(a);
            //            double sa = Math.Sin(a);

            //            tempReal[i, j] += inputReal[i, k] * ca - inputImag[i, k] * sa;
            //            tempImaginary[i, j] += inputReal[i, k] * sa + inputImag[i, k] * ca;
            //        }
            //    }
            //}

            ////calculate the fourier transform of the rows
            //for (j = 0; j < width; j++)
            //{
            //    //This is the 1D DFT:
            //    for (i = 0; i < width; i++)
            //    {
            //        double scale = -2 * (Math.PI * i) / width;

            //        for (k = 0; k < width; k++)
            //        {
            //            double a = k * scale;
            //            double ca = Math.Cos(a);
            //            double sa = Math.Sin(a);

            //            outputReal[i, j]      += tempReal[k, j] * ca - tempImaginary[k, j] * sa;
            //            outputImaginary[i, j] += tempReal[k, j] * sa + tempImaginary[k, j] * ca;
            //        }
            //    }
            //}

            //for (i = 0; i < width; i++)
            //{
            //    for (j = 0; j < width; j++)
            //    {
            //        outputReal[i, j] = ScaleForInverse(outputReal[i, j], inputReal.RowCount, inputReal.ColumnCount);
            //        outputImaginary[i, j] = ScaleForInverse(outputImaginary[i, j], inputImag.RowCount, inputImag.ColumnCount);
            //    }
            //}




            return new Pair<Matrix>(outputReal, outputImaginary);
        }

        protected virtual double ScaleForInverse(double x, int rows, int cols)
        {
            return x;
        }
    }
}
