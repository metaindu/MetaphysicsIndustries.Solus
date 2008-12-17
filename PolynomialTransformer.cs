using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class PolynomialTransformer : ExpressionTransformer<VariableTransformArgs>
    {
        public static bool ContainsVariable(Expression expr, Variable variable)
        {
            if (expr is Literal) return false;
            if (expr is VariableAccess) return ((VariableAccess)expr).Variable == variable;
            if (expr is FunctionCall)
            {
                foreach (Expression arg in ((FunctionCall)expr).Arguments)
                {
                    if (ContainsVariable(arg, variable)) return true;
                }
                return false;
            }
            return false;
        }

        public override bool CanTransform(Expression expr, VariableTransformArgs args)
        {
            if (expr is Literal) return true;
            if (expr is VariableAccess) return true;
            if (expr is FunctionCall)
            {
                Function func = ((FunctionCall)expr).Function;
                Expression[] fargs = ((FunctionCall)expr).Arguments.ToArray();

                if (func is DivisionOperation || func is MultiplicationOperation || func is AdditionOperation)
                {
                    return true;
                }
                else if (func is ExponentOperation)
                {
                    return fargs[1] is Literal || !(ContainsVariable(fargs[0], args.Variable));
                }
                else
                {
                    foreach (Expression arg in fargs)
                    {
                        if (ContainsVariable(arg, args.Variable))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            return false;
        }

        public override Expression Transform(Expression expr, VariableTransformArgs args)
        {
            throw new NotImplementedException();

            Dictionary<Literal, Set<Expression>> coeffs = new Dictionary<Literal, Set<Expression>>();

            if (expr.IsFunction(BinaryOperation.Division))
            {

            }
            else if (expr.IsFunction(AssociativeCommutativeOperation.Multiplication))
            {
                FunctionCall call = expr.As<FunctionCall>();

                
                Set<Expression> adds = new Set<Expression>();
                foreach (Expression arg in call.Arguments)
                {
                    if (arg.IsFunction(AssociativeCommutativeOperation.Addition) && ContainsVariable(arg, args.Variable))
                    {
                        adds.Add(arg);
                    }
                }

                if (adds.Count > 0)
                {
                    List<Expression> fargs = new List<Expression>(call.Arguments);
                    foreach (Expression arg in adds)
                    {
                        fargs.Remove(arg);
                    }
                }
            }

            throw new NotImplementedException();
        }
    }
}
