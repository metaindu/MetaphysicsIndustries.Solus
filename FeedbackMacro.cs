using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class FeedbackMacro : Macro
    {
        public static readonly FeedbackMacro Value = new FeedbackMacro();

        protected FeedbackMacro()
        {
            Name = "feedback";
            NumArguments = 2;
        }

        public override Expression InternalCall(IEnumerable<Expression> args, Dictionary<string, Expression> vars)
        {
            Expression g = args.ElementAt(0);
            Expression h = args.ElementAt(1);

            return new FunctionCall(
                        DivisionOperation.Value,
                        g,
                        new FunctionCall(
                            AdditionOperation.Value,
                            new Literal(1),
                            new FunctionCall(
                                MultiplicationOperation.Value,
                                g,
                                h)));
        }
    }
}
