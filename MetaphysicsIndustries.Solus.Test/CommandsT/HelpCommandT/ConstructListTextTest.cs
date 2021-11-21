
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

using MetaphysicsIndustries.Solus.Commands;
using MetaphysicsIndustries.Solus.Values;
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

        [Test]
        public void WithCustomFunctionYieldsText()
        {
            // given
            var env = new SolusEnvironment();
            var f = new MockFunction(new[] { Types.Scalar }, "f");
            f.DocStringV = "asdf";
            env.SetVariable(f.DisplayName, f);
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
  abs acos acot acsc asec asin atan atan2 ceil cos cot csc dist distsq f 
  floor ln load_image log log10 log2 sec sin size tan unitstep 

Macros:
  assign delete derive feedback if rand sqrt subst 

Additional topics:
  solus t 
", result);
        }

        [Test]
        public void WithCustomFunctionAsVariableYieldsText()
        {
            // given
            var env = new SolusEnvironment();
            var f = new MockFunction(new[] { Types.Scalar }, "f");
            f.DocStringV = "asdf";
            env.SetVariable("f", f);
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
  abs acos acot acsc asec asin atan atan2 ceil cos cot csc dist distsq f 
  floor ln load_image log log10 log2 sec sin size tan unitstep 

Macros:
  assign delete derive feedback if rand sqrt subst 

Additional topics:
  solus t 
", result);
        }
    }
}
