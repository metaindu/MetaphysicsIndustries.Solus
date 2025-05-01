using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class IsWellDefinedFunction : Function
    {
        public static readonly IsWellDefinedFunction Value =
            new IsWellDefinedFunction();

        private IsWellDefinedFunction()
        {
        }

        public override string Name => "is_well_defined";

        public override IReadOnlyList<Parameter> Parameters { get; } =
            new List<Parameter>
            {
                new Parameter("expr", Sets.Expressions.Value)
            };

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes) => Booleans.Value;

        public override IFunctionType FunctionType { get; } =
            Sets.Functions.Get(
                Booleans.Value,
                Sets.Expressions.Value);
    }
}