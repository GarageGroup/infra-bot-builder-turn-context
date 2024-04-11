using System;
using System.Threading;
using System.Threading.Tasks;
using DeepEqual.Syntax;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using Moq;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace GarageGroup.Infra.Bot.Builder.TurnContext.Extensions.Test;

partial class TurnContextExtensionsTest
{
    [Fact]
    public static async Task ReplaceActivityAsync_ContextIsNull_ExpectArgumentNullException()
    {
        ITurnContext context = null!;
        var activity = MessageFactory.Text(SomeString);
        var cancellationToken = new CancellationToken(canceled: false);

        var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAsync);
        Assert.Equal("context", ex.ParamName);

        async Task TestAsync()
            =>
            _ = await context.ReplaceActivityAsync(AnotherString, activity, cancellationToken);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(MixedWhiteSpacesString)]
    public static async Task ReplaceActivityAsync_ActivityIdIsNullOrWhiteSpace_ExpectArgumentException(
        string? activityId)
    {
        var response = new ResourceResponse("Some ID");
        var mockTurnContext = CreateMockTurnContext(response, activity: SomeMsteamsActivity);

        var activity = MessageFactory.Text(SomeString);
        var cancellationToken = new CancellationToken(canceled: false);

        var ex = await Assert.ThrowsAsync<ArgumentException>(TestAsync);
        Assert.Equal("activityId", ex.ParamName);

        async Task TestAsync()
            =>
            _ = await mockTurnContext.Object.ReplaceActivityAsync(activityId!, activity, cancellationToken);
    }

    [Fact]
    public static async Task ReplaceActivityAsync_ActvityIsNull_ExpectArgumentNullException()
    {
        var response = new ResourceResponse("Some ID");
        var mockTurnContext = CreateMockTurnContext(response, activity: SomeMsteamsActivity);

        IActivity activity = null!;
        var cancellationToken = new CancellationToken(canceled: false);

        var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAsync);
        Assert.Equal("activity", ex.ParamName);

        async Task TestAsync()
            =>
            _ = await mockTurnContext.Object.ReplaceActivityAsync(AnotherString, activity, cancellationToken);
    }

    [Fact]
    public static void ReplaceActivityAsync_CancellationTokenIsCanceled_ExpectCanceledTask()
    {
        var response = new ResourceResponse("Some ID");
        var mockTurnContext = CreateMockTurnContext(response, activity: SomeMsteamsActivity);

        var activityId = "Some ActivityId";
        var activity = MessageFactory.Text(SomeString);

        var cancellationToken = new CancellationToken(canceled: true);
        var actualTask = mockTurnContext.Object.ReplaceActivityAsync(activityId, activity, cancellationToken);

        Assert.True(actualTask.IsCanceled);
    }

    [Theory]
    [InlineData(Channels.Msteams)]
    [InlineData("MsTeams")]
    [InlineData("MSTEAMS")]
    public static async Task ReplaceActivityAsync_ChannelIsMsteams_ExpectActivityUpdateCalledOnce(
        string channelId)
    {
        var response = new ResourceResponse("Some ID");

        var sourceActivity = new Activity
        {
            ChannelId = channelId
        };

        var mockTurnContext = CreateMockTurnContext(response, IsActualActivity, sourceActivity);

        var activityId = "Some ActivityId";
        var activity = MessageFactory.Text(SomeString);

        var cancellationToken = new CancellationToken(canceled: false);
        _ = await mockTurnContext.Object.ReplaceActivityAsync(activityId, activity, cancellationToken);

        mockTurnContext.Verify(c => c.UpdateActivityAsync(It.IsAny<IActivity>(), cancellationToken), Times.Once);

        static void IsActualActivity(IActivity actual)
        {
            var expected = MessageFactory.Text(SomeString);
            expected.Id = "Some ActivityId";

            actual.ShouldDeepEqual(expected);
        }
    }

    [Theory]
    [InlineData(Channels.Telegram)]
    [InlineData("Telegram")]
    [InlineData("TELEGRAM")]
    public static async Task ReplaceActivityAsync_ChannelIsTelegram_ExpectActivityDeleteCalledOnce(
        string channelId)
    {
        var response = new ResourceResponse("Some ID");

        var sourceActivity = new Activity
        {
            ChannelId = channelId
        };

        var mockTurnContext = CreateMockTurnContext(response, activity: sourceActivity);

        var activityId = "Some ActivityId";
        var activity = MessageFactory.Text(SomeString);

        var cancellationToken = new CancellationToken(canceled: false);
        _ = await mockTurnContext.Object.ReplaceActivityAsync(activityId, activity, cancellationToken);

        mockTurnContext.Verify(c => c.DeleteActivityAsync(activityId, cancellationToken), Times.Once);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(Channels.Msteams)]
    [InlineData(Channels.Webchat)]
    [InlineData(Channels.Emulator)]
    public static async Task ReplaceActivityAsync_ChannelIsNotTelegram_ExpectActivityDeleteCalledNever(
        string? channelId)
    {
        var response = new ResourceResponse("Some ID");

        var sourceActivity = new Activity
        {
            ChannelId = channelId
        };

        var mockTurnContext = CreateMockTurnContext(response, activity: sourceActivity);

        var activityId = "Some ActivityId";
        var activity = MessageFactory.Text(SomeString);

        var cancellationToken = new CancellationToken(canceled: false);
        _ = await mockTurnContext.Object.ReplaceActivityAsync(activityId, activity, cancellationToken);

        mockTurnContext.Verify(static c => c.DeleteActivityAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(Channels.Telegram)]
    [InlineData(Channels.Webchat)]
    [InlineData(Channels.Emulator)]
    public static async Task ReplaceActivityAsync_ChannelIsNotMsteams_ExpectActivitySendCalledOnce(
        string? channelId)
    {
        var response = new ResourceResponse("Some ID");

        var sourceActivity = new Activity
        {
            ChannelId = channelId
        };

        var mockTurnContext = CreateMockTurnContext(response, activity: sourceActivity);

        var activityId = "Some ActivityId";
        var activity = MessageFactory.Text(SomeString);

        var cancellationToken = new CancellationToken(canceled: false);
        _ = await mockTurnContext.Object.ReplaceActivityAsync(activityId, activity, cancellationToken);

        mockTurnContext.Verify(c => c.SendActivityAsync(activity, cancellationToken), Times.Once);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(Channels.Msteams, null)]
    [InlineData(Channels.Telegram, EmptyString)]
    [InlineData(Channels.Emulator, "Some resource Id")]
    public static async Task ReplaceActivityAsync_AllIsCorrect_ExpectCorrectResponse(
        string? channelId, string? responseId)
    {
        var response = new ResourceResponse(responseId);

        var sourceActivity = new Activity
        {
            ChannelId = channelId
        };

        var mockTurnContext = CreateMockTurnContext(response, activity: sourceActivity);

        var activityId = "Some ActivityId";
        var activity = MessageFactory.Text(SomeString);

        var cancellationToken = new CancellationToken(canceled: false);
        var actual = await mockTurnContext.Object.ReplaceActivityAsync(activityId, activity, cancellationToken);

        Assert.Same(response, actual);
    }
}