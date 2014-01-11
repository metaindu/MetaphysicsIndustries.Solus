using System;
using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus
{
    public class VariableToArgumentNumberMapper
    {
        readonly Dictionary<string, byte> _dictionary = new Dictionary<string, byte>();

        public byte this [ string name ]
        {
            get
            {
                if (!_dictionary.ContainsKey(name))
                {
                    _dictionary.Add(name, (byte)_dictionary.Count);
                }

                return _dictionary[name];
            }
        }
    }
}

