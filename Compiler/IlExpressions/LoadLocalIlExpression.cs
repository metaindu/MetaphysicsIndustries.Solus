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
        
        public override void GetInstructions(IList<Instruction> instructions)
        {
            instructions.Add(Instruction.LoadLocalVariable(VarNumber));
        }
    }
}