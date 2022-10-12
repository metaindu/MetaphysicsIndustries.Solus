
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
using System.Reflection;
using System.Reflection.Emit;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public interface IILGenerator
    {
        void Emit(OpCode opCode);
        void Emit(OpCode opCode, sbyte intArg);
        void Emit(OpCode opCode, short intArg);
        void Emit(OpCode opCode, int intArg);
        void Emit(OpCode opCode, long intArg);
        void Emit(OpCode opCode, float floatArg);
        void Emit(OpCode opCode, double doubleArg);
        void Emit(OpCode opCode, MethodInfo methodArg);
        void Emit(OpCode opCode, ConstructorInfo constructorArg);
        void Emit(OpCode opCode, string constructorArg);
        void Emit(OpCode opCode, Label labelArg);
        void Emit(OpCode opCode, Type typeArg);
    }

    public class ILGeneratorAdapter : IILGenerator
    {
        private readonly ILGenerator _gen;
        public ILGeneratorAdapter(ILGenerator gen) => _gen = gen;
        public void Emit(OpCode opCode) => _gen.Emit(opCode);
        public void Emit(OpCode opCode, sbyte sbyteArg) => _gen.Emit(opCode, sbyteArg);
        public void Emit(OpCode opCode, short shortArg) => _gen.Emit(opCode, shortArg);
        public void Emit(OpCode opCode, int intArg) => _gen.Emit(opCode, intArg);
        public void Emit(OpCode opCode, long longArg) => _gen.Emit(opCode, longArg);
        public void Emit(OpCode opCode, float floatArg) => _gen.Emit(opCode, floatArg);
        public void Emit(OpCode opCode, double doubleArg) => _gen.Emit(opCode, doubleArg);
        public void Emit(OpCode opCode, MethodInfo methodArg) => _gen.Emit(opCode, methodArg);
        public void Emit(OpCode opCode, ConstructorInfo constructorArg) => _gen.Emit(opCode, constructorArg);
        public void Emit(OpCode opCode, string stringArg) => _gen.Emit(opCode, stringArg);
        public void Emit(OpCode opCode, Label labelArg) => _gen.Emit(opCode, labelArg);
        public void Emit(OpCode opCode, Type typeArg) => _gen.Emit(opCode, typeArg);
    }

    public class ILRecorder : IILGenerator
    {
        public struct Record
        {
            public OpCode OpCode;
            public sbyte SbyteArg;
            public short ShortArg;
            public int IntArg;
            public long LongArg;
            public float FloatArg;
            public double DoubleArg;
            public MethodInfo MethodArg;
            public ConstructorInfo ConstructorArg;
            public string StringArg;
            public Label LabelArg;
            public Type TypeArg;
        }

        private readonly IILGenerator _next;
        public readonly List<Record> Records = new List<Record>();
        public ILRecorder(IILGenerator next) => _next = next;

        public void Emit(OpCode opCode)
        {
            Records.Add(new Record { OpCode = opCode });
            _next.Emit(opCode);
        }

        public void Emit(OpCode opCode, sbyte sbyteArg)
        {
            Records.Add(new Record { OpCode = opCode, SbyteArg = sbyteArg });
            _next.Emit(opCode, sbyteArg);
        }

        public void Emit(OpCode opCode, short shortArg)
        {
            Records.Add(new Record { OpCode = opCode, ShortArg = shortArg });
            _next.Emit(opCode, shortArg);
        }

        public void Emit(OpCode opCode, int intArg)
        {
            Records.Add(new Record { OpCode = opCode, IntArg = intArg });
            _next.Emit(opCode, intArg);
        }

        public void Emit(OpCode opCode, long longArg)
        {
            Records.Add(new Record { OpCode = opCode, LongArg = longArg });
            _next.Emit(opCode, longArg);
        }

        public void Emit(OpCode opCode, float floatArg)
        {
            Records.Add(new Record { OpCode = opCode, FloatArg = floatArg });
            _next.Emit(opCode, floatArg);
        }

        public void Emit(OpCode opCode, double doubleArg)
        {
            Records.Add(new Record { OpCode = opCode, DoubleArg = doubleArg });
            _next.Emit(opCode, doubleArg);
        }

        public void Emit(OpCode opCode, MethodInfo methodArg)
        {
            Records.Add(new Record { OpCode = opCode, MethodArg = methodArg });
            _next.Emit(opCode, methodArg);
        }

        public void Emit(OpCode opCode, ConstructorInfo constructorArg)
        {
            Records.Add(new Record { OpCode = opCode, ConstructorArg = constructorArg });
            _next.Emit(opCode, constructorArg);
        }

        public void Emit(OpCode opCode, string stringArg)
        {
            Records.Add(new Record { OpCode = opCode, StringArg = stringArg });
            _next.Emit(opCode, stringArg);
        }

        public void Emit(OpCode opCode, Label labelArg)
        {
            Records.Add(new Record { OpCode = opCode, LabelArg = labelArg });
            _next.Emit(opCode, labelArg);
        }

        public void Emit(OpCode opCode, Type typeArg)
        {
            Records.Add(new Record { OpCode = opCode, TypeArg = typeArg });
            _next.Emit(opCode, typeArg);
        }
    }
}