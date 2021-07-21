
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
using System.Text;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class VarsCommand : Command
    {
        public static readonly VarsCommand Value = new VarsCommand();

        public override string Name => "vars";
        public override string DocString =>
            @"vars - Print a list of all defined variables";

        public override void Execute(string input, SolusEnvironment env)
        {
            var sb = new StringBuilder();
            foreach (var name in env.Variables.Keys)
            {
                var value = env.Variables[name];
                var valueString = value.ToString();

                if (value is VectorExpression vector)
                {
                    valueString = "Vector (" + vector.Length.ToString() + ")";
                }
                else if (value is MatrixExpression mat)
                {
                    valueString = "Matrix (" + mat.RowCount + ", " + mat.ColumnCount + ")";
                }

                sb.AppendLine($"{name} = {valueString}");
            }
            
            Console.Write(sb.ToString());
        }
    }
}