
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
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(
            UserDefinedFunction func, NascentMethod nm,
            VariableIdentityMap variables,
            List<Expression> arguments)
        {
            // TODO: the variables in the udf definition should be
            //       implemented as locals
            int i;
            var seq = new List<IlExpression>();
            var variables2 = variables.CreateChild();
            for (i = 0; i < func.Parameters.Count; i++)
            {
                var name = func.Parameters[i].Name;
                var value = arguments[i];
                var arg = ConvertToIlExpression(arguments[i], nm,
                    variables);
                var varType = arg.ResultType;
                var local = nm.CreateLocal(varType, name);
                variables2.SetVariable(name, new VariableIdentity
                {
                    Name = name,
                    IlType = varType,
                    Value = value,
                    Source = VariableSource.Local,
                    LocalSource = local,
                });
                seq.Add(new StoreLocalIlExpression(local, arg));
            }

            var ilexpr = ConvertToIlExpression(func.Expression, nm,
                variables2);
            seq.Add(ilexpr);
            return new IlExpressionSequence(seq);
        }
    }
}
