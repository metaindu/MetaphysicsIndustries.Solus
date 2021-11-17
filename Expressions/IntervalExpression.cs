
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

using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class IntervalExpression : Expression
    {
        public IntervalExpression(Expression lowerBound, bool openLowerBound,
            Expression upperBound, bool openUpperBound)
        {
            LowerBound = lowerBound;
            OpenLowerBound = openLowerBound;
            UpperBound = upperBound;
            OpenUpperBound = openUpperBound;
        }

        public Expression LowerBound { get; }
        public bool OpenLowerBound { get; }
        public Expression UpperBound { get; }
        public bool OpenUpperBound { get; }

        public override IMathObject Eval(SolusEnvironment env)
        {
            var lower = LowerBound.Eval(env);
            if (!lower.IsIsScalar(env))
                throw new OperandException("Lower bound is not a scalar");
            var upper = UpperBound.Eval(env);
            if (!upper.IsIsScalar(env))
                throw new OperandException("Upper bound is not a scalar");
            return new Interval(lower.ToNumber().Value, OpenLowerBound,
                upper.ToNumber().Value, OpenUpperBound, false);
        }

        public override Expression Clone()
        {
            return new IntervalExpression(LowerBound, OpenLowerBound,
                UpperBound, OpenUpperBound);
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
            LowerBound.AcceptVisitor(visitor);
            UpperBound.AcceptVisitor(visitor);
        }

        public override IMathObject Result => IntervalMathObject.Value;
    }
}
