using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Xunit;

namespace GarageGroup.Infra.Bot.Builder.TurnContext.Extensions.Tests;

partial class TurnContextExtensionsTest
{
    [Fact]
    public static void IsMsteamsChannel_TurnContextIsNull_ExpectFalse()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsMsteamsChannel();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Emulator)]
    [InlineData(Channels.Msteams + " ")]
    [InlineData(Channels.Telegram)]
    [InlineData(Channels.Webchat)]
    public static void IsMsteamsChannel_ChannelIdIsNotMsteams_ExpectFalse(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsMsteamsChannel();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(Channels.Msteams)]
    [InlineData("MsTeams")]
    [InlineData("MSTEAMS")]
    public static void IsMsteamsChannel_ChannelIdIsMsteams_ExpectTrue(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsMsteamsChannel();
        Assert.True(actual);
    }

    [Fact]
    public static void IsNotMsteamsChannel_TurnContextIsNull_ExpectTrue()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsNotMsteamsChannel();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Emulator)]
    [InlineData(Channels.Msteams + "\t")]
    [InlineData(Channels.Telegram)]
    [InlineData(Channels.Webchat)]
    public static void IsNotMsteamsChannel_ChannelIdIsNotMsteams_ExpectTrue(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotMsteamsChannel();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(Channels.Msteams)]
    [InlineData("MsTeams")]
    [InlineData("MSTEAMS")]
    public static void IsNotMsteamsChannel_ChannelIdIsMsteams_ExpectFalse(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotMsteamsChannel();
        Assert.False(actual);
    }
}