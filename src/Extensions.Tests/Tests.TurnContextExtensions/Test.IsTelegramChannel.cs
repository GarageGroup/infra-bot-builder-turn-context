using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Xunit;

namespace GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests;

partial class TurnContextExtensionsTest
{
    [Fact]
    public void IsTelegramChannel_TurnContextIsNull_ExpectFalse()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsTelegramChannel();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Emulator)]
    [InlineData(Channels.Telegram + " ")]
    [InlineData(Channels.Msteams)]
    [InlineData(Channels.Webchat)]
    public void IsTelegramChannel_ChannelIdIsNotTelegram_ExpectFalse(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsTelegramChannel();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(Channels.Telegram)]
    [InlineData("Telegram")]
    [InlineData("TELEGRAM")]
    public void IsTelegramChannel_ChannelIdIsTelegram_ExpectTrue(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsTelegramChannel();
        Assert.True(actual);
    }

    [Fact]
    public void IsNotTelegramChannel_TurnContextIsNull_ExpectTrue()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsNotTelegramChannel();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Emulator)]
    [InlineData(Channels.Telegram + "\t")]
    [InlineData(Channels.Msteams)]
    [InlineData(Channels.Webchat)]
    public void IsNotTelegramChannel_ChannelIdIsNotTelegram_ExpectTrue(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotTelegramChannel();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(Channels.Telegram)]
    [InlineData("Telegram")]
    [InlineData("TELEGRAM")]
    public void IsNotTelegramChannel_ChannelIdIsTelegram_ExpectFalse(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotTelegramChannel();
        Assert.False(actual);
    }
}