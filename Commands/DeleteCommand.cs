
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class DeleteCommand : Command
    {
        public static readonly DeleteCommand Value =
            new DeleteCommand();

        public override string Name => "delete";

        public override string DocString =>
            @"delete - Delete one or more object

  delete <var> [<var>...]

  var
    The name of a variable, function, or macro.
";

        public override void Execute(string input, SolusEnvironment env,
            ICommandData data)
        {
            var data2 = (DeleteCommandData) data;
            var unknown = data2.Names.Where(name =>
                !env.ContainsVariable(name) &&
                !env.ContainsMacro(name)).ToList();

            if (unknown.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine("The following variables do not exist:");
                foreach (var s in unknown)
                    sb.AppendLine(s);
                Console.WriteLine(sb.ToString());
                return;
            }

            foreach (var name in data2.Names)
            {
                env.RemoveVariable(name);
                env.RemoveMacro(name);
            }

            Console.WriteLine("The variables were deleted successfully.");
        }
    }

    public class DeleteCommandData : ICommandData
    {
        public DeleteCommandData(IEnumerable<string> names)
        {
            Names = names.ToArray();
        }

        public Command Command => DeleteCommand.Value;
        public string[] Names { get; }
    }
}
