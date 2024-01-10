namespace SunamoStopwatch._sunamo;

internal class NH
{
    /// <summary>
    /// Must be object to use in EqualityComparer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private static object ReturnZero<T>()
    {
        var t = typeof(T);
        if (t == Types.tDouble)
        {
            return NumConsts.zeroDouble;
        }
        else if (t == Types.tInt)
        {
            return NumConsts.zeroInt;
        }
        else if (t == Types.tFloat)
        {
            return NumConsts.zeroFloat;
        }
        ThrowEx.NotImplementedCase(t.FullName);
        return null;
    }

    internal static T Sum<T>(List<T> list)
    {
        dynamic sum = 0;
        foreach (var item in list)
        {
            sum += item;
        }
        return sum;
    }

    internal static T Average<T>(List<T> list)
    {
        return Average<T>(Sum<T>(list), list.Count);
    }

    internal static T Average<T>(dynamic gridWidth, dynamic columnsCount)
    {
        if (EqualityComparer<T>.Default.Equals(columnsCount, (T)NH.ReturnZero<T>()))
        {
            return (T)NH.ReturnZero<T>();
        }

        if (EqualityComparer<T>.Default.Equals(gridWidth, (T)NH.ReturnZero<T>()))
        {
            return (T)NH.ReturnZero<T>();
        }

        dynamic result = gridWidth / columnsCount;
        return result;
    }
}
