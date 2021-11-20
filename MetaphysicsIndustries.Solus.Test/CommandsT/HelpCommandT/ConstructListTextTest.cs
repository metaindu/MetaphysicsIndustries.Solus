using MetaphysicsIndustries.Solus.Commands;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CommandsT.HelpCommandT
{
    [TestFixture]
    public class ConstructListTextTest
    {
        [Test]
        public void WithDefaultEnvironmentAndCommandsYieldsText()
        {
            // given
            var env = new SolusEnvironment();
            var cs = new CommandSet();
            cs.AddCommand(DeleteCommand.Value);
            cs.AddCommand(FuncAssignCommand.Value);
            cs.AddCommand(HelpCommand.Value);
            cs.AddCommand(VarAssignCommand.Value);
            cs.AddCommand(VarsCommand.Value);
            // when
            var result = HelpCommand.ConstructListText(env, cs);
            // then
            Assert.AreEqual(@"Commands:
  delete func_assign help var_assign vars 

Functions:
  abs acos acot acsc asec asin atan atan2 ceil cos cot csc dist distsq floor 
  ln load_image log log10 log2 sec sin size tan unitstep 

Macros:
  assign delete derive feedback if rand sqrt subst 

Additional topics:
  solus t 
", result);
        }
    }
}