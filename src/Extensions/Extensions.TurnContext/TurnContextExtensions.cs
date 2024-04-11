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
        RegexReplacement =
        [
            new(CreateFirstRegexReplacement(), string.Empty),
            new(CreateSecondRegexReplacement(), "\u2063\n\r\n\r\u2063"),
            new(CreateThirdRegexReplacement(), string.Empty)
        ];

        RegexReplacementWithDefaultStyle = RegexReplacement.Union(
        [
            new(CreateReplacementWithDefaultStyleRegex(), @"\\\$1")
        ]).ToArray();

        RegexReplacementWithSpecificStyle = RegexReplacement.Union(
        [
            new(CreateReplacementWithSpecificStyleRegex(), @"\$1")
        ]).ToArray();
    }

    [GeneratedRegex(@"[^a-zA-Zа-яА-ЯёЁ0-9\.\-,\?!\s:;()\\n\\r\\\""']+", RegexOptions.CultureInvariant)]
    private static partial Regex CreateFirstRegexReplacement();

    [GeneratedRegex("(?<!\\r)(\\n)(?!\\r)", RegexOptions.CultureInvariant)]
    private static partial Regex CreateSecondRegexReplacement();

    [GeneratedRegex(@"\\(?!n|r|"")", RegexOptions.CultureInvariant)]
    private static partial Regex CreateThirdRegexReplacement();

    [GeneratedRegex(@"(\-|\!|\(|\)|\.)", RegexOptions.CultureInvariant)]
    private static partial Regex CreateReplacementWithDefaultStyleRegex();

    [GeneratedRegex(@"(\-|\!|\(|\)|\.)", RegexOptions.CultureInvariant)]
    private static partial Regex CreateReplacementWithSpecificStyleRegex();
}