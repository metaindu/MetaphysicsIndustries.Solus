
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
using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Macros;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus
{
    public class SolusEnvironment
    {
        /// <summary>
        /// An environment is a mapping between variable names and values.
        /// The names are string and the values are of type IMathObject.
        /// Often, the values are expressions, but this is not necessary. Any
        /// IMathObject is a valid value to set for a variable.
        ///
        /// By default, an environment will include mappings for various
        /// typical builtins, like trig functions.
        ///
        /// An environment can be a child of another environment. If a
        /// variable is not defined in the child environment, then the parent
        /// will be search. This process can be repeated recursively.
        /// </summary>
        /// <param name="useDefaults">
        /// Whether to include the default name mappings.</param>
        /// <param name="parent">
        /// The next environment for recursive lookup
        /// </param>
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

            SetVariable("Real", Reals.Value);
            SetVariable("Boolean", Booleans.Value);
            SetVariable("Interval", Intervals.Value);
            SetVariable("String", Strings.Value);
            SetVariable("Set", Sets.Sets.Value);
            SetVariable("Vector", AllVectors.Value);
            SetVariable("VectorR2", Vectors.R2);
            SetVariable("VectorR3", Vectors.R3);
            SetVariable("Matrix", AllMatrices.Value);
            SetVariable("MatrixM2x2", Matrices.M2x2);
            SetVariable("MatrixM2x3", Matrices.M2x3);
            SetVariable("MatrixM2x4", Matrices.M2x4);
            SetVariable("MatrixM3x2", Matrices.M3x2);
            SetVariable("MatrixM3x3", Matrices.M3x3);
            SetVariable("MatrixM3x4", Matrices.M3x4);
            SetVariable("MatrixM4x2", Matrices.M4x2);
            SetVariable("MatrixM4x3", Matrices.M4x3);
            SetVariable("MatrixM4x4", Matrices.M4x4);
        }

        protected readonly SolusEnvironment Parent;

        protected struct VariableIdentity
        {
            public VariableIdentity(IMathObject value, bool isType)
            {
                Value = value;
                IsType = isType;
            }

            public readonly IMathObject Value;
            public bool IsType;
        }

        protected readonly Dictionary<string, VariableIdentity> Variables =
            new Dictionary<string, VariableIdentity>();

        protected readonly HashSet<string> RemovedVariables =
            new HashSet<string>();

        public IMathObject GetVariable(string name)
        {
            if (RemovedVariables.Contains(name))
                return null;
            if (Variables.ContainsKey(name))
            {
                var vi = Variables[name];
                if (!vi.IsType)
                    return vi.Value;
            }
            if (Parent != null)
                return Parent.GetVariable(name);
            return null;
        }

        public ISet GetVariableType(string name)
        {
            if (RemovedVariables.Contains(name))
                return null;
            if (Variables.ContainsKey(name))
            {
                var vi = Variables[name];
                if (vi.IsType)
                    return (ISet)vi.Value;
                var value = vi.Value;
                if (value.IsIsExpression(this))
                    return ((Expression)value).GetResultType(this);
                return value.GetMathType();
            }
            if (Parent != null)
                return Parent.GetVariableType(name);
            return null;
        }

        public void SetVariable(string name, IMathObject value)
        {
            RemovedVariables.Remove(name);
            Variables[name] = new VariableIdentity(value, false);
        }

        public void SetVariableType(string name, ISet value)
        {
            RemovedVariables.Remove(name);
            Variables[name] = new VariableIdentity(value, true);
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
