namespace SunamoStopwatch._sunamo.SunamoDictionary;

internal class DictionaryHelper
{
    #region AddOrCreate

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

    internal static void AddOrCreate<TKey, TValue>(IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value,
        bool isPreventingDuplicates = false, Dictionary<TKey, List<string>>? stringDictionary = null)
        where TKey : notnull
    {
        AddOrCreate<TKey, TValue, object>(dictionary, key, value, isPreventingDuplicates, stringDictionary);
    }

    #endregion
}
