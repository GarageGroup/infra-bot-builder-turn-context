using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;

namespace GarageGroup.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static bool IsEmulatorChannel([NotNullWhen(true)] this ITurnContext? turnContext)
        =>
        string.Equals(turnContext?.Activity?.ChannelId, Channels.Emulator, StringComparison.InvariantCultureIgnoreCase);

    public static bool IsNotEmulatorChannel([NotNullWhen(false)] this ITurnContext? turnContext)
        =>
        string.Equals(turnContext?.Activity?.ChannelId, Channels.Emulator, StringComparison.InvariantCultureIgnoreCase) is false;
}