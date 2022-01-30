using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace GGroupp.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static bool IsMessageType([NotNullWhen(true)] this ITurnContext? turnContext)
        =>
        string.Equals(turnContext?.Activity?.Type, ActivityTypes.Message, StringComparison.InvariantCultureIgnoreCase);

    public static bool IsNotMessageType([NotNullWhen(false)] this ITurnContext? turnContext)
        =>
        string.Equals(turnContext?.Activity?.Type, ActivityTypes.Message, StringComparison.InvariantCultureIgnoreCase) is false;
}