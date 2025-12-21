namespace SunamoStopwatch;

public static class StopwatchStatic
{
    public const string takes = " takes ";
    public static StringBuilder sbElapsed = new();
    private static readonly StopwatchHelper sw = new();

    public static long ElapsedMS => sw.ElapsedMS;

    public static string lastMessage => sw.lastMessage;

    /// <summary>
    ///     Call Start() Aganin
    /// </summary>
    /// <param name="notTranslateAbleString"></param>
    public static void PrintElapsedAndContinue(string notTranslateAbleString)
    {
        StopAndPrintElapsed(notTranslateAbleString);
        Start();
    }

    public static void SaveElapsed(string v)
    {
        var list = sw.sw.ElapsedMilliseconds;
        sw.Reset();
        var message = v + StopwatchHelper.takes + list + "ms";
        sbElapsed.AppendLine(message);
    }

    public static string CalculateAverageOfTakes(List<string> list, Func<List<int>, string> nhAverage)
    {
        var dictionary = new Dictionary<string, List<int>>();

        foreach (var item in list)
            if (item.Contains(takes))
            {
                var d2 = SHSplit.Split(item, takes);
                var tp = d2[1].Replace("ms", string.Empty);

                DictionaryHelper.AddOrCreate(dictionary, d2[0], int.Parse(tp));
            }

        var stringBuilder = new StringBuilder();
        foreach (var item in dictionary) stringBuilder.AppendLine(item.Key + " " + nhAverage(item.Value) + "ms");

        return stringBuilder.ToString();
    }

    #region Reset,Start,Stop

    public static void Start()
    {
        sw.Start();
    }

    public static void Reset()
    {
        sw.Reset();
    }

    #endregion

    #region StopAnd*

    public static long StopAndEllapsedMs()
    {
        var list = sw.sw.ElapsedMilliseconds;
        sw.sw.Reset();
        return list;
    }

    /// <summary>
    ///     Write ElapsedMilliseconds
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public static long StopAndPrintElapsed(string operation)
    {
        return sw.StopAndPrintElapsed(operation);
    }

    /// <summary>
    ///     Write ElapsedMilliseconds to debug, TSL. For more return long
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="p"></param>
    /// <param name="parametry"></param>
    /// <returns></returns>
    public static long StopAndPrintElapsed(string operation, string p, params string[] parametry)
    {
        return sw.StopAndPrintElapsed(operation, p, parametry);
    }

    #endregion
}