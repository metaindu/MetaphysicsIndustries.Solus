
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
            Assert.That(result.Expr, Is.SameAs(expr));
            Assert.That(result.Indexes.Count, Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(result.Indexes[0]);
            Assert.That(((Literal) result.Indexes[0]).Value.ToFloat(),
                Is.EqualTo(1));
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
            Assert.That(ca2, Is.Not.SameAs(ca));
            Assert.That(ca2.Expr, Is.SameAs(ca.Expr));
            Assert.That(ca2.Indexes.Count, Is.EqualTo(1));
            Assert.That(ca2.Indexes[0], Is.SameAs(ca.Indexes[0]));
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
            Assert.That(exprs.Count, Is.EqualTo(4));
            Assert.That(exprs[0], Is.SameAs(ca));
            Assert.That(exprs[1], Is.SameAs(expr));
            Assert.That(exprs[2], Is.SameAs(indexes[0]));
            Assert.That(exprs[3], Is.SameAs(indexes[1]));
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
            Assert.That(result, Is.EqualTo("a[b, c]"));
        }
    }
}
