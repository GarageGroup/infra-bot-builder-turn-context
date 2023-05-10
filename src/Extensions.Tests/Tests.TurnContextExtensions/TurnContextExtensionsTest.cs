using System;
using System.Threading;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Moq;

namespace GarageGroup.Infra.Bot.Builder.TurnContext.Extensions.Tests;

public static partial class TurnContextExtensionsTest
{
    private const string EmptyString = "";

    private static ITurnContext CreateStubTurnContext(Activity activity)
        =>
        Mock.Of<ITurnContext>(
            c => c.Activity == activity);

    private static Mock<ITurnContext> CreateMockTurnContext(ResourceResponse response, Action<IActivity>? callback = null)
    {
        var mock = new Mock<ITurnContext>();

        var m = mock.Setup(c => c.SendActivityAsync(It.IsAny<IActivity>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);
        if (callback is not null)
        {
            m.Callback<IActivity, CancellationToken>((activity, _) => callback.Invoke(activity));
        }

        return mock;
    }
}