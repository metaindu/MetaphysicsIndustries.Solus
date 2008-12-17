using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MetaphysicsIndustries.Solus;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Ligra
{
    public partial class LigraControl : UserControl
    {

        public virtual void RenderExpression(Graphics g, Expression expr, PointF pt, Pen pen, Brush brush)
        {
            InternalRenderExpression(g, expr, pt, pen, brush, new Dictionary<Expression, SizeF>());
        }

        protected virtual void InternalRenderExpression(Graphics g, Expression expr, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            SizeF size = CalcExpressionSize(g, expr, expressionSizeCache);

            if (DrawBoxes)
            {
                g.DrawRectangle(Pens.Red, pt.X, pt.Y, size.Width, size.Height);
            }

            pt += new SizeF(2, 2);

            if (expr is FunctionCall)
            {
                RenderFunctionCallExpression(g, expr as FunctionCall, pt, pen, brush, expressionSizeCache);
            }
            else if (expr is Literal)
            {
                RenderLiteralExpression(g, expr as Literal, pt, pen, brush, expressionSizeCache);
            }
            else if (expr is VariableAccess)
            {
                RenderVariableAccess(g, expr as VariableAccess, pt, pen, brush, expressionSizeCache);
            }
            else if (expr is ColorExpression)
            {
                InternalRenderExpression(g, expr.Eval(null), pt, pen, brush, expressionSizeCache);
            }
            else if (expr is RandomExpression)
            {
                RenderRandomExpression(g, expr as RandomExpression, pt, pen, brush, expressionSizeCache);
            }
            else if (expr is AssignExpression)
            {
                RenderAssignExpression(g, (AssignExpression)expr, pt, pen, brush, expressionSizeCache);
            }
            else
            {
                throw new InvalidOperationException("Unknown expression type: " + expr.ToString());
            }
        }

        private void RenderAssignExpression(Graphics g, AssignExpression assignExpression, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected void RenderRandomExpression(Graphics g, RandomExpression randomExpression, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            g.DrawString("rand()", Font, brush, pt);
        }

        protected void RenderVariableAccess(Graphics g, VariableAccess variableAccess, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            if (variableAccess.Variable is DerivativeOfVariable)
            {
                RenderDerivativeOfVariable(g, variableAccess, pt, pen, brush, expressionSizeCache);
            }
            else
            {
                g.DrawString(variableAccess.Variable.Name, Font, brush, pt);
            }
        }

        protected void RenderDerivativeOfVariable(Graphics g, VariableAccess variableAccess, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            DerivativeOfVariable derivativeOfVariable = (DerivativeOfVariable)variableAccess.Variable;

            int upperOrder = 0;
            Dictionary<Variable, int> lowerOrders = new Dictionary<Variable, int>();

            upperOrder = derivativeOfVariable.Order;

            string upperString = "d" + (upperOrder > 1 ? upperOrder.ToString() : string.Empty) + derivativeOfVariable.Variable.Name;
            string lowerString = "d" + derivativeOfVariable.LowerVariable.Name + (upperOrder > 1 ? upperOrder.ToString() : string.Empty);

            SizeF size = g.MeasureString(upperString, Font);
            size += new SizeF(4, 4);
            g.DrawString(upperString, Font, brush, pt + new SizeF(2, 2));
            g.DrawLine(pen, pt.X, pt.Y + size.Height, pt.X + size.Width, pt.Y + size.Height);
            g.DrawString(lowerString, Font, brush, pt.X + 2, pt.Y + size.Height + 2);
        }

        protected void RenderLiteralExpression(Graphics g, Literal literal, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            g.DrawString(literal.ToString(), Font, brush, pt);
        }

        protected void RenderFunctionCallExpression(Graphics g, FunctionCall functionCall, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            if (functionCall.Function is Operation)
            {
                RenderOperation(g, functionCall, pt, pen, brush, expressionSizeCache);
            }
            else
            {
                SizeF displayNameSize = g.MeasureString(functionCall.Function.DisplayName, Font) + new SizeF(2, 0);
                float parenWidth = g.MeasureString("(", Font).Width;// 10;
                SizeF commaSize = g.MeasureString(", ", Font);

                bool first;
                float frontWidth = displayNameSize.Width + parenWidth;
                SizeF allArgSize = new SizeF(0, 0);

                first = true;
                foreach (Expression argument in functionCall.Arguments)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        allArgSize.Width += commaSize.Width;
                    }

                    SizeF argSize = CalcExpressionSize(g, argument, expressionSizeCache);

                    allArgSize.Width += argSize.Width;
                    allArgSize.Height = Math.Max(allArgSize.Height, argSize.Height);
                }

                float currentXOffset = 0;

                first = true;
                foreach (Expression argument in functionCall.Arguments)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        g.DrawString(", ", Font, brush, pt + new SizeF(frontWidth + currentXOffset, (allArgSize.Height - commaSize.Height) / 2));
                        currentXOffset += commaSize.Width;
                    }

                    SizeF argSize = CalcExpressionSize(g, argument, expressionSizeCache);

                    InternalRenderExpression(g, argument, pt + new SizeF(frontWidth + currentXOffset, (allArgSize.Height - argSize.Height) / 2), pen, brush, expressionSizeCache);
                    currentXOffset += argSize.Width;
                }

                RectangleF rect = new RectangleF(pt.X + displayNameSize.Width + 2, pt.Y, parenWidth, allArgSize.Height);
                RenderOpenParenthesis(g, rect, pen, brush);
                rect.X += allArgSize.Width + parenWidth;
                RenderCloseParenthesis(g, rect, pen, brush);

                g.DrawString(functionCall.Function.DisplayName, Font, brush, pt + new SizeF(2, allArgSize.Height / 2 - displayNameSize.Height / 2));
            }
        }

        private void RenderOperation(Graphics g, FunctionCall functionCall, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            if (functionCall.Function is BinaryOperation)
            {
                RenderBinaryOperation(g, functionCall, pt, pen, brush, expressionSizeCache);
            }
            else if (functionCall.Function is AssociativeCommutativeOperation)
            {
                RenderAssociativeCommutativOperation(g, functionCall, pt, pen, brush, expressionSizeCache);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void RenderAssociativeCommutativOperation(Graphics g, FunctionCall functionCall, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            AssociativeCommutativeOperation operation = functionCall.Function as AssociativeCommutativeOperation;

            string symbol = operation.DisplayName;
            SizeF symbolSize = g.MeasureString(symbol, Font);

            float parenWidth = 10;
            RectangleF parenRect = new RectangleF(0, 0, parenWidth, 0);

            SizeF allArgSize = new SizeF(0, 0);

            Set<Expression> hasParens = new Set<Expression>();

            bool first = true;
            foreach (Expression arg in functionCall.Arguments)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    allArgSize.Width += symbolSize.Width;
                }

                SizeF argSize = CalcExpressionSize(g, arg, expressionSizeCache);

                if (arg is FunctionCall &&
                    (arg as FunctionCall).Function is Operation &&
                    ((arg as FunctionCall).Function as Operation).Precedence < (operation as Operation).Precedence)
                {
                    hasParens.Add(arg);
                    argSize.Width += 2 * parenWidth;
                }

                allArgSize.Width += argSize.Width;
                allArgSize.Height = Math.Max(allArgSize.Height, argSize.Height);
            }

            float x = pt.X;

            first = true;
            foreach (Expression arg in functionCall.Arguments)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    g.DrawString(symbol, Font, brush, x, pt.Y + (allArgSize.Height - symbolSize.Height) / 2);
                    x += symbolSize.Width;
                }

                SizeF argSize = CalcExpressionSize(g, arg, expressionSizeCache);

                if (hasParens.Contains(arg))
                {
                    parenRect.X = x;
                    parenRect.Y = pt.Y + (allArgSize.Height - argSize.Height) / 2;
                    parenRect.Height = argSize.Height;
                    RenderOpenParenthesis(g, parenRect, pen, brush);
                    x += parenRect.Width;
                }

                InternalRenderExpression(g, arg, new PointF(x, pt.Y + (allArgSize.Height - argSize.Height) / 2), pen, brush, expressionSizeCache);
                x += argSize.Width;

                if (hasParens.Contains(arg))
                {
                    parenRect.X = x;
                    RenderCloseParenthesis(g, parenRect, pen, brush);
                    x += parenRect.Width;
                }
            }
        }

        //private void RenderAdditionOperation(Graphics g, FunctionCall functionCall, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        //{

        //}

        private void RenderBinaryOperation(Graphics g, FunctionCall functionCall, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            if (functionCall.Function is DivisionOperation)
            {
                RenderDivisionOperation(g, functionCall, pt, pen, brush, expressionSizeCache);
            }
            else
            {
                string symbol = //" " + 
                    functionCall.Function.DisplayName
                    //+ " "
                    ;

                SizeF leftOperandSize = CalcExpressionSize(g, functionCall.Arguments[0], expressionSizeCache);
                SizeF rightOperandSize = CalcExpressionSize(g, functionCall.Arguments[1], expressionSizeCache);
                SizeF symbolSize = g.MeasureString(symbol, Font);
                float maxHeight = Math.Max(Math.Max(leftOperandSize.Height, rightOperandSize.Height), symbolSize.Height);

                float parenWidth = 10;

                bool leftParens = false;
                bool rightParens = false;

                if (NeedsLeftParen(functionCall))
                {
                    //leftOperandSize.Width += parenWidth * 2;
                    leftParens = true;
                }

                if (NeedsRightParen(functionCall))
                {
                    //rightOperandSize.Width += parenWidth * 2;
                    rightParens = true;
                }

                float x = pt.X;
                float y;
                RectangleF parenRect = new RectangleF(0, 0, 0, 0);

                parenRect.Width = parenWidth;

                y = pt.Y + (maxHeight - leftOperandSize.Height) / 2;
                parenRect.Y = y;
                parenRect.Height = leftOperandSize.Height;

                if (leftParens)
                {
                    parenRect.X = x;
                    RenderOpenParenthesis(g, parenRect, pen, brush);
                    x += parenWidth;
                }

                InternalRenderExpression(g, functionCall.Arguments[0], new PointF(x, y), pen, brush, expressionSizeCache);
                x += leftOperandSize.Width;

                if (leftParens)
                {
                    parenRect.X = x;
                    RenderCloseParenthesis(g, parenRect, pen, brush);
                    x += parenWidth;
                }




                y = pt.Y + (maxHeight - symbolSize.Height) / 2;
                g.DrawString(symbol, Font, brush, new PointF(x, y));
                if (DrawBoxes)
                {
                    g.DrawRectangle(Pens.Yellow, x, y, symbolSize.Width, symbolSize.Height);
                }
                x += symbolSize.Width;




                y = pt.Y + (maxHeight - rightOperandSize.Height) / 2;
                parenRect.Y = y;
                parenRect.Height = rightOperandSize.Height;

                if (rightParens)
                {
                    parenRect.X = x;
                    RenderOpenParenthesis(g, parenRect, pen, brush);
                    x += parenWidth;
                }

                InternalRenderExpression(g, functionCall.Arguments[1], new PointF(x, y), pen, brush, expressionSizeCache);
                x += rightOperandSize.Width;

                if (rightParens)
                {
                    parenRect.X = x;
                    RenderCloseParenthesis(g, parenRect, pen, brush);
                    x += parenWidth;
                }
            }
        }

        private static bool NeedsRightParen(FunctionCall functionCall)
        {
            return (functionCall.Arguments[1] is FunctionCall &&
                                (functionCall.Arguments[1] as FunctionCall).Function is Operation &&
                                ((functionCall.Arguments[1] as FunctionCall).Function as Operation).Precedence < (functionCall.Function as Operation).Precedence) ||
                                (functionCall.Arguments[1] is FunctionCall && (functionCall.Arguments[1] as FunctionCall).Function is ExponentOperation) ||
                                (functionCall.Arguments[1] is FunctionCall && (functionCall.Arguments[1] as FunctionCall).Function is DivisionOperation);
        }

        private static bool NeedsLeftParen(FunctionCall functionCall)
        {
            return (functionCall.Arguments[0] is FunctionCall &&
                                (functionCall.Arguments[0] as FunctionCall).Function is Operation &&
                                ((functionCall.Arguments[0] as FunctionCall).Function as Operation).Precedence < (functionCall.Function as Operation).Precedence) ||
                                (functionCall.Arguments[0] is FunctionCall && (functionCall.Arguments[0] as FunctionCall).Function is ExponentOperation) ||
                                (functionCall.Arguments[0] is FunctionCall && (functionCall.Arguments[0] as FunctionCall).Function is DivisionOperation);
        }

        private void RenderDivisionOperation(Graphics g, FunctionCall functionCall, PointF pt, Pen pen, Brush brush, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            SizeF size1 = CalcExpressionSize(g, functionCall.Arguments[0], expressionSizeCache);
            SizeF size2 = CalcExpressionSize(g, functionCall.Arguments[1], expressionSizeCache);
            SizeF size = new SizeF(Math.Max(size1.Width, size2.Width), size1.Height + size2.Height);

            float lineExtraWidth = 4;
            float lineHeightSpacing = 4;

            InternalRenderExpression(g, functionCall.Arguments[0], pt + new SizeF(lineExtraWidth + (size.Width - size1.Width) / 2, 0), pen, brush, expressionSizeCache);
            InternalRenderExpression(g, functionCall.Arguments[1], pt + new SizeF(lineExtraWidth + (size.Width - size2.Width) / 2, size1.Height + lineHeightSpacing), pen, brush, expressionSizeCache);

            g.DrawLine(pen,
                pt.X,
                    pt.Y + size1.Height + lineHeightSpacing / 2,
                pt.X + size.Width + 2 * lineExtraWidth,
                    pt.Y + size1.Height + lineHeightSpacing / 2);
        }

        protected void RenderCloseParenthesis(Graphics g, RectangleF rect, Pen pen, Brush brush)
        {
            rect.Width -= 2;

            //g.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Top);
            //g.DrawLine(pen, rect.Right, rect.Top, rect.Right, rect.Bottom);
            //g.DrawLine(pen, rect.Left, rect.Bottom, rect.Right, rect.Bottom);

            g.DrawArc(pen, rect.Left, rect.Top, rect.Width, 2 * rect.Width, 270, 90);
            g.DrawLine(pen, rect.Right, rect.Top + rect.Width, rect.Right, rect.Bottom - rect.Width);
            g.DrawArc(pen, rect.Left, rect.Bottom - 2 * rect.Width, rect.Width, 2 * rect.Width, 0, 90);

            //float x = rect.Width;
            //float y = rect.Height;
            //float r = y * y / (8 * x) + x / 2;
            //float theta = (float)(Math.Asin(r / (2 * y)) * 180 / Math.PI);

            //RectangleF r2 = new RectangleF(rect.X - 2 * r + x, rect.Y + y / 2 - r, 2 * r, 2 * r);
            //g.DrawArc(pen, r2, -theta, 2 * theta);
        }

        protected void RenderOpenParenthesis(Graphics g, RectangleF rect, Pen pen, Brush brush)
        {
            rect.X += 2;
            rect.Width -= 2;

            //g.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Top);
            //g.DrawLine(pen, rect.Left, rect.Top, rect.Left, rect.Bottom);
            //g.DrawLine(pen, rect.Left, rect.Bottom, rect.Right, rect.Bottom);

            g.DrawArc(pen, rect.Left, rect.Top, rect.Width, 2 * rect.Width, 180, 90);
            g.DrawLine(pen, rect.Left, rect.Top + rect.Width, rect.Left, rect.Bottom - rect.Width);
            g.DrawArc(pen, rect.Left, rect.Bottom - 2 * rect.Width, rect.Width, 2 * rect.Width, 90, 90);

            //float x = rect.Width;
            //float y = rect.Height;
            //float r = y * y / (8 * x) + x / 2;
            //float theta = (float)(Math.Asin(r / (2 * y)) * 180 / Math.PI);

            //RectangleF r2 = new RectangleF(rect.X, rect.Y + y / 2 - r, 2 * r, 2 * r);
            //g.DrawArc(pen, r2, 180 - theta, 2 * theta);
        }

    }
}