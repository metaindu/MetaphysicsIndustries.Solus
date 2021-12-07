
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
using MetaphysicsIndustries.Giza;
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using Expression = MetaphysicsIndustries.Solus.Expressions.Expression;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public class ILCompiler
    {
        public Expression.CompiledExpression Compile(Expression expr)
        {
            var varmap = new VariableToArgumentNumberMapper();
            var instructions = ConvertToInstructions(expr, varmap);
            var args = varmap.GetVariableNamesInIndexOrder();

            DynamicMethod method =
                new DynamicMethod(
                    name: this.ToString(),
                    returnType: typeof(float),
                    parameterTypes: new []
                    {
                        typeof(Dictionary<string, float>)
                    });

            var gen = method.GetILGenerator();

            var dtype = typeof(Dictionary<string, float>);
            var get_Item = dtype.GetProperty("Item").GetGetMethod();

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
                Logger.WriteLine(
                    "[{2}] IL_{0:X4} {1}",
                    gen.ILOffset,
                    instruction.ToString(),
                    instructionOffsets.Count);
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen);
            }

            foreach (var instruction in instructions)
            {
                Logger.WriteLine(
                    "[{2}] IL_{0:X4} {1}",
                    gen.ILOffset,
                    instruction.ToString(),
                    instructionOffsets.Count);
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen);
            }

            foreach (var instruction in shutdown)
            {
                Logger.WriteLine(
                    "[{2}] IL_{0:X4} {1}",
                    gen.ILOffset, instruction.ToString(),
                    instructionOffsets.Count);
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen);
            }

            var del =
                (Func<Dictionary<string, float>, float>)method.CreateDelegate(
                    typeof(Func<Dictionary<string, float>, float>));

            return new Expression.CompiledExpression{
                Method = del,
                CompiledVars = args
            };
        }

        public IMathObject FastEval(Expression expr, SolusEnvironment env)
        {
            Expression.CompiledExpression compiled = null;
            return FastEval(expr, env, ref compiled);
        }
        public IMathObject FastEval(Expression expr, SolusEnvironment env,
            ref Expression.CompiledExpression compiled)
        {
            var eval = new BasicEvaluator();
            var bakedEnv = new Dictionary<string, float>();
            if (compiled != null)
            {
                foreach (var var in compiled.CompiledVars)
                {
                    var target = env.GetVariable(var);
                    if (target.IsIsExpression(env))
                        target = eval.Eval((Expression)target, env);
                    bakedEnv[var] = target.ToNumber().Value;
                }
            }
            else
            {
                // static initialize Instruction
                Instruction.LoadConstant(0).ToString();

                compiled = Compile(expr);
            }

            return compiled.Method(bakedEnv).ToNumber();
        }

        // compile expressions

        public IEnumerable<Instruction> ConvertToInstructions(
            Expression expr, VariableToArgumentNumberMapper varmap)
        {
            if (expr is FunctionCall call)
                return ConvertToInstructions(call, varmap);
            if (expr is Literal lit)
                return ConvertToInstructions(lit, varmap);
            if (expr is VariableAccess va)
                return ConvertToInstructions(va, varmap);
            throw new ArgumentException(
                $"Unsupported expresssion type: \"{expr}\"", nameof(expr));
        }

        public IEnumerable<Instruction> ConvertToInstructions(
             FunctionCall expr, VariableToArgumentNumberMapper varmap)
        {
            if (expr.Function is Literal literal &&
                literal.Value is Function f)
                return ConvertToInstructions(f, varmap, expr.Arguments);
            // TODO:
            throw new NotImplementedException(
                "What should be done? Should the expression be " +
                "evaluated? Compiled?");
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            Literal expr, VariableToArgumentNumberMapper varmap)
        {
            if (expr.Value.IsIsScalar(null))
                return new []
                {
                    Instruction.LoadConstant(expr.Value.ToFloat())
                };
            throw new NotImplementedException(
                "currently only implemented for numbers.");
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            VariableAccess expr, VariableToArgumentNumberMapper varmap)
        {
            return new []
            {
                Instruction.LoadLocalVariable(varmap[expr.VariableName])
            };
        }

        // compile functions

        public IEnumerable<Instruction> ConvertToInstructions(Function func,
            VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            if (func is AdditionOperation ao)
                return ConvertToInstructions(ao, varmap, arguments);
            if (func is UnitStepFunction usf)
                return ConvertToInstructions(usf, varmap, arguments);
            if (func is CosineFunction c)
                return ConvertToInstructions(c, varmap, arguments);
            if (func is SineFunction s)
                return ConvertToInstructions(s, varmap, arguments);
            if (func is ExponentOperation eo)
                return ConvertToInstructions(eo, varmap, arguments);
            if (func is MultiplicationOperation mo)
                return ConvertToInstructions(mo, varmap, arguments);
            if (func is NegationOperation no)
                return ConvertToInstructions(no, varmap, arguments);
            throw new ArgumentException(
                $"Unsupported function type: \"{func}\"", nameof(func));
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            AdditionOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();

            bool first = true;
            foreach (var arg in arguments)
            {
                if (!first &&
                    arg is FunctionCall call &&
                    call.Function is Literal literal &&
                    literal.Value is Function f &&
                    f == NegationOperation.Value)
                {
                    instructions.AddRange(
                        ConvertToInstructions(call.Arguments[0], varmap));
                    instructions.Add(Instruction.Sub());
                }
                else
                {
                    instructions.AddRange(ConvertToInstructions(arg, varmap));
                    if (!first)
                    {
                        instructions.Add(Instruction.Add());
                    }
                    first = false;
                }
            }

            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            UnitStepFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();

            instructions.AddRange(
                ConvertToInstructions(arguments[0], varmap));

            instructions.Add(Instruction.LoadConstant(0.0f));
            instructions.Add(Instruction.CompareLessThan());
            instructions.Add(Instruction.LoadConstant(1));
            instructions.Add(Instruction.CompareLessThan());
            instructions.Add(Instruction.ConvertR4());

            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            CosineFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            List<Instruction> instructions = new List<Instruction>();
            instructions.AddRange(
                ConvertToInstructions(arguments[0], varmap));
            instructions.Add(
                Instruction.Call(
                    typeof(System.Math).GetMethod(
                        "Cos", new Type[] { typeof(float) })));
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            SineFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            List<Instruction> instructions = new List<Instruction>();
            instructions.AddRange(
                ConvertToInstructions(arguments[0], varmap));
            instructions.Add(
                Instruction.Call(
                    typeof(System.Math).GetMethod(
                        "Sin", new Type[] { typeof(float) })));
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            ExponentOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            List<Instruction> instructions = new List<Instruction>();

            instructions.AddRange(
                ConvertToInstructions(arguments[0], varmap));

            if (arguments[1] is Literal lit)
            {
                var value = lit.Value.ToFloat();

                if (value == 1f)
                {
                    return instructions;
                }
                if (value == value.Round() &&
                    value > 1 &&
                    value < 16)
                {
                    int i;
                    for (i = 1; i < value; i++)
                    {
                        instructions.Add(Instruction.Dup());
                    }
                    for (i = 1; i < value; i++)
                    {
                        instructions.Add(Instruction.Mul());
                    }
                    return instructions;
                }
                if (value == 1 / 2.0f)
                {
                    instructions.Add(
                        Instruction.Call(
                            typeof(System.Math).GetMethod(
                                "Sqrt", new Type[] { typeof(float) })));

                    return instructions;
                }
            }

            instructions.AddRange(
                ConvertToInstructions(arguments[1], varmap));
            instructions.Add(
                Instruction.Call(
                    typeof(System.Math).GetMethod("Pow")));
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            MultiplicationOperation func,
            VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            foreach (var arg in arguments)
                instructions.AddRange(
                    ConvertToInstructions(arg, varmap));
            for (int i = 1; i < arguments.Count; i++)
                instructions.Add(Instruction.Mul());
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            NegationOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            List<Instruction> instructions = new List<Instruction>();
            instructions.AddRange(
                ConvertToInstructions(arguments[0], varmap));
            instructions.Add(new Instruction {
                ArgType=Instruction.ArgumentType.None,
                OpCode=OpCodes.Neg
            });
            return instructions;
        }
    }
}
