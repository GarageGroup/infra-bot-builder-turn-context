using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace GGroupp.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static Task<ResourceResponse> SetTypingStatusAsync(this ITurnContext context, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<ResourceResponse>(cancellationToken);
        }

        var typingActivity = new Activity
        {
            Type = ActivityTypes.Typing
        };

        return context.SendActivityAsync(typingActivity, cancellationToken);
    }
}