
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

using System;
using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class FuncAssignCommand : Command
    {
        public static readonly FuncAssignCommand Value =
            new FuncAssignCommand();

        public override string Name => "func_assign";

        public override string DocString =>
            @"function assign - define a new function

  <name>(<p1>, <p2>, <p3>...) := <expr>

  name
    The name of the function to create.
  p1, p2, p3...
    Names of zero or more parameters to the function.
  expr
    The body of the function. Occurrences of any parameters will be replaced
    with the values passed as arguments when the function is called.";

        public override void Execute(string input, SolusEnvironment env,
            ICommandData data)
        {
            var func = ((FuncAssignCommandData) data).Func;
            env.SetVariable(func.DisplayName, func);

            var varrefs = func.Argnames.Select(x => new VariableAccess(x));
            var fcall = new FunctionCall(func, varrefs);
            Console.WriteLine($"{fcall} := {func.Expression}");
        }
    }

    public class FuncAssignCommandData : ICommandData
    {
        public FuncAssignCommandData(UserDefinedFunction func)
        {
            Func = func;
        }

        public Command Command => FuncAssignCommand.Value;
        public UserDefinedFunction Func { get; }
    }
}
