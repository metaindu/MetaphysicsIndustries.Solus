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

        public string[] GetVariableNamesInIndexOrder()
        {
            var names = new string[_dictionary.Count];

            foreach (var kvp in _dictionary)
            {
                names[kvp.Value] = kvp.Key;
            }

            return names;
        }

        public void Clear()
        {
            _dictionary.Clear();
        }
    }
}

