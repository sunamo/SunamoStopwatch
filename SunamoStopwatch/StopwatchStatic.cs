namespace SunamoStopwatch;

/// <summary>
/// Represents the stopwatch static type.
/// </summary>
public static class StopwatchStatic
{
    /// <summary>
    /// The takes constant.
    /// </summary>
    public const string Takes = " takes ";

    /// <summary>
    /// Gets or sets the elapsed string builder.
    /// </summary>
    public static StringBuilder ElapsedStringBuilder { get; set; } = new();

    private static readonly StopwatchHelper stopwatchHelper = new();

    /// <summary>
    /// Gets the elapsed milliseconds.
    /// </summary>
    public static long ElapsedMilliseconds => stopwatchHelper.ElapsedMilliseconds;

    /// <summary>
    /// Gets the last message.
    /// </summary>
    public static string? LastMessage => stopwatchHelper.LastMessage;

    /// <summary>
    /// Print elapsed and continue.
    /// </summary>
    /// <returns>The print elapsed and continue result.</returns>
    public static void PrintElapsedAndContinue(string operationName)
    {
        StopAndPrintElapsed(operationName);
        Start();
    }

    /// <summary>
    /// Save elapsed.
    /// </summary>
    /// <param name="operationName">The operation name.</param>
    /// <returns>The save elapsed result.</returns>
    public static void SaveElapsed(string operationName)
    {
        var elapsedMilliseconds = stopwatchHelper.Stopwatch.ElapsedMilliseconds;
        stopwatchHelper.Reset();
        var message = operationName + StopwatchHelper.Takes + elapsedMilliseconds + "ms";
        ElapsedStringBuilder.AppendLine(message);
    }

    /// <summary>
    /// Initializes a new instance of the calculate average of takes class.
    /// </summary>
    /// <param name="operationName">The operation name.</param>
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

    /// <summary>
    /// Initializes a new instance of the start class.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="parts[0]">The parts 0.</param>
    /// <param name="int.Parse(millisecondsText)">The int parse milliseconds text.</param>
    public static void Start()
    {
        stopwatchHelper.Start();
    }

    /// <summary>
    /// Initializes a new instance of the reset class.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    public static void Reset()
    {
        stopwatchHelper.Reset();
    }

    #endregion

    #region StopAnd*

    /// <summary>
    /// Initializes a new instance of the stop and elapsed milliseconds class.
    /// </summary>
    public static long StopAndElapsedMilliseconds()
    {
        var elapsedMilliseconds = stopwatchHelper.Stopwatch.ElapsedMilliseconds;
        stopwatchHelper.Stopwatch.Reset();
        return elapsedMilliseconds;
    }

    /// <summary>
    /// Stop and print elapsed.
    /// </summary>
    /// <returns>The stop and print elapsed result.</returns>
    public static long StopAndPrintElapsed(string operationName)
    {
        return stopwatchHelper.StopAndPrintElapsed(operationName);
    }

    /// <summary>
    /// Stop and print elapsed.
    /// </summary>
    /// <returns>The stop and print elapsed result.</returns>
    public static long StopAndPrintElapsed(string operationName, string formatSuffix, params string[] formatArguments)
    {
        return stopwatchHelper.StopAndPrintElapsed(operationName, formatSuffix, formatArguments);
    }

    #endregion
}
