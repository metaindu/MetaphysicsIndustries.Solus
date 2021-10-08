
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

        public readonly Dictionary<string, Expression> Variables = new Dictionary<string, Expression>();
        public readonly Dictionary<string, Function> Functions = new Dictionary<string, Function>();
        public readonly Dictionary<string, Macro> Macros = new Dictionary<string, Macro>();
        public readonly Dictionary<string, Command> Commands =
            new Dictionary<string, Command>();

        public void AddFunction(Function func)
        {
            Functions.Add(func.DisplayName, func);
        }

        public void AddMacro(Macro macro)
        {
            Macros.Add(macro.Name, macro);
        }

        public void AddCommand(Command command)
        {
            Commands.Add(command.Name, command);
        }

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
