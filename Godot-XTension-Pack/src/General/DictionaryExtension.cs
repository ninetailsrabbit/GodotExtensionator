namespace Godot_XTension_Pack {
    public static class DictionaryExtension {

        /// <summary>
        /// Adds all key-value pairs from another dictionary to the current dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionaries.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionaries.</typeparam>
        /// <param name="dictionary">The target dictionary to which elements will be added.</param>
        /// <param name="range">The source dictionary from which elements will be copied.</param>
        /// <remarks>
        /// This extension method iterates through each key-value pair in the source dictionary (`range`)
        /// and adds them individually to the target dictionary (`dictionary`) using the `Add` method.
        /// Note that duplicate keys in the source dictionary will cause a `DuplicateKeyException` to be thrown
        /// unless the target dictionary's behavior is specifically designed to handle duplicates.
        /// </remarks>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> range) {
            foreach (var item in range)
                dictionary.TryAdd(item.Key, item.Value);
        }

        /// <summary>
        /// Adds a key-value pair to a dictionary or updates an existing value associated with the key.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to be modified.</param>
        /// <param name="key">The key to be added or updated.</param>
        /// <param name="value">The value to be associated with the key.</param>
        /// <returns>The current value associated with the key after the operation (updated value if the key existed, or the newly added value).</returns>
        /// <remarks>
        /// This extension method provides a concise way to manage key-value pairs in a dictionary.
        /// It leverages `ContainsKey` to check for existing keys and performs either an update or an `Add` operation accordingly.
        /// The method returns the final value associated with the key, making it convenient for further processing.
        /// </remarks>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(new KeyValuePair<TKey, TValue>(key, value));

            return dictionary[key];
        }

        /// <summary>
        /// Determines whether the dictionary contains at least one of the specified keys.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary (must not be null).</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to be checked.</param>
        /// <param name="keys">A variable number of keys to search for in the dictionary.</param>
        /// <returns>True if the dictionary contains at least one of the provided keys, False otherwise.</returns>
        public static bool ContainsAnyKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, params TKey[] keys) where TKey : notnull {
            return keys.Any(dictionary.ContainsKey);
        }

        /// <summary>
        /// Determines whether the dictionary contains all of the specified keys.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary (must not be null).</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to be checked.</param>
        /// <param name="keys">A variable number of keys to search for in the dictionary.</param>
        /// <returns>True if the dictionary contains all of the provided keys, False otherwise.</returns>
        /// <remarks>
        /// This extension method employs the `All` method on the `keys` array to check if `ContainsKey` returns `true` for every key.
        /// It provides a convenient way to ensure all necessary keys are present in the dictionary.
        /// </remarks>
        public static bool ContainsAllKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, params TKey[] keys) where TKey : notnull {
            return keys.All(dictionary.ContainsKey);
        }
    }
}
