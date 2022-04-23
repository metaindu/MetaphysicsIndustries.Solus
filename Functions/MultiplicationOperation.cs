
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
using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class MultiplicationOperation : AssociativeCommutativeOperation
    {
        public static readonly MultiplicationOperation Value = new MultiplicationOperation();

        protected MultiplicationOperation()
        {
            Name = "*";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Multiplication; }
        }

        //public override bool IsCommutative
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        //public override bool IsAssociative
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        public override bool Collapses
        {
            get
            {
                return true;
            }
        }

        public override float CollapseValue
        {
            get
            {
                return 0;
            }
        }

        public override IMathObject GetResult(IEnumerable<IMathObject> args)
        {
            // s*s=s
            // s*v=v
            // v*s=v
            // v*v=X
            // s*m=m
            // m*s=m
            // v*m=v?
            // m*V=v?
            // m*m=m
            var args1 = args.ToList();
            var current = args1[0];
            var n = args1.Count;
            for (int i = 1; i < n; i++)
            {
                var cs = current.IsIsScalar(null);
                var cv = current.IsIsVector(null);
                var cm = current.IsIsMatrix(null);
                if (!cs && !cv && !cm)
                    return null;
                var next = args1[i];
                var ns = next.IsIsScalar(null);
                var nv = next.IsIsVector(null);
                var nm = next.IsIsMatrix(null);
                if (!ns && !nv && !nm)
                    return null;

                if (cs)
                {
                    // if (ns)
                    // {
                    //     // current = next;
                    // }
                    // else if (nv)
                    // {
                    //     current = next;
                    // }
                    // else // if (nm)
                    // {
                    //     current = next;
                    // }
                    current = next;
                }
                else if (cv)
                {
                    if (ns)
                    {
                        // current = current;
                    }
                    else if (nv)
                    {
                        throw new NotImplementedException();
                    }
                    else // if (nm)
                    {
                        throw new NotImplementedException();
                    }
                }
                else // if (cm)
                {
                    if (ns)
                    {
                        // current = current;
                    }
                    else if (nv)
                    {
                        throw new NotImplementedException();
                    }
                    else // if (nm)
                    {
                        if (current.GetDimension(null, 1) !=
                            next.GetDimension(null, 0))
                            throw new OperandException(
                                "Matrix dimensions don't match for matrix " +
                                "multiplication");
                        var tt = current.GetDimension(null, 0);
                        if (!tt.HasValue)
                            throw new InvalidOperationException(
                                "Null row count?");
                        var r = tt.Value;
                        tt = next.GetDimension(null, 1);
                        if (!tt.HasValue)
                            throw new InvalidOperationException(
                                "Null column count?");
                        var c = tt.Value;
                        current = new MatrixMathObject(r, c);
                    }
                }
            }

            return current;
        }
    }
}
