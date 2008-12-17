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

        public SizeF CalcExpressionSize(Graphics g, Expression expr)
        {
            return CalcExpressionSize(g, expr, new Dictionary<Expression, SizeF>());
        }

        protected SizeF CalcExpressionSize(Graphics g, Expression expr, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            if (expressionSizeCache.ContainsKey(expr))
            {
                return expressionSizeCache[expr];
            }

            SizeF size;

            if (expr is FunctionCall)
            {
                size = CalcFunctionCallSize(g, expr, expressionSizeCache);
            }
            else if (expr is Literal)
            {
                size = g.MeasureString((expr as Literal).ToString(), Font);
            }
            else if (expr is VariableAccess)
            {
                if (((VariableAccess)expr).Variable is DerivativeOfVariable)
                {
                    DerivativeOfVariable derivativeOfVariable = (DerivativeOfVariable)(((VariableAccess)expr).Variable);

                    int upperOrder = 0;
                    Dictionary<Variable, int> lowerOrders = new Dictionary<Variable, int>();

                    upperOrder = derivativeOfVariable.Order;

                    string upperString = "d" + (upperOrder > 1 ? upperOrder.ToString() : string.Empty) + derivativeOfVariable.Variable.Name;
                    string lowerString = "d" + derivativeOfVariable.LowerVariable.Name + (upperOrder > 1 ? upperOrder.ToString() : string.Empty);

                    SizeF size2 = g.MeasureString(upperString, Font);
                    SizeF size3 = g.MeasureString(lowerString, Font);

                    size = new SizeF(Math.Max(size2.Width, size3.Width), size2.Height + size3.Height + 2);
                }
                else
                {
                    size = g.MeasureString((expr as VariableAccess).Variable.Name, Font);
                }
            }
            else if (expr is ColorExpression)
            {
                size = CalcExpressionSize(g, expr.Eval(null), expressionSizeCache);
            }
            else if (expr is RandomExpression)
            {
                size = g.MeasureString("rand()", Font);
            }
            else if (expr is AssignExpression)
            {
                AssignExpression expr2 = (AssignExpression)expr;

                size = g.MeasureString(expr2.Variable.Name, Font);

                SizeF size2 = g.MeasureString(" = ", Font);
                size.Width += size2.Width;
                size.Height = Math.Max(size.Height, size2.Height);

                size2 = CalcExpressionSize(g, expr2.Value);
                size.Width += size2.Width;
                size.Height = Math.Max(size.Height, size2.Height);
            }
            else
            {
                throw new InvalidOperationException();
            }

            //margin
            size += new SizeF(4, 4);

            expressionSizeCache[expr] = size;

            return size;
        }

        private SizeF CalcFunctionCallSize(Graphics g, Expression expr, Dictionary<Expression, SizeF> expressionSizeCache)
        {
            SizeF size;
            FunctionCall functionCall = expr as FunctionCall;

            if (functionCall.Function is Operation)
            {
                //if (call.Function is UnaryOperation)
                //{
                //}
                //else
                if (functionCall.Function is BinaryOperation)
                {
                    if (functionCall.Function is DivisionOperation)
                    {
                        SizeF topOperandSize = CalcExpressionSize(g, functionCall.Arguments[0], expressionSizeCache);
                        SizeF bottomOperandSize = CalcExpressionSize(g, functionCall.Arguments[1], expressionSizeCache);

                        float width = Math.Max(topOperandSize.Width, bottomOperandSize.Width);
                        float height = topOperandSize.Height + bottomOperandSize.Height;
                        float lineExtraWidth = 2 * 4;
                        float lineHeightSpacing = 4;

                        size = new SizeF(width + lineExtraWidth, height + lineHeightSpacing);
                    }
                    else
                    {
                        string operatorSymbol = //" " + 
                            functionCall.Function.DisplayName
                            //+ " "
                            ;

                        SizeF leftOperandSize = CalcExpressionSize(g, functionCall.Arguments[0], expressionSizeCache);
                        SizeF rightOperandSize = CalcExpressionSize(g, functionCall.Arguments[1], expressionSizeCache);
                        SizeF operatorSymbolSize = g.MeasureString(operatorSymbol, Font);

                        float parenWidth = 10;

                        //if (functionCall.Arguments[0] is FunctionCall &&
                        //    (functionCall.Arguments[0] as FunctionCall).Function is Operation &&
                        //    ((functionCall.Arguments[0] as FunctionCall).Function as Operation).Precedence < (functionCall.Function as Operation).Precedence)
                        if (NeedsLeftParen(functionCall))
                        {
                            leftOperandSize.Width += parenWidth * 2;
                        }

                        //if (functionCall.Arguments[1] is FunctionCall &&
                        //    (functionCall.Arguments[1] as FunctionCall).Function is Operation &&
                        //    ((functionCall.Arguments[1] as FunctionCall).Function as Operation).Precedence < (functionCall.Function as Operation).Precedence)
                        if (NeedsRightParen(functionCall))
                        {
                            rightOperandSize.Width += parenWidth * 2;
                        }

                        float width = leftOperandSize.Width + rightOperandSize.Width + operatorSymbolSize.Width;
                        float height = Math.Max(Math.Max(leftOperandSize.Height, rightOperandSize.Height), operatorSymbolSize.Height);

                        size = new SizeF(width, height);
                    }
                }
                else if (functionCall.Function is AssociativeCommutativeOperation)
                {
                    AssociativeCommutativeOperation operation = functionCall.Function as AssociativeCommutativeOperation;
                    string symbol = operation.DisplayName;
                    SizeF symbolSize = g.MeasureString(symbol, Font);

                    float parenWidth = 10;

                    size = new SizeF(0, 0);

                    bool first = true;
                    foreach (Expression arg in functionCall.Arguments)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            size.Width += symbolSize.Width;
                        }

                        SizeF argSize = CalcExpressionSize(g, arg, expressionSizeCache);

                        if (arg is FunctionCall &&
                            (arg as FunctionCall).Function is Operation &&
                            ((arg as FunctionCall).Function as Operation).Precedence < (operation as Operation).Precedence)
                        {
                            argSize.Width += parenWidth * 2;
                        }


                        size.Width += argSize.Width;
                        size.Height = Math.Max(size.Height, argSize.Height);
                    }

                }
                else
                {
                    throw new InvalidOperationException("Unknown Operation: " + functionCall.Function.ToString());
                }
            }
            else
            {
                SizeF displayNameSize;
                SizeF openParenSize;
                SizeF closeParenSize;
                SizeF commaSize;
                SizeF allArgSize;

                displayNameSize = g.MeasureString(functionCall.Function.DisplayName, Font) + new SizeF(2, 0);
                openParenSize = g.MeasureString("(", Font);
                closeParenSize = g.MeasureString(")", Font);
                commaSize = g.MeasureString(", ", Font);

                allArgSize = new SizeF(0, 0);

                bool first = true;
                foreach (Expression arg in functionCall.Arguments)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        allArgSize.Width += commaSize.Width;
                    }
                    SizeF argSize = CalcExpressionSize(g, arg, expressionSizeCache);
                    allArgSize.Width += argSize.Width;
                    allArgSize.Height = Math.Max(allArgSize.Height, argSize.Height);
                }

                float width;
                float height;

                width = displayNameSize.Width + openParenSize.Width + allArgSize.Width + closeParenSize.Width;
                height = Math.Max(Math.Max(Math.Max(displayNameSize.Height, openParenSize.Height), commaSize.Height), allArgSize.Height);

                size = new SizeF(width, height);
            }
            return size;
        }

    }
}