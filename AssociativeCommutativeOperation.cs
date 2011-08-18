using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class AssociativeCommutativeOperation : Operation
    {
        private static AdditionOperation _addition = new AdditionOperation();
        public static AdditionOperation Addition
        {
            get { return _addition; }
        }
        private static MultiplicationOperation _multiplication = new MultiplicationOperation();
        public static MultiplicationOperation Multiplication
        {
            get { return _multiplication; }
        }
        private static BitwiseOrOperation _bitwiseOr = new BitwiseOrOperation();
        public static BitwiseOrOperation BitwiseOr
        {
            get { return _bitwiseOr; }
        }
        //public static readonly BitwiseAndOperation BitwiseAnd = new BitwiseAndOperation();



        protected override void CheckArguments(Expression[] args)
        {
            if (args.Length < 2)
            {
                throw new InvalidOperationException("Wrong number of arguments given to " + DisplayName + " (given " + args.Length.ToString() + ", require at least 2)");
            }

            if (args.Length != Types.Count)
            {
                Types.Clear();

                foreach (Expression arg in args)
                {
                    Types.Add(typeof(Expression));
                }
            }

            base.CheckArguments(args);
        }

        //protected override Expression InternalCleanUp(Expression[] args)
        //{
        //    if (args.Length == 1)
        //    {
        //        return args[0];
        //    }

        //    args = CleanUpPartAssociativeOperation(args);

        //    if (args.Length == 1)
        //    {
        //        return args[0];
        //    }

        //    if (Collapses)
        //    {
        //        foreach (Expression arg in args)
        //        {
        //            if (arg is Literal && (arg as Literal).Value == CollapseValue)
        //            {
        //                return new Literal(CollapseValue);
        //            }
        //        }
        //    }

        //    if (Culls)
        //    {
        //        List<Expression> args2 = new List<Expression>(args.Length);
        //        foreach (Expression arg in args)
        //        {
        //            if (!(arg is Literal) || (arg as Literal).Value != CullValue)
        //            {
        //                args2.Add(arg);
        //            }
        //        }

        //        if (args2.Count < args.Length)
        //        {
        //            args = args2.ToArray();
        //        }
        //    }

        //    if (args.Length == 1)
        //    {
        //        return args[0];
        //    }

        //    bool call = true;
        //    foreach (Expression arg in args)
        //    {
        //        if (!(arg is Literal))
        //        {
        //            call = false;
        //            break;
        //        }
        //    }

        //    if (call)
        //    {
        //        return Call(null, args);
        //    }

        //    return new FunctionCall(this, args);
        //}

        //protected override Expression[] InternalCleanUpPartAssociativeOperation(Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals)
        //{
        //    List<Expression> newArgs = new List<Expression>(nonLiterals.Count + 1);
        //    newArgs.Add(combinedLiteral);
        //    newArgs.AddRange(nonLiterals);
        //    return newArgs.ToArray();
        //}

        public override bool IsCommutative
        {
            get
            {
                return true;
            }
        }

        public override bool IsAssociative
        {
            get
            {
                return true;
            }
        }

        //if the operation collapses, then any argument that evaluates to the collapse value will cause the result of the entire operation to be that value
        //e.g. a * 0 = 0

        public virtual bool Collapses
        {
            get { return false; }
        }

        public virtual float CollapseValue
        {
            get { return 0; }
        }

        //if the operation culls, then any argument that evaluates to the cull value should be removed
        //e.g. a + 0 = a

        public virtual bool Culls
        {
            get { return true; }
        }

        public virtual float CullValue 
        {
            get { return IdentityValue; }
        }
    }
}
