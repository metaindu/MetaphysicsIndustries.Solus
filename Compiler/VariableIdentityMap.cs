
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

using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public class VariableIdentityMap
    {
        public VariableIdentityMap(VariableIdentityMap parent = null)
        {
            Parent = parent;
        }

        private readonly Dictionary<string, VariableIdentity> _variables =
            new Dictionary<string, VariableIdentity>();

        private readonly HashSet<string> _removedVariables =
            new HashSet<string>();

        public VariableIdentityMap Parent { get; }

        public VariableIdentity this[string name]
        {
            get => GetVariable(name);
            set => SetVariable(name, value);
        }

        public VariableIdentity GetVariable(string name)
        {
            if (_removedVariables.Contains(name))
                return null;
            if (_variables.ContainsKey(name))
                return _variables[name];
            if (Parent != null)
                return Parent[name];
            return null;
        }

        public void SetVariable(string name, VariableIdentity value)
        {
            _removedVariables.Remove(name);
            _variables[name] = value;
        }

        public bool ContainsVariable(string name)
        {
            if (_removedVariables.Contains(name)) return false;
            if (_variables.ContainsKey(name)) return true;
            if (Parent != null) return Parent.ContainsVariable(name);
            return false;
        }

        public void RemoveVariable(string name)
        {
            _variables.Remove(name);
            _removedVariables.Add(name);
        }

        public VariableIdentityMap CreateChild() =>
            new VariableIdentityMap(this);
    }
}
