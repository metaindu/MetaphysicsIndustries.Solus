
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ComponentAccessT
{
    [TestFixture]
    public class ComponentAccessTest
    {
        [Test]
        public void CreateArgsSetValues()
        {
            // given
            var expr = new Literal(
                new Vector(new float[] {1, 2, 3}));
            var indexes = new Expression[] {new Literal(1)};
            // when
            var result = new ComponentAccess(expr, indexes);
            // then
            Assert.AreSame(expr, result.Expr);
            Assert.AreNotSame(indexes, result.Indexes);
            Assert.AreEqual(1, result.Indexes.Count);
            Assert.IsInstanceOf<Literal>(result.Indexes[0]);
            Assert.AreEqual(1,
                ((Literal) result.Indexes[0]).Value.ToFloat());
        }

        [Test]
        public void EvalYieldsTheIndicatedComponent()
        {
            // given
            var expr = new Literal(
                new Vector(new float[] {1, 2, 3}));
            var indexes = new Expression[] {new Literal(1)};
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.Eval(null);
            // then
            Assert.IsFalse(result.IsVector(null));
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(2, result.ToFloat());
        }

        [Test]
        public void CloneYieldsShallowCopy()
        {
            // given
            var expr = new VariableAccess("a");
            var indexes = new Expression[] {new VariableAccess("b")};
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.Clone();
            // then
            Assert.IsInstanceOf<ComponentAccess>(result);
            var ca2 = (ComponentAccess) result;
            Assert.AreNotSame(ca, ca2);
            Assert.AreSame(ca.Expr, ca2.Expr);
            Assert.AreEqual(1, ca2.Indexes.Count);
            Assert.AreSame(ca.Indexes[0], ca2.Indexes[0]);
        }

        [Test]
        public void AcceptVisitorVisitsOtherExpressions()
        {
            // given
            var expr = new VariableAccess("a");
            var indexes = new Expression[]
            {
                new VariableAccess("b"),
                new VariableAccess("c")
            };
            var ca = new ComponentAccess(expr, indexes);
            var visitor = new DelegateExpressionVisitor();
            var exprs = new List<Expression>();
            visitor.VarVisitor = exprs.Add;
            visitor.ComponentAccessVisitor = exprs.Add;
            // when
            ca.AcceptVisitor(visitor);
            // then
            Assert.AreEqual(4, exprs.Count);
            Assert.AreSame(ca, exprs[0]);
            Assert.AreSame(expr, exprs[1]);
            Assert.AreSame(indexes[0], exprs[2]);
            Assert.AreSame(indexes[1], exprs[3]);
        }

        [Test]
        public void TestToString()
        {
            // given
            var expr = new VariableAccess("a");
            var indexes = new Expression[]
            {
                new VariableAccess("b"),
                new VariableAccess("c")
            };
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.ToString();
            // then
            Assert.AreEqual("a[b, c]", result);
        }
    }
}
