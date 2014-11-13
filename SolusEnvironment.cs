using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SolusEnvironment
    {
        public SolusEnvironment(bool useDefaults = true)
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
                    AddFunction(func);
                }

                var macros = new List<Macro>
                {
                    SqrtMacro.Value,
                    RandMacro.Value,
                    DeriveMacro.Value,
                    FeedbackMacro.Value,
                    SubstMacro.Value,
                    AssignMacro.Value,
                    DeleteMacro.Value,
                };

                foreach (var macro in macros)
                {
                    AddMacro(macro);
                }
            }
        }

        public readonly Dictionary<string, Expression> Variables = new Dictionary<string, Expression>();
        public readonly Dictionary<string, Function> Functions = new Dictionary<string, Function>();
        public readonly Dictionary<string, Macro> Macros = new Dictionary<string, Macro>();

        public void AddFunction(Function func)
        {
            Functions.Add(func.DisplayName, func);
        }

        public void AddMacro(Macro macro)
        {
            Macros.Add(macro.Name, macro);
        }

        public SolusEnvironment CreateChildEnvironment()
        {
            SolusEnvironment env2 = new SolusEnvironment(false);

            foreach (string name in this.Variables.Keys)
            {
                env2.Variables[name] = this.Variables[name];
            }

            foreach (var func in this.Functions.Values)
            {
                env2.AddFunction(func);
            }

            foreach (var macro in this.Macros.Values)
            {
                env2.AddMacro(macro);
            }

            return env2;
        }
    }
}
