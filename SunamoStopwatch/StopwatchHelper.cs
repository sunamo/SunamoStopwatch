namespace SunamoStopwatch;

public class StopwatchHelper
{
    public const string Takes = " takes ";

    public string? LastMessage { get; set; }

    public StringBuilder ElapsedStringBuilder { get; set; } = new();

    public Stopwatch Stopwatch { get; set; } = new();

    public long ElapsedMilliseconds => Stopwatch.ElapsedMilliseconds;

    public void SaveElapsed(string operationName)
    {
        var elapsedMilliseconds = Stopwatch.ElapsedMilliseconds;
        Stopwatch.Reset();
        var message = operationName + Takes + elapsedMilliseconds + "ms";
        ElapsedStringBuilder.AppendLine(message);
    }

    #region Reset,Start,Stop

    public void Reset()
    {
        Stopwatch.Reset();
    }

    public void Start()
    {
        Stopwatch.Reset();
        Stopwatch.Start();
    }

    public string Stop()
    {
        var result = Stopwatch.ElapsedMilliseconds + "ms";
        Stopwatch.Reset();
        return result;
    }

    #endregion

    #region StopAnd*

    public long StopAndPrintElapsed(string operationName, string formatSuffix, params string[] formatArguments)
    {
        var elapsedMilliseconds = Stopwatch.ElapsedMilliseconds;
        Stopwatch.Reset();
        LastMessage = string.Format(operationName + Takes + elapsedMilliseconds + "ms" + formatSuffix, formatArguments);
        Console.WriteLine(LastMessage);
        return elapsedMilliseconds;
    }

    public long StopAndPrintElapsed(string operationName)
    {
        return StopAndPrintElapsed(operationName, string.Empty);
    }

    #endregion
}
