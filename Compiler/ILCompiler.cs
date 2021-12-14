
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
            var ilexpr = ConvertToIlExpression(expr, varmap);
            var args = varmap.GetVariableNamesInIndexOrder();
            var instructions = new List<Instruction>();
            ilexpr.GetInstructions(instructions);

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

        public IlExpression ConvertToIlExpression(
            Expression expr, VariableToArgumentNumberMapper varmap)
        {
            if (expr is FunctionCall call)
                return ConvertToIlExpression(call, varmap);
            if (expr is Literal lit)
                return ConvertToIlExpression(lit, varmap);
            if (expr is VariableAccess va)
                return ConvertToIlExpression(va, varmap);
            throw new ArgumentException(
                $"Unsupported expresssion type: \"{expr}\"", nameof(expr));
        }

        public IlExpression ConvertToIlExpression(
             FunctionCall expr, VariableToArgumentNumberMapper varmap)
        {
            if (expr.Function is Literal literal &&
                literal.Value is Function f)
                return ConvertToIlExpression(f, varmap, expr.Arguments);

            // TODO:
            throw new NotImplementedException(
                "What should be done? Should the expression be " +
                "evaluated? Compiled?");
        }

        public IlExpression ConvertToIlExpression(
            Literal expr, VariableToArgumentNumberMapper varmap)
        {
            if (expr.Value.IsIsScalar(null))
                return new LoadConstantIlExpression(expr.Value.ToFloat());
            throw new NotImplementedException(
                "currently only implemented for numbers.");
        }

        public IlExpression ConvertToIlExpression(
            VariableAccess expr, VariableToArgumentNumberMapper varmap)
        {
            return new LoadLocalIlExpression(varmap[expr.VariableName]);
        }

        // compile functions

        public IlExpression ConvertToIlExpression(Function func,
            VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            switch (func)
            {
                case AbsoluteValueFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case AdditionOperation ao:
                    return ConvertToIlExpression(ao, varmap, arguments);
                case ArccosecantFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case ArccosineFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case ArccotangentFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case ArcsecantFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case ArcsineFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case Arctangent2Function ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case ArctangentFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case BitwiseAndOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case BitwiseOrOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case CeilingFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case CosecantFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case CosineFunction c:
                    return ConvertToIlExpression(c, varmap, arguments);
                case CotangentFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case DistFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case DistSqFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case DivisionOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case EqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case ExponentOperation eo:
                    return ConvertToIlExpression(eo, varmap, arguments);
                case FactorialFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case FloorFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case GreaterThanComparisonOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case GreaterThanOrEqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case LessThanComparisonOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case LessThanOrEqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case LoadImageFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case Log10Function ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case Log2Function ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case LogarithmFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case LogicalAndOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case LogicalOrOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case MaximumFiniteFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case MaximumFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case MinimumFiniteFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case MinimumFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case ModularDivision ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case MultiplicationOperation mo:
                    return ConvertToIlExpression(mo, varmap, arguments);
                case NaturalLogarithmFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case NegationOperation no:
                    return ConvertToIlExpression(no, varmap, arguments);
                case NotEqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case SecantFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case SineFunction s:
                    return ConvertToIlExpression(s, varmap, arguments);
                case SizeFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case TangentFunction ff:
                    return ConvertToIlExpression(ff, varmap, arguments);
                case UnitStepFunction usf:
                    return ConvertToIlExpression(usf, varmap, arguments);
                default:
                    throw new ArgumentException(
                        $"Unsupported function type: \"{func}\"",
                        nameof(func));
            }
        }

        public IlExpression ConvertToIlExpression(
            AbsoluteValueFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            return new CallIlExpression(
                new Func<double, double>(Math.Abs),
                ConvertToIlExpression(arguments[0], varmap));
        }

        public IlExpression ConvertToIlExpression(
            AdditionOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
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
                            ConvertToIlExpression(
                                call.Arguments[0], varmap));
                }
                else
                {
                    if (first)
                        expr = ConvertToIlExpression(arg, varmap);
                    else
                        expr = new AddIlExpression(expr,
                                ConvertToIlExpression(
                                    arg, varmap));
                    first = false;
                }
            }

            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArccosecantFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                typeof(Math).GetMethod(
                    "Asin", new [] { typeof(float) }),
                new DivIlExpression(
                    new LoadConstantIlExpression(1f),
                        ConvertToIlExpression(arguments[0],
                            varmap)));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArccosineFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Acos),
                    ConvertToIlExpression(arguments[0], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArccotangentFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Atan2),
                new LoadConstantIlExpression(1f),
                    ConvertToIlExpression(arguments[0], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArcsecantFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Acos),
                new DivIlExpression(
                    new LoadConstantIlExpression(1f),
                        ConvertToIlExpression(
                            arguments[0], varmap)));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArcsineFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Asin),
                    ConvertToIlExpression(arguments[0], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            Arctangent2Function func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Atan2),
                    ConvertToIlExpression(arguments[0], varmap),
                    ConvertToIlExpression(arguments[1], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArctangentFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Atan),
                    ConvertToIlExpression(arguments[0], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            BitwiseAndOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new AndIlExpression(
                    new ConvertI4IlExpression(
                            ConvertToIlExpression(
                                arguments[0], varmap)),
                    new ConvertI4IlExpression(
                            ConvertToIlExpression(
                                arguments[1], varmap))));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            BitwiseOrOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new OrIlExpression(
                    new ConvertI4IlExpression(
                            ConvertToIlExpression(
                                arguments[0], varmap)),
                    new ConvertI4IlExpression(
                            ConvertToIlExpression(
                                arguments[1], varmap))));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            CeilingFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Ceiling),
                    ConvertToIlExpression(arguments[0], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            CosecantFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new DivIlExpression(
                new LoadConstantIlExpression(1f),
                new CallIlExpression(
                    new Func<double, double>(Math.Sin),
                        ConvertToIlExpression(
                            arguments[0], varmap)));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            CosineFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Cos),
                    ConvertToIlExpression(arguments[0], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            CotangentFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new DivIlExpression(
                new LoadConstantIlExpression(1f),
                new CallIlExpression(
                    new Func<double, double>(Math.Tan),
                        ConvertToIlExpression(
                            arguments[0], varmap)));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            DistFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var x = ConvertToIlExpression(arguments[0], varmap);
            var y = ConvertToIlExpression(arguments[1], varmap);
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Sqrt),
                new AddIlExpression(
                    new MulIlExpression(
                        x,
                        new DupIlExpression(x)),
                    new MulIlExpression(
                        y,
                        new DupIlExpression(y))));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            DistSqFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var x = ConvertToIlExpression(arguments[0], varmap);
            var y = ConvertToIlExpression(arguments[1], varmap);
            var expr = new AddIlExpression(
                new MulIlExpression(
                    x,
                    new DupIlExpression(x)),
                new MulIlExpression(
                    y,
                    new DupIlExpression(y)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            DivisionOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new DivIlExpression(
                    ConvertToIlExpression(arguments[0], varmap),
                    ConvertToIlExpression(arguments[1], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            EqualComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                        ConvertToIlExpression(arguments[0],
                            varmap),
                        ConvertToIlExpression(arguments[1],
                            varmap)));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ExponentOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            return new CallIlExpression(
                new Func<double, double, double>(Math.Pow),
                ConvertToIlExpression(arguments[0], varmap),
                ConvertToIlExpression(arguments[1], varmap));
        }

        public IlExpression ConvertToIlExpression(
            FactorialFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            FloorFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Floor),
                    ConvertToIlExpression(arguments[0], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            GreaterThanComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareGreaterThanIlExpression(
                        ConvertToIlExpression(arguments[0],
                            varmap),
                        ConvertToIlExpression(arguments[1],
                            varmap)));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            GreaterThanOrEqualComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new CompareLessThanIlExpression(
                            ConvertToIlExpression(arguments[0],
                                varmap),
                            ConvertToIlExpression(arguments[1],
                                varmap))));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            LessThanComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareLessThanIlExpression(
                        ConvertToIlExpression(arguments[0],
                            varmap),
                        ConvertToIlExpression(arguments[1],
                            varmap)));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            LessThanOrEqualComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new CompareGreaterThanIlExpression(
                            ConvertToIlExpression(arguments[0],
                                varmap),
                            ConvertToIlExpression(arguments[1],
                                varmap))));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            LoadImageFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            Log10Function func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Log10),
                    ConvertToIlExpression(arguments[0], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            Log2Function func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Log),
                    ConvertToIlExpression(arguments[0], varmap),
                new LoadConstantIlExpression(2f));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            LogarithmFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Log),
                    ConvertToIlExpression(arguments[0], varmap),
                    ConvertToIlExpression(arguments[1], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
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
                                    ConvertToIlExpression(arguments[0],
                                        varmap))),
                        new CompareEqualIlExpression(
                            new LoadConstantIlExpression(0),
                            new ConvertI4IlExpression(
                                    ConvertToIlExpression(arguments[1],
                                        varmap))))));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
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
                                    ConvertToIlExpression(arguments[0],
                                        varmap))),
                        new CompareEqualIlExpression(
                            new LoadConstantIlExpression(0),
                            new ConvertI4IlExpression(
                                    ConvertToIlExpression(arguments[1],
                                        varmap)))),
                    new LoadConstantIlExpression(2)));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            MaximumFiniteFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            MaximumFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            MinimumFiniteFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            MinimumFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            ModularDivision func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new RemIlExpression(
                    new ConvertI4IlExpression(
                            ConvertToIlExpression(arguments[0],
                                varmap)),
                    new ConvertI4IlExpression(
                            ConvertToIlExpression(arguments[1],
                                varmap))));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            MultiplicationOperation func,
            VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = ConvertToIlExpression(arguments[0], varmap);
            int i;
            for (i = 1; i < arguments.Count; i++)
                expr = new MulIlExpression(
                    expr,
                        ConvertToIlExpression(arguments[i],
                            varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            NaturalLogarithmFunction func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Log),
                    ConvertToIlExpression(arguments[0], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            NegationOperation func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new NegIlExpression(
                    ConvertToIlExpression(arguments[0], varmap));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            NotEqualComparisonOperation func,
            VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new CompareEqualIlExpression(
                            ConvertToIlExpression(
                                arguments[0], varmap),
                            ConvertToIlExpression(
                                arguments[1], varmap))));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            SecantFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var instructions = new List<Instruction>();
            var expr = new DivIlExpression(
                new LoadConstantIlExpression(1f),
                new CallIlExpression(
                    new Func<double, double>(Math.Cos),
                        ConvertToIlExpression(
                            arguments[0], varmap)));
            expr.GetInstructions(instructions);
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            SineFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Sin),
                ConvertToIlExpression(
                    arguments[0], varmap));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            SizeFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            TangentFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Tan),
                ConvertToIlExpression(
                    arguments[0], varmap));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            UnitStepFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new CompareLessThanIlExpression(
                    new CompareLessThanIlExpression(
                        ConvertToIlExpression(
                            arguments[0], varmap),
                        new LoadConstantIlExpression(0f)),
                    new LoadConstantIlExpression(1)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            UserDefinedFunction func, VariableToArgumentNumberMapper varmap,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }
    }
}
