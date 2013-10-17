using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class RandomExpression : Expression
    {
        static Random _random = new Random();

        float _value = (float)_random.NextDouble();

        public override Literal Eval(SolusEnvironment env)
        {
            return new Literal(
                //_value);
                (float)_random.NextDouble());
        }

        public override Expression Clone()
        {
            return new RandomExpression();
        }
    }
}
