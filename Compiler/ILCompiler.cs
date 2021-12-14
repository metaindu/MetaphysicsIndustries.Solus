
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
using System.Linq;
using System.Reflection.Emit;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public class ILCompiler
    {
        public CompiledExpression Compile(Expression expr)
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

            foreach (var instruction in setup)
            {
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen);
            }

            foreach (var instruction in instructions)
            {
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen);
            }

            foreach (var instruction in shutdown)
            {
                instructionOffsets.Add(gen.ILOffset);
                instruction.Emit(gen);
            }

            var del =
                (Func<Dictionary<string, float>, float>)method.CreateDelegate(
                    typeof(Func<Dictionary<string, float>, float>));

            return new CompiledExpression{
                Method = del,
                CompiledVars = args
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
            var bakedEnv = new Dictionary<string, float>();
            if (compiled != null)
                BakeEnvironment(compiled, env, eval, ref bakedEnv);
            else
            {
                // static initialize Instruction
                Instruction.LoadConstant(0).ToString();

                compiled = Compile(expr);
            }

            return compiled.Method(bakedEnv).ToNumber();
        }

        public void BakeEnvironment(
            CompiledExpression compiled, SolusEnvironment env,
            IEvaluator eval, ref Dictionary<string, float> bakedEnv)
        {
            if (bakedEnv == null)
                bakedEnv = new Dictionary<string, float>();
            bakedEnv.Clear();
            foreach (var var in compiled.CompiledVars)
            {
                var target = env.GetVariable(var);
                if (target.IsIsExpression(env))
                    target = eval.Eval((Expression)target, env);
                bakedEnv[var] = target.ToNumber().Value;
            }
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
            switch (func)
            {
                case AbsoluteValueFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case AdditionOperation ao:
                    return ConvertToInstructions(ao, varmap, arguments);
                case ArccosecantFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case ArccosineFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case ArccotangentFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case ArcsecantFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case ArcsineFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case Arctangent2Function ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case ArctangentFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case BitwiseAndOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case BitwiseOrOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case CeilingFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case CosecantFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case CosineFunction c:
                    return ConvertToInstructions(c, varmap, arguments);
                case CotangentFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case DistFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case DistSqFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case DivisionOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case EqualComparisonOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case ExponentOperation eo:
                    return ConvertToInstructions(eo, varmap, arguments);
                case FactorialFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case FloorFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case GreaterThanComparisonOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case GreaterThanOrEqualComparisonOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case LessThanComparisonOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case LessThanOrEqualComparisonOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case LoadImageFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case Log10Function ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case Log2Function ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case LogarithmFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case LogicalAndOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case LogicalOrOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case MaximumFiniteFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case MaximumFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case MinimumFiniteFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case MinimumFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case ModularDivision ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case MultiplicationOperation mo:
                    return ConvertToInstructions(mo, varmap, arguments);
                case NaturalLogarithmFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case NegationOperation no:
                    return ConvertToInstructions(no, varmap, arguments);
                case NotEqualComparisonOperation ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case SecantFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case SineFunction s:
                    return ConvertToInstructions(s, varmap, arguments);
                case SizeFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case TangentFunction ff:
                    return ConvertToInstructions(ff, varmap, arguments);
                case UnitStepFunction usf:
                    return ConvertToInstructions(usf, varmap, arguments);
                default:
                    throw new ArgumentException(
                        $"Unsupported function type: \"{func}\"",
                        nameof(func));
            }
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            AbsoluteValueFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Abs),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            AdditionOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            IlExpression expr = new RawInstructions();

            bool first = true;
            foreach (var arg in arguments)
            {
                if (!first &&
                    arg is FunctionCall call &&
                    call.Function is Literal literal &&
                    literal.Value is Function f &&
                    f == NegationOperation.Value)
                {
                    expr = new SubIlExpression(expr,
                        new RawInstructions(
                            ConvertToInstructions(
                                call.Arguments[0], varmap).ToArray()));
                }
                else
                {
                    if (first)
                        expr = new RawInstructions(
                            ConvertToInstructions(arg, varmap).ToArray());
                    else
                        expr = new AddIlExpression(expr,
                            new RawInstructions(
                                ConvertToInstructions(
                                    arg, varmap).ToArray()));
                    first = false;
                }
            }

            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            ArccosecantFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                typeof(Math).GetMethod(
                    "Asin", new [] { typeof(float) }),
                new DivIlExpression(
                    new LoadConstantIlExpression(1f),
                    new RawInstructions(
                        ConvertToInstructions(arguments[0],
                            varmap).ToArray())));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            ArccosineFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Acos),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            ArccotangentFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Atan2),
                new LoadConstantIlExpression(1f),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            ArcsecantFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Acos),
                new DivIlExpression(
                    new LoadConstantIlExpression(1f),
                    new RawInstructions(
                        ConvertToInstructions(
                            arguments[0], varmap).ToArray())));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            ArcsineFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Asin),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            Arctangent2Function func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Atan2),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()),
                new RawInstructions(
                    ConvertToInstructions(arguments[1], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            ArctangentFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Atan),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            BitwiseAndOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new AndIlExpression(
                    new ConvertI4IlExpression(
                        new RawInstructions(
                            ConvertToInstructions(
                                arguments[0], varmap).ToArray())),
                    new ConvertI4IlExpression(
                        new RawInstructions(
                            ConvertToInstructions(
                                arguments[1], varmap).ToArray()))));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            BitwiseOrOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new OrIlExpression(
                    new ConvertI4IlExpression(
                        new RawInstructions(
                            ConvertToInstructions(
                                arguments[0], varmap).ToArray())),
                    new ConvertI4IlExpression(
                        new RawInstructions(
                            ConvertToInstructions(
                                arguments[1], varmap).ToArray()))));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            CeilingFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Ceiling),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            CosecantFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new DivIlExpression(
                new LoadConstantIlExpression(1f),
                new CallIlExpression(
                    new Func<double, double>(Math.Sin),
                    new RawInstructions(
                        ConvertToInstructions(
                            arguments[0], varmap).ToArray())));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            CosineFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Cos),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            CotangentFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new DivIlExpression(
                new LoadConstantIlExpression(1f),
                new CallIlExpression(
                    new Func<double, double>(Math.Tan),
                    new RawInstructions(
                        ConvertToInstructions(
                            arguments[0], varmap).ToArray())));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            DistFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Sqrt),
                new AddIlExpression(
                    new IlExpressionSequence(
                        new RawInstructions(
                            ConvertToInstructions(arguments[0],
                                varmap).ToArray()),
                        new RawInstructions(
                            Instruction.Dup(),
                            Instruction.Mul())),
                    new IlExpressionSequence(
                        new RawInstructions(
                            ConvertToInstructions(arguments[1],
                                varmap).ToArray()),
                        new RawInstructions(
                            Instruction.Dup(),
                            Instruction.Mul()))));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            DistSqFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new AddIlExpression(
                new IlExpressionSequence(
                    new RawInstructions(
                        ConvertToInstructions(arguments[0],
                            varmap).ToArray()),
                    new RawInstructions(
                        Instruction.Dup(),
                        Instruction.Mul())),
                new IlExpressionSequence(
                    new RawInstructions(
                        ConvertToInstructions(arguments[1],
                            varmap).ToArray()),
                    new RawInstructions(
                        Instruction.Dup(),
                        Instruction.Mul())));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            DivisionOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new DivIlExpression(
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()),
                new RawInstructions(
                    ConvertToInstructions(arguments[1], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            EqualComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new RawInstructions(
                        ConvertToInstructions(arguments[0],
                            varmap).ToArray()),
                    new RawInstructions(
                        ConvertToInstructions(arguments[1],
                            varmap).ToArray())));
            expr.GetInstructions(instructions);
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
            FactorialFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            FloorFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Floor),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            GreaterThanComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareGreaterThanIlExpression(
                    new RawInstructions(
                        ConvertToInstructions(arguments[0],
                            varmap).ToArray()),
                    new RawInstructions(
                        ConvertToInstructions(arguments[1],
                            varmap).ToArray())));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            GreaterThanOrEqualComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new CompareLessThanIlExpression(
                        new RawInstructions(
                            ConvertToInstructions(arguments[0],
                                varmap).ToArray()),
                        new RawInstructions(
                            ConvertToInstructions(arguments[1],
                                varmap).ToArray()))));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            LessThanComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareLessThanIlExpression(
                    new RawInstructions(
                        ConvertToInstructions(arguments[0],
                            varmap).ToArray()),
                    new RawInstructions(
                        ConvertToInstructions(arguments[1],
                            varmap).ToArray())));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            LessThanOrEqualComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new CompareGreaterThanIlExpression(
                        new RawInstructions(
                            ConvertToInstructions(arguments[0],
                                varmap).ToArray()),
                        new RawInstructions(
                            ConvertToInstructions(arguments[1],
                                varmap).ToArray()))));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            LoadImageFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            Log10Function func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Log10),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            Log2Function func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Log),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()),
                new LoadConstantIlExpression(2f));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            LogarithmFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Log),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()),
                new RawInstructions(
                    ConvertToInstructions(arguments[1], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            LogicalAndOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new AddIlExpression(
                        new CompareEqualIlExpression(
                            new LoadConstantIlExpression(0),
                            new ConvertI4IlExpression(
                                new RawInstructions(
                                    ConvertToInstructions(arguments[0],
                                        varmap).ToArray()))),
                        new CompareEqualIlExpression(
                            new LoadConstantIlExpression(0),
                            new ConvertI4IlExpression(
                                new RawInstructions(
                                    ConvertToInstructions(arguments[1],
                                        varmap).ToArray()))))));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            LogicalOrOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareLessThanIlExpression(
                    new AddIlExpression(
                        new CompareEqualIlExpression(
                            new LoadConstantIlExpression(0),
                            new ConvertI4IlExpression(
                                new RawInstructions(
                                    ConvertToInstructions(arguments[0],
                                        varmap).ToArray()))),
                        new CompareEqualIlExpression(
                            new LoadConstantIlExpression(0),
                            new ConvertI4IlExpression(
                                new RawInstructions(
                                    ConvertToInstructions(arguments[1],
                                        varmap).ToArray())))),
                    new LoadConstantIlExpression(2)));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            MaximumFiniteFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            MaximumFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            MinimumFiniteFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            MinimumFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            ModularDivision func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new RemIlExpression(
                    new ConvertI4IlExpression(
                        new RawInstructions(
                            ConvertToInstructions(arguments[0],
                                varmap).ToArray())),
                    new ConvertI4IlExpression(
                        new RawInstructions(
                            ConvertToInstructions(arguments[1],
                                varmap).ToArray()))));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            MultiplicationOperation func,
            VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            IlExpression expr = new RawInstructions(
                ConvertToInstructions(arguments[0], varmap).ToArray());
            int i;
            for (i = 1; i < arguments.Count; i++)
                expr = new MulIlExpression(
                    expr,
                    new RawInstructions(
                        ConvertToInstructions(arguments[i],
                            varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            NaturalLogarithmFunction func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Log),
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            NegationOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new NegIlExpression(
                new RawInstructions(
                    ConvertToInstructions(arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            NotEqualComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new CompareEqualIlExpression(
                        new RawInstructions(
                            ConvertToInstructions(
                                arguments[0], varmap).ToArray()),
                        new RawInstructions(
                            ConvertToInstructions(
                                arguments[1], varmap).ToArray()))));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            SecantFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new DivIlExpression(
                new LoadConstantIlExpression(1f),
                new CallIlExpression(
                    new Func<double, double>(Math.Cos),
                    new RawInstructions(
                        ConvertToInstructions(
                            arguments[0], varmap).ToArray())));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            SineFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Sin),
                new RawInstructions(
                    ConvertToInstructions(
                        arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            SizeFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            TangentFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Tan),
                new RawInstructions(
                    ConvertToInstructions(
                        arguments[0], varmap).ToArray()));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            UnitStepFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareLessThanIlExpression(
                    new CompareLessThanIlExpression(
                        new RawInstructions(
                            ConvertToInstructions(
                                arguments[0], varmap).ToArray()),
                        new LoadConstantIlExpression(0f)),
                    new LoadConstantIlExpression(1)));
            expr.GetInstructions(instructions);
            return instructions;
        }

        public IEnumerable<Instruction> ConvertToInstructions(
            UserDefinedFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }
    }
}
