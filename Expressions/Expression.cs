
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

/*****************************************************************************
 *                                                                           *
 *  Expression.cs                                                            *
 *                                                                           *
 *  The basic unit of calculation and parse trees.                           *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using MetaphysicsIndustries.Giza;
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
{
	public abstract class Expression
	{
        public abstract IMathObject Eval(SolusEnvironment env);

        public class CompiledExpression
        {
            public Func<Dictionary<string, float>, float> Method;
            public string[] CompiledVars;
        }

        CompiledExpression _compiled;

        public IMathObject FastEval(SolusEnvironment env)
        {
            Dictionary<string, float> bakedEnv = new Dictionary<string, float>();
            if (_compiled != null)
            {
                foreach (var var in _compiled.CompiledVars)
                {
                    bakedEnv[var] =
                        env.GetVariable(var).Eval(env).ToNumber().Value;
                }
            }

            try
            {
                return FastEval(bakedEnv).ToNumber();
            }
            catch (Exception ignored)
            {
                return Eval(env);
            }
        }
        public float FastEval(Dictionary<string, float> env)
        {
            if (_compiled == null)
            {
                Instruction.LoadConstant(0).ToString();
                _compiled = Compile();
            }

            return _compiled.Method(env);
        }

        public abstract Expression Clone();

        public abstract void AcceptVisitor(IExpressionVisitor visitor);

        public void AcceptVisitor(
            Action<Literal> literalVisitor = null,
            Action<FunctionCall> funcVisitor = null,
            Action<VariableAccess> varVisitor = null,
            Action<DerivativeOfVariable> dvarVisitor = null)
        {
            if (literalVisitor == null) literalVisitor = DelegateExpressionVisitor.DoNothing<Literal>;
            if (funcVisitor == null) funcVisitor = DelegateExpressionVisitor.DoNothing<FunctionCall>;
            if (varVisitor == null) varVisitor = DelegateExpressionVisitor.DoNothing<VariableAccess>;
            if (dvarVisitor == null) dvarVisitor = DelegateExpressionVisitor.DoNothing<DerivativeOfVariable>;

            var visitor = new DelegateExpressionVisitor {
                LiteralVisitor = literalVisitor,
                FuncVisitor = funcVisitor,
                VarVisitor = varVisitor,
                DvarVisitor = dvarVisitor,
            };

            AcceptVisitor(visitor);
        }



        protected virtual void InternalApplyToExpressionTree(SolusAction action, bool applyToChildrenBeforeParent)
        {
        }
        public void ApplyToExpressionTree(SolusAction action)
        {
            ApplyToExpressionTree(action, true);
        }
        public void ApplyToExpressionTree(SolusAction action, bool applyToChildrenBeforeParent)
        {
            if (!applyToChildrenBeforeParent)
            {
                action(this);
            }

            InternalApplyToExpressionTree(action, applyToChildrenBeforeParent);

            if (applyToChildrenBeforeParent)
            {
                action(this);
            }
        }

        public virtual Expression PreliminaryEval(SolusEnvironment env)
        {
            return this;
        }

        public bool IsFunction(Function function)
        {
            return (this is FunctionCall && ((FunctionCall)this).Function == function);
        }

        public T As<T>()
            where T : Expression
        {
            return this as T;
        }

        public static string ToString(Expression expr)
        {
            if (expr == null)
            {
                return "[null]";
            }

            return expr.ToString();
        }

        public virtual IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap)
        {
            throw new NotImplementedException();
        }

        public CompiledExpression Compile()
        {
            var varmap = new VariableToArgumentNumberMapper();
            var instructions = ConvertToInstructions(varmap);
            var args = varmap.GetVariableNamesInIndexOrder();

            DynamicMethod method =
                new DynamicMethod(
                    name: this.ToString(),
                    returnType: typeof(float),
                    parameterTypes: new [] { typeof(Dictionary<string, float>) });

            var gen = method.GetILGenerator();

            var get_Item = typeof(Dictionary<string, float>).GetProperty("Item").GetGetMethod();

            ushort n = 0;
            var setup = new List<Instruction>();
            var locals = new List<LocalBuilder>();
            foreach (var arg in args)
            {
                locals.Add(gen.DeclareLocal(typeof(float)));

                setup.Add(Instruction.LoadArgument(0));
                setup.Add(Instruction.LoadString(arg));
                setup.Add(Instruction.Call(get_Item));
                setup.Add(Instruction.StoreLocalVariable(n));
                n++;
            }

            var shutdown = new List<Instruction>();
            shutdown.Add(Instruction.Return());

            var instructionOffsets = new List<int>();

            Logger.Log.Clear();

            foreach (var instruction in setup)
            {
                Logger.WriteLine("[{2}] IL_{0:X4} {1}", gen.ILOffset, instruction.ToString(), instructionOffsets.Count);
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen);
            }

            foreach (var instruction in instructions)
            {
                Logger.WriteLine("[{2}] IL_{0:X4} {1}", gen.ILOffset, instruction.ToString(), instructionOffsets.Count);
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen);
            }

            foreach (var instruction in shutdown)
            {
                Logger.WriteLine("[{2}] IL_{0:X4} {1}", gen.ILOffset, instruction.ToString(), instructionOffsets.Count);
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen);
            }

            var del = (Func<Dictionary<string, float>, float>)method.CreateDelegate(typeof(Func<Dictionary<string, float>, float>));

            return new CompiledExpression{
                Method = del,
                CompiledVars = args
            };
        }

        public abstract bool IsResultScalar(SolusEnvironment env);
        public abstract bool IsResultVector(SolusEnvironment env);
        public abstract bool IsResultMatrix (SolusEnvironment env);
        public abstract int GetResultTensorRank(SolusEnvironment env);
        public abstract bool IsResultString(SolusEnvironment env);
        public abstract int GetResultDimension(SolusEnvironment env, int index);
        public abstract int[] GetResultDimensions(SolusEnvironment env);
        public abstract int GetResultVectorLength(SolusEnvironment env);
        public abstract int GetResultStringLength(SolusEnvironment env);
    }
}
