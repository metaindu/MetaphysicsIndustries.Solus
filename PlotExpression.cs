using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public class PlotExpression : Expression
    {
        public PlotExpression(Variable variable, params Expression[] expressionsToPlot)
            : this(variable, (IEnumerable<Expression>)expressionsToPlot)
        {
        }
        public PlotExpression(Variable variable, IEnumerable<Expression> expressionsToPlot)
        {
            _variable = variable;
            _expressionsToPlot = expressionsToPlot.ToArray();
        }

        private Variable _variable;
        public Variable Variable
        {
            get { return _variable; }
            set { _variable = value; }
        }

        private Expression[] _expressionsToPlot;
        public Expression[] ExpressionsToPlot
        {
            get { return _expressionsToPlot; }
            set { _expressionsToPlot = value; }
        }


        public override Literal Eval(VariableTable varTable)
        {
            if (ExpressionsToPlot.Length > 0)
            {
                return ExpressionsToPlot[ExpressionsToPlot.Length - 1].Eval(varTable);
            }
            else
            {
                return new Literal(0);
            }
        }

        public override Expression Clone()
        {
            return new PlotExpression(Variable, ExpressionsToPlot);
        }
    }
}
