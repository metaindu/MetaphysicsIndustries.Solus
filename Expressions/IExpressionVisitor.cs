
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

namespace MetaphysicsIndustries.Solus.Expressions
{
    public interface IExpressionVisitor
    {
        void Visit(Literal literal);
        void Visit(FunctionCall func);
        void Visit(VariableAccess var);
        void Visit(DerivativeOfVariable dvar);

        void Visit(SolusMatrix matrix);
        void Visit(SolusVector vector);
    }

    public class DelegateExpressionVisitor : IExpressionVisitor
    {
        public static void DoNothing<T>(T t) { }

        public Action<Literal> LiteralVisitor = DoNothing<Literal>;
        public Action<FunctionCall> FuncVisitor = DoNothing<FunctionCall>;
        public Action<VariableAccess> VarVisitor = DoNothing<VariableAccess>;
        public Action<DerivativeOfVariable> DvarVisitor = DoNothing<DerivativeOfVariable>;
        public Action<SolusMatrix> MatrixVisitor = DoNothing<SolusMatrix>;
        public Action<SolusVector> VectorVisitor = DoNothing<SolusVector>;

        public void Visit(Literal literal)
        {
            LiteralVisitor(literal);
        }

        public void Visit(FunctionCall func)
        {
            FuncVisitor(func);
        }

        public void Visit(VariableAccess var)
        {
            VarVisitor(var);
        }

        public void Visit(DerivativeOfVariable dvar)
        {
            DvarVisitor(dvar);
        }

        public void Visit(SolusMatrix matrix)
        {
            MatrixVisitor(matrix);
        }
        public void Visit(SolusVector vector)
        {
            VectorVisitor(vector);
        }

    }
}

