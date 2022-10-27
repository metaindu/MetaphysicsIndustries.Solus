
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

namespace MetaphysicsIndustries.Solus.Compiler.IlExpressions
{
    public class IlExpressionSequence : IlExpression
    {
        public IlExpressionSequence(params IlExpression[] expressions)
            : this(null, expressions)
        {
        }
        public IlExpressionSequence(IList<IlExpression> expressions)
            : this(null, expressions)
        {
        }
        public IlExpressionSequence(Type resultType, params IlExpression[] expressions)
        {
            _resultType = resultType;
            Expressions = expressions;
        }
        public IlExpressionSequence(Type resultType, IList<IlExpression> expressions)
        {
            _resultType = resultType;
            var exprs = new IlExpression[expressions.Count];
            expressions.CopyTo(exprs, 0);
            Expressions = exprs;
        }

        public IlExpression[] Expressions;
        private Type _resultType;

        protected override void GetInstructionsInternal(NascentMethod nm)
        {
            foreach (var expr in Expressions)
                expr.GetInstructions(nm);
        }

        public override Type ResultType
        {
            get
            {
                if (_resultType != null)
                    return _resultType;
                return Expressions[Expressions.Length - 1].ResultType;
            }
        }
    }
}
