
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
using MetaphysicsIndustries.Solus.Evaluators;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(
            StoreOp1 op, NascentMethod nm,
            VariableIdentityMap variables)
        {
            if (op is IGenericStoreOp g)
                return ConvertToIlExpression(op, g.ElementType, nm, variables);
            if (op is VectorStoreOp vso)
                return ConvertToIlExpression(vso, nm, variables);

            throw new ArgumentException(
                $"Unsupported store operation: \"{op}\"", nameof(op));
        }

        public IlExpression ConvertToIlExpression(
            StoreOp1 op,
            Type elementType,
            NascentMethod nm,
            VariableIdentityMap variables)
        {
            // var storeParam = nm.CreateParam()
            // return new IlExpressionSequence(
            //     new DupIlExpression(),
            //     )
            throw new NotImplementedException();
        }

        public IlExpression ConvertToIlExpression(
            VectorStoreOp op,
            NascentMethod nm,
            VariableIdentityMap variables)
        {
            throw new NotImplementedException();
        }
    }
}