
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

using System;

namespace MetaphysicsIndustries.Solus.Exceptions
{
    public class ValueException : SolusException
    {
        public ValueException(string paramName, string message)
            : base(FormatMessage(paramName, message), null)
        {
            ParamName = paramName;
        }

        public ValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public string ParamName { get; }

        public static string FormatMessage(string paramName, string message)
        {
            if (paramName == null && message == null)
                return "The value was incorrect";
            if (paramName == null)
                return message;
            if (message == null)
                return $"The value was incorrect: {paramName}";
            return $"{message}: {paramName}";
        }

        public static ValueException Null(string paramName) =>
            new ValueException(paramName, "Value cannot be null");
    }
}
