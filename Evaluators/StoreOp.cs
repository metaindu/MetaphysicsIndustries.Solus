
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

namespace MetaphysicsIndustries.Solus.Evaluators
{
    public interface IGenericStoreOp
    {
        Type ElementType { get; }
    }

    public abstract class StoreOp1
    {
        public abstract void Store(int index, IMathObject value);
        public abstract void SetMinArraySize(int length);
    }

    public class StoreOp1<T> : StoreOp1, IGenericStoreOp
        where T : IMathObject
    {
        public T[] Values;

        public override void Store(int index, IMathObject value)
        {
            Values[index] = (T)value;
        }

        public override void SetMinArraySize(int length)
        {
            if (Values == null || Values.Length < length)
                Values = new T[length];
        }

        public Type ElementType => typeof(T);
    }

    public abstract class StoreOp2
    {
        public abstract void Store(int index0, int index1,
            IMathObject value);

        public abstract void SetMinArraySize(int length0, int length1);
    }

    public class StoreOp2<T> : StoreOp2
        where T : IMathObject
    {
        public T[,] Values;

        public override void Store(int index0, int index1,
            IMathObject value)
        {
            Values[index0, index1] = (T)value;
        }

        public override void SetMinArraySize(int length0, int length1)
        {
            if (Values == null ||
                Values.GetLength(0) < length0 ||
                Values.GetLength(1) < length1)
            {
                Values = new T[length0, length1];
            }
        }
    }

    public abstract class StoreOp3
    {
        public abstract void Store(int index0, int index1, int index2,
            IMathObject value);

        public abstract void SetMinArraySize(int length0, int length1,
            int length2);
    }

    public class StoreOp3<T> : StoreOp3
        where T : IMathObject
    {
        public T[,,] Values;

        public override void Store(int index0, int index1, int index2,
            IMathObject value)
        {
            Values[index0, index1, index2] = (T)value;
        }

        public override void SetMinArraySize(int length0, int length1,
            int length2)
        {
            if (Values == null ||
                Values.GetLength(0) < length0 ||
                Values.GetLength(1) < length1 ||
                Values.GetLength(2) < length2)
            {
                Values = new T[length0, length1, length2];
            }
        }
    }
}
