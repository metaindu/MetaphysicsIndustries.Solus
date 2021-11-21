
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

using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Solus.Commands;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Macros;

namespace MetaphysicsIndustries.Solus
{
    public class SolusEnvironment
    {
        public SolusEnvironment(bool useDefaults = true,
            SolusEnvironment parent = null)
        {
            Parent = parent;

            if (!useDefaults) return;

            var functions = new List<Function>()
            {
                SineFunction.Value,
                CosineFunction.Value,
                TangentFunction.Value,
                NaturalLogarithmFunction.Value,
                Log2Function.Value,
                Log10Function.Value,
                AbsoluteValueFunction.Value,
                SecantFunction.Value,
                CosecantFunction.Value,
                CotangentFunction.Value,
                ArccosineFunction.Value,
                ArcsineFunction.Value,
                ArctangentFunction.Value,
                ArcsecantFunction.Value,
                ArccosecantFunction.Value,
                ArccotangentFunction.Value,
                CeilingFunction.Value,
                FloorFunction.Value,
                UnitStepFunction.Value,
                Arctangent2Function.Value,
                LogarithmFunction.Value,
                DistFunction.Value,
                DistSqFunction.Value,
                LoadImageFunction.Value,
                SizeFunction.Value,
            };

            foreach (var func in functions)
            {
                SetVariable(func.DisplayName, func);
            }

            var macros = new List<Macro>
            {
                SqrtMacro.Value,
                RandMacro.Value,
                DeriveMacro.Value,
                FeedbackMacro.Value,
                SubstMacro.Value,
                AssignMacro.Value,
                DeleteMacro.Value,
                IfMacro.Value,
            };

            foreach (var macro in macros)
            {
                AddMacro(macro);
            }
        }

        protected readonly SolusEnvironment Parent;

        protected readonly Dictionary<string, IMathObject> Variables =
            new Dictionary<string, IMathObject>();

        protected readonly HashSet<string> RemovedVariables =
            new HashSet<string>();

        protected readonly Dictionary<string, Macro> Macros =
            new Dictionary<string, Macro>();

        protected readonly HashSet<string> RemovedMacros =
            new HashSet<string>();

        protected readonly Dictionary<string, Command> Commands =
            new Dictionary<string, Command>();

        protected readonly HashSet<string> RemovedCommands =
            new HashSet<string>();

        public IMathObject GetVariable(string name)
        {
            if (RemovedVariables.Contains(name))
                return null;
            if (Variables.ContainsKey(name))
                return Variables[name];
            if (Parent != null)
                return Parent.GetVariable(name);
            return null;
        }

        public void SetVariable(string name, IMathObject value)
        {
            RemovedVariables.Remove(name);
            Variables[name] = value;
        }

        public bool ContainsVariable(string name)
        {
            if (RemovedVariables.Contains(name)) return false;
            if (Variables.ContainsKey(name)) return true;
            if (Parent != null) return Parent.ContainsVariable(name);
            return false;
        }

        public void RemoveVariable(string name)
        {
            Variables.Remove(name);
            RemovedVariables.Add(name);
        }

        public int CountVariables()
        {
            return GetVariableNames().Count();
        }

        private HashSet<string> __GetVariableNames_cache;

        public IEnumerable<string> GetVariableNames()
        {
            if (__GetVariableNames_cache == null)
                __GetVariableNames_cache = new HashSet<string>();
            __GetVariableNames_cache.AddRange(Variables.Keys);
            if (Parent != null)
                __GetVariableNames_cache.AddRange(Parent.GetVariableNames());
            bool isRemoved(string name) => RemovedVariables.Contains(name);
            __GetVariableNames_cache.RemoveWhere(isRemoved);
            return __GetVariableNames_cache;
        }

        public void AddMacro(Macro macro) =>
            SetMacro(macro.Name, macro);

        public Macro GetMacro(string name)
        {
            if (RemovedMacros.Contains(name))
                return null;
            if (Macros.ContainsKey(name))
                return Macros[name];
            if (Parent != null)
                return Parent.GetMacro(name);
            return null;
        }

        public void SetMacro(string name, Macro value)
        {
            RemovedMacros.Remove(name);
            Macros[name] = value;
        }

        public bool ContainsMacro(string name)
        {
            if (RemovedMacros.Contains(name)) return false;
            if (Macros.ContainsKey(name)) return true;
            if (Parent != null) return Parent.ContainsMacro(name);
            return false;
        }

        public void RemoveMacro(string name)
        {
            Macros.Remove(name);
            RemovedMacros.Add(name);
        }

        public int CountMacros()
        {
            return GetMacroNames().Count();
        }

        private HashSet<string> __GetMacroNames_cache;

        public IEnumerable<string> GetMacroNames()
        {
            if (__GetMacroNames_cache == null)
                __GetMacroNames_cache = new HashSet<string>();
            __GetMacroNames_cache.Clear();
            __GetMacroNames_cache.AddRange(Macros.Keys);
            if (Parent != null)
                __GetMacroNames_cache.AddRange(Parent.GetMacroNames());
            bool isRemoved(string name) => RemovedMacros.Contains(name);
            __GetMacroNames_cache.RemoveWhere(isRemoved);
            return __GetMacroNames_cache;
        }

        public SolusEnvironment Clone()
        {
            var clone = Instantiate(false);
            PopulateClone(clone);
            return clone;
        }

        protected virtual SolusEnvironment Instantiate(
            bool useDefaults = false, SolusEnvironment parent = null)
        {
            return new SolusEnvironment(useDefaults, parent);
        }

        protected virtual void PopulateClone(SolusEnvironment clone)
        {
            foreach (var name in GetVariableNames())
                clone.SetVariable(name, GetVariable(name));
            foreach (var name in GetMacroNames())
                clone.SetMacro(name, GetMacro(name));
        }

        public SolusEnvironment CreateChildEnvironment() =>
            Instantiate(false, this);
    }
}
