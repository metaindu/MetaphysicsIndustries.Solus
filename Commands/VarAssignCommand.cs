
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

using System;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class VarAssignCommand : Command
    {
        public static readonly VarAssignCommand Value =
            new VarAssignCommand();

        public override string Name => "var_assign";

        public override string DocString =>
            @"var assign - store a value as a variable

  <name> := <expr>

  name
    The name of the variable.
  expr
    An expression to set as the value of the variable.";

        public override void Execute(string input, SolusEnvironment env,
            ICommandData data)
        {
            var data2 = (VarAssignCommandData) data;
            env.SetVariable(data2.Name, data2.Expr);
            Console.WriteLine($"{data2.Name} := {data2.Expr}");
        }
    }

    public class VarAssignCommandData : ICommandData
    {
        public VarAssignCommandData(string name, Expression expr)
        {
            Name = name;
            Expr = expr;
        }

        public Command Command => VarAssignCommand.Value;
        public string Name { get; }
        public Expression Expr { get; }
    }
}
