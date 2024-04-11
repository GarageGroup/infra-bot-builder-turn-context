using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace GarageGroup.Infra.Bot.Builder.TurnContext.Extensions.Test;

partial class TurnContextExtensionsTest
{
    [Fact]
    public static void IsEmulatorChannel_TurnContextIsNull_ExpectFalse()
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
    public static void IsEmulatorChannel_ChannelIdIsNotEmulator_ExpectFalse(string? channelId)
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
    public static void IsEmulatorChannel_ChannelIdIsEmulator_ExpectTrue(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsEmulatorChannel();
        Assert.True(actual);
    }

    [Fact]
    public static void IsNotEmulatorChannel_TurnContextIsNull_ExpectTrue()
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
    public static void IsNotEmulatorChannel_ChannelIdIsNotEmulator_ExpectTrue(string? channelId)
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
    public static void IsNotEmulatorChannel_ChannelIdIsEmulator_ExpectFalse(string channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.IsNotEmulatorChannel();
        Assert.False(actual);
    }
}