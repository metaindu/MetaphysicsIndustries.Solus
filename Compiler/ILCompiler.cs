
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
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public CompiledExpression Compile(Expression expr)
        {
            var nm = new NascentMethod();
            var ilexpr = ConvertToIlExpression(expr, nm);
            ilexpr.GetInstructions(nm);

            DynamicMethod method =
                new DynamicMethod(
                    name: this.ToString(),
                    returnType: typeof(object),
                    parameterTypes: new []
                    {
                        typeof(CompiledEnvironment)
                    });

            var gen = method.GetILGenerator();
            var gen2 = new ILRecorder(new ILGeneratorAdapter(gen));

            var dtype = typeof(CompiledEnvironment);
            var get_Item = dtype.GetProperty("Item").GetGetMethod();

            ushort n = 0;
            var setup = new List<Instruction>();
            var locals = new List<LocalBuilder>();

            int compileVarsCount = 0;
            int i;
            for (i = 0; i < nm.Locals.Count; i++)
                if (nm.Locals[i].Usage == IlLocalUsage.InitFromCompiledEnv)
                    compileVarsCount++;
            var cenv = new string[compileVarsCount];
            IlParam cenvParam = null;
            int cenvParamIndex = -1;

            compileVarsCount = 0;
            for (i = 0; i < nm.Locals.Count; i++)
            {
                var ilLocal = nm.Locals[i];
                locals.Add(gen.DeclareLocal(ilLocal.LocalType));
                switch (ilLocal.Usage)
                {
                    case IlLocalUsage.InitFromCompiledEnv:
                        cenv[compileVarsCount] = ilLocal.VariableName;
                        compileVarsCount++;
                        if (cenvParam == null)
                        {
                            cenvParam = nm.CreateParam(
                                typeof(CompiledEnvironment));
                            cenvParamIndex =
                                nm.GetParamIndex(cenvParam);
                        }

                        setup.Add(Instruction.LoadArgument(
                            (ushort)cenvParamIndex));
                        setup.Add(
                            Instruction.LoadString(ilLocal.VariableName));
                        setup.Add(Instruction.Call(get_Item));
                        setup.Add(Instruction.StoreLocalVariable(n));
                        break;
                }
            }

            var shutdown = new List<Instruction>();
            var resultType = ilexpr.ResultType;
            if (resultType == typeof(byte) ||
                resultType == typeof(sbyte) ||
                resultType == typeof(short) ||
                resultType == typeof(ushort) ||
                resultType == typeof(int) ||
                resultType == typeof(uint) ||
                resultType == typeof(long) ||
                resultType == typeof(ulong) ||
                resultType == typeof(float) ||
                resultType == typeof(double) ||
                resultType == typeof(bool))
                shutdown.Add(Instruction.Box(typeof(float)));
            shutdown.Add(Instruction.Return());

            var instructionOffsets = new List<int>();

            foreach (var instruction in setup)
            {
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen2);
            }

            Dictionary<IlLabel, Label> labels =
                new Dictionary<IlLabel, Label>();

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

            Func<CompiledEnvironment, object> del;
            try
            {
                del =
                    (Func<CompiledEnvironment, object>)method.CreateDelegate(
                        typeof(Func<CompiledEnvironment, object>));
            }
            catch (InvalidProgramException ipe)
            {
                Console.WriteLine(ipe);
                throw;
            }

            return new CompiledExpression{
                Method = del,
                CompiledVars = cenv,
                nm = nm,
                ilexpr = ilexpr,
                setup = setup,
                shutdown = shutdown,
            };
        }

        public IMathObject FastEval(Expression expr, SolusEnvironment env)
        {
            CompiledExpression compiled = null;
            return FastEval(expr, env, ref compiled);
        }
        public IMathObject FastEval(Expression expr, SolusEnvironment env,
            ref CompiledExpression compiled)
        {
            var eval = new BasicEvaluator();
            var cenv = new CompiledEnvironment();
            if (compiled != null)
                cenv = CompileEnvironment(compiled, env, eval);
            else
            {
                // static initialize Instruction
                Instruction.LoadConstant(0).ToString();

                compiled = Compile(expr);
            }

            return compiled.Evaluate(cenv).ToNumber();
        }

        public CompiledEnvironment CompileEnvironment(
            CompiledExpression compiled, SolusEnvironment env,
            IEvaluator eval)
        {
            var cenv = new CompiledEnvironment();
            foreach (var var in compiled.CompiledVars)
            {
                var target = env.GetVariable(var);
                if (target != null)
                {
                    if (target.IsIsExpression(env))
                        target = eval.Eval((Expression)target, env);
                    cenv[var] = target.ToNumber().Value;
                }
            }

            return cenv;
        }
    }
}
