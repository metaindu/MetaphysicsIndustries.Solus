using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class RandomExpression : Expression
    {
        static Random _random = new Random();

        double _value = _random.NextDouble();

        public override Literal Eval(VariableTable varTable)
        {
            return new Literal(
                //_value);
                _random.NextDouble());
        }

        public override Expression Clone()
        {
            return new RandomExpression();
        }
    }
}
