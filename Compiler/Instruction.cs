
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
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
using System.Reflection;
using System.Reflection.Emit;
using MetaphysicsIndustries.Solus.Exceptions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public struct Instruction
    {
        public enum ArgumentType
        {
            None,
            I1,
            I2,
            I4,
            I8,
            UI1,
            UI2,
            R4,
            R8,
            Method,
            Constructor,
            String,
            Label,
            Type,
        };

        public ArgumentType ArgType;
        public OpCode OpCode;

        public long IntArg;
        public ulong UIntArg;
        public float FloatArg;
        public double DoubleArg;
        public MethodInfo MethodArg;
        public ConstructorInfo ConstructorArg;
        public string StringArg;
        public IlLabel LabelArg;
        public Type TypeArg;

        public override string ToString()
        {
            try
            {
                string arg = "";
                switch (ArgType)
                {
                case ArgumentType.I1:
                case ArgumentType.I2:
                case ArgumentType.I4:
                case ArgumentType.I8: arg = string.Format(" {0}", IntArg); break;
                case ArgumentType.UI1:
                case ArgumentType.UI2: arg = string.Format(" {0}", UIntArg); break;
                case ArgumentType.R4: arg = string.Format(" {0}", FloatArg); break;
                case ArgumentType.R8: arg = string.Format(" {0}", DoubleArg); break;
                case ArgumentType.Method:
                    arg = string.Format(" {0}.{1}",
                        (MethodArg != null ? MethodArg.DeclaringType.Name : "(null)"),
                        (MethodArg != null ? MethodArg.Name : "(null)"));
                    break;
                case ArgumentType.Constructor:
                    if (ConstructorArg == null)
                        arg = " (null).(null)";
                    else if (ConstructorArg.DeclaringType == null)
                        arg = $" (null).{MethodArg.Name}";
                    else
                        arg = $" {ConstructorArg.DeclaringType.Name}." +
                              $"{MethodArg.Name}";
                    break;
                case ArgumentType.String:
                    arg = string.Format(" \"{0}\"",
                        StringArg.
                            Replace("\\", "\\\\").
                            Replace("\r", "\\r").
                            Replace("\n", "\\n").
                            Replace("\t", "\\t").
                            Replace("\"", "\\\""));
                    break;
                case ArgumentType.Label:
                    arg = LabelArg.ToString();
                    break;
                case ArgumentType.Type:
                    arg = $" {TypeArg}";
                    break;
                }

                return string.Format("{0} {1}", OpCode.Name, arg);
            }
            catch (Exception e)
            {
                return string.Format("{0} {1}", OpCode.Name, ArgType.ToString());
            }
        }

        public void Emit(IILGenerator gen, Label label=default)
        {
            switch (ArgType)
            {
            case ArgumentType.None:
                gen.Emit(OpCode);
                break;
            case ArgumentType.I1:
                gen.Emit(OpCode, (sbyte)IntArg);
                break;
            case ArgumentType.I2:
                gen.Emit(OpCode, (short)IntArg);
                break;
            case ArgumentType.I4:
                gen.Emit(OpCode, (int)IntArg);
                break;
            case ArgumentType.I8:
                gen.Emit(OpCode, (long)IntArg);
                break;
            case ArgumentType.UI1:
                gen.Emit(OpCode, (byte)UIntArg);
                break;
            case ArgumentType.UI2:
                gen.Emit(OpCode, (ushort)UIntArg);
                break;
            case ArgumentType.R4:
                gen.Emit(OpCode, FloatArg);
                break;
            case ArgumentType.R8:
                gen.Emit(OpCode, DoubleArg);
                break;
            case ArgumentType.Method:
                gen.Emit(OpCode, MethodArg);
                break;
            case ArgumentType.Constructor:
                gen.Emit(OpCode, ConstructorArg);
                break;
            case ArgumentType.String:
                gen.Emit(OpCode, StringArg);
                break;
            case ArgumentType.Label:
                gen.Emit(OpCode, label);
                break;
            case ArgumentType.Type:
                gen.Emit(OpCode, TypeArg);
                break;
            default:
                throw new ValueException(nameof(ArgType), "Unknown value");
            }
        }

        public static Instruction LoadArgument(ushort argNumber)
        {
            switch (argNumber)
            {
            case 0: return new Instruction { OpCode = OpCodes.Ldarg_0 };
            case 1: return new Instruction { OpCode = OpCodes.Ldarg_1 };
            case 2: return new Instruction { OpCode = OpCodes.Ldarg_2 };
            case 3: return new Instruction { OpCode = OpCodes.Ldarg_3 };
            }

            if (argNumber < 256)
            {
                return new Instruction {
                    ArgType = ArgumentType.UI1,
                    UIntArg = argNumber,
                    OpCode = OpCodes.Ldarg_S
                };
            }

            return new Instruction {
                ArgType = ArgumentType.UI2,
                UIntArg = argNumber,
                OpCode = OpCodes.Ldarg
            };

        }

        public static Instruction LoadConstant(long value)
        {
            switch (value)
            {
            case 0: return new Instruction { OpCode = OpCodes.Ldc_I4_0 };
            case 1: return new Instruction { OpCode = OpCodes.Ldc_I4_1 };
            case 2: return new Instruction { OpCode = OpCodes.Ldc_I4_2 };
            case 3: return new Instruction { OpCode = OpCodes.Ldc_I4_3 };
            case 4: return new Instruction { OpCode = OpCodes.Ldc_I4_4 };
            case 5: return new Instruction { OpCode = OpCodes.Ldc_I4_5 };
            case 6: return new Instruction { OpCode = OpCodes.Ldc_I4_6 };
            case 7: return new Instruction { OpCode = OpCodes.Ldc_I4_7 };
            case 8: return new Instruction { OpCode = OpCodes.Ldc_I4_8 };
            case -1: return new Instruction { OpCode = OpCodes.Ldc_I4_M1 };
            }

            if (value >= sbyte.MinValue &&
                value <= sbyte.MaxValue)
            {
                return new Instruction {
                    ArgType = ArgumentType.I1,
                    IntArg = value,
                    OpCode = OpCodes.Ldc_I4_S
                };
            }

            if (value >= int.MinValue &&
                value <= int.MaxValue)
            {
                return new Instruction {
                    ArgType = ArgumentType.I4,
                    IntArg = value,
                    OpCode = OpCodes.Ldc_I4
                };
            }

            return new Instruction {
                ArgType = ArgumentType.I8,
                IntArg = value,
                OpCode = OpCodes.Ldc_I8
            };
        }
        public static Instruction LoadConstant(float value)
        {
            return new Instruction {
                ArgType = ArgumentType.R4,
                FloatArg = value,
                OpCode = OpCodes.Ldc_R4
            };
        }
        public static Instruction LoadConstant(double value)
        {
            return new Instruction {
                ArgType = ArgumentType.R8,
                DoubleArg = value,
                OpCode = OpCodes.Ldc_R8
            };
        }

        public static Instruction Call(MethodInfo mi)
        {
            if (mi == null) throw ValueException.Null(nameof(mi));

            return new Instruction {
                ArgType = ArgumentType.Method,
                MethodArg = mi,
                OpCode = OpCodes.Call
            };
        }

        public static Instruction Call(Delegate del) => Call(del.Method);

        public static Instruction CallVirtual(MethodInfo mi)
        {
            if (mi == null) throw ValueException.Null(nameof(mi));

            return new Instruction {
                ArgType = ArgumentType.Method,
                MethodArg = mi,
                OpCode = OpCodes.Callvirt
            };
        }

        public static Instruction CallVirtual(Delegate del) =>
            CallVirtual(del.Method);

        public static Instruction Dup()
        {
            return new Instruction { OpCode = OpCodes.Dup };
        }

        public static Instruction Mul()
        {
            return new Instruction { OpCode = OpCodes.Mul };
        }

        public static Instruction Div() =>
            new Instruction { OpCode = OpCodes.Div };

        public static Instruction Rem() =>
            new Instruction { OpCode = OpCodes.Rem };

        public static Instruction Add()
        {
            return new Instruction { OpCode = OpCodes.Add };
        }
        public static Instruction Sub()
        {
            return new Instruction { OpCode = OpCodes.Sub };
        }

        public static Instruction Neg() =>
            new Instruction { OpCode = OpCodes.Neg };

        public static Instruction CompareLessThan()
        {
            return new Instruction { OpCode = OpCodes.Clt };
        }
        public static Instruction CompareGreaterThan() =>
            new Instruction { OpCode = OpCodes.Cgt };

        public static Instruction CompareEqual() =>
            new Instruction { OpCode = OpCodes.Ceq };

        public static Instruction LoadLocalVariable(ushort varNumber)
        {
            switch (varNumber)
            {
            case 0: return new Instruction { OpCode = OpCodes.Ldloc_0 };
            case 1: return new Instruction { OpCode = OpCodes.Ldloc_1 };
            case 2: return new Instruction { OpCode = OpCodes.Ldloc_2 };
            case 3: return new Instruction { OpCode = OpCodes.Ldloc_3 };
            }

            if (varNumber < 256)
            {
                return new Instruction {
                    ArgType = ArgumentType.UI1,
                    UIntArg = varNumber,
                OpCode = OpCodes.Ldloc_S
                };
            }

            return new Instruction {
                ArgType = ArgumentType.UI2,
                UIntArg = varNumber,
                OpCode = OpCodes.Ldloc
            };
        }

        public static Instruction StoreLocalVariable(ushort varNumber)
        {
            switch (varNumber)
            {
            case 0: return new Instruction { OpCode = OpCodes.Stloc_0 };
            case 1: return new Instruction { OpCode = OpCodes.Stloc_1 };
            case 2: return new Instruction { OpCode = OpCodes.Stloc_2 };
            case 3: return new Instruction { OpCode = OpCodes.Stloc_3 };
            }

            if (varNumber < 256)
            {
                return new Instruction {
                    ArgType = ArgumentType.UI1,
                    UIntArg = varNumber,
                    OpCode = OpCodes.Stloc_S
                };
            }

            return new Instruction {
                ArgType = ArgumentType.UI2,
                UIntArg = varNumber,
                OpCode = OpCodes.Stloc
            };
        }

        public static Instruction LoadLocalVariableAddress(ushort varNumber)
        {
            if (varNumber < 256)
            {
                return new Instruction {
                    ArgType = ArgumentType.UI1,
                    UIntArg = varNumber,
                    OpCode = OpCodes.Ldloca_S
                };
            }

            return new Instruction {
                ArgType = ArgumentType.UI2,
                UIntArg = varNumber,
                OpCode = OpCodes.Ldloca
            };
        }

        public static Instruction LoadString(string value)
        {
            return new Instruction {
                ArgType = ArgumentType.String,
                StringArg = value,
                OpCode = OpCodes.Ldstr
            };
        }

        public static Instruction LoadNull() =>
            new Instruction { OpCode = OpCodes.Ldnull };

        public static Instruction ConvertR4()
        {
            return new Instruction { OpCode = OpCodes.Conv_R4 };
        }

        public static Instruction ConvertI4() =>
            new Instruction { OpCode = OpCodes.Conv_I4 };

        public static Instruction Box(Type type) =>
            new Instruction
            {
                OpCode = OpCodes.Box,
                ArgType = ArgumentType.Type,
                TypeArg = type
            };
        public static Instruction Unbox(Type type) =>
            new Instruction
            {
                OpCode = OpCodes.Unbox,
                ArgType = ArgumentType.Type,
                TypeArg = type
            };
        public static Instruction UnboxAny(Type type) =>
            new Instruction
            {
                OpCode = OpCodes.Unbox_Any,
                ArgType = ArgumentType.Type,
                TypeArg = type
            };

        public static Instruction Return()
        {
            return new Instruction { OpCode = OpCodes.Ret };
        }

        public static Instruction And() =>
            new Instruction { OpCode = OpCodes.And };
        public static Instruction Or() =>
            new Instruction { OpCode = OpCodes.Or };
        public static Instruction Xor() =>
            new Instruction { OpCode = OpCodes.Xor };
        public static Instruction Not() =>
            new Instruction { OpCode = OpCodes.Not };

        public static Instruction Nop() =>
            new Instruction { OpCode = OpCodes.Nop };

        public static Instruction Pop() =>
            new Instruction { OpCode = OpCodes.Pop };

        public static Instruction Branch(IlLabel label) =>
            new Instruction
            {
                OpCode = OpCodes.Br,
                ArgType = ArgumentType.Label,
                LabelArg = label
            };
        public static Instruction BrTrue(IlLabel label) =>
            new Instruction
            {
                OpCode = OpCodes.Brtrue,
                ArgType = ArgumentType.Label,
                LabelArg = label
            };
        public static Instruction BrFalse(IlLabel label) =>
            new Instruction
            {
                OpCode = OpCodes.Brfalse,
                ArgType = ArgumentType.Label,
                LabelArg = label
            };

        public static Instruction Throw() =>
            new Instruction
            {
                OpCode = OpCodes.Throw,
                ArgType = ArgumentType.None
            };

        public static Instruction NewArr(Type type) =>
            new Instruction
            {
                OpCode = OpCodes.Newarr,
                ArgType = ArgumentType.Type,
                TypeArg = type
            };

        public static Instruction LdElem(Type type) =>
            new Instruction
            {
                OpCode = OpCodes.Ldelem,
                ArgType = ArgumentType.Type,
                TypeArg = type
            };
        public static Instruction LdElemR4() =>
            new Instruction
            {
                OpCode = OpCodes.Ldelem_R4,
            };

        public static Instruction StElem(Type type) =>
            new Instruction
            {
                OpCode = OpCodes.Stelem,
                ArgType = ArgumentType.Type,
                TypeArg = type
            };
        public static Instruction StElemR4() =>
            new Instruction
            {
                OpCode = OpCodes.Stelem_R4,
            };

        public static Instruction NewObj(ConstructorInfo ci) =>
            new Instruction
            {
                OpCode = OpCodes.Newobj,
                ArgType = ArgumentType.Constructor,
                ConstructorArg = ci
            };

        public static Instruction LdStr(string value) =>
            new Instruction
            {
                OpCode = OpCodes.Ldstr,
                ArgType = ArgumentType.String,
                StringArg = value,
            };
    }
}

