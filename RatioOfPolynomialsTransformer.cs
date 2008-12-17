using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class RatioOfPolynomialsTransformer : PolynomialTransformer
    {
        public static readonly RatioOfPolynomialsTransformer DefaultInstance = new RatioOfPolynomialsTransformer();

        //public override bool CanTransform(Expression expr, VariableTransformArgs args)
        //{
        //    if (expr is Literal) return true;
        //    if (expr is VariableAccess) return true;
        //    if (expr is FunctionCall)
        //    {
        //        Function func = ((FunctionCall)expr).Function;
        //        Expression[] fargs = ((FunctionCall)expr).Arguments.ToArray();

        //        if (func is DivisionOperation || func is MultiplicationOperation || func is AdditionOperation)
        //        {
        //            return true;
        //        }
        //        else if (func is ExponentOperation)
        //        {
        //            return fargs[1] is Literal || !(ContainsVariable(fargs[0], args.Variable));
        //        }
        //        else
        //        {
        //            foreach (Expression arg in fargs)
        //            {
        //                if (ContainsVariable(arg, args.Variable))
        //                {
        //                    return false;
        //                }
        //            }
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        public override Expression Transform(Expression expr, VariableTransformArgs args)
        {
            if (expr is FunctionCall && ((FunctionCall)expr).Function is DivisionOperation)
            {
                List<Expression> fargs = ((FunctionCall)expr).Arguments;

                return new FunctionCall(new DivisionOperation(),
                    base.Transform(fargs[0], args),
                    base.Transform(fargs[1], args));
            }
            else
            {
                return base.Transform(expr, args);
            }
        }
    }
}
