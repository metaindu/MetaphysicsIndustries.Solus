using System;
using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus
{
    public class StringExpression : Expression
    {
        public StringExpression(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public readonly string Value;
        public override Literal Eval(SolusEnvironment env)
        {
            throw new System.NotImplementedException(
                "Strings can not be treated as numbers.");
        }

        public override Expression Clone()
        {
            return new StringExpression(Value);
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}
