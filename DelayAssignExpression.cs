using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class DelayAssignExpression : Expression
    {
        public DelayAssignExpression(Variable variable, Expression expression)
        {
            _variable = variable;
            _expression = expression;
        }

        private Variable _variable;

        public Variable Variable
        {
            get { return _variable; }
            set { _variable = value; }
        }

        private Expression _expression;

        public Expression Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        public override Literal Eval(VariableTable varTable)
        {
            Literal result = Expression.Eval(varTable);

            varTable[Variable] = result;//.Value;

            return result;
        }

        public override Expression Clone()
        {
            return new DelayAssignExpression(Variable, Expression);
        }

        //public override Expression CleanUp()
        //{
        //    return new DelayAssignExpression(Variable, Expression.CleanUp());
        //}

        protected override void InternalApplyToExpressionTree(SolusAction action, bool applyToChildrenBeforeParent)
        {
            if (Expression != null)
            {
                Expression.ApplyToExpressionTree(action, applyToChildrenBeforeParent);
            }
        }
    }
}
