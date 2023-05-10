using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;

namespace GarageGroup.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static bool IsMsteamsChannel([NotNullWhen(true)] this ITurnContext? turnContext)
        =>
        string.Equals(turnContext?.Activity?.ChannelId, Channels.Msteams, StringComparison.InvariantCultureIgnoreCase);

    public static bool IsNotMsteamsChannel([NotNullWhen(false)] this ITurnContext? turnContext)
        =>
        string.Equals(turnContext?.Activity?.ChannelId, Channels.Msteams, StringComparison.InvariantCultureIgnoreCase) is false;
}