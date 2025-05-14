
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Test
{
    class MockExpression : Expression
    {
        public MockExpression(
            Func<SolusEnvironment, IMathObject> evalf=null,
            Func<Expression> cloneF=null,
            Action<IExpressionVisitor> acceptVisitorF=null,
            ISet result=null)
        {
            EvalF = evalf;
            CloneF = cloneF;
            AcceptVisitorF = acceptVisitorF;
            if (result == null)
                result = new MockSet();
            _result = result;
        }

        public Func<SolusEnvironment, IMathObject> EvalF;
        public override IMathObject CustomEval(SolusEnvironment env)
        {
            if (EvalF != null) return EvalF(env);
            throw new System.NotImplementedException();
        }

        public override bool ProvidesCustomEval => true;

        public Func<Expression> CloneF;
        public override Expression Clone()
        {
            if (CloneF != null) return CloneF();
            throw new NotImplementedException();
        }

        public Action<IExpressionVisitor> AcceptVisitorF;
        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            if (CloneF != null)
            {
                AcceptVisitorF(visitor);
                return;
            }

            throw new NotImplementedException();
        }

        private ISet _result;
        public override ISet GetResultType(SolusEnvironment env) =>
            _result;
        public void SetResult(ISet value) => _result = value;
    }
}
