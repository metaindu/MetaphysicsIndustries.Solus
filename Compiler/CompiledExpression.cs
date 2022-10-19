
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
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public class CompiledExpression
    {
        public Func<CompiledEnvironment, object> Method;
        public string[] CompiledVars;

        // diagnostics
        public NascentMethod nm;
        public IlExpression ilexpr;
        public List<Instruction> setup;
        public List<Instruction> shutdown;

        public IMathObject Evaluate(CompiledEnvironment cenv)
        {
            var result = Method(cenv);
            if (result is float f)
                return f.ToNumber();
            if (result is float[] v)
                return v.ToVector();
            if (result is float[,] m)
                return m.ToMatrix();
            if (result is string s)
                return s.ToStringValue();

            throw new InvalidOperationException(
                $"Unsupported result type: {result.GetType()}");
        }
    }
}
