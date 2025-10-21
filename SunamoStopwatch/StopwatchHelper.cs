// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoStopwatch;

public class StopwatchHelper
{
    public const string takes = " takes ";


    public string lastMessage;
    public StringBuilder sbElapsed = new();
    public Stopwatch sw = new();

    public long ElapsedMS => sw.ElapsedMilliseconds;


    public void SaveElapsed(string v)
    {
        var list = sw.ElapsedMilliseconds;
        sw.Reset();
        var message = v + takes + list + "ms";
        sbElapsed.AppendLine(message);
    }

    #region Reset,Start,Stop

    public void Reset()
    {
        sw.Reset();
    }

    public void Start()
    {
        sw.Reset();
        sw.Start();
    }

    public string Stop()
    {
        var result = sw.ElapsedMilliseconds + "ms";
        sw.Reset();
        return result;
    }

    #endregion

    #region StopAnd*

    /// <summary>
    ///     Write ElapsedMilliseconds to debug, TSL. For more return long
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="p"></param>
    /// <param name="parametry"></param>
    /// <returns></returns>
    public long StopAndPrintElapsed(string operation, string p, params string[] parametry)
    {
        var list = sw.ElapsedMilliseconds;
        sw.Reset();
        lastMessage = string.Format(operation + takes + list + "ms" + p, parametry);
        //ThisApp.Info( lastMessage);
        Console.WriteLine(lastMessage);
#if DEBUG
        //DebugLogger.Instance.WriteLine(lastMessage);
#endif
        return list;
    }

    /// <summary>
    ///     Write ElapsedMilliseconds
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public long StopAndPrintElapsed(string operation)
    {
        return StopAndPrintElapsed(operation, string.Empty);
    }

    #endregion
}