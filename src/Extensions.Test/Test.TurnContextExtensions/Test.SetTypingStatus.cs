using System;
using System.Threading;
using System.Threading.Tasks;
using DeepEqual.Syntax;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Moq;
using Xunit;

namespace GarageGroup.Infra.Bot.Builder.TurnContext.Extensions.Test;

partial class TurnContextExtensionsTest
{
    [Fact]
    public static async Task SetTypingStatusAsync_ContextIsNull_ExpectArgumentNullException()
    {
        ITurnContext context = null!;
        var cancellationToken = new CancellationToken(canceled: false);

        var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAsync);
        Assert.Equal("context", ex.ParamName);

        async Task TestAsync()
            =>
            _ = await context.SetTypingStatusAsync(cancellationToken);
    }

    [Fact]
    public static void SetTypingStatusAsync_CancellationTokenIsCanceled_ExpectCanceledTask()
    {
        var response = new ResourceResponse("Some ID");
        var mockTurnContext = CreateMockTurnContext(response);

        var cancellationToken = new CancellationToken(canceled: true);
        var actualTask = mockTurnContext.Object.SetTypingStatusAsync(cancellationToken);

        Assert.True(actualTask.IsCanceled);
    }

    [Fact]
    public static async Task SetTypingStatusAsync_CancellationTokenIsNotCanceled_ExpectSendActivityCalledOnce()
    {
        var response = new ResourceResponse("Some ID");
        var mockTurnContext = CreateMockTurnContext(response, IsTypingStatusActivity);

        var cancellationToken = new CancellationToken(canceled: false);
        _ = await mockTurnContext.Object.SetTypingStatusAsync(cancellationToken);

        mockTurnContext.Verify(c => c.SendActivityAsync(It.IsAny<IActivity>(), cancellationToken), Times.Once);

        static void IsTypingStatusActivity(IActivity actual)
        {
            var expected = new Activity
            {
                Type = ActivityTypes.Typing
            };

            actual.ShouldDeepEqual(expected);
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some resource Id")]
    public static async Task SetTypingStatusAsync_CancellationTokenIsNotCanceled_ExpectCorrectResponse(
        string? responseId)
    {
        var response = new ResourceResponse(responseId);
        var mockTurnContext = CreateMockTurnContext(response);

        var cancellationToken = new CancellationToken(canceled: false);
        var actual = await mockTurnContext.Object.SetTypingStatusAsync(cancellationToken);

        Assert.Same(response, actual);
    }
}