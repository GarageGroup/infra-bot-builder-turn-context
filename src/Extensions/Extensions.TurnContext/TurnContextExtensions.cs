using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GarageGroup.Infra.Bot.Builder;

public static partial class TurnContextExtensions
{
    private static readonly IReadOnlyCollection<KeyValuePair<Regex, string>> RegexReplacement;

    private static readonly IReadOnlyCollection<KeyValuePair<Regex, string>> RegexReplacementWithDefaultStyle;

    private static readonly IReadOnlyCollection<KeyValuePair<Regex, string>> RegexReplacementWithSpecificStyle;

    private const string BoldStyleSign = "**";

    private const string ItalicStyleSign = "*";

    static TurnContextExtensions()
    {
        RegexReplacement = new KeyValuePair<Regex, string>[]
        {
            new(new(@"[^a-zA-Zа-яА-ЯёЁ0-9\.\-,\?!\s:;()\\n\\r\\\""']+", RegexOptions.CultureInvariant), string.Empty),
            new(new("(?<!\\r)(\\n)(?!\\r)", RegexOptions.CultureInvariant), "\u2063\n\r\n\r\u2063"),
            new(new(@"\\(?!n|r|"")", RegexOptions.CultureInvariant), string.Empty)
        };

        RegexReplacementWithDefaultStyle = RegexReplacement.Union(new KeyValuePair<Regex, string>[]
        {
            new(new(@"(\-|\!|\(|\)|\.)", RegexOptions.CultureInvariant), @"\\\$1")
        }).ToArray();

        RegexReplacementWithSpecificStyle = RegexReplacement.Union(new KeyValuePair<Regex, string>[]
        {
            new(new(@"(\-|\!|\(|\)|\.)", RegexOptions.CultureInvariant), @"\$1")
        }).ToArray();
    }
}