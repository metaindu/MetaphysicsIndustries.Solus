
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
    public class DerivativeOfVariable : Expression
    {
        public DerivativeOfVariable(string variable, string lowerVariable)
        {
            _variable = variable;
            _order = 1;
            _lowerVariable = lowerVariable;
        }
        public DerivativeOfVariable(DerivativeOfVariable variable, string lowerVariable)
        {
            if (variable.LowerVariable == lowerVariable)
            {
                _variable = variable.Variable;
                _order = variable.Order + 1;
            }
            else
            {
                _variable = variable.Name;
                _order = 1;
            }
            _lowerVariable = lowerVariable;
        }

        private string _variable;

        public string Variable
        {
            get { return _variable; }
        }

        private int _order;

        public int Order
        {
            get { return _order; }
        }

        private string _lowerVariable;
        public string LowerVariable
        {
            get { return _lowerVariable; }
        }


        public string Name
        {
            get
            {
                return "d" + (Order > 1 ? Order.ToString() : "") + Variable + "/d" + LowerVariable + (Order > 1 ? Order.ToString() : "");
            }
        }

        public override Expression Clone()
        {
            throw new NotImplementedException();
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override IMathObject Result =>
            throw new NotImplementedException();
    }
}
