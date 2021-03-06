using System;
using System.Reflection;
using System.Reflection.Emit;

namespace MetaphysicsIndustries.Solus
{
    public struct Instruction
    {
        public enum ArgumentType { None, I1, I2, I4, I8, UI1, UI2, R4, R8, Method, String };

        public ArgumentType ArgType;
        public OpCode OpCode;

        public long IntArg;
        public ulong UIntArg;
        public float FloatArg;
        public double DoubleArg;
        public MethodInfo MethodArg;
        public string StringArg;

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
                case ArgumentType.String:
                    arg = string.Format(" \"{0}\"",
                        StringArg.
                            Replace("\\", "\\\\").
                            Replace("\r", "\\r").
                            Replace("\n", "\\n").
                            Replace("\t", "\\t").
                            Replace("\"", "\\\""));
                    break;
                }

                return string.Format("{0} {1}", OpCode.Name, arg);
            }
            catch (Exception e)
            {
                return string.Format("{0} {1}", OpCode.Name, ArgType.ToString());
            }
        }

        public void Emit(ILGenerator gen)
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
            case ArgumentType.String:
                gen.Emit(OpCode, StringArg);
                break;
            default:
                throw new ArgumentOutOfRangeException();
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
                value >= int.MaxValue)
            {
                return new Instruction {
                    ArgType = ArgumentType.I4,
                    IntArg = value,
                    OpCode = OpCodes.Ldc_I4_S
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
            if (mi == null) throw new ArgumentNullException("mi");

            return new Instruction {
                ArgType = ArgumentType.Method,
                MethodArg = mi,
                OpCode = OpCodes.Call
            };
        }
        public static Instruction CallVirtual(MethodInfo mi)
        {
            if (mi == null) throw new ArgumentNullException("mi");

            return new Instruction {
                ArgType = ArgumentType.Method,
                MethodArg = mi,
                OpCode = OpCodes.Callvirt
            };
        }

        public static Instruction Dup()
        {
            return new Instruction { OpCode = OpCodes.Dup };
        }
        public static Instruction Mul()
        {
            return new Instruction { OpCode = OpCodes.Mul };
        }
        public static Instruction Add()
        {
            return new Instruction { OpCode = OpCodes.Add };
        }
        public static Instruction Sub()
        {
            return new Instruction { OpCode = OpCodes.Sub };
        }

        public static Instruction CompareLessThan()
        {
            return new Instruction { OpCode = OpCodes.Clt };
        }

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

        public static Instruction LoadString(string value)
        {
            return new Instruction {
                ArgType = ArgumentType.String,
                StringArg = value,
                OpCode = OpCodes.Ldstr
            };
        }

        public static Instruction ConvertR4()
        {
            return new Instruction { OpCode = OpCodes.Conv_R4 };
        }

        public static Instruction Return()
        {
            return new Instruction { OpCode = OpCodes.Ret };
        }
    }
}

