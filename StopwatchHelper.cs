namespace SunamoStopwatch;

public class StopwatchHelper
{
    public Stopwatch sw = new Stopwatch();
    public const string takes = " takes ";


    public string lastMessage = null;
    public StringBuilder sbElapsed = new StringBuilder();

    public long ElapsedMS
    {
        get
        {
            return sw.ElapsedMilliseconds;
        }
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
        var r = sw.ElapsedMilliseconds + "ms";
        sw.Reset();
        return r;
    }
    #endregion

    #region StopAnd*
    /// <summary>
    /// Write ElapsedMilliseconds to debug, TSL. For more return long
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="p"></param>
    /// <param name="parametry"></param>
    /// <returns></returns>
    public long StopAndPrintElapsed(string operation, string p, params string[] parametry)
    {
        var l = sw.ElapsedMilliseconds;
        sw.Reset();
        lastMessage = string.Format(operation + takes + l + "ms" + p, parametry);
        ThisApp.Info( lastMessage);
#if DEBUG
        //DebugLogger.Instance.WriteLine(lastMessage);
#endif
        return l;
    }

    /// <summary>
    /// Write ElapsedMilliseconds
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public long StopAndPrintElapsed(string operation)
    {
        return StopAndPrintElapsed(operation, string.Empty);
    }
    #endregion



    public void SaveElapsed(string v)
    {
        var l = sw.ElapsedMilliseconds;
        sw.Reset();
        var m = v + takes + l + "ms";
        sbElapsed.AppendLine(m);
    }
}
