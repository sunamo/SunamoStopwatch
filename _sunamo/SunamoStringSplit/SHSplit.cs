namespace SunamoStopwatch._sunamo.SunamoStringSplit;
internal class SHSplit
{
    internal static List<string> SplitMore(string item, params string[] space)
    {
        return item.Split(space, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
