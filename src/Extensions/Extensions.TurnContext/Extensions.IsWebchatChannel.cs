using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;

namespace GarageGroup.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static bool IsWebchatChannel([NotNullWhen(true)] this ITurnContext? turnContext)
        =>
        string.Equals(turnContext?.Activity?.ChannelId, Channels.Webchat, StringComparison.InvariantCultureIgnoreCase);

    public static bool IsNotWebchatChannel([NotNullWhen(false)] this ITurnContext? turnContext)
        =>
        string.Equals(turnContext?.Activity?.ChannelId, Channels.Webchat, StringComparison.InvariantCultureIgnoreCase) is false;
}