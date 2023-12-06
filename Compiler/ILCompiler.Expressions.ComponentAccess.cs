
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        static Type[] GetTypeArrayOfInt(int length)
        {
            var a = new Type[length];
            int i;
            for (i = 0; i < length; i++)
                a[i] = typeof(int);
            return a;
        }

        public IlExpression ConvertToIlExpression(ComponentAccess expr,
            NascentMethod nm, SolusEnvironment env,
            VariableIdentityMap variables)
        {
            var expr2 = ConvertToIlExpression(expr.Expr, nm, env, variables);
            var indexes2 = new IlExpression[expr.Indexes.Count];
            int i;
            for (i = 0; i < indexes2.Length; i++)
            {
                indexes2[i] = new ConvertI4IlExpression(
                    ConvertToIlExpression(expr.Indexes[i], nm, env,
                        variables));
            }

            // TODO: check expr.ResultType against the number of indexes
            //       eventually, we will have a type system that can tel us
            //       exactly what we should expect (real number, integer,
            //       vector on R^3, etc.) at any point in the computation.
            //       Until then, we use approximations.
            if (expr2.ResultType == typeof(string))
            {
                if (expr.Indexes.Count != 1)
                    throw new OperandException(
                        "Wrong number of indexes for the expression");
                var charType = typeof(char);
                var toString = charType.GetMethod("ToString",
                    Type.EmptyTypes);
                var strType = typeof(string);
                var getChars = strType.GetMethod("get_Chars",
                    new[] { typeof(int) });
                var local = nm.CreateLocal();
                local.LocalType = typeof(char);

                return new IlExpressionSequence(
                    new StoreLocalIlExpression(
                        local,
                        new CallIlExpression(
                            getChars,
                            expr2, indexes2[0])),
                    new CallIlExpression(
                        toString,
                        new LoadLocalAddrIlExpression(local)));
            }

            // assume vector (and not string) for now
            if (expr.Indexes.Count == 1)
                return new LoadElemIlExpression(expr2, indexes2[0]);

            // higher rank tensor
            if (expr.Indexes.Count >= 2)
            {
                var arrayType = typeof(float).MakeArrayType(
                    expr.Indexes.Count);
                var getMethod = arrayType.GetMethod("Get",
                    GetTypeArrayOfInt(expr.Indexes.Count));
                var args = new IlExpression[indexes2.Length + 1];
                args[0] = expr2;
                indexes2.CopyTo(args, 1);
                var callExpr = new CallIlExpression(getMethod, args);
                return callExpr;
            }

            // TODO: string?

            throw new NotImplementedException();
        }
    }
}
