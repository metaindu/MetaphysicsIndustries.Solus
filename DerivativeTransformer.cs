using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class DerivativeTransformer : ExpressionTransformer<VariableTransformArgs>
    {
        public override bool CanTransform(Expression expr, VariableTransformArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Expression Transform(Expression expr, VariableTransformArgs args)
        {
            if (expr == null) { throw new ArgumentNullException("expr"); }
            if (args == null) { throw new ArgumentNullException("args"); }

            Expression d = GetDerivative(expr, args.Variable);

            CleanUpTransformer cleanup = new CleanUpTransformer();
            return cleanup.CleanUp(d);
        }

        public Expression GetDerivative(Expression expr, string var)
        {
            Expression derivative;

            derivative = InternalGetDerivative(expr, var);

            CleanUpTransformer cleanup = new CleanUpTransformer();
            derivative = cleanup.CleanUp(derivative);

            return derivative;
        }

        private Expression InternalGetDerivative(Expression expr, string var)
        {
            Expression derivative;

            if (expr is Literal)
            {
                derivative = new Literal(0);
            }
            else if (expr is VariableAccess)
            {
                derivative = GetDerivativeOfVariableAccess(expr as VariableAccess, var);
            }
            else if (expr is DerivativeOfVariable)
            {
                derivative = new DerivativeOfVariable((DerivativeOfVariable)expr, var);
            }
            else if (expr is FunctionCall)
            {
                derivative = GetDerivativeOfFunctionCall(expr as FunctionCall, var);
            }
            else
            {
                throw new InvalidOperationException("Unknown kind of expression: " + expr.ToString());
            }

            return derivative;
        }

        protected Expression GetDerivativeOfFunctionCall(FunctionCall functionCall, string var)
        {

            if (functionCall.Function is Operation)
            {
                return GetDerivativeOfOperation(functionCall, var);
            }
            else if (functionCall.Arguments.Count == 1)
            {
                Expression functionDerivative;
                Expression argumentDerivative;

                Function function = functionCall.Function;

                if (function == CosineFunction.Value)
                {
                    functionDerivative = new FunctionCall(MultiplicationOperation.Value,
                        new Literal(-1),
                        new FunctionCall(SineFunction.Value, functionCall.Arguments[0]));
                }
                else if (function == SineFunction.Value)
                {
                    functionDerivative = new FunctionCall(CosineFunction.Value, functionCall.Arguments[0]);
                }
                else
                {
                    throw new NotImplementedException("Can't derive \"" + function.Name + "\"");
                }

                if (functionDerivative is Literal && (functionDerivative as Literal).Value == 0)
                {
                    return functionDerivative;
                }

                argumentDerivative = GetDerivative(functionCall.Arguments[0], var);

                if (argumentDerivative is Literal && (argumentDerivative as Literal).Value == 0)
                {
                    return argumentDerivative;
                }

                return new FunctionCall(MultiplicationOperation.Value, functionDerivative, argumentDerivative);

            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected Expression GetDerivativeOfOperation(FunctionCall functionCall, string var)
        {


            if (functionCall.Function is BinaryOperation)
            {
                return GetDerivativeOfBinaryOperation(functionCall, var);
            }
            else if (functionCall.Function is AssociativeCommutativeOperation)
            {
                return GetDerivativeOfAssociativeCommutativOperation(functionCall, var);
            }
            else
            {
                throw new InvalidOperationException("Unknown operation: " + functionCall.Function.ToString());
            }
        }

        private Expression GetDerivativeOfAssociativeCommutativOperation(FunctionCall functionCall, string var)
        {


            if (functionCall.Function == AdditionOperation.Value)
            {
                return GetDerivativeOfAdditionOperation(functionCall, var);
            }
            else if (functionCall.Function == MultiplicationOperation.Value)
            {
                return GetDerivativeOfMultiplicationOperation(functionCall, var);
            }
            else
            {
                throw new InvalidOperationException("Unknown operation: " + functionCall.Function.ToString());
            }
        }

        protected Expression GetDerivativeOfBinaryOperation(FunctionCall functionCall, string var)
        {

            BinaryOperation binaryOperation = functionCall.Function as BinaryOperation;

            if (binaryOperation == DivisionOperation.Value)
            {
                return GetDerivativeOfDivisionOperation(functionCall, var);
            }
            else if (binaryOperation == ExponentOperation.Value)
            {
                return GetDerivativeOfExponentOperation(functionCall, var);
            }
            else
            {
                throw new InvalidOperationException("Unknown binary operation: " + functionCall.Function.ToString());
            }
        }

        private Expression GetDerivativeOfExponentOperation(FunctionCall functionCall, string var)
        {

            Expression leftClone = functionCall.Arguments[0];//.Clone();
            Expression rightClone = functionCall.Arguments[1];//.Clone();
            Expression leftDerivative = GetDerivative(leftClone, var);
            Expression rightDerivative = GetDerivative(rightClone, var);

            if (leftClone is Literal && (leftClone as Literal).Value == 0)
            {
                return leftClone;
            }

            bool leftDerivZero = (leftDerivative is Literal && (leftDerivative as Literal).Value == 0);
            bool rightDerivZero = (rightDerivative is Literal && (rightDerivative as Literal).Value == 0);
            bool rightCloneZero = (rightClone is Literal && (rightClone as Literal).Value == 0);

            if ((leftDerivZero || rightCloneZero) && rightDerivZero)
            {
                return leftDerivative;
            }

            Expression rightMinusOne;
            if (rightClone is Literal)
            {
                rightMinusOne = rightClone.Clone();
                (rightMinusOne as Literal).Value--;
            }
            else
            {
                rightMinusOne =
                    new FunctionCall(AdditionOperation.Value,
                        rightClone.Clone(),
                        new Literal(-1));
            }

            Expression leftSide = null;

            if ((!leftDerivZero) && (!rightCloneZero))
            {
                Expression newExponent;

                if (rightMinusOne is Literal && (rightMinusOne as Literal).Value == 1)
                {
                    newExponent = leftClone;//.Clone();
                }
                else
                {
                    newExponent =
                        new FunctionCall(ExponentOperation.Value,
                            leftClone,//.Clone(),
                            rightMinusOne);
                }

                leftSide =
                    new FunctionCall(MultiplicationOperation.Value,
                        new FunctionCall(MultiplicationOperation.Value,
                            rightClone,
                            newExponent),
                        leftDerivative);
            }

            if (rightDerivZero)
            {
                return leftSide;
            }

            Expression rightSide =
                new FunctionCall(MultiplicationOperation.Value,
                    new FunctionCall(NaturalLogarithmFunction.Value,
                        leftClone),//.Clone()),
                    new FunctionCall(MultiplicationOperation.Value,
                        new FunctionCall(ExponentOperation.Value,
                            leftClone,//.Clone(),
                            rightClone),//.Clone()),
                        rightDerivative));

            if (leftDerivZero || rightCloneZero)
            {
                return rightSide;
            }

            return new FunctionCall(AdditionOperation.Value,
                leftSide,
                rightSide);
        }

        private Expression GetDerivativeOfDivisionOperation(FunctionCall functionCall, string var)
        {

            Expression highClone = functionCall.Arguments[0];//.Clone();
            Expression lowClone = functionCall.Arguments[1];//.Clone();
            Expression highDerivative = GetDerivative(highClone, var);
            Expression lowDerivative = GetDerivative(lowClone, var);

            return new FunctionCall(DivisionOperation.Value,
                        new FunctionCall(AdditionOperation.Value,
                            new FunctionCall(MultiplicationOperation.Value,
                                highDerivative,
                                lowClone),
                            new FunctionCall(MultiplicationOperation.Value,
                                new Literal(-1),
                                new FunctionCall(MultiplicationOperation.Value,
                                    highClone,
                                    lowDerivative))),
                        new FunctionCall(ExponentOperation.Value,
                            lowClone,
                            new Literal(2)));
        }

        private Expression GetDerivativeOfAdditionOperation(FunctionCall functionCall, string var)
        {

            List<Expression> derivatives = new List<Expression>();

            foreach (Expression arg in functionCall.Arguments)
            {
                Expression deriv = GetDerivative(arg, var);

                if (!(deriv is Literal) || (deriv as Literal).Value != 0)
                {
                    derivatives.Add(deriv);
                }
            }

            if (derivatives.Count > 0)
            {
                return new FunctionCall(AdditionOperation.Value, derivatives.ToArray());
            }
            else
            {
                return new Literal(0);
            }
        }

        private Expression GetDerivativeOfMultiplicationOperation(FunctionCall functionCall, string var)
        {

            MultiplicationOperation operation = functionCall.Function as MultiplicationOperation;

            List<Expression> args = functionCall.Arguments;

            if (operation.Collapses)
            {
                //we may expand this optimization to other 
                //functions or operations in the future, hence 
                //the use of Collapses and CollapseValue instead
                //of just 0
                foreach (Expression arg in args)
                {
                    if (arg is Literal && (arg as Literal).Value == operation.CollapseValue)
                    {
                        return new Literal(operation.CollapseValue);
                    }
                }
            }

            Dictionary<Expression, Expression> clones = new Dictionary<Expression, Expression>();
            Dictionary<Expression, Expression> derivs = new Dictionary<Expression, Expression>();

            foreach (Expression arg in args)
            {
                clones[arg] = arg;//.Clone();
                derivs[arg] = GetDerivative(arg, var);
            }

            List<Expression> addTerms = new List<Expression>(args.Count);
            List<Expression> multTerm = new List<Expression>(args.Count);

            foreach (Expression argd in args)
            {
                multTerm.Clear();

                foreach (Expression arg in args)
                {
                    if (argd == arg)
                    {
                        Expression deriv = derivs[argd];
                        if (operation.Collapses && deriv is Literal &&
                            (deriv as Literal).Value == operation.CollapseValue)
                        {
                            multTerm.Clear();
                            break;
                        }
                        else if (operation.Culls && deriv is Literal &&
                                (deriv as Literal).Value == operation.CullValue)
                        {
                            //a*1 = a, do nothing
                        }
                        else
                        {
                            multTerm.Add(deriv);
                        }
                    }
                    else
                    {
                        multTerm.Add(clones[arg]);
                    }
                }

                if (multTerm.Count >= 2)
                {
                    addTerms.Add(new FunctionCall(MultiplicationOperation.Value, multTerm.ToArray()));
                }
                else if (multTerm.Count == 1)
                {
                    addTerms.Add(multTerm[0]);
                }
            }

            if (addTerms.Count >= 2)
            {
                return new FunctionCall(AdditionOperation.Value, addTerms.ToArray());
            }
            else if (addTerms.Count == 1)
            {
                return addTerms[0];
            }
            else
            {
                return new Literal(AdditionOperation.Value.IdentityValue);
            }



            //Debug.WriteLine("GetDerivativeOfMultiplicationOperation is assuming two arguments");

            //Expression leftClone = functionCall.Arguments[0].Clone();

            //if (leftClone is Literal && (leftClone as Literal).Value == 0)
            //{
            //    return leftClone;
            //}

            //Expression rightClone = functionCall.Arguments[1].Clone();

            //if (rightClone is Literal && (rightClone as Literal).Value == 0)
            //{
            //    return rightClone;
            //}

            //Expression leftDerivative = GetDerivative(leftClone);

            ////if (

            //Expression rightDerivative = GetDerivative(rightClone);

            //bool leftDerivZero = (leftDerivative is Literal && (leftDerivative as Literal).Value == 0);
            //bool rightDerivZero = (rightDerivative is Literal && (rightDerivative as Literal).Value == 0);

            //if (leftDerivZero && rightDerivZero)
            //{
            //    return leftDerivative;
            //}
            //else if (leftDerivZero)
            //{
            //    return new FunctionCall(AssociativeCommutativOperation.Multiplication,
            //                leftClone,
            //                rightDerivative);
            //}
            //else if (rightDerivZero)
            //{
            //    return new FunctionCall(AssociativeCommutativOperation.Multiplication,
            //                leftDerivative,
            //                rightClone);
            //}

            //return new FunctionCall(AssociativeCommutativOperation.Addition,
            //            new FunctionCall(AssociativeCommutativOperation.Multiplication,
            //                leftDerivative,
            //                rightClone),
            //            new FunctionCall(AssociativeCommutativOperation.Multiplication,
            //                leftClone,
            //                rightDerivative));
        }

        protected Expression GetDerivativeOfVariableAccess(VariableAccess variableAccess, string var)
        {
            if (variableAccess.VariableName == var)
            {
                return new Literal(1);
            }
            else
            {
                return new DerivativeOfVariable(variableAccess.VariableName, var);
            }
        }

    }
}
