using System;
using Microsoft.Bot.Builder;

namespace GarageGroup.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static Optional<Guid> GetCardActionValueOrAbsent(this ITurnContext? turnContext)
        =>
        turnContext.IsMessageType() ? turnContext.ParseCardActionValueOrAbsent() : default;

    private static Optional<Guid> ParseCardActionValueOrAbsent(this ITurnContext? turnContext)
        =>
        CardActionValueJson.DeserializeOrAbsent(
            turnContext?.Activity?.Value is not null ? turnContext?.Activity?.Value.ToString() : turnContext?.Activity?.Text)
        .Map(
            valueJson => valueJson.Id);
}