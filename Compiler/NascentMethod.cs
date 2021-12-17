
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public class NascentMethod
    {
        readonly Dictionary<string, byte> _localIndexesByName = new Dictionary<string, byte>();
        public readonly List<IlLocal> Locals = new List<IlLocal>();

        public byte CreateIndexOfLocalForVariableName(string name)
        {
            if (!_localIndexesByName.ContainsKey(name))
            {
                int index = _localIndexesByName.Count;
                _localIndexesByName.Add(name, (byte)index);
                Locals.Add(new IlLocal
                {
                    Usage = IlLocalUsage.BakedVariable,
                    VariableName = name,
                });
                return (byte)index;
            }

            return _localIndexesByName[name];
        }

        public readonly List<Instruction> Instructions =
            new List<Instruction>();

        private readonly Dictionary<IlExpression, int> _expressionLocations =
            new Dictionary<IlExpression, int>();

        private readonly Dictionary<int, List<IlExpression>>
            _expressionsByLocation =
                new Dictionary<int, List<IlExpression>>();

        public void RecordExpressionLocation(IlExpression expr)
        {
            if (_expressionLocations.ContainsKey(expr))
                return;
            int index = Instructions.Count;
            _expressionLocations[expr] = index;
            if (!_expressionsByLocation.ContainsKey(index))
                _expressionsByLocation[index] = new List<IlExpression>();
            _expressionsByLocation[Instructions.Count].Add(expr);
        }

        private Dictionary<IlExpression, IlLabel> _labelsByExpression =
            new Dictionary<IlExpression, IlLabel>();

        public IlLabel GetOrCreateExpressionLabel(IlExpression expr)
        {
            if (!_labelsByExpression.ContainsKey(expr))
            {
                var label = new IlLabel(expr);
                _labelsByExpression[expr] = label;
            }

            return _labelsByExpression[expr];
        }

        public IEnumerable<IlLabel> GetAllLabels() =>
            _labelsByExpression.Values;

        public IEnumerable<IlLabel> GetLabelsByLocation(int index)
        {
            if (!_expressionsByLocation.ContainsKey(index)) return null;
            var exprs = _expressionsByLocation[index];
            var labels = new List<IlLabel>(exprs.Count);
            int i;
            for (i = 0; i < exprs.Count; i++)
                if (_labelsByExpression.ContainsKey(exprs[i]))
                    labels.Add(_labelsByExpression[exprs[i]]);
            return labels;
        }
    }
}
