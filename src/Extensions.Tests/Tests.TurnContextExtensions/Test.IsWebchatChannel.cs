using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Xunit;

namespace GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests;

partial class TurnContextExtensionsTest
{
    [Fact]
    public static void IsWebchatChannel_TurnContextIsNull_ExpectFalse()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsWebchatChannel();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Emulator)]
    [InlineData(Channels.Webchat + " ")]
    [InlineData(Channels.Msteams)]
    [InlineData(Channels.Telegram)]
    public static void IsWebchatChannel_ChannelIdIsNotWebchat_ExpectFalse(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsWebchatChannel();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(Channels.Webchat)]
    [InlineData("WebChat")]
    [InlineData("WEBCHAT")]
    public static void IsWebchatChannel_ChannelIdIsWebchat_ExpectTrue(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsWebchatChannel();
        Assert.True(actual);
    }

    [Fact]
    public static void IsNotWebchatChannel_TurnContextIsNull_ExpectTrue()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsNotWebchatChannel();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Emulator)]
    [InlineData(Channels.Webchat + "\t")]
    [InlineData(Channels.Msteams)]
    [InlineData(Channels.Telegram)]
    public static void IsNotWebchatChannel_ChannelIdIsNotWebchat_ExpectTrue(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotWebchatChannel();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(Channels.Webchat)]
    [InlineData("WebChat")]
    [InlineData("WEBCHAT")]
    public static void IsNotWebchatChannel_ChannelIdIsWebchat_ExpectFalse(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotWebchatChannel();
        Assert.False(actual);
    }
}