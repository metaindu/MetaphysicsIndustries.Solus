
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
using MetaphysicsIndustries.Solus.Commands;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Macros;

namespace MetaphysicsIndustries.Solus
{
    public class SolusEnvironment
    {
        public SolusEnvironment(bool useDefaults = true)
        {
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
                AddFunction(func);
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

            var commands = new List<Command>
            {
                DeleteCommand.Value,
                FuncAssignCommand.Value,
                HelpCommand.Value,
                VarAssignCommand.Value,
                VarsCommand.Value,
            };
            foreach (var command in commands)
            {
                AddCommand(command);
            }
        }

        protected readonly Dictionary<string, Expression> Variables = new Dictionary<string, Expression>();
        protected readonly Dictionary<string, Function> Functions = new Dictionary<string, Function>();
        protected readonly Dictionary<string, Macro> Macros = new Dictionary<string, Macro>();
        protected readonly Dictionary<string, Command> Commands =
            new Dictionary<string, Command>();

        public Expression GetVariable(string name) => Variables[name];

        public void SetVariable(string name, Expression value) =>
            Variables[name] = value;

        public bool ContainsVariable(string name) =>
            Variables.ContainsKey(name);

        public void RemoveVariable(string name) => Variables.Remove(name);
        public int CountVariables() => Variables.Count;
        public IEnumerable<string> GetVariableNames() => Variables.Keys;

        public void AddFunction(Function func) =>
            SetFunction(func.DisplayName, func);

        public Function GetFunction(string name) => Functions[name];

        public void SetFunction(string name, Function value) =>
            Functions[name] = value;

        public bool ContainsFunction(string name) =>
            Functions.ContainsKey(name);

        public void RemoveFunction(string name) => Functions.Remove(name);
        public int CountFunctions() => Functions.Count;
        public IEnumerable<string> GetFunctionNames() => Functions.Keys;

        public void AddMacro(Macro macro) =>
            SetMacro(macro.Name, macro);

        public Macro GetMacro(string name) => Macros[name];

        public void SetMacro(string name, Macro value) =>
            Macros[name] = value;

        public bool ContainsMacro(string name) =>
            Macros.ContainsKey(name);

        public void RemoveMacro(string name) => Macros.Remove(name);
        public int CountMacros() => Macros.Count;
        public IEnumerable<string> GetMacroNames() => Macros.Keys;

        public void AddCommand(Command command) =>
            SetCommand(command.Name, command);

        public Command GetCommand(string name) => Commands[name];

        public void SetCommand(string name, Command value) =>
            Commands[name] = value;

        public bool ContainsCommand(string name) =>
            Commands.ContainsKey(name);

        public bool RemoveCommand(string name) => Commands.Remove(name);
        public int CountCommands() => Commands.Count;
        public IEnumerable<string> GetCommandNames() => Commands.Keys;

        public SolusEnvironment Clone()
        {
            var clone = InstantiateForClone(false);
            PopulateClone(clone);
            return clone;
        }

        protected virtual SolusEnvironment InstantiateForClone(
            bool useDefaults=false)
        {
            return new SolusEnvironment(useDefaults);
        }

        protected virtual void PopulateClone(SolusEnvironment clone)
        {
            foreach (var name in Variables.Keys)
                clone.Variables[name] = Variables[name];
            foreach (var func in Functions.Values)
                clone.AddFunction(func);
            foreach (var macro in Macros.Values)
                clone.AddMacro(macro);
            foreach (var command in Commands.Values)
                clone.AddCommand(command);
        }

        public SolusEnvironment CreateChildEnvironment() =>
            throw new NotImplementedException();
    }
}
