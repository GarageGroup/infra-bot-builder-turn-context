using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace GarageGroup.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static Optional<string> RecognizeCommandOrAbsent(this ITurnContext? turnContext, params string[] commandNames)
        =>
        InnerRecognizeCommandOrAbsent(
            turnContext, commandNames ?? Array.Empty<string>());

    private static Optional<string> InnerRecognizeCommandOrAbsent(this ITurnContext? turnContext, IReadOnlyCollection<string> commandNames)
    {
        return turnContext.InnerParseCommandOrAbsent().Filter(IsCommandMatch);

        bool IsCommandMatch(string text)
            =>
            commandNames.Any() is false || commandNames.Contains(text, StringComparer.InvariantCultureIgnoreCase);
    }

    private static Optional<string> InnerParseCommandOrAbsent(this ITurnContext? turnContext)
    {
        if (turnContext is null || turnContext.IsNotMessageType())
        {
            return default;
        }

        var text = turnContext.Activity.RemoveRecipientMention();
        if(string.IsNullOrWhiteSpace(text))
        {
            return default;
        }

        text = text.TrimEnd();
        if (turnContext.IsTelegramChannel())
        {
            if ((text.Length > 1 is false) || (text.StartsWith('/') is false))
            {
                return default;
            }

            text = text[1..];
        }

        return Optional.Present(text);
    }
}