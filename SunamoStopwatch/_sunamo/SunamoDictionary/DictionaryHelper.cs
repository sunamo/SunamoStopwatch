namespace SunamoStopwatch._sunamo.SunamoDictionary;

/// <summary>
/// Provides helper methods for dictionary operations.
/// </summary>
internal class DictionaryHelper
{
    #region AddOrCreate

    /// <summary>
    /// Adds a value to the list associated with the given key. Creates a new list if the key does not exist.
    /// Supports duplicate prevention and optional string-based comparison via a parallel dictionary.
    /// When the key implements IList and TCollectionType is not Object, keys are compared using SequenceEqual.
    /// </summary>
    /// <typeparam name="TKey">Type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">Type of the values stored in the lists.</typeparam>
    /// <typeparam name="TCollectionType">Element type used for sequence comparison when the key is an IList.</typeparam>
    /// <param name="dictionary">Target dictionary to add the value to.</param>
    /// <param name="key">Key under which the value is stored.</param>
    /// <param name="value">Value to add to the list.</param>
    /// <param name="isPreventingDuplicates">When true, prevents adding duplicate values to the list.</param>
    /// <param name="stringDictionary">Optional parallel dictionary for string-based duplicate comparison.</param>
    internal static void AddOrCreate<TKey, TValue, TCollectionType>(IDictionary<TKey, List<TValue>> dictionary,
        TKey key, TValue value,
        bool isPreventingDuplicates = false, Dictionary<TKey, List<string>>? stringDictionary = null)
        where TKey : notnull
    {
        var isComparingWithStrings = false;
        if (stringDictionary != null) isComparingWithStrings = true;

        if (key is IList && typeof(TCollectionType) != typeof(Object))
        {
            var keyAsList = key as IList<TCollectionType>;
            var isKeyFound = false;
            foreach (var item in dictionary)
            {
                var existingKeyAsList = item.Key as IList<TCollectionType>;
                if (existingKeyAsList!.SequenceEqual(keyAsList!)) isKeyFound = true;
            }

            if (isKeyFound)
            {
                foreach (var item in dictionary)
                {
                    var existingKeyAsList = item.Key as IList<TCollectionType>;
                    if (existingKeyAsList!.SequenceEqual(keyAsList!))
                    {
                        if (isPreventingDuplicates)
                            if (item.Value.Contains(value))
                                return;
                        item.Value.Add(value);
                    }
                }
            }
            else
            {
                List<TValue> valueList = new();
                valueList.Add(value);
                dictionary.Add(key, valueList);

                if (isComparingWithStrings)
                {
                    List<string> stringValueList = new();
                    stringValueList.Add(value!.ToString()!);
                    stringDictionary!.Add(key, stringValueList);
                }
            }
        }
        else
        {
            var shouldAdd = true;
            lock (dictionary)
            {
                if (dictionary.ContainsKey(key))
                {
                    if (isPreventingDuplicates)
                    {
                        if (dictionary[key].Contains(value))
                            shouldAdd = false;
                        else if (isComparingWithStrings)
                            if (stringDictionary![key].Contains(value!.ToString()!))
                                shouldAdd = false;
                    }

                    if (shouldAdd)
                    {
                        var existingValues = dictionary[key];

                        if (existingValues != null) existingValues.Add(value);

                        if (isComparingWithStrings)
                        {
                            var existingStringValues = stringDictionary![key];

                            if (existingStringValues != null) existingStringValues.Add(value!.ToString()!);
                        }
                    }
                }
                else
                {
                    if (!dictionary.ContainsKey(key))
                    {
                        List<TValue> valueList = new();
                        valueList.Add(value);
                        dictionary.Add(key, valueList);
                    }
                    else
                    {
                        dictionary[key].Add(value);
                    }

                    if (isComparingWithStrings)
                    {
                        if (!stringDictionary!.ContainsKey(key))
                        {
                            List<string> stringValueList = new();
                            stringValueList.Add(value!.ToString()!);
                            stringDictionary.Add(key, stringValueList);
                        }
                        else
                        {
                            stringDictionary[key].Add(value!.ToString()!);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Adds a value to the list associated with the given key. Creates a new list if the key does not exist.
    /// </summary>
    /// <typeparam name="TKey">Type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">Type of the values stored in the lists.</typeparam>
    /// <param name="dictionary">Target dictionary to add the value to.</param>
    /// <param name="key">Key under which the value is stored.</param>
    /// <param name="value">Value to add to the list.</param>
    /// <param name="isPreventingDuplicates">When true, prevents adding duplicate values to the list.</param>
    /// <param name="stringDictionary">Optional parallel dictionary for string-based duplicate comparison.</param>
    internal static void AddOrCreate<TKey, TValue>(IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value,
        bool isPreventingDuplicates = false, Dictionary<TKey, List<string>>? stringDictionary = null)
        where TKey : notnull
    {
        AddOrCreate<TKey, TValue, object>(dictionary, key, value, isPreventingDuplicates, stringDictionary);
    }

    #endregion
}
