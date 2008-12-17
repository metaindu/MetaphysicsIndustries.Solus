using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class FourierTransformVectorFilter : VectorFilter
    {
        protected virtual double Sign
        {
            get { return -1; }
        }

        public override Vector Apply(Vector input)
        {
            //Xk = sigma(n=0,N-1,xn*e^(-2pi*i*n*k/N)

            //e^(i*theta) = cos(theta + i*sin(theta)


            Vector x = new Vector(input.Length);
            //Vector phase = new Vector(input.Length);

            int k;
            int n;
            double real;
            double imag;
            double z;

            double twoPi = -2 * Math.PI * Sign;
            double scale;
            double value;

            for (k = 0; k < input.Length; k++)
            {
                real = 0;
                imag = 0;

                scale = twoPi * k / input.Length;
                for (n = 0; n < input.Length; n++)
                {
                    z = n * scale;
                    value = input[n];
                    real += value * Math.Cos(z);
                    imag += value * Math.Sin(z);
                }
                x[k] = (float)Math.Sqrt(real * real + imag * imag);
                x[k] = ScaleForInverse(x[k], input.Length);
                //phase[k] = Math.Atan2(imag, real);
            }

            return x;
        }

        protected virtual double ScaleForInverse(double x, int length)
        {
            return x;
        }

        public virtual Pair<Vector> Apply2(Vector input)
        {
            return Apply2(new Pair<Vector>(input, null));
        }

        public virtual Pair<Vector> Apply2(Pair<Vector> input)
        {
            //Xk = sigma(n=0,N-1,xn*e^(-2pi*i*n*k/N)

            //e^(i*theta) = cos(theta + i*sin(theta)

            Vector inputReal = input.First;
            Vector inputImag = input.Second;

            if (inputImag == null) { inputImag = new Vector(inputReal.Length); }

            Vector outputReal = new Vector(inputReal.Length);
            Vector outputImaginary = new Vector(inputImag.Length);
            //Vector phase = new Vector(input.Length);

            int k;
            int n;
            double real;
            double imag;
            double z;

            double twoPi = (-2 * Math.PI * Sign);
            double scale;
            double valueReal;
            double valueImag;
            double ca;
            double sa;

            for (k = 0; k < inputReal.Length; k++)
            {
                real = 0;
                imag = 0;

                scale = twoPi * k / inputReal.Length;
                for (n = 0; n < inputReal.Length; n++)
                {
                    z = n * scale;
                    valueReal = inputReal[n];
                    valueImag = inputImag[n];
                    ca = Math.Cos(z);
                    sa = Math.Sin(z);
                    real += valueReal * ca - valueImag * sa;
                    imag += valueReal * sa + valueImag * ca;
                }
                outputReal[k] = real;
                outputImaginary[k] = imag;
                outputReal[k] = ScaleForInverse(outputReal[k], inputReal.Length);
                outputImaginary[k] = ScaleForInverse(outputImaginary[k], inputImag.Length);
                //x[k] = Math.Sqrt(real * real + imag * imag);
                //phase[k] = Math.Atan2(imag, real);
            }

            return new Pair<Vector>(outputReal, outputImaginary);
        }
    }
}
