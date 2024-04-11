using System;
using System.Text.RegularExpressions;
using Microsoft.Bot.Builder;

namespace GarageGroup.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static Optional<Match> RecognizeCommandPatternOrAbsent(this ITurnContext? turnContext, string pattern)
    {
        if (turnContext is null || string.IsNullOrWhiteSpace(pattern))
        {
            return default;
        }

        return turnContext.InnerParseCommandOrAbsent().FlatMap(RecognizeOrAbsent);

        Optional<Match> RecognizeOrAbsent(string text)
        {
            var commandMatch = Regex.Match(input: text, pattern: pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            return commandMatch.Success ? Optional.Present(commandMatch) : default;
        }
    }
}