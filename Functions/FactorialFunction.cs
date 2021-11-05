
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class FactorialFunction : SingleArgumentFunction
    {
        public static readonly FactorialFunction Value =
            new FactorialFunction();

        private static Dictionary<int, float> _presets = new Dictionary<int, float>();

        static FactorialFunction()
        {
            _presets.Add(0,1);
            _presets.Add(1,1);
            _presets.Add(2,2);
            _presets.Add(3,6);
            _presets.Add(4,24);
            _presets.Add(5,120);
            _presets.Add(10, 3628800);

        }

        protected override IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args)
        {
            float p = args[0].ToNumber().Value;

            if (p != (int)p) throw new ArgumentException("Argument must be an integer");

            return GetValue((int)p).ToNumber();
        }

        private float GetValue(int p)
        {
            if (!_presets.ContainsKey(p))
            {
                _presets[p] = p * GetValue(p - 1);
            }

            return _presets[p];
        }

        protected FactorialFunction()
        {
            Name = "Factorial";
        }

        public override string DisplayName
        {
            get { return "!"; }
        }

        public override string ToString(List<Expression> arguments)
        {
            //treat as post-fix unary operator?

            return base.ToString(arguments);
        }

        public override IMathObject GetResult(IEnumerable<IMathObject> args)
        {
            return ScalarMathObject.Value;
        }
    }
}
