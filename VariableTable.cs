using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class VariableTable : IDictionary<string, Expression>
    {
        Dictionary<string, Expression> _valuesByName = new Dictionary<string, Expression>();

        #region IDictionary<string,Expression> Members

        public void Add(string key, Expression value)
        {
            _valuesByName.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _valuesByName.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return _valuesByName.Keys; }
        }

        public bool Remove(string key)
        {
            if (ContainsKey(key))
            {
                return _valuesByName.Remove(key);
            }

            return false;
        }

        public bool TryGetValue(string key, out Expression value)
        {
            return _valuesByName.TryGetValue(key, out value);
        }

        public ICollection<Expression> Values
        {
            get { return _valuesByName.Values; }
        }

        public Expression this[string key]
        {
            get
            {
                return _valuesByName[key];
            }
            set { }
        }

        #endregion

        #region ICollection<KeyValuePair<string,Expression>> Members

        public void Add(KeyValuePair<string, Expression> item)
        {
            Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<string, Expression> item)
        {
            return (_valuesByName as IDictionary<string, Expression>).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, Expression>[] array, int arrayIndex)
        {
            (_valuesByName as IDictionary<string, Expression>).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, Expression> item)
        {
            return Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,Variable>> Members

        public IEnumerator<KeyValuePair<string, Expression>> GetEnumerator()
        {
            return _valuesByName.GetEnumerator();
        }

        #endregion

        #region ICollection implementation

        public void Clear()
        {
            _valuesByName.Clear();
        }

        public int Count
        {
            get
            {
                return _valuesByName.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region IEnumerable implementation

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        //public Variable CreateTempVariable()
        //{
        //    int x = 0;
        //    while (ContainsKey("+temp_" + x.ToString("8X")))
        //    {
        //        x++;
        //    }

        //    Variable var = new Variable("+temp_" + x.ToString("8X"));
        //    Add(var);
        //    return var;
        //}
    }
}
