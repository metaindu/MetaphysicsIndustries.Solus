
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
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

namespace MetaphysicsIndustries.Solus
{
    public interface IMathObject
    {
        bool? IsScalar(SolusEnvironment env);
        bool? IsVector(SolusEnvironment env);
        bool? IsMatrix(SolusEnvironment env);
        int? GetTensorRank(SolusEnvironment env);
        bool? IsString(SolusEnvironment env);
        int? GetDimension(SolusEnvironment env, int index);
        int[] GetDimensions(SolusEnvironment env);
        int? GetVectorLength(SolusEnvironment env);
        // TODO: int? GetStringLength(SolusEnvironment env);
        bool? IsInterval(SolusEnvironment env);
        bool? IsFunction(SolusEnvironment env);
        bool? IsExpression(SolusEnvironment env);

        bool IsConcrete { get; }

        string DocString { get; }
    }

    public interface ITensor : IMathObject
    {
    }

    public interface IVector : ITensor
    {
        int Length { get; }
        IMathObject GetComponent(int index);
        IMathObject this[int index] { get; }
    }

    public interface IMatrix : ITensor
    {
        int RowCount { get; }
        int ColumnCount { get; }
        IMathObject GetComponent(int row, int column);
        IMathObject this[int row,int column] { get; }
    }

    public class ScalarMathObject : IMathObject
    {
        public static readonly ScalarMathObject Value = new ScalarMathObject();
        public bool? IsScalar(SolusEnvironment env) => true;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => 0;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;

        public bool IsConcrete => false;

        public string DocString => "";
    }

    public class IntervalMathObject : IMathObject
    {
        public static readonly IntervalMathObject Value =
            new IntervalMathObject();
        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => null;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => true;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;

        public bool IsConcrete => false;

        public string DocString => "";
    }
}
