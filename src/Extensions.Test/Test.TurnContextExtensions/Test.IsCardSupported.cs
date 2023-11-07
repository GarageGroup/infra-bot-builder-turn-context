using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Xunit;

namespace GarageGroup.Infra.Bot.Builder.TurnContext.Extensions.Test;

partial class TurnContextExtensionsTest
{
    [Fact]
    public static void IsCardSupported_TurnContextIsNull_ExpectFalse()
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
    public static void IsCardSupported_ChannelIdIsNotMsteamsOrNotWebchatOrNotEmulator_ExpectFalse(string? channelId)
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
    public static void IsCardSupported_ChannelIdIsMsteamsOrWebchatOrEmulator_ExpectTrue(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsCardSupported();
        Assert.True(actual);
    }

    [Fact]
    public static void IsNotCardSupported_TurnContextIsNull_ExpectTrue()
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
    public static void IsNotCardSupported_ChannelIdIsNotMsteamsOrNotWebchatOrNotEmulator_ExpectTrue(string? channelId)
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
    public static void IsNotCardSupported_ChannelIdIsMsteamsOrWebchatOrEmulator_ExpectFalse(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotCardSupported();
        Assert.False(actual);
    }
}