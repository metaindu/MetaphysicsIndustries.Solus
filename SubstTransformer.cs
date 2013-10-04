using System;
using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus
{
    public class SubstTransformer : ExpressionTransformer<SubstTransformer.SubstTransformArgs>
    {
        public class SubstTransformArgs : TransformArgs
        {
            public string VariableToReplace;
            public Expression ExpressionToInsert;
        }

        public override bool CanTransform (Expression expr, SubstTransformArgs args)
        {
            throw new System.NotImplementedException ();
        }

        public override Expression Transform(Expression expr, SubstTransformArgs args)
        {
            return Subst(expr, args.VariableToReplace, args.ExpressionToInsert);
        }

        public Expression Subst(Expression exprToTransform, string variableToReplace, Expression exprToInsert)
        {
            if (exprToTransform is FunctionCall)
            {
                List<Expression> args = new List<Expression>(((FunctionCall)exprToTransform).Arguments);
                int i;
                for (i = 0; i < args.Count; i++)
                {
                    args[i] = Subst(args[i], variableToReplace, exprToInsert);
                }

                return new FunctionCall(((FunctionCall)exprToTransform).Function, args);
            }
            else if (exprToTransform is VariableAccess)
            {
                if (((VariableAccess)exprToTransform).VariableName == variableToReplace)
                {
                    return exprToInsert;
                }
                else
                {
                    return exprToTransform;
                }
            }
            else if (exprToTransform is Literal)
            {
                return exprToTransform;
            }
            else
            {
                throw new InvalidOperationException("Unknown expression type or invalid target of substitution");
            }
        }
    }
}

