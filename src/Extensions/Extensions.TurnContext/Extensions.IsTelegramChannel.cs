using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;

namespace GGroupp.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static bool IsTelegramChannel([NotNullWhen(true)] this ITurnContext? turnContext)
        =>
        string.Equals(turnContext?.Activity?.ChannelId, Channels.Telegram, StringComparison.InvariantCultureIgnoreCase);

    public static bool IsNotTelegramChannel([NotNullWhen(false)] this ITurnContext? turnContext)
        =>
        string.Equals(turnContext?.Activity?.ChannelId, Channels.Telegram, StringComparison.InvariantCultureIgnoreCase) is false;
}