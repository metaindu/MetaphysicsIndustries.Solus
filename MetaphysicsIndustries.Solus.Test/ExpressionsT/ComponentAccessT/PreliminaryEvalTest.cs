
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

using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ComponentAccessT
{
    [TestFixture]
    public class PreliminaryEvalTest
    {
        static Vector vector(params float[] values) => new Vector(values);

        static Literal vliteral(params float[] values) =>
            new Literal(new Vector(values));

        static VectorExpression varvector(params string[] values) =>
            new VectorExpression(values.Length,
                values.Select(
                    s => (Expression)new VariableAccess(s)).ToArray());

        static Expression[] litindexes(params float[] values) =>
            values.Select(v => (Expression) new Literal(v)).ToArray();

        static Expression[] varindexes(params string[] values) =>
            values.Select(
                v => (Expression) new VariableAccess(v)).ToArray();

        private static SolusEnvironment empty = new SolusEnvironment();

        [Test]
        public void LiteralExprWithNonLiteralIndexYieldsComponentAccessExpr()
        {
            // given
            var expr = vliteral(1, 2, 3);
            var indexes = new Expression[] {new VariableAccess("a")};
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.PreliminaryEval(empty);
            // then
            Assert.IsInstanceOf<ComponentAccess>(result);
            var ca2 = (ComponentAccess) result;
            Assert.IsInstanceOf<Literal>(ca2.Expr);
            Assert.AreEqual(1, ca2.Indexes.Count);
            Assert.IsInstanceOf<VariableAccess>(ca2.Indexes[0]);
        }

        [Test]
        public void LiteralExprWithLiteralIndexYieldsLiteralComponent()
        {
            // given
            var expr = vliteral(4, 5, 6);
            var indexes = new Expression[] {new Literal(1.ToNumber())};
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.PreliminaryEval(empty);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var lit = (Literal) result;
            Assert.IsTrue(lit.Value.IsScalar(null));
            Assert.AreEqual(5, lit.Value.ToFloat());
        }

        [Test]
        public void VectorExprWithNonLiteralIndexYieldsComponentAccessExpr()
        {
            // given
            var expr = varvector("a", "b", "c");
            var indexes = varindexes("x");
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.PreliminaryEval(empty);
            // then
            Assert.IsInstanceOf<ComponentAccess>(result);
            var ca2 = (ComponentAccess) result;
            Assert.IsInstanceOf<VectorExpression>(ca2.Expr);
            Assert.AreEqual(3, ((VectorExpression) ca2.Expr).Length);
            Assert.AreEqual(1, ca2.Indexes.Count);
            Assert.IsInstanceOf<VariableAccess>(ca2.Indexes[0]);
        }

        [Test]
        public void VectorExprWithLiteralIndexYieldsExprComponent()
        {
            // given
            var expr = varvector("a", "b", "c");
            var indexes = litindexes(1);
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.PreliminaryEval(empty);
            // then
            Assert.IsInstanceOf<VariableAccess>(result);
            var va = (VariableAccess) result;
            Assert.AreEqual("b", va.VariableName);
        }

        [Test]
        public void VarAccessWithLiteralIndexYieldsComponentAccessExpr()
        {
            // given
            var expr = new VariableAccess("a");
            var indexes = litindexes(1);
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.PreliminaryEval(empty);
            // then
            Assert.IsInstanceOf<ComponentAccess>(result);
            var ca2 = (ComponentAccess) result;
            Assert.IsInstanceOf<VariableAccess>(ca2.Expr);
            Assert.AreEqual("a",
                ((VariableAccess) ca2.Expr).VariableName);
            Assert.AreEqual(1, ca2.Indexes.Count);
            Assert.IsInstanceOf<Literal>(ca2.Indexes[0]);
        }
    }
}
