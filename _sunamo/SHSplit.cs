namespace SunamoStopwatch._sunamo;
internal class SHSplit
{
    internal static List<string> Split(string item, params string[] space)
    {
        return item.Split(space, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
