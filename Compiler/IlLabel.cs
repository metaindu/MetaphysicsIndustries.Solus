using MetaphysicsIndustries.Solus.Compiler.IlExpressions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public class IlLabel
    {
        private static int _id = 0;
        public int Id = _id++;
        public IlLabel(IlExpression ilexpr) => IlExpr = ilexpr;
        public IlExpression IlExpr { get; }
        public override string ToString() => $"IlLabel({Id}, {IlExpr})";
    }
}