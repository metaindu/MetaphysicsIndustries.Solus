using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class VariableTable : IDictionary<Variable, double>, IDictionary<string, Variable>, ICollection<Variable>
    {
        Dictionary<Variable, double> _currentValues = new Dictionary<Variable, double>();
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
                _currentValues.Add(item, 0);
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

        public ICollection<string> Keys
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

        public ICollection<Variable> Values
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

        #region IDictionary<Variable,double> Members

        public void Add(Variable key, double value)
        {
            Add(key);
            this[key] = value;
        }

        public bool ContainsKey(Variable key)
        {
            return Contains(key);
        }

        ICollection<Variable> IDictionary<Variable, double>.Keys
        {
            get { return _currentValues.Keys; }
        }

        public bool TryGetValue(Variable key, out double value)
        {
            return _currentValues.TryGetValue(key, out value);
        }

        ICollection<double> IDictionary<Variable, double>.Values
        {
            get { return _currentValues.Values; }
        }

        public double this[Variable key]
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

        #region ICollection<KeyValuePair<Variable,double>> Members

        public void Add(KeyValuePair<Variable, double> item)
        {
            Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<Variable, double> item)
        {
            return (_currentValues as IDictionary<Variable, double>).Contains(item);
        }

        public void CopyTo(KeyValuePair<Variable, double>[] array, int arrayIndex)
        {
            (_currentValues as IDictionary<Variable, double>).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<Variable, double> item)
        {
            return Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<Variable,double>> Members

        IEnumerator<KeyValuePair<Variable, double>> IEnumerable<KeyValuePair<Variable, double>>.GetEnumerator()
        {
            return (_currentValues as IDictionary<Variable, double>).GetEnumerator();
        }

        #endregion
    }
}
