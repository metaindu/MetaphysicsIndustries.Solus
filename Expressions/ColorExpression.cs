
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
using System.Drawing;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
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



        public override IMathObject Eval(SolusEnvironment env)
        {
            return new Number(0xFFFFFF & _color.ToArgb());
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

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override IMathObject Result { get; } = new ResultC();
        private class ResultC : IMathObject
        {
            public bool? IsScalar(SolusEnvironment env) => true;
            public bool? IsVector(SolusEnvironment env) => false;
            public bool? IsMatrix(SolusEnvironment env) => false;
            public int? GetTensorRank(SolusEnvironment env) => 0;
            public bool? IsString(SolusEnvironment env) => false;

            public int? GetDimension(SolusEnvironment env, int index)
            {
                throw new IndexOutOfRangeException(
                    "Color expressions do not have dimensions");
            }

            public int[] GetDimensions(SolusEnvironment env)
            {
                throw new IndexOutOfRangeException(
                    "Color expressions do not have dimensions");
            }

            public int? GetVectorLength(SolusEnvironment env)
            {
                throw new InvalidOperationException(
                    "Color expressions do not have a length");
            }

            public bool IsConcrete => false;
        }
    }
}
