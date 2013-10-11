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
            return SolusParser1.ConvertFeedbackExpression(args, vars);
        }
    }
}
