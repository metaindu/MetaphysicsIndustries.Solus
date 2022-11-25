
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class SizeFunction : Function
    {
        public static readonly SizeFunction Value = new SizeFunction();

        protected SizeFunction()
            : base(new Types[0], "size")
        {
        }

        public override void CheckArguments(IMathObject[] args)
        {
            if (args.Length != 1)
                throw new ArgumentException(
                    $"Wrong number of arguments given to " +
                    $"{DisplayName} (expected 1 but got " +
                    $"{args.Length})");
            var argtype = args[0].GetMathType();
            if (argtype != Types.Vector &&
                argtype != Types.Matrix &&
                argtype != Types.String)
            {
                throw new ArgumentException(
                    $"Argument wrong type: expected " +
                    $"Vector or Matrix or String but got {argtype}");
            }
        }

        private class ResultC : IMathObject
        {
            public ResultC(IMathObject obj) => _obj = obj;
            private readonly IMathObject _obj;
            public bool? IsScalar(SolusEnvironment env) => false;
            public bool? IsVector(SolusEnvironment env) => true;
            public bool? IsMatrix(SolusEnvironment env) => false;
            public int? GetTensorRank(SolusEnvironment env) => 1;
            public bool? IsString(SolusEnvironment env) => false;

            public int? GetDimension(SolusEnvironment env, int index)
            {
                if (index != 0) throw new IndexException();
                return _obj.GetTensorRank(env);
            }

            public int[] GetDimensions(SolusEnvironment env)
            {
                var n = _obj.GetTensorRank(env);
                if (!n.HasValue)
                    return null;
                var dimensions = new int[n.Value];
                for (var i = 0; i < n; i++)
                {
                    var dim = _obj.GetDimension(env, i);
                    if (!dim.HasValue)
                        return null;
                    dimensions[i] = dim.Value;
                }

                return dimensions;
            }

            public int? GetVectorLength(SolusEnvironment env) =>
                GetDimension(env, 0);

            public bool? IsInterval(SolusEnvironment env) => false;
            public bool? IsFunction(SolusEnvironment env) => false;
            public bool? IsExpression(SolusEnvironment env) => false;

            public bool IsConcrete => false;
            public string DocString => "";
        }

        public override IMathObject GetResultType(SolusEnvironment env,
            IEnumerable<IMathObject> argTypes)
        {
            return new ResultC(argTypes.First());
        }
    }
}
