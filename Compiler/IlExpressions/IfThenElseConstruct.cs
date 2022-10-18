using System;

namespace MetaphysicsIndustries.Solus.Compiler.IlExpressions
{
    public class IfThenElseConstruct : IlExpression
    {
        public IfThenElseConstruct(IlExpression condition=null,
            IlExpression thenBlock=null, IlExpression elseBlock=null)
        {
            Condition = condition;
            ThenBlock = thenBlock;
            ElseBlock = elseBlock;
        }

        public IlExpression Condition { get; }
        public IlExpression ThenBlock { get; }
        public IlExpression ElseBlock { get; }

        protected override void GetInstructionsInternal(NascentMethod nm)
        {
            if (Condition != null)
                Condition.GetInstructions(nm);
            if (ThenBlock != null && ElseBlock != null)
            {
                var nop = new NopIlExpression();
                var br1 = new BrFalseIlExpression(ElseBlock);
                br1.GetInstructions(nm);
                ThenBlock.GetInstructions(nm);
                var br2 = new BranchIlExpression(nop);
                br2.GetInstructions(nm);
                ElseBlock.GetInstructions(nm);
                nop.GetInstructions(nm);
            }
            else if (ThenBlock != null)
            {
                var nop = new NopIlExpression();
                var br = new BrFalseIlExpression(nop);
                br.GetInstructions(nm);
                ThenBlock.GetInstructions(nm);
                nop.GetInstructions(nm);
            }
            else if (ElseBlock != null)
            {
                var nop = new NopIlExpression();
                var br = new BrTrueIlExpression(nop);
                br.GetInstructions(nm);
                ElseBlock.GetInstructions(nm);
                nop.GetInstructions(nm);
            }
            else
            {
                nm.Instructions.Add(Instruction.Pop());
            }
        }

        public override Type ResultType =>
            throw new NotImplementedException();
    }
}