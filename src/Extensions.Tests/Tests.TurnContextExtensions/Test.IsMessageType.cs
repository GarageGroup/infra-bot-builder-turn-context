using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Xunit;

namespace GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests;

partial class TurnContextExtensionsTest
{
    [Fact]
    public void IsMessageType_TurnContextIsNull_ExpectFalse()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsMessageType();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Message0")]
    public void IsMessageType_ActivityTypeIsNotMessage_ExpectFalse(string? activityType)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            Type = activityType
        });

        var actual = turnContext.IsMessageType();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(ActivityTypes.Message)]
    [InlineData("Message")]
    [InlineData("MESSAGE")]
    public void IsMessageType_ActivityTypeIsMessage_ExpectTrue(string activityType)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            Type = activityType
        });

        var actual = turnContext.IsMessageType();
        Assert.True(actual);
    }

    [Fact]
    public void IsNotMessageType_TurnContextIsNull_ExpectTrue()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsNotMessageType();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Message0")]
    public void IsNotMessageType_ChannelIdIsNotEmulator_ExpectTrue(string? activityType)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            Type = activityType
        });

        var actual = turnContext.IsNotMessageType();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(ActivityTypes.Message)]
    [InlineData("Message")]
    [InlineData("MESSAGE")]
    public void IsNotMessageType_ChannelIdIsEmulator_ExpectFalse(string activityType)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            Type = activityType
        });

        var actual = turnContext.IsNotMessageType();
        Assert.False(actual);
    }
}