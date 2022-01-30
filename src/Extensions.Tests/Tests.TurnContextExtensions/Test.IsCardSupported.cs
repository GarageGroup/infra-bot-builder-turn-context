using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Xunit;

namespace GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests;

partial class TurnContextExtensionsTest
{
    [Fact]
    public void IsCardSupported_TurnContextIsNull_ExpectFalse()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsCardSupported();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Telegram)]
    public void IsCardSupported_ChannelIdIsNotMsteamsOrNotWebchatOrNotEmulator_ExpectFalse(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsCardSupported();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(Channels.Emulator)]
    [InlineData(Channels.Webchat)]
    [InlineData(Channels.Msteams)]
    [InlineData("Emulator")]
    [InlineData("WEBCHAT")]
    [InlineData("MSTeams")]
    public void IsCardSupported_ChannelIdIsMsteamsOrWebchatOrEmulator_ExpectTrue(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsCardSupported();
        Assert.True(actual);
    }

    [Fact]
    public void IsNotCardSupported_TurnContextIsNull_ExpectTrue()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsNotCardSupported();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Telegram)]
    public void IsNotCardSupported_ChannelIdIsNotMsteamsOrNotWebchatOrNotEmulator_ExpectTrue(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotCardSupported();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(Channels.Emulator)]
    [InlineData(Channels.Webchat)]
    [InlineData(Channels.Msteams)]
    [InlineData("Emulator")]
    [InlineData("WEBCHAT")]
    [InlineData("MSTeams")]
    public void IsNotCardSupported_ChannelIdIsMsteamsOrWebchatOrEmulator_ExpectFalse(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotCardSupported();
        Assert.False(actual);
    }
}