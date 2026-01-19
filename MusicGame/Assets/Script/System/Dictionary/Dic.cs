using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Dictionary
{
    public class Dic<Tkey, Tvalue>
    {
        Dictionary<Tkey, Tvalue> dictionary;
        public Dic()
        {
            dictionary = new();
        }
        public Dic(Dictionary<Tkey, Tvalue> dictionary)
        {
            this.dictionary = dictionary;
        }

        public Tvalue GetValue(Tkey key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary.GetValueOrDefault(key);
            }
            else
            {
                Debug.LogError($"Error! key {key} is not found");
                return default;
            }
        }
        public void Add(Tkey key, Tvalue value)
        {
            dictionary.Add(key, value);
        }
        public bool Contains(Tkey key)
        {
            return (dictionary.ContainsKey(key));
        }
        public void TryRemove(Tkey key)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }
        }
    }
}
