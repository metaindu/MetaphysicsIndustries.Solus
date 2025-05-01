using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class ParseExprFunction : Function
    {
        public static readonly ParseExprFunction
            Value = new ParseExprFunction();

        private ParseExprFunction()
        {
        }

        public override string Name => "parse_expr";

        public override IReadOnlyList<Parameter> Parameters { get; } =
            new List<Parameter>
            {
                new Parameter("s", Strings.Value)
            };

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes) => Sets.Expressions.Value;

        public Expression ParseExpr(string s)
        {
            var p = new SolusParser();
            return p.GetExpression(s);
        }
    }
}