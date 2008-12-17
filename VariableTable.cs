using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class VariableTable : IDictionary<Variable, Expression>, IDictionary<string, Variable>, ICollection<Variable>
    {
        Dictionary<Variable, Expression> _currentValues = new Dictionary<Variable, Expression>();
        Set<Variable> _allVariables = new Set<Variable>();
        Dictionary<string, Variable> _variablesByName = new Dictionary<string, Variable>();

        #region ICollection<Variable> Members

        public void Add(Variable item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }

            if (!Contains(item))
            {
                if (ContainsKey(item.Name))
                {
                    throw new InvalidOperationException("A variable by that name already exists in the variable table.");
                }

                _allVariables.Add(item);
                _currentValues.Add(item, Literal.Zero);
                _variablesByName.Add(item.Name, item);

                item.OnValueChanged(this);
            }
        }

        public void Clear()
        {
            _allVariables.Clear();
            _currentValues.Clear();
            _variablesByName.Clear();
        }

        public bool Contains(Variable item)
        {
            return _allVariables.Contains(item);
        }

        public void CopyTo(Variable[] array, int arrayIndex)
        {
            _allVariables.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _allVariables.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Variable item)
        {
            if (Contains(item))
            {
                bool ret;

                ret = _allVariables.Remove(item);
                _currentValues.Remove(item);
                _variablesByName.Remove(item.Name);

                return ret;
            }

            return false;
        }

        #endregion

        #region IEnumerable<Variable> Members

        public IEnumerator<Variable> GetEnumerator()
        {
            return _allVariables.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IDictionary<string,Variable> Members

        public void Add(string key, Variable value)
        {
            Add(value);
        }

        public bool ContainsKey(string key)
        {
            return _variablesByName.ContainsKey(key);
        }

        ICollection<string> IDictionary<string, Variable>.Keys
        {
            get { return _variablesByName.Keys; }
        }

        public bool Remove(string key)
        {
            if (ContainsKey(key))
            {
                return Remove(this[key]);
            }

            return false;
        }

        public bool TryGetValue(string key, out Variable value)
        {
            return _variablesByName.TryGetValue(key, out value);
        }

        ICollection<Variable> IDictionary<string, Variable>.Values
        {
            get { return _variablesByName.Values; }
        }

        public Variable this[string key]
        {
            get
            {
                return _variablesByName[key];
            }
            set { }
        }

        #endregion

        #region ICollection<KeyValuePair<string,Variable>> Members

        public void Add(KeyValuePair<string, Variable> item)
        {
            Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<string, Variable> item)
        {
            return (_variablesByName as IDictionary<string, Variable>).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, Variable>[] array, int arrayIndex)
        {
            (_variablesByName as IDictionary<string, Variable>).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, Variable> item)
        {
            return Remove(item.Value);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,Variable>> Members

        IEnumerator<KeyValuePair<string, Variable>> IEnumerable<KeyValuePair<string, Variable>>.GetEnumerator()
        {
            return _variablesByName.GetEnumerator();
        }

        #endregion

        #region IDictionary<Variable,Expression> Members

        public void Add(Variable key, Expression value)
        {
            Add(key);
            this[key] = value;
        }

        public bool ContainsKey(Variable key)
        {
            return Contains(key);
        }

        public ICollection<Variable> Keys
        {
            get { return _currentValues.Keys; }
        }

        public bool TryGetValue(Variable key, out Expression value)
        {
            return _currentValues.TryGetValue(key, out value);
        }

        public ICollection<Expression> Values
        {
            get { return _currentValues.Values; }
        }

        public Expression this[Variable key]
        {
            get { return _currentValues[key]; }
            set
            {
                Add(key);
                _currentValues[key] = value;
                key.OnValueChanged(this);
            }
        }

        #endregion

        #region ICollection<KeyValuePair<Variable,Expression>> Members

        public void Add(KeyValuePair<Variable, Expression> item)
        {
            Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<Variable, Expression> item)
        {
            return (_currentValues as IDictionary<Variable, Expression>).Contains(item);
        }

        public void CopyTo(KeyValuePair<Variable, Expression>[] array, int arrayIndex)
        {
            (_currentValues as IDictionary<Variable, Expression>).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<Variable, Expression> item)
        {
            return Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<Variable,Expression>> Members

        IEnumerator<KeyValuePair<Variable, Expression>> IEnumerable<KeyValuePair<Variable, Expression>>.GetEnumerator()
        {
            return (_currentValues as IDictionary<Variable, Expression>).GetEnumerator();
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
