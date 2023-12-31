using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Management_System.DataStructures
{
    public class KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private const int InitialCapacity = 1;
        private const double LoadFactorThreshold = 0.75;

        private KeyValuePair<TKey, TValue>[] buckets;
        public int Count;

        public MyDictionary()
        {
            buckets = new KeyValuePair<TKey, TValue>[InitialCapacity];
        }

        private int GetBucketIndex(TKey key)
        {
            int hashCode = key.GetHashCode();
            return Math.Abs(hashCode % buckets.Length);
        }

        private void Resize()
        {
            int newCapacity = buckets.Length * 2;
            var newBuckets = new KeyValuePair<TKey, TValue>[newCapacity];

            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] != null)
                {
                    int newIndex = Math.Abs(buckets[i].Key.GetHashCode() % newCapacity);
                    while (newBuckets[newIndex] != null)
                    {
                        newIndex = (newIndex + 1) % newCapacity;
                    }
                    newBuckets[newIndex] = buckets[i];
                }
            }

            buckets = newBuckets;
        }

        public void Add(TKey key, TValue value)
        {
            if ((double)Count / buckets.Length > LoadFactorThreshold)
            {
                Resize();
            }

            int index = GetBucketIndex(key);

            if (buckets[index] == null)
            {
                buckets[index] = new KeyValuePair<TKey, TValue>(key, value);
                Count++;
                return;
            }

            if (buckets[index].Key != null && buckets[index].Key.Equals(key))
            {
                buckets[index] = new KeyValuePair<TKey, TValue>(key, value);
            }
            else
            {
                while (buckets[index] != null && !buckets[index].Key.Equals(key))
                {
                    index = (index + 1) % buckets.Length;

                    if (index == GetBucketIndex(key))
                    {
                        throw new InvalidOperationException("Hash table is full.");
                    }
                }

                if (buckets[index] == null)
                {
                    buckets[index] = new KeyValuePair<TKey, TValue>(key, value);
                    Count++;
                }
                else
                {
                    buckets[index] = new KeyValuePair<TKey, TValue>(key, value);
                }
            }
        }

        public bool HasKey(TKey key)
        {
            int index = GetBucketIndex(key);
            int startIndex = index;

            while (true)
            {
                if (buckets[index] != null && buckets[index].Key.Equals(key))
                    return true;

                index = (index + 1) % buckets.Length;

                if (index == startIndex)
                    break;
            }

            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                return buckets[GetBucketIndex(key)].Value;
            }
            set
            {
                buckets[GetBucketIndex(key)].Value = value;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var pair in buckets)
            {
                if (pair != null)
                {
                    yield return pair;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
