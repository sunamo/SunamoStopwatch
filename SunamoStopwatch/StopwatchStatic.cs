namespace SunamoStopwatch;

public static class StopwatchStatic
{
    public const string Takes = " takes ";

    public static StringBuilder ElapsedStringBuilder { get; set; } = new();

    private static readonly StopwatchHelper stopwatchHelper = new();

    public static long ElapsedMilliseconds => stopwatchHelper.ElapsedMilliseconds;

    public static string? LastMessage => stopwatchHelper.LastMessage;

    public static void PrintElapsedAndContinue(string operationName)
    {
        StopAndPrintElapsed(operationName);
        Start();
    }

    public static void SaveElapsed(string operationName)
    {
        var elapsedMilliseconds = stopwatchHelper.Stopwatch.ElapsedMilliseconds;
        stopwatchHelper.Reset();
        var message = operationName + StopwatchHelper.Takes + elapsedMilliseconds + "ms";
        ElapsedStringBuilder.AppendLine(message);
    }

    public static string CalculateAverageOfTakes(List<string> list, Func<List<int>, string> averageCalculator)
    {
        var dictionary = new Dictionary<string, List<int>>();

        foreach (var item in list)
            if (item.Contains(Takes))
            {
                var parts = SHSplit.Split(item, Takes);
                var millisecondsText = parts[1].Replace("ms", string.Empty);

                DictionaryHelper.AddOrCreate(dictionary, parts[0], int.Parse(millisecondsText));
            }

        var stringBuilder = new StringBuilder();
        foreach (var item in dictionary) stringBuilder.AppendLine(item.Key + " " + averageCalculator(item.Value) + "ms");

        return stringBuilder.ToString();
    }

    #region Reset,Start,Stop

    public static void Start()
    {
        stopwatchHelper.Start();
    }

    public static void Reset()
    {
        stopwatchHelper.Reset();
    }

    #endregion

    #region StopAnd*

    public static long StopAndElapsedMilliseconds()
    {
        var elapsedMilliseconds = stopwatchHelper.Stopwatch.ElapsedMilliseconds;
        stopwatchHelper.Stopwatch.Reset();
        return elapsedMilliseconds;
    }

    public static long StopAndPrintElapsed(string operationName)
    {
        return stopwatchHelper.StopAndPrintElapsed(operationName);
    }

    public static long StopAndPrintElapsed(string operationName, string formatSuffix, params string[] formatArguments)
    {
        return stopwatchHelper.StopAndPrintElapsed(operationName, formatSuffix, formatArguments);
    }

    #endregion
}
