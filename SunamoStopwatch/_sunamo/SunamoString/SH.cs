namespace SunamoStopwatch._sunamo.SunamoString;

/// <summary>
/// Provides string helper delegates for line-based text operations.
/// </summary>
internal class SH
{
    /// <summary>
    /// Delegate for splitting text into individual lines.
    /// </summary>
    internal static Func<string, List<string>>? GetLines { get; set; }
}
