
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
using System.Text;
using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public class MultiplicationOperation : AssociativeCommutativeOperation
    {
        public static readonly MultiplicationOperation Value = new MultiplicationOperation();

        protected MultiplicationOperation()
        {
            Name = "*";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Multiplication; }
        }

        //public override bool IsCommutative
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        //public override bool IsAssociative
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            float value = 1;
            int i;
            for (i = 0; i < args.Length; i++)
            {
                value *= args[i].Value;
            }
            return new Literal(value);
        }

        public override bool Collapses
        {
            get
            {
                return true;
            }
        }

        public override float CollapseValue
        {
            get
            {
                return 0;
            }
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            var instructions = new List<Instruction>();

            foreach (var arg in arguments)
            {
                instructions.AddRange(arg.ConvertToInstructions(varmap));
            }

            int i;
            for (i = 1; i < arguments.Count; i++)
            {
                instructions.Add(Instruction.Mul());
            }

            return instructions;
        }
    }
}
