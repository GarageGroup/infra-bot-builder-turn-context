using System;
using Microsoft.Bot.Builder;

namespace GarageGroup.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static Optional<Guid> GetCardActionValueOrAbsent(this ITurnContext? turnContext)
    {
        if (turnContext.IsNotMessageType())
        {
            return default;
        }

        var text = turnContext?.Activity?.Value is not null ? turnContext?.Activity?.Value.ToString() : turnContext?.Activity?.Text;
        return CardActionValueJson.DeserializeOrAbsent(text).Map(GetId);

        static Guid GetId(CardActionValueJson valueJson)
            =>
            valueJson.Id;
    }

    private static Optional<Guid> ParseCardActionValueOrAbsent(this ITurnContext? turnContext)
    {
        return CardActionValueJson.DeserializeOrAbsent(
            turnContext?.Activity?.Value is not null ? turnContext?.Activity?.Value.ToString() : turnContext?.Activity?.Text)
        .Map(
            valueJson => valueJson.Id);
    }
}