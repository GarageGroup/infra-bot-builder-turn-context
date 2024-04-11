using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using Moq;

namespace GarageGroup.Infra.Bot.Builder.TurnContext.Extensions.Test;

public static partial class TurnContextExtensionsTest
{
    private static readonly Activity SomeMsteamsActivity
        =
        new()
        {
            ChannelId = Channels.Msteams
        };

    private static ITurnContext CreateStubTurnContext(Activity activity)
        =>
        Mock.Of<ITurnContext>(
            c => c.Activity == activity);

    private static Mock<ITurnContext> CreateMockTurnContext(
        ResourceResponse response, Action<IActivity>? callback = null, Activity? activity = null)
    {
        var mock = new Mock<ITurnContext>();

        if (activity is not null)
        {
            _ = mock.SetupGet(static c => c.Activity).Returns(activity);
        }

        var mSend = mock.Setup(static c => c.SendActivityAsync(It.IsAny<IActivity>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

        if (callback is not null)
        {
            mSend.Callback<IActivity, CancellationToken>((activity, _) => callback.Invoke(activity));
        }

        var mUpdate = mock.Setup(static c => c.UpdateActivityAsync(It.IsAny<IActivity>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

        if (callback is not null)
        {
            mUpdate.Callback<IActivity, CancellationToken>((activity, _) => callback.Invoke(activity));
        }

        _ = mock.Setup(static c => c.DeleteActivityAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        return mock;
    }
}