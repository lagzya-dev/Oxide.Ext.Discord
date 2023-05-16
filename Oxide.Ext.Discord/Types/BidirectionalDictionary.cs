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

        public bool ContainsKey(TKey key)
        {
            return _keyToValue.ContainsKey(key);
        }
        
        public bool ContainsKey(TValue key)
        {
            return _valueToKey.ContainsKey(key);
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _keyToValue.TryGetValue(key, out value);
        }
        
        public bool TryGetValue(TValue key, out TKey value)
        {
            return _valueToKey.TryGetValue(key, out value);
        }
        
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

        public bool Remove(TKey key)
        {
            TValue value = _keyToValue[key];
            return _valueToKey.Remove(value) && _keyToValue.Remove(key);
        }
        
        public bool Remove(TValue key)
        {
            TKey valueKey = _valueToKey[key];
            return _valueToKey.Remove(key) && _keyToValue.Remove(valueKey);
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