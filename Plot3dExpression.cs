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
            float xMin, float xMax,
            float yMin, float yMax,
            float zMin, float zMax,
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

        private float _xMin;
        public float XMin
        {
            get { return _xMin; }
            set { _xMin = value; }
        }
        private float _xMax;
        public float XMax
        {
            get { return _xMax; }
            set { _xMax = value; }
        }
        private float _yMin;
        public float YMin
        {
            get { return _yMin; }
            set { _yMin = value; }
        }
        private float _yMax;
        public float YMax
        {
            get { return _yMax; }
            set { _yMax = value; }
        }
        private float _zMin;
        public float ZMin
        {
            get { return _zMin; }
            set { _zMin = value; }
        }
        private float _zMax;
        public float ZMax
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
