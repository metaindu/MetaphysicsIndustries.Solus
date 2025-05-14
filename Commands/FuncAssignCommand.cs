
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;

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
            var data2 = (FuncAssignCommandData)data;
            var paramList = new List<Parameter>();
            int i;
            for (i = 0; i < data2.Parameters.Count; i++)
            {
                var name = data2.Parameters[i].Value1;
                ISet type = Reals.Value;
                var typeRef = data2.Parameters[i].Value2;
                if (typeRef != null)
                {
                    var refValue = env.GetVariable(typeRef.VariableName);
                    if (refValue != null)
                    {
                        if (!refValue.IsIsSet(env))
                            throw new TypeException(
                                "Parameter type must be a set");
                        type = refValue.ToSet();
                    }
                }

                paramList.Add(new Parameter(name, type));
            }

            var func = new UserDefinedFunction(data2.FuncName, paramList,
                data2.Expr);

            env.SetVariable(func.DisplayName, func);

            var varrefs = new List<VariableAccess>();
            foreach (var p in func.Parameters)
                varrefs.Add(new VariableAccess(p.Name));
            var fcall = new FunctionCall(func, varrefs);
            Console.WriteLine($"{fcall} := {func.Expression}");
        }
    }

    public class FuncAssignCommandData : ICommandData
    {
        public FuncAssignCommandData(string funcName,
            List<STuple<string, VariableAccess>> parameters, Expression expr)
        {
            FuncName = funcName;
            Parameters = parameters;
            Expr = expr;
        }

        public Command Command => FuncAssignCommand.Value;
        public readonly string FuncName;
        public readonly List<STuple<string, VariableAccess>> Parameters;
        public readonly Expression Expr;
    }
}
