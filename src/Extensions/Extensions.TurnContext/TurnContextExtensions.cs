using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GGroupp.Infra.Bot.Builder;

public static partial class TurnContextExtensions
{
    private static readonly IReadOnlyCollection<KeyValuePair<Regex, string>> RegexReplacement;

    static TurnContextExtensions()
        =>
        RegexReplacement = new KeyValuePair<Regex, string>[]
        {
            new(new(@"[^a-zA-Zа-яА-Я0-9\.,\-\?!\s:;()\\n\\r\\\""']+", RegexOptions.CultureInvariant), string.Empty),
            new(new("(?<!\\r)(\\n)(?!\\r)", RegexOptions.CultureInvariant), "\u2063\n\r\n\r\u2063"),
            new(new(@"\\(?!n|r|"")", RegexOptions.CultureInvariant), string.Empty)
        };
}