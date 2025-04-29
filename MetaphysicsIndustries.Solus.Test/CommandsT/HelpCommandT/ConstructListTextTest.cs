
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
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
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
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
            Assert.That(result, Is.EqualTo(@"Commands:
  delete func_assign help var_assign vars 

Functions:
  abs acos acot acsc asec asin atan atan2 ceil cos cot csc derive dist distsq 
  floor if ln load_image log log10 log2 rand sec sin size sqrt tan unitstep 

Macros:
  subst 

Types:
  Boolean Interval Matrix MatrixM2x2 MatrixM2x3 MatrixM2x4 MatrixM3x2 
  MatrixM3x3 MatrixM3x4 MatrixM4x2 MatrixM4x3 MatrixM4x4 Real Set String 
  Vector VectorR2 VectorR3 

Values:
  false true 

Additional topics:
  solus t 
"));
        }

        [Test]
        public void WithCustomFunctionYieldsText()
        {
            // given
            var env = new SolusEnvironment();
            var f = new MockFunction(
              new[] { new Parameter("", Reals.Value) }, "f");
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
            Assert.That(result, Is.EqualTo(@"Commands:
  delete func_assign help var_assign vars 

Functions:
  abs acos acot acsc asec asin atan atan2 ceil cos cot csc derive dist distsq 
  f floor if ln load_image log log10 log2 rand sec sin size sqrt tan unitstep 

Macros:
  subst 

Types:
  Boolean Interval Matrix MatrixM2x2 MatrixM2x3 MatrixM2x4 MatrixM3x2 
  MatrixM3x3 MatrixM3x4 MatrixM4x2 MatrixM4x3 MatrixM4x4 Real Set String 
  Vector VectorR2 VectorR3 

Values:
  false true 

Additional topics:
  solus t 
"));
        }

        [Test]
        public void WithCustomFunctionAsVariableYieldsText()
        {
            // given
            var env = new SolusEnvironment();
            var f = new MockFunction(
              new[] { new Parameter("", Reals.Value) },
              "f");
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
            Assert.That(result, Is.EqualTo(@"Commands:
  delete func_assign help var_assign vars 

Functions:
  abs acos acot acsc asec asin atan atan2 ceil cos cot csc derive dist distsq 
  f floor if ln load_image log log10 log2 rand sec sin size sqrt tan unitstep 

Macros:
  subst 

Types:
  Boolean Interval Matrix MatrixM2x2 MatrixM2x3 MatrixM2x4 MatrixM3x2 
  MatrixM3x3 MatrixM3x4 MatrixM4x2 MatrixM4x3 MatrixM4x4 Real Set String 
  Vector VectorR2 VectorR3 

Values:
  false true 

Additional topics:
  solus t 
"));
        }
    }
}
