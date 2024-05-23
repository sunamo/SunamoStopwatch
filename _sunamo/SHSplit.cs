namespace SunamoStopwatch;
public class SHSplit
{
    public static List<string> Split(string item, params string[] space)
    {
        return item.Split(space, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
