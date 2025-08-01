
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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

namespace MetaphysicsIndustries.Solus.Compiler.IlExpressions
{
    public class LoadConstantIlExpression : IlExpression
    {
        public LoadConstantIlExpression(double value) =>
            Instruction = Instruction.LoadConstant(value);
        public LoadConstantIlExpression(float value) =>
            Instruction = Instruction.LoadConstant(value);
        public LoadConstantIlExpression(long value) =>
            Instruction = Instruction.LoadConstant(value);
        public LoadConstantIlExpression(bool value) =>
            Instruction = Instruction.LoadConstant(value ? 1L : 0L);

        public Instruction Instruction { get; }

        protected override void GetInstructionsInternal(NascentMethod nm)
        {
            nm.Instructions.Add(Instruction);
        }

        public override Type ResultType {
            get
            {
                switch (Instruction.ArgType)
                {
                    case Instruction.ArgumentType.I1:
                    case Instruction.ArgumentType.I2:
                    case Instruction.ArgumentType.I4:
                        return typeof(int);
                    case Instruction.ArgumentType.I8:
                        return typeof(long);
                    case Instruction.ArgumentType.R4:
                        return typeof(float);
                    case Instruction.ArgumentType.R8:
                        return typeof(double);
                    case Instruction.ArgumentType.String:
                        return typeof(string);
                }

                throw new ArgumentException(
                    $"Unsupported argument type, {Instruction.ArgType}");
            }
        }
    }
}
