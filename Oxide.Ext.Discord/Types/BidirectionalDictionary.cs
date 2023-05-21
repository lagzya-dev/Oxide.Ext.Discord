using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Oxide.Ext.Discord.Types
{
    public class BidirectionalDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _keyToValue = new Dictionary<TKey, TValue>();
        private readonly Dictionary<TValue, TKey> _valueToKey = new Dictionary<TValue, TKey>();

        public int Count => _keyToValue.Count;
        
        public bool IsReadOnly => false;

        public bool ContainsKey(TKey key) => _keyToValue.ContainsKey(key);

        public bool ContainsKey(TValue key) => _valueToKey.ContainsKey(key);

        public bool TryGetValue(TKey key, out TValue value) => _keyToValue.TryGetValue(key, out value);

        public bool TryGetValue(TValue key, out TKey value) => _valueToKey.TryGetValue(key, out value);

        public TValue this[TKey key]
        {
            get => _keyToValue[key];
            set
            {
                _keyToValue[key] = value;
                _valueToKey[value] = key;
            }
        }
        
        public TKey this[TValue key]
        {
            get => _valueToKey[key];
            set
            {
                _valueToKey[key] = value;
                _keyToValue[value] = key;
            }
        }

        public void Add(TKey key, TValue value)
        {
            _valueToKey.Add(value, key);
            _keyToValue.Add(key, value);
        }
        
        public void Add(TValue key, TKey value)
        {
            _keyToValue.Add(value, key);
            _valueToKey.Add(key, value);
        }

        public void AddKey(TKey key, TValue value)
        {
            _keyToValue[key] = value;
        }
        
        public void AddValue(TValue key, TKey value)
        {
            _valueToKey[key] = value;
        }

        public bool Remove(TKey key)
        {
            if (!_keyToValue.TryGetValue(key, out TValue valueKey))
            {
                return false;
            }
            
            _keyToValue.Remove(key);
            if (_valueToKey.TryGetValue(valueKey, out TKey _))
            {
                _valueToKey.Remove(valueKey);
            }

            return true;
        }
        
        public bool Remove(TValue key)
        {
            if (!_valueToKey.TryGetValue(key, out TKey valueKey))
            {
                return false;
            }
            
            _valueToKey.Remove(key);
            if (_keyToValue.TryGetValue(valueKey, out TValue _))
            {
                _keyToValue.Remove(valueKey);
            }

            return true;
        }

        public void Clear()
        {
            _keyToValue.Clear();
            _valueToKey.Clear();
        }

        public ReadOnlyDictionary<TKey, TValue> AsReadOnlyKeyToValue() => new ReadOnlyDictionary<TKey, TValue>(_keyToValue);
        public ReadOnlyDictionary<TValue, TKey> AsReadOnlyValueToKey() => new ReadOnlyDictionary<TValue, TKey>(_valueToKey);

        public ICollection<TKey> AsKeyCollection() => _keyToValue.Keys;
        public ICollection<TValue> AsValueCollection() => _valueToKey.Keys;
    }
}