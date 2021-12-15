using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus.Compiler.IlExpressions
{
    public class LoadLocalIlExpression : IlExpression
    {
        public LoadLocalIlExpression(ushort varNumber)
        {
            VarNumber = varNumber;
        }

        public ushort VarNumber { get; }
        
        public override void GetInstructions(NascentMethod nm)
        {
            nm.Instructions.Add(Instruction.LoadLocalVariable(VarNumber));
        }
    }
}