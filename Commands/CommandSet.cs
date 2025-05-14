
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

using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class CommandSet
    {
        private readonly Dictionary<string, Command> _commands =
            new Dictionary<string, Command>();

        public void AddCommand(Command command) =>
            SetCommand(command.Name, command);

        public Command GetCommand(string name) =>
            _commands.ContainsKey(name) ? _commands[name] : null;

        public void SetCommand(string name, Command value) =>
            _commands[name] = value;

        public bool ContainsCommand(string name) =>
            _commands.ContainsKey(name);

        public void RemoveCommand(string name) => _commands.Remove(name);
        public int CountCommands() => _commands.Count;
        public IEnumerable<string> GetCommandNames() => _commands.Keys;
    }
}