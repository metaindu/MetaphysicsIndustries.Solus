
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
            new DeleteCommand(new string[] { });

        public DeleteCommand(IEnumerable<string> names)
        {
            _names = names.ToArray();
        }

        private readonly string[] _names;

        public override string Name => "delete";
        public override string DocString =>
            @"delete - Delete one or more object

  delete <var> [<var>...]
  del <var> [<var>...]

  var
    The name of a variable, function, or macro.
";

        public override void Execute(string input, SolusEnvironment env)
        {
            var unknown = _names.Where(name => 
                !env.Variables.ContainsKey(name) && 
                !env.Functions.ContainsKey(name) && 
                !env.Macros.ContainsKey(name)).ToList();

            if (unknown.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine("The following variables do not exist:");
                foreach (var s in unknown)
                    sb.AppendLine(s);
                Console.WriteLine(sb.ToString());
                return;
            }

            foreach (var name in _names)
            {
                env.Variables.Remove(name);
                env.Functions.Remove(name);
                env.Macros.Remove(name);
            }
            
            Console.WriteLine("The variables were deleted successfully.");
        }
    }
}