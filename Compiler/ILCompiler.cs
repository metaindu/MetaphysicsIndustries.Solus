
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
using System.Reflection.Emit;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        /// <summary>
        /// Convert an expression tree into a compiled form that can be
        /// easily executed.
        /// </summary>
        /// <param name="expr">The expression to compile</param>
        /// <param name="variableTypesByName">
        /// A mapping of variable names to IMathObject's representing the
        /// variables' types.
        /// </param>
        /// <returns>CompiledExpression</returns>
        /// <exception cref="NameException">
        /// If a variable reference appears in the expression but there is no
        /// corresponding entry in variableTypesByName
        /// </exception>
        public CompiledExpression Compile(Expression expr,
            IDictionary<string, IMathObject> variableTypesByName=null)
        {
            var nm = new NascentMethod();

            var ilexpr = ConvertToIlExpression(expr, nm);

            var varNames = new string[nm.Params.Count];
            var paramTypes = new Type[nm.Params.Count];
            var typeEnv = new SolusEnvironment();
            int i = 0;
            foreach (var param in nm.Params)
            {
                var varName = param.ParamName;
                varNames[i] = varName;

                if (!variableTypesByName.ContainsKey(varName))
                    throw new NameException(
                        $"The variable \"{varName}\" doesn't have " +
                        $"a runtime type defined in `variableTypesByName`");
                var varValue = variableTypesByName[varName];
                typeEnv.SetVariable(varName, varValue);
                var paramType = ResolveType(varValue);
                param.ParamType = paramType;
                paramTypes[i] = paramType;
                i++;
            }

            var returnType = ResolveType(expr.Result, typeEnv);

            ilexpr.GetInstructions(nm);

            DynamicMethod method =
                new DynamicMethod(
                    name: this.ToString(),
                    returnType: returnType,
                    parameterTypes: paramTypes);

            var gen = method.GetILGenerator();
            var gen2 = new ILRecorder(new ILGeneratorAdapter(gen));

            var setup = new List<Instruction>();

            var shutdown = new List<Instruction>();
            shutdown.Add(Instruction.Return());

            var instructionOffsets = new List<int>();

            foreach (var instruction in setup)
            {
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen2);
            }

            var labels = new Dictionary<IlLabel, Label>();

            foreach (var ilLabel in nm.GetAllLabels())
                labels[ilLabel] = gen.DefineLabel();

            i = 0;
            foreach (var instruction in nm.Instructions)
            {
                var ilLabels = nm.GetLabelsByLocation(i);
                if (ilLabels != null)
                    foreach (var ilLabel in ilLabels)
                        gen.MarkLabel(labels[ilLabel]);
                instructionOffsets.Add(gen.ILOffset);
                Label label = default;
                if (instruction.LabelArg != null)
                    label = labels[instruction.LabelArg];
                instruction.Emit(gen2, label);
                i++;
            }

            foreach (var instruction in shutdown)
            {
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen2);
            }

            Delegate del;

            var genTypeArgs = new List<Type>();
            genTypeArgs.AddRange(paramTypes);
            genTypeArgs.Add(returnType);
            var genericTypeName = string.Format("{0}`{1}",
                "MetaphysicsIndustries.Solus.Compiler.CompiledExpression",
                genTypeArgs.Count);
            var genericType =
                typeof(ILCompiler).Assembly.GetType(genericTypeName);
            var delegateType = genericType.MakeGenericType(
                genTypeArgs.ToArray());
            try
            {
                del = method.CreateDelegate(delegateType);
            }
            catch (InvalidProgramException ipe)
            {
                Console.WriteLine(ipe);
                throw;
            }

            return new CompiledExpression{
                Method = del,
                DelegateType = delegateType,
                VariableNames = varNames,
                ParameterTypes = paramTypes,
                nm = nm,
                ilexpr = ilexpr,
                setup = setup,
                shutdown = shutdown,
            };
        }

        public Type ResolveType(IMathObject value,
            SolusEnvironment typeEnv=null)
        {
            if (value.IsIsString(typeEnv))
                return typeof(string);
            if (value.IsIsScalar(typeEnv))
                return typeof(float);
            if (value.IsIsVector(typeEnv))
                return typeof(float[]);
            if (value.IsIsMatrix(typeEnv))
                return typeof(float[,]);
            throw new NotImplementedException(
                $"Unrecognized type, {value.GetType()}");
        }
    }
}
