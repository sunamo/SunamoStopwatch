namespace SunamoStopwatch;

/// <summary>
/// Represents the stopwatch helper type.
/// </summary>
public class StopwatchHelper
{
    /// <summary>
    /// The takes constant.
    /// </summary>
    public const string Takes = " takes ";

    /// <summary>
    /// Gets or sets the last message.
    /// </summary>
    public string? LastMessage { get; set; }

    /// <summary>
    /// Gets or sets the elapsed string builder.
    /// </summary>
    public StringBuilder ElapsedStringBuilder { get; set; } = new();

    /// <summary>
    /// Gets or sets the stopwatch.
    /// </summary>
    public Stopwatch Stopwatch { get; set; } = new();

    /// <summary>
    /// Gets the elapsed milliseconds.
    /// </summary>
    public long ElapsedMilliseconds => Stopwatch.ElapsedMilliseconds;

    /// <summary>
    /// Save elapsed.
    /// </summary>
    /// <returns>The save elapsed result.</returns>
    public void SaveElapsed(string operationName)
    {
        var elapsedMilliseconds = Stopwatch.ElapsedMilliseconds;
        Stopwatch.Reset();
        var message = operationName + Takes + elapsedMilliseconds + "ms";
        ElapsedStringBuilder.AppendLine(message);
    }

    #region Reset,Start,Stop

    /// <summary>
    /// Reset.
    /// </summary>
    /// <param name="operationName">The operation name.</param>
    /// <returns>The reset result.</returns>
    public void Reset()
    {
        Stopwatch.Reset();
    }

    /// <summary>
    /// Start.
    /// </summary>
    /// <param name="operationName">The operation name.</param>
    public void Start()
    {
        Stopwatch.Reset();
        Stopwatch.Start();
    }

    /// <summary>
    /// Stop.
    /// </summary>
    /// <returns>The stop result.</returns>
    public string Stop()
    {
        var result = Stopwatch.ElapsedMilliseconds + "ms";
        Stopwatch.Reset();
        return result;
    }

    #endregion

    #region StopAnd*

    /// <summary>
    /// Initializes a new instance of the stop and print elapsed class.
    /// </summary>
    public long StopAndPrintElapsed(string operationName, string formatSuffix, params string[] formatArguments)
    {
        var elapsedMilliseconds = Stopwatch.ElapsedMilliseconds;
        Stopwatch.Reset();
        LastMessage = string.Format(operationName + Takes + elapsedMilliseconds + "ms" + formatSuffix, formatArguments);
        Console.WriteLine(LastMessage);
        return elapsedMilliseconds;
    }

    /// <summary>
    /// Stop and print elapsed.
    /// </summary>
    /// <param name="operationName">The operation name.</param>
    /// <param name="formatSuffix">The format suffix.</param>
    /// <param name="formatArguments">The format arguments.</param>
    /// <returns>The stop and print elapsed result.</returns>
    public long StopAndPrintElapsed(string operationName)
    {
        return StopAndPrintElapsed(operationName, string.Empty);
    }

    #endregion
}
