using System.Diagnostics.CodeAnalysis;
using Microsoft.Bot.Builder;

namespace GarageGroup.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static bool IsCardSupported([NotNullWhen(true)] this ITurnContext? turnContext)
        =>
        turnContext.IsMsteamsChannel() || turnContext.IsEmulatorChannel() || turnContext.IsWebchatChannel();

    public static bool IsNotCardSupported([NotNullWhen(false)] this ITurnContext? turnContext)
        =>
        turnContext.IsNotMsteamsChannel() && turnContext.IsNotEmulatorChannel() && turnContext.IsNotWebchatChannel();
}