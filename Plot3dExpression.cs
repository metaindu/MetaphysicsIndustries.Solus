using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MetaphysicsIndustries.Solus
{
    public class Plot3dExpression : Expression
    {
        //public Plot3dExpression(Variable independentVariableX, Variable independentVariableY, Expression expressionToPlot)
        //    : this(independentVariableX, independentVariableY, expressionToPlot, Pens.Black, Brushes.Green)
        //{
        //}
        public Plot3dExpression(
            Variable independentVariableX,
            Variable independentVariableY, 
            Expression expressionToPlot,
            double xMin, double xMax,
            double yMin, double yMax,
            double zMin, double zMax,
            Pen wirePen, Brush fillBrush)
        {
            _independentVariableX = independentVariableX;
            _independentVariableY = independentVariableY;
            _expressionToPlot = expressionToPlot;
            _wirePen = wirePen;
            _fillBrush = fillBrush;
            _xMin = xMin;
            _xMax = xMax;
            _yMin = yMin;
            _yMax = yMax;
            _zMin = zMin;
            _zMax = zMax;
        }

        private Variable _independentVariableX;
        public Variable IndependentVariableX
        {
            get { return _independentVariableX; }
            set { _independentVariableX = value; }
        }

        private Variable _independentVariableY;
        public Variable IndependentVariableY
        {
            get { return _independentVariableY; }
            set { _independentVariableY = value; }
        }

        private Expression _expressionToPlot;
        public Expression ExpressionToPlot
        {
            get { return _expressionToPlot; }
            set { _expressionToPlot = value; }
        }


        private Pen _wirePen;
        public Pen WirePen
        {
            get { return _wirePen; }
            set { _wirePen = value; }
        }

        private Brush _fillBrush;
        public Brush FillBrush
        {
            get { return _fillBrush; }
            set { _fillBrush = value; }
        }

        private double _xMin;
        public double XMin
        {
            get { return _xMin; }
            set { _xMin = value; }
        }
        private double _xMax;
        public double XMax
        {
            get { return _xMax; }
            set { _xMax = value; }
        }
        private double _yMin;
        public double YMin
        {
            get { return _yMin; }
            set { _yMin = value; }
        }
        private double _yMax;
        public double YMax
        {
            get { return _yMax; }
            set { _yMax = value; }
        }
        private double _zMin;
        public double ZMin
        {
            get { return _zMin; }
            set { _zMin = value; }
        }
        private double _zMax;
        public double ZMax
        {
            get { return _zMax; }
            set { _zMax = value; }
        }


        public override Literal Eval(VariableTable varTable)
        {
            if (ExpressionToPlot != null)
            {
                return ExpressionToPlot.Eval(varTable);
            }
            else
            {
                return new Literal(0);
            }
        }

        public override Expression Clone()
        {
            return new Plot3dExpression(
                IndependentVariableX, 
                IndependentVariableY, 
                ExpressionToPlot,
                XMin, XMax,
                YMin, YMax,
                ZMin, ZMax,
                WirePen, FillBrush);
        }
    }
}
