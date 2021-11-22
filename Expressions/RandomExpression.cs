
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

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class RandomExpression : Expression
    {
        static Random _random = new Random();

        float _value = (float)_random.NextDouble();

        public override IMathObject Eval(SolusEnvironment env)
        {
            return ((float)_random.NextDouble()).ToNumber();
        }

        public override Expression Clone()
        {
            return new RandomExpression();
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override IMathObject Result { get; } = new ResultC();

        private class ResultC : IMathObject
        {
            public bool? IsScalar(SolusEnvironment env) => true;
            public bool? IsVector(SolusEnvironment env) => false;
            public bool? IsMatrix(SolusEnvironment env) => false;
            public int? GetTensorRank(SolusEnvironment env) => 0;
            public bool? IsString(SolusEnvironment env) => false;
            public int? GetDimension(SolusEnvironment env, int index) => null;
            public int[] GetDimensions(SolusEnvironment env) => null;
            public int? GetVectorLength(SolusEnvironment env) => null;
            public bool? IsInterval(SolusEnvironment env) => false;
            public bool? IsFunction(SolusEnvironment env) => false;
            public bool? IsExpression(SolusEnvironment env) => false;
            public bool IsConcrete => false;
            public string DocString => "";
        }
    }
}
