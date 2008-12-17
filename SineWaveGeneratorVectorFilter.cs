using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SineWaveGeneratorVectorFilter : VectorFilter
    {
        public SineWaveGeneratorVectorFilter(double amplitude, double frequency, double phaseOffset)
        {
            _amplitude = amplitude;
            _frequency = frequency;
            _phaseOffset = phaseOffset;
        }

        double _amplitude;
        double _frequency;
        double _phaseOffset;

        public override Vector Apply(Vector input)
        {
            int i;
            Vector output = new Vector(input.Length);

            for (i = 0; i < input.Length; i++)
            {
                output[i] += _amplitude * Math.Sin(2 * Math.PI * _frequency * i + _phaseOffset);
            }

            return output;
        }
    }
}
