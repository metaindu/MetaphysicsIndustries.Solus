using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class PolynomialSimplifier : ExpressionTransformer
    {
        public override bool CanTransform(Expression expr)
        {
            throw new NotImplementedException();
        }

        public override Expression Transform(Expression expr)
        {
            if (expr is Literal) return expr;
            if (expr is VariableAccess) return expr;

            if (expr is FunctionCall)
            {
                TransformFunctionCall((FunctionCall)expr);
            }

            throw new NotImplementedException();
        }

        private void TransformFunctionCall(FunctionCall fc)
        {
            if (fc.Function is ExponentOperation)
            {
                if (fc.Arguments[1] is Literal && Literal.IsInteger(((Literal)fc.Arguments[1]).Value) &&
                    fc.Arguments[0] is FunctionCall)
                {
                    FunctionCall fcarg = (FunctionCall)fc.Arguments[0];
                    if (fcarg.Function is AdditionOperation ||
                        fcarg.Function is MultiplicationOperation)
                    {
                        //this is all wrong

                        List<Expression> terms = new List<Expression>();
                        foreach (Expression e in fcarg.Arguments)
                        {
                            terms.Add(Transform(e));
                        }

                        FunctionCall newfc = new FunctionCall();
                        newfc.Function = fcarg.Function;

                        int n = (int)(((Literal)fc.Arguments[1]).Value);
                        int i;
                        for (i = 0; i < n; i++)
                        {
                            newfc.Arguments.AddRange(terms);
                        }
                    }
                }
            }
        }
    }
}
