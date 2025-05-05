using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class IsWellFormedFunction : Function
    {
        public static readonly IsWellFormedFunction Value =
            new IsWellFormedFunction();

        private IsWellFormedFunction()
        {
        }

        public override string Name => "is_well_formed";

        public override IReadOnlyList<Parameter> Parameters { get; } =
            new List<Parameter>
            {
                new Parameter("expr", Sets.Expressions.Value)
            };

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes) => Booleans.Value;
    }
}