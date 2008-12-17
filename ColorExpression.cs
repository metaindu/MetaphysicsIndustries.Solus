using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MetaphysicsIndustries.Solus
{
    public class ColorExpression : Expression
    {
        public static readonly ColorExpression Black = new ColorExpression(Color.Black);
        public static readonly ColorExpression White = new ColorExpression(Color.White);
        public static readonly ColorExpression Gray = new ColorExpression(Color.Gray);
        public static readonly ColorExpression Red = new ColorExpression(Color.Red);
        public static readonly ColorExpression Green = new ColorExpression(Color.Green);
        public static readonly ColorExpression Blue = new ColorExpression(Color.Blue);
        public static readonly ColorExpression Yellow = new ColorExpression(Color.Yellow);
        public static readonly ColorExpression Cyan = new ColorExpression(Color.Cyan);
        public static readonly ColorExpression Magenta = new ColorExpression(Color.Magenta);

        public ColorExpression()
            : this(Color.Black)
        {
        }
        public ColorExpression(Color color)
        {
            Color = color;
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;

                    _pen = new Pen(_color);
                }
            }
        }

        private Pen _pen;
        public Pen Pen
        {
            get { return _pen; }
        }

        public Brush Brush
        {
            get { return _pen.Brush; }
        }



        public override Literal Eval(VariableTable varTable)
        {
            return new Literal(0xFFFFFF & _color.ToArgb());
        }

        public override Expression Clone()
        {
            return new ColorExpression(_color);
        }

        public override string ToString()
        {
            if (Color == Color.Black) { return "Black"; }
            if (Color == Color.White) { return "White"; }
            if (Color == Color.Gray) { return "Gray"; }
            if (Color == Color.Red) { return "Red"; }
            if (Color == Color.Green) { return "Green"; }
            if (Color == Color.Blue) { return "Blue"; }
            if (Color == Color.Yellow) { return "Yellow"; }
            if (Color == Color.Cyan) { return "Cyan"; }
            if (Color == Color.Magenta) { return "Magenta"; }

            return Color.ToString();
        }
    }
}
