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
        var l = sw.sw.ElapsedMilliseconds;
        sw.Reset();
        var m = v + StopwatchHelper.takes + l + "ms";
        sbElapsed.AppendLine(m);
    }

    public static string CalculateAverageOfTakes(string li, Func<List<int>, string> nhAverage)
    {
        var l = SHGetLines.GetLines(li);

        var d = new Dictionary<string, List<int>>();

        foreach (var item in l)
            if (item.Contains(takes))
            {
                var d2 = SHSplit.SplitMore(item, takes);
                var tp = d2[1].Replace("ms", string.Empty);

                DictionaryHelper.AddOrCreate(d, d2[0], int.Parse(tp));
            }

        var sb = new StringBuilder();
        foreach (var item in d) sb.AppendLine(item.Key + " " + nhAverage(item.Value) + "ms");

        return sb.ToString();
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
        var l = sw.sw.ElapsedMilliseconds;
        sw.sw.Reset();
        return l;
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