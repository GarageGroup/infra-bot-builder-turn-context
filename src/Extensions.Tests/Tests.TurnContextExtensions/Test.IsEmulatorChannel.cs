using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Xunit;

namespace GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests;

partial class TurnContextExtensionsTest
{
    [Fact]
    public void IsEmulatorChannel_TurnContextIsNull_ExpectFalse()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsEmulatorChannel();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Msteams)]
    [InlineData(Channels.Emulator + " ")]
    [InlineData(Channels.Telegram)]
    [InlineData(Channels.Webchat)]
    public void IsEmulatorChannel_ChannelIdIsNotEmulator_ExpectFalse(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsEmulatorChannel();
        Assert.False(actual);
    }

    [Theory]
    [InlineData(Channels.Emulator)]
    [InlineData("Emulator")]
    [InlineData("EMULATOR")]
    public void IsEmulatorChannel_ChannelIdIsEmulator_ExpectTrue(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsEmulatorChannel();
        Assert.True(actual);
    }

    [Fact]
    public void IsNotEmulatorChannel_TurnContextIsNull_ExpectTrue()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.IsNotEmulatorChannel();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Msteams)]
    [InlineData(Channels.Emulator + "\t")]
    [InlineData(Channels.Telegram)]
    [InlineData(Channels.Webchat)]
    public void IsNotEmulatorChannel_ChannelIdIsNotEmulator_ExpectTrue(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotEmulatorChannel();
        Assert.True(actual);
    }

    [Theory]
    [InlineData(Channels.Emulator)]
    [InlineData("Emulator")]
    [InlineData("EMULATOR")]
    public void IsNotEmulatorChannel_ChannelIdIsEmulator_ExpectFalse(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotEmulatorChannel();
        Assert.False(actual);
    }
}