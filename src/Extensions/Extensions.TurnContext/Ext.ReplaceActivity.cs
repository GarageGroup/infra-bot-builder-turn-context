using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;

namespace GarageGroup.Infra.Bot.Builder;

partial class TurnContextExtensions
{
    public static Task<ResourceResponse> ReplaceActivityAsync(
        this ITurnContext context, string activityId, IActivity activity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (string.IsNullOrWhiteSpace(activityId))
        {
            throw new ArgumentException("ActivityId must be specifed", nameof(activityId));
        }

        ArgumentNullException.ThrowIfNull(activity);

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<ResourceResponse>(cancellationToken);
        }

        if (string.Equals(context.Activity?.ChannelId, Channels.Msteams, StringComparison.InvariantCultureIgnoreCase))
        {
            activity.Id = activityId;
            return context.UpdateActivityAsync(activity, cancellationToken);
        }

        if (string.Equals(context.Activity?.ChannelId, Channels.Telegram, StringComparison.InvariantCultureIgnoreCase) is false)
        {
            return context.SendActivityAsync(activity, cancellationToken);
        }

        return context.InnerForceReplaceActivityAsync(activityId, activity, cancellationToken);
    }

    private static async Task<ResourceResponse> InnerForceReplaceActivityAsync(
        this ITurnContext context, string activityId, IActivity activity, CancellationToken cancellationToken)
    {
        var deletionTask = context.DeleteActivityAsync(activityId, cancellationToken);
        var activityTask = context.SendActivityAsync(activity, cancellationToken);

        await Task.WhenAll(deletionTask, activityTask).ConfigureAwait(false);
        return activityTask.Result;
    }
}