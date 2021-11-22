
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
                SubstMacro.Value,
                IfMacro.Value,
            };

            foreach (var macro in macros)
            {
                SetVariable(macro.Name, macro);
            }
        }

        protected readonly SolusEnvironment Parent;

        protected readonly Dictionary<string, IMathObject> Variables =
            new Dictionary<string, IMathObject>();

        protected readonly HashSet<string> RemovedVariables =
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
        }

        public SolusEnvironment CreateChildEnvironment() =>
            Instantiate(false, this);
    }
}
