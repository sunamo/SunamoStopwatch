namespace SunamoStopwatch._sunamo.SunamoStringSplit;

/// <summary>
/// Provides string splitting utilities.
/// </summary>
internal class SHSplit
{
    /// <summary>
    /// Splits the text by the specified delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="delimiters">Delimiters to split by.</param>
    /// <returns>List of non-empty segments.</returns>
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
