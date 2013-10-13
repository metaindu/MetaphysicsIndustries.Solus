using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class Environment
    {
        public Environment(bool useDefaults = true)
        {
            if (useDefaults)
            {
                var functions = new List<Function>()
                {
                    SineFunction.Value,
                    CosineFunction.Value,
                    TangentFunction.Value,
                    NaturalLogarithmFunction.Value,
                    Log2Function.Value,
                    Log10Function.Value,
                    AbsoluteValueFunction.Value,
                    SecantFunction.Value,
                    CosecantFunction.Value,
                    CotangentFunction.Value,
                    ArccosineFunction.Value,
                    ArcsineFunction.Value,
                    ArctangentFunction.Value,
                    ArcsecantFunction.Value,
                    ArccosecantFunction.Value,
                    ArccotangentFunction.Value,
                    CeilingFunction.Value,
                    FloorFunction.Value,
                    UnitStepFunction.Value,
                    Arctangent2Function.Value,
                    LogarithmFunction.Value,
                    IfFunction.Value,
                    DistFunction.Value,
                    DistSqFunction.Value,
                };

                foreach (var func in functions)
                {
                    Functions.Add(func.Name, func);
                }

                var macros = new List<Macro>
                {
                    SqrtMacro.Value,
                    RandMacro.Value,
                    DeriveMacro.Value,
                    FeedbackMacro.Value,
                    SubstMacro.Value,
                };

                foreach (var macro in macros)
                {
                    Macros.Add(macro.Name, macro);
                }
            }
        }

        public readonly Dictionary<string, Expression> Variables = new Dictionary<string, Expression>();
        public readonly Dictionary<string, Function> Functions = new Dictionary<string, Function>();
        public readonly Dictionary<string, Macro> Macros = new Dictionary<string, Macro>();
    }
}
