
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public class ILCompiler
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
                locals.Add(gen.DeclareLocal(typeof(float)));
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

        // compile expressions

        public IlExpression ConvertToIlExpression(
            Expression expr, NascentMethod nm)
        {
            if (expr is FunctionCall call)
                return ConvertToIlExpression(call, nm);
            if (expr is Literal lit)
                return ConvertToIlExpression(lit, nm);
            if (expr is VariableAccess va)
                return ConvertToIlExpression(va, nm);
            if (expr is ComponentAccess ca)
                return ConvertToIlExpression(ca, nm);
            if (expr is VectorExpression ve)
                return ConvertToIlExpression(ve, nm);
            if (expr is MatrixExpression me)
                return ConvertToIlExpression(me, nm);
            throw new ArgumentException(
                $"Unsupported expression type: \"{expr}\"", nameof(expr));
        }

        public IlExpression ConvertToIlExpression(
            FunctionCall expr, NascentMethod nm)
        {
            if (expr.Function is Literal literal &&
                literal.Value is Function f)
                return ConvertToIlExpression(f, nm, expr.Arguments);

            // TODO:
            throw new NotImplementedException(
                "What should be done? Should the expression be " +
                "evaluated? Compiled?");
        }

        public IlExpression ConvertToIlExpression(
            Literal expr, NascentMethod nm)
        {
            if (expr.Value.IsIsScalar(null))
            {
                var value = expr.Value.ToFloat();
                return new LoadConstantIlExpression(value);
            }

            if (expr.Value.IsIsVector(null))
            {
                var v = expr.Value.ToVector();
                var seq = new List<IlExpression>();
                var newarr = new NewArrIlExpression(
                    typeof(float),
                    new LoadConstantIlExpression(v.Length));
                seq.Add(newarr);
                int i;
                // for (i = 0; i < v.Length; i++)
                //     seq.Add();
                for (i = 0; i < v.Length; i++)
                {
                    seq.Add(
                        new StoreElemIlExpression(
                            array_: new DupIlExpression(newarr),
                            index: new LoadConstantIlExpression(i),
                            value: new LoadConstantIlExpression(
                                v[i].ToNumber().Value)));
                }

                return new IlExpressionSequence(seq);
            }

            if (expr.Value.IsIsMatrix(null))
            {
                var m = expr.Value.ToMatrix();
                var arrayType = typeof(float[,]);
                var ctor = arrayType.GetConstructor(
                    new[] { typeof(int), typeof(int) });
                var setMethod = arrayType.GetMethod("Set",
                    new[] { typeof(int), typeof(int), typeof(float) });
                var seq = new List<IlExpression>();
                seq.Add(new NewObjIlExpression(ctor,
                    new LoadConstantIlExpression(m.RowCount),
                    new LoadConstantIlExpression(m.ColumnCount)));
                var dup = new DupIlExpression();
                int r, c;
                for (r = 0; r < m.RowCount; r++)
                for (c = 0; c < m.ColumnCount; c++)
                    seq.Add(
                        new CallIlExpression(
                            setMethod,
                            dup,
                            new LoadConstantIlExpression(r),
                            new LoadConstantIlExpression(c),
                            new LoadConstantIlExpression(m[r, c].ToFloat())));

                return new IlExpressionSequence(seq);
            }

            throw new NotImplementedException(
                "currently only implemented for numbers, vectors, " +
                "and matrices.");
        }

        public IlExpression ConvertToIlExpression(
            VariableAccess expr, NascentMethod nm)
        {
            var index =
                nm.CreateIndexOfLocalForVariableName(expr.VariableName);
            var local = nm.Locals[index];
            return new LoadLocalIlExpression(local);
        }

        static Type[] GetTypeArrayOfInt(int length)
        {
            var a = new Type[length];
            int i;
            for (i = 0; i < length; i++)
                a[i] = typeof(int);
            return a;
        }

        public IlExpression ConvertToIlExpression(ComponentAccess expr,
            NascentMethod nm)
        {
            var expr2 = ConvertToIlExpression(expr.Expr, nm);
            var indexes2 = new IlExpression[expr.Indexes.Count];
            int i;
            for (i = 0; i < indexes2.Length; i++)
            {
                indexes2[i] = new ConvertI4IlExpression(
                    ConvertToIlExpression(expr.Indexes[i], nm));
            }

            // assume vector (and not string) for now
            if (expr.Indexes.Count == 1)
                return new LoadElemIlExpression(expr2, indexes2[0]);

            // higher rank tensor
            if (expr.Indexes.Count >= 2)
            {
                var arrayType = typeof(float).MakeArrayType(
                    expr.Indexes.Count);
                var getMethod = arrayType.GetMethod("Get",
                    GetTypeArrayOfInt(expr.Indexes.Count));
                var args = new IlExpression[indexes2.Length + 1];
                args[0] = expr2;
                indexes2.CopyTo(args, 1);
                var callExpr = new CallIlExpression(getMethod, args);
                return callExpr;
            }

            // TODO: string?

            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(VectorExpression expr,
            NascentMethod nm)
        {
            var seq = new List<IlExpression>();
            var newarr = new NewArrIlExpression(
                typeof(float),
                new LoadConstantIlExpression(expr.Length));
            seq.Add(newarr);
            int i;
            for (i = 0; i < expr.Length; i++)
                seq.Add(
                    new StoreElemIlExpression(
                        array_: new DupIlExpression(newarr),
                        index: new LoadConstantIlExpression(i),
                        value: ConvertToIlExpression(expr[i], nm)));

            return new IlExpressionSequence(seq);
        }

        public IlExpression ConvertToIlExpression(MatrixExpression expr,
            NascentMethod nm)
        {
            var arrayType = typeof(float[,]);
            var ctor = arrayType.GetConstructor(
                new[] { typeof(int), typeof(int) });
            var setMethod = arrayType.GetMethod("Set",
                new[] { typeof(int), typeof(int), typeof(float) });
            var seq = new List<IlExpression>();
            var newobj = new NewObjIlExpression(ctor,
                new LoadConstantIlExpression(expr.RowCount),
                new LoadConstantIlExpression(expr.ColumnCount));
            seq.Add(newobj);
            var dup = new DupIlExpression(newobj);
            int r, c;
            for (r = 0; r < expr.RowCount; r++)
            for (c = 0; c < expr.ColumnCount; c++)
                seq.Add(
                    new CallIlExpression(
                        setMethod,
                        dup,
                        new LoadConstantIlExpression(r),
                        new LoadConstantIlExpression(c),
                        ConvertToIlExpression(expr[r, c], nm)));
            return new IlExpressionSequence(seq);
        }

        // compile functions

        public IlExpression ConvertToIlExpression(Function func,
            NascentMethod nm, List<Expression> arguments)
        {
            switch (func)
            {
                case AbsoluteValueFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case AdditionOperation ao:
                    return ConvertToIlExpression(ao, nm, arguments);
                case ArccosecantFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case ArccosineFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case ArccotangentFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case ArcsecantFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case ArcsineFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case Arctangent2Function ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case ArctangentFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case BitwiseAndOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case BitwiseOrOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case CeilingFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case CosecantFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case CosineFunction c:
                    return ConvertToIlExpression(c, nm, arguments);
                case CotangentFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case DistFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case DistSqFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case DivisionOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case EqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case ExponentOperation eo:
                    return ConvertToIlExpression(eo, nm, arguments);
                case FactorialFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case FloorFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case GreaterThanComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case GreaterThanOrEqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case LessThanComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case LessThanOrEqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case LoadImageFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case Log10Function ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case Log2Function ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case LogarithmFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case LogicalAndOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case LogicalOrOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case MaximumFiniteFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case MaximumFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case MinimumFiniteFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case MinimumFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case ModularDivision ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case MultiplicationOperation mo:
                    return ConvertToIlExpression(mo, nm, arguments);
                case NaturalLogarithmFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case NegationOperation no:
                    return ConvertToIlExpression(no, nm, arguments);
                case NotEqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case SecantFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case SineFunction s:
                    return ConvertToIlExpression(s, nm, arguments);
                case SizeFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case TangentFunction ff:
                    return ConvertToIlExpression(ff, nm, arguments);
                case UnitStepFunction usf:
                    return ConvertToIlExpression(usf, nm, arguments);
                default:
                    // if (func.ProvidesCustomCall)
                    // {
                    //     var args2 = new IlExpression[arguments.Count];
                    //     int i;
                    //     for (i = 0; i < arguments.Count; i++)
                    //         args2[i] = ConvertToIlExpression(arguments[i], nm);
                    //     var expr = new CallIlExpression(new Func< func.CustomCall, args2);
                    //
                    //     var expr = ConvertToIlExpression(arguments[0], nm);
                    //     int i;
                    //     for (i = 1; i < arguments.Count; i++)
                    //     {
                    //         expr = new CallIlExpression(
                    //             new Func<float, float, float>(Math.Max),
                    //             expr,
                    //             ConvertToIlExpression(arguments[i], nm));
                    //     }
                    //
                    //     return expr;
                    //
                    //
                    //     var expr = new CallIlExpression(
                    //         new Func<double, double>(Math.Tan),
                    //         ConvertToIlExpression(arguments[0], nm));
                    //     return expr;
                    // }
                    throw new ArgumentException(
                        $"Unsupported function type: \"{func}\"",
                        nameof(func));
            }
        }

        public IlExpression ConvertToIlExpression(
            AbsoluteValueFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            return new CallIlExpression(
                new Func<double, double>(Math.Abs),
                ConvertToIlExpression(arguments[0], nm));
        }

        public IlExpression ConvertToIlExpression(
            AdditionOperation func, NascentMethod nm,
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
                        ConvertToIlExpression(call.Arguments[0], nm));
                }
                else
                {
                    if (first)
                        expr = ConvertToIlExpression(arg, nm);
                    else
                        expr = new AddIlExpression(expr,
                            ConvertToIlExpression(arg, nm));
                    first = false;
                }
            }

            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArccosecantFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                typeof(Math).GetMethod(
                    "Asin", new [] { typeof(float) }),
                new DivIlExpression(
                    new LoadConstantIlExpression(1f),
                    ConvertToIlExpression(arguments[0], nm)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArccosineFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });
            var test2 = new CompareGreaterThanIlExpression(
                new DupIlExpression(),
                new LoadConstantIlExpression(1f));
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Acos));
            var seq = new List<IlExpression>();
            var arg = ConvertToIlExpression(arguments[0], nm);
            seq.Add(arg);
            seq.Add(
                new CompareLessThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(-1f)));
            seq.Add(new BrFalseIlExpression(test2));
            seq.Add(
                new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Argument less than -1"),
                        new LoadNullIlExpression())));
            seq.Add(test2);
            seq.Add(new BrFalseIlExpression(expr));
            seq.Add(
                new ThrowIlExpression(
                    new NewObjIlExpression(ctor,
                        new LoadStringIlExpression("Argument greater than 1"),
                        new LoadNullIlExpression())));
            seq.Add(expr);

            return new IlExpressionSequence(seq);
        }

        public IlExpression ConvertToIlExpression(
            ArccotangentFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Atan2),
                new LoadConstantIlExpression(1f),
                ConvertToIlExpression(arguments[0], nm));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArcsecantFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Acos),
                new DivIlExpression(
                    new LoadConstantIlExpression(1f),
                    ConvertToIlExpression(arguments[0], nm)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArcsineFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var arg = ConvertToIlExpression(arguments[0], nm);

            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });

            var checkLess = new IfThenElseConstruct(
                new CompareLessThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(-1f)),
                new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Argument less than -1"),
                        new LoadNullIlExpression())));
            var checkGreater = new IfThenElseConstruct(
                new CompareGreaterThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(1f)),
                new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Argument greater than 1"),
                        new LoadNullIlExpression())));
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Asin));

            return new IlExpressionSequence(
                arg,
                checkLess,
                checkGreater,
                expr,
                new ConvertR4IlExpression());
        }

        public IlExpression ConvertToIlExpression(
            Arctangent2Function func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Atan2),
                ConvertToIlExpression(arguments[0], nm),
                ConvertToIlExpression(arguments[1], nm));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ArctangentFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Atan),
                ConvertToIlExpression(arguments[0], nm));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            BitwiseAndOperation func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new AndIlExpression(
                    new ConvertI4IlExpression(
                        ConvertToIlExpression(arguments[0], nm)),
                    new ConvertI4IlExpression(
                        ConvertToIlExpression(arguments[1], nm))));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            BitwiseOrOperation func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new OrIlExpression(
                    new ConvertI4IlExpression(
                        ConvertToIlExpression(arguments[0], nm)),
                    new ConvertI4IlExpression(
                        ConvertToIlExpression(arguments[1], nm))));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            CeilingFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Ceiling),
                ConvertToIlExpression(arguments[0], nm));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            CosecantFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new DivIlExpression(
                new LoadConstantIlExpression(1f),
                new CallIlExpression(
                    new Func<double, double>(Math.Sin),
                    ConvertToIlExpression(arguments[0], nm)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            CosineFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Cos),
                ConvertToIlExpression(arguments[0], nm));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            CotangentFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new DivIlExpression(
                new LoadConstantIlExpression(1f),
                new CallIlExpression(
                    new Func<double, double>(Math.Tan),
                    ConvertToIlExpression(arguments[0], nm)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            DistFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var x = ConvertToIlExpression(arguments[0], nm);
            var y = ConvertToIlExpression(arguments[1], nm);
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
            DistSqFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var x = ConvertToIlExpression(arguments[0], nm);
            var y = ConvertToIlExpression(arguments[1], nm);
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
            DivisionOperation func, NascentMethod nm,
            List<Expression> arguments)
        {
            var left = ConvertToIlExpression(arguments[0], nm);
            var right = ConvertToIlExpression(arguments[1], nm);

            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });

            var checkZero = new IfThenElseConstruct(
                new CompareEqualIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(0f)),
                new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Division by zero"),
                        new LoadNullIlExpression())));

            var expr = new DivIlExpression();
            return new IlExpressionSequence(
                left,
                right,
                checkZero,
                expr);
        }

        public IlExpression ConvertToIlExpression(
            EqualComparisonOperation func,
            NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    ConvertToIlExpression(arguments[0], nm),
                    ConvertToIlExpression(arguments[1], nm)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ExponentOperation func, NascentMethod nm,
            List<Expression> arguments)
        {
            return new CallIlExpression(
                new Func<double, double, double>(Math.Pow),
                ConvertToIlExpression(arguments[0], nm),
                ConvertToIlExpression(arguments[1], nm));
        }

        public IlExpression ConvertToIlExpression(
            FactorialFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            FloorFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Floor),
                ConvertToIlExpression(arguments[0], nm));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            GreaterThanComparisonOperation func,
            NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new CompareGreaterThanIlExpression(
                    ConvertToIlExpression(arguments[0], nm),
                    ConvertToIlExpression(arguments[1], nm)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            GreaterThanOrEqualComparisonOperation func,
            NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new CompareLessThanIlExpression(
                        ConvertToIlExpression(arguments[0], nm),
                        ConvertToIlExpression(arguments[1], nm))));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            LessThanComparisonOperation func,
            NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new CompareLessThanIlExpression(
                    ConvertToIlExpression(arguments[0], nm),
                    ConvertToIlExpression(arguments[1], nm)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            LessThanOrEqualComparisonOperation func,
            NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new CompareGreaterThanIlExpression(
                        ConvertToIlExpression(arguments[0], nm),
                        ConvertToIlExpression(arguments[1], nm))));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            LoadImageFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            Log10Function func, NascentMethod nm,
            List<Expression> arguments)
        {
            var arg = ConvertToIlExpression(arguments[0], nm);

            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });

            var checkNotPos = new IfThenElseConstruct(
                new CompareGreaterThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(0f)),
                elseBlock: new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Argument must be positive"),
                        new LoadNullIlExpression())));

            var expr = new CallIlExpression(
                new Func<double, double>(Math.Log10));

            return new IlExpressionSequence(
                arg,
                checkNotPos,
                expr,
                new ConvertR4IlExpression());
        }

        public IlExpression ConvertToIlExpression(
            Log2Function func, NascentMethod nm,
            List<Expression> arguments)
        {
            var arg = ConvertToIlExpression(arguments[0], nm);

            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });

            var checkNotPos = new IfThenElseConstruct(
                new CompareGreaterThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(0f)),
                elseBlock: new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Argument must be positive"),
                        new LoadNullIlExpression())));

            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Log),
                // arg,
                new LoadConstantIlExpression(2f));

            return new IlExpressionSequence(
                arg,
                checkNotPos,
                expr,
                new ConvertR4IlExpression());
        }

        public IlExpression ConvertToIlExpression(
            LogarithmFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });

            var arg = ConvertToIlExpression(arguments[0], nm);

            var checkArgNotPos = new IfThenElseConstruct(
                new CompareGreaterThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(0f)),
                elseBlock: new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Argument must be positive"),
                        new LoadNullIlExpression())));

            var base_ = ConvertToIlExpression(arguments[1], nm);

            var checkBaseNotPos = new IfThenElseConstruct(
                new CompareGreaterThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(0f)),
                elseBlock: new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Base must be positive"),
                        new LoadNullIlExpression())));
            var checkBaseNotOne = new IfThenElseConstruct(
                new CompareEqualIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(1f)),
                thenBlock: new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Base must not be one"),
                        new LoadNullIlExpression())));

            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Log)
                // arg,
                // base_
                );
            return new IlExpressionSequence(
                arg,
                checkArgNotPos,
                base_,
                checkBaseNotPos,
                checkBaseNotOne,
                expr,
                new ConvertR4IlExpression());
        }

        public IlExpression ConvertToIlExpression(
            LogicalAndOperation func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new AddIlExpression(
                        new CompareEqualIlExpression(
                            new LoadConstantIlExpression(0),
                            new ConvertI4IlExpression(
                                ConvertToIlExpression(arguments[0], nm))),
                        new CompareEqualIlExpression(
                            new LoadConstantIlExpression(0),
                            new ConvertI4IlExpression(
                                ConvertToIlExpression(arguments[1], nm))))));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            LogicalOrOperation func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new CompareLessThanIlExpression(
                    new AddIlExpression(
                        new CompareEqualIlExpression(
                            new LoadConstantIlExpression(0),
                            new ConvertI4IlExpression(
                                ConvertToIlExpression(arguments[0], nm))),
                        new CompareEqualIlExpression(
                            new LoadConstantIlExpression(0),
                            new ConvertI4IlExpression(
                                ConvertToIlExpression(arguments[1], nm)))),
                    new LoadConstantIlExpression(2)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            MaximumFiniteFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var exprs = new List<IlExpression>(8 * arguments.Count + 7);
            exprs.Add(new LoadConstantIlExpression(float.NegativeInfinity));
            var nop = new NopIlExpression();
            IlExpression first = nop;
            for (int i = arguments.Count - 1; i >= 0; i--)
            {
                var calcArg = ConvertToIlExpression(arguments[i], nm);
                var call1 = new CallIlExpression(
                    new Func<float, bool>(float.IsInfinity),
                    new DupIlExpression());
                var call2 = new CallIlExpression(
                    new Func<float, bool>(float.IsNaN),
                    new DupIlExpression());
                var pop = new PopIlExpression();
                var br1 = new BrTrueIlExpression(pop);
                var br2 = new BrTrueIlExpression(pop);
                var call3 = new CallIlExpression(
                    new Func<float, float, float>(Math.Max));
                var br3 = new BranchIlExpression(first);

                exprs.Insert(1, calcArg);
                exprs.Insert(2, call1);
                exprs.Insert(3, br1);
                exprs.Insert(4, call2);
                exprs.Insert(5, br2);
                exprs.Insert(6, call3);
                exprs.Insert(7, br3);
                exprs.Insert(8, pop);

                first = calcArg;
            }

            exprs.Add(nop);

            exprs.Add(
                new CallIlExpression(
                    new Func<float, bool>(float.IsNegativeInfinity),
                    new DupIlExpression()));
            var conv = new ConvertR4IlExpression();
            exprs.Add(new BrFalseIlExpression(conv));
            exprs.Add(new PopIlExpression());
            exprs.Add(new LoadConstantIlExpression(float.NaN));
            exprs.Add(conv);

            var seq = new IlExpressionSequence(exprs.ToArray());
            return seq;
        }

        public IlExpression ConvertToIlExpression(
            MaximumFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = ConvertToIlExpression(arguments[0], nm);
            int i;
            for (i = 1; i < arguments.Count; i++)
            {
                expr = new CallIlExpression(
                    new Func<float, float, float>(Math.Max),
                    expr,
                    ConvertToIlExpression(arguments[i], nm));
            }

            return expr;
        }

        public IlExpression ConvertToIlExpression(
            MinimumFiniteFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var exprs = new List<IlExpression>(8 * arguments.Count + 7);
            exprs.Add(new LoadConstantIlExpression(float.PositiveInfinity));
            var nop = new NopIlExpression();
            IlExpression first = nop;
            for (int i = arguments.Count - 1; i >= 0; i--)
            {
                var calcArg = ConvertToIlExpression(arguments[i], nm);
                var call1 = new CallIlExpression(
                    new Func<float, bool>(float.IsInfinity),
                    new DupIlExpression());
                var call2 = new CallIlExpression(
                    new Func<float, bool>(float.IsNaN),
                    new DupIlExpression());
                var pop = new PopIlExpression();
                var br1 = new BrTrueIlExpression(pop);
                var br2 = new BrTrueIlExpression(pop);
                var call3 = new CallIlExpression(
                    new Func<float, float, float>(Math.Min));
                var br3 = new BranchIlExpression(first);

                exprs.Insert(1, calcArg);
                exprs.Insert(2, call1);
                exprs.Insert(3, br1);
                exprs.Insert(4, call2);
                exprs.Insert(5, br2);
                exprs.Insert(6, call3);
                exprs.Insert(7, br3);
                exprs.Insert(8, pop);

                first = calcArg;
            }

            exprs.Add(nop);

            exprs.Add(
                new CallIlExpression(
                    new Func<float, bool>(float.IsPositiveInfinity),
                    new DupIlExpression(nop)));
            var conv = new ConvertR4IlExpression();
            exprs.Add(new BrFalseIlExpression(conv));
            exprs.Add(new PopIlExpression());
            exprs.Add(new LoadConstantIlExpression(float.NaN));
            exprs.Add(conv);

            var seq = new IlExpressionSequence(exprs.ToArray());
            return seq;
        }

        public IlExpression ConvertToIlExpression(
            MinimumFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = ConvertToIlExpression(arguments[0], nm);
            int i;
            for (i = 1; i < arguments.Count; i++)
            {
                expr = new CallIlExpression(
                    new Func<float, float, float>(Math.Min),
                    expr,
                    ConvertToIlExpression(arguments[i], nm));
            }

            return expr;
        }

        public IlExpression ConvertToIlExpression(
            ModularDivision func, NascentMethod nm,
            List<Expression> arguments)
        {
            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });

            var left =
                new ConvertI4IlExpression(
                    ConvertToIlExpression(arguments[0], nm));
            var right =
                new ConvertI4IlExpression(
                    ConvertToIlExpression(arguments[1], nm));

            var checkZero = new IfThenElseConstruct(
                new CompareEqualIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(0)),
                new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Division by zero"),
                        new LoadNullIlExpression())));

            var expr = new ConvertR4IlExpression(
                new RemIlExpression());

            return new IlExpressionSequence(
                left,
                right,
                checkZero,
                expr);
        }

        public IlExpression ConvertToIlExpression(
            MultiplicationOperation func,
            NascentMethod nm, List<Expression> arguments)
        {
            var expr = ConvertToIlExpression(arguments[0], nm);
            int i;
            for (i = 1; i < arguments.Count; i++)
                expr = new MulIlExpression(
                    expr,
                    ConvertToIlExpression(arguments[i], nm));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            NaturalLogarithmFunction func,
            NascentMethod nm,
            List<Expression> arguments)
        {

            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });

            var arg = ConvertToIlExpression(arguments[0], nm);

            var checkNotPos = new IfThenElseConstruct(
                new CompareGreaterThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(0f)),
                elseBlock: new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Argument must be positive"),
                        new LoadNullIlExpression())));

            var expr = new CallIlExpression(
                new Func<double, double>(Math.Log));
            return new IlExpressionSequence(
                arg,
                checkNotPos,
                expr,
                new ConvertR4IlExpression());
        }

        public IlExpression ConvertToIlExpression(
            NegationOperation func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new NegIlExpression(
                ConvertToIlExpression(arguments[0], nm));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            NotEqualComparisonOperation func,
            NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new CompareEqualIlExpression(
                    new LoadConstantIlExpression(0),
                    new CompareEqualIlExpression(
                        ConvertToIlExpression(arguments[0], nm),
                        ConvertToIlExpression(arguments[1], nm))));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            SecantFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new DivIlExpression(
                new LoadConstantIlExpression(1f),
                new CallIlExpression(
                    new Func<double, double>(Math.Cos),
                    ConvertToIlExpression(arguments[0], nm)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            SineFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Sin),
                ConvertToIlExpression(arguments[0], nm));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            SizeFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            TangentFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Tan),
                ConvertToIlExpression(arguments[0], nm));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            UnitStepFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var expr = new ConvertR4IlExpression(
                new CompareLessThanIlExpression(
                    new CompareLessThanIlExpression(
                        ConvertToIlExpression(arguments[0], nm),
                        new LoadConstantIlExpression(0f)),
                    new LoadConstantIlExpression(1)));
            return expr;
        }

        public IlExpression ConvertToIlExpression(
            UserDefinedFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(StoreOp1 op,
            NascentMethod nm)
        {
            if (op is IGenericStoreOp g)
                return ConvertToIlExpression(op, g.ElementType, nm);
            if (op is VectorStoreOp vso)
                return ConvertToIlExpression(vso, nm);

            throw new ArgumentException(
                $"Unsupported store operation: \"{op}\"", nameof(op));
        }

        public IlExpression ConvertToIlExpression(StoreOp1 op,
            Type elementType, NascentMethod nm)
        {
            // var storeParam = nm.CreateParam()
            // return new IlExpressionSequence(
            //     new DupIlExpression(),
            //     )
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(VectorStoreOp op,
            NascentMethod nm)
        {
            throw new NotImplementedException();
        }
    }
}
