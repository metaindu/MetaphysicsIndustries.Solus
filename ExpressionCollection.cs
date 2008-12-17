
/*****************************************************************************
 *                                                                           *
 *  ExpressionCollection.cs                                                  *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  An ordered list of expressions.                                          *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;
//using MetaphysicsIndustries.Sandbox;
//using MetaphysicsIndustries.Build;

namespace MetaphysicsIndustries.Solus
{
	public class ExpressionCollection : List<Expression>, IDisposable//, ISandboxMappable
	{
        public void Dispose()
        {
            Clear();
        }

        //#region ISandboxMappable Members

        //public MemberDescriptor[] GetCustomMembers()
        //{
        //    List<MemberDescriptor> members = new List<MemberDescriptor>();

        //    members.Add(new MemberDescriptor("Count", SystemType.GetSystemType(typeof(int)), new PropertyValueGetter(this, GetType().GetProperty("Count"))));

        //    int i = 0;
        //    foreach (Expression expr in this)
        //    {
        //        members.Add(new MemberDescriptor(i.ToString(), expr));
        //        i++;
        //    }

        //    return members.ToArray();
        //}

        //public ObjectDescriptor GetCustomObjectDescriptor()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public bool UsesCustomMembers()
        //{
        //    return true;
        //}

        //public bool UsesCustomObjectDescriptor()
        //{
        //    return false;
        //}

        //#endregion
    }
}
