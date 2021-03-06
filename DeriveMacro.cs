﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class DeriveMacro : Macro
    {
        public static readonly DeriveMacro Value = new DeriveMacro();

        protected DeriveMacro()
        {
            Name = "derive";
            NumArguments = 2;
        }

        public override Expression InternalCall(IEnumerable<Expression> args, SolusEnvironment env)
        {
            DerivativeTransformer derive = new DerivativeTransformer();
            Expression expr = args.First();
            var v = ((VariableAccess)args.ElementAt(1)).VariableName;

            return derive.Transform(expr, new VariableTransformArgs(v));
        }

        public override string DocString
        {
            get
            {
                return "The derive operator\n  derive(f(x), x)\n\nReturns the derivative of f(x) with respect to x.";
            }
        }
    }
}
