using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SubstMacro : Macro
    {
        public static readonly SubstMacro Value = new SubstMacro();

        protected SubstMacro()
        {
            Name = "subst";
            NumArguments = 3;
        }

        public override Expression InternalCall(IEnumerable<Expression> args, Environment env)
        {
            SubstTransformer subst = new SubstTransformer();
            CleanUpTransformer cleanup = new CleanUpTransformer();
            var var = ((VariableAccess)args.ElementAt(1)).VariableName;
            return cleanup.CleanUp(subst.Subst(args.First(), var, args.ElementAt(2)));
        }
    }
}
