
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
using MetaphysicsIndustries.Solus.Exceptions;

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class VariableAccess : Expression
    {
        public VariableAccess()
            : this(null)
        {
        }

        public VariableAccess(string variableName)
        {
            if (string.IsNullOrEmpty(variableName))
                throw ValueException.Null(nameof(variableName));

            VariableName = variableName;
        }

        private readonly HashSet<VariableAccess> _visitedVarrefs =
            new HashSet<VariableAccess>();

        public IMathObject GetFinalReferencedValue(SolusEnvironment env)
        {
            // TODO: Replace this whole method with something better, some
            // better perspective

            IMathObject dest = this;
            // TODO: Not thread-safe
            _visitedVarrefs.Clear();
            while (dest is VariableAccess va)
            {
                if (_visitedVarrefs.Contains(va))
                    // found a cycle
                    // TODO: choose a more appropriate exception class
                    throw new InvalidOperationException();
                _visitedVarrefs.Add(va);
                // TODO: check for missing var name
                dest = env.GetVariable(va.VariableName);
            }

            return dest;
        }

        public override Expression Clone()
        {
            return new VariableAccess(VariableName);
        }

        public string VariableName;

        public override string ToString()
        {
            return VariableName;
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override ISet GetResultType(SolusEnvironment env)
        {
            if (env == null)
                throw new NameException(
                    $"Variable not found: {VariableName}");
            if (!env.ContainsVariable(VariableName))
                throw new NameException(
                    $"Variable not found: {VariableName}");
            return env.GetVariableType(VariableName);
        }
    }
}
