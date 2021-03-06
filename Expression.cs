
/*****************************************************************************
 *                                                                           *
 *  Expression.cs                                                            *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright � 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The basic unit of calculation and parse trees.                           *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;

using System.Reflection.Emit;
using System.Linq;
using System.Text;
using MetaphysicsIndustries.Giza;

namespace MetaphysicsIndustries.Solus
{
	public abstract class Expression : IDisposable, ICloneable
	{
        public virtual void Dispose()
        {
        }

        public abstract Literal Eval(SolusEnvironment env);

        public class CompiledExpression
        {
            public Func<Dictionary<string, float>, float> Method;
            public string[] CompiledVars;
        }

        CompiledExpression _compiled;

        public Literal FastEval(SolusEnvironment env)
        {
            Dictionary<string, float> bakedEnv = new Dictionary<string, float>();
            if (_compiled != null)
            {
                foreach (var var in _compiled.CompiledVars)
                {
                    bakedEnv[var] = env.Variables[var].Eval(env).Value;
                }
            }

            try
            {
                return new Literal(FastEval(bakedEnv));
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
        public static Expression Clone(Expression expr)
        {
            //used by Array.ConvertAll
            return expr.Clone();
        }

        //public delegate T Transformer<T>(Expression expr, VariableTable env);
        //public abstract T Transform<T>(VariableTable env, Transformer<T> transformer);
        //public static T Transform<T>(Expression expr, VariableTable env, Transformer<T> transformer)
        //{
        //    expr.Transform<T>(env, transformer);
        //}

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        //public virtual Expression CleanUp()
        //{
        //    return this;
        //}

        //public abstract Expression PreliminaryEval(VariableTable env);
        //public abstract GetDerivative(Variable

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

        //public Expression PreliminaryEval(VariableTable env)
        //{
        //    Expression evalExpr = InternalPreliminaryEval(env);
        //    Expression cleanExpr = evalExpr.CleanUp();
        //    return cleanExpr;
        //}

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

//            var env_Variables = typeof(SolusEnvironment).GetField("Variables");
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
    }
}
