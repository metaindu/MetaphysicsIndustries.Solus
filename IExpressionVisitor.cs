using System;

namespace MetaphysicsIndustries.Solus
{
    public interface IExpressionVisitor
    {
        void Visit(Literal literal);
        void Visit(FunctionCall func);
        void Visit(VariableAccess var);
        void Visit(DerivativeOfVariable dvar);

        void Visit(DelayAssignExpression dassign);
        void Visit(SolusMatrix matrix);
        void Visit(SolusVector vector);
    }

    public class DelegateExpressionVisitor : IExpressionVisitor
    {
        public static void DoNothing<T>(T t) { }

        public Action<Literal> LiteralVisitor = DoNothing<Literal>;
        public Action<FunctionCall> FuncVisitor = DoNothing<FunctionCall>;
        public Action<VariableAccess> VarVisitor = DoNothing<VariableAccess>;
        public Action<DerivativeOfVariable> DvarVisitor = DoNothing<DerivativeOfVariable>;
        public Action<DelayAssignExpression> DassignVisitor = DoNothing<DelayAssignExpression>;
        public Action<SolusMatrix> MatrixVisitor = DoNothing<SolusMatrix>;
        public Action<SolusVector> VectorVisitor = DoNothing<SolusVector>;

        public void Visit(Literal literal)
        {
            LiteralVisitor(literal);
        }

        public void Visit(FunctionCall func)
        {
            FuncVisitor(func);
        }

        public void Visit(VariableAccess var)
        {
            VarVisitor(var);
        }

        public void Visit(DerivativeOfVariable dvar)
        {
            DvarVisitor(dvar);
        }

        public void Visit(DelayAssignExpression dassign)
        {
            DassignVisitor(dassign);
        }
        public void Visit(SolusMatrix matrix)
        {
            MatrixVisitor(matrix);
        }
        public void Visit(SolusVector vector)
        {
            VectorVisitor(vector);
        }

    }
}

