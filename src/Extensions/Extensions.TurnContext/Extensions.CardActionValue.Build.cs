using System;
using Microsoft.Bot.Builder;

namespace GGroupp.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static object BuildCardActionValue(this ITurnContext? turnContext, Guid valueId)
    {
        var valueJson = new CardActionValueJson(valueId);
        return turnContext.IsCardSupported() ? valueJson : valueJson.Serialize();
    }
}