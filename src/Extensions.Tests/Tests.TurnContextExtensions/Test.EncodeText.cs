using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Xunit;
using static GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests.ActivityTextTestData;

namespace GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests;

partial class TurnContextExtensionsTest
{
    [Fact]
    public static void EncodeText_TurnContextIsNullAndSourceTextIsNull_ExpectEmptyString()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.EncodeText(null);
        Assert.Empty(actual);
    }

    [Theory]
    [InlineData(EmptyString)]
    [InlineData("Some text")]
    [InlineData(TargetTextMessageWithALotOfNewLines)]
    [InlineData("Строка с __комментами__ **жирныйш шрифт**, \"\"две кавычки\"\"")]
    public static void EncodeText_TurnContextIsNullAndSourceTextIsNotNull_ExpectSourceText(string source)
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.EncodeText(source);
        Assert.Equal(source, actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some channel")]
    [InlineData(Channels.Emulator)]
    [InlineData(Channels.Webchat)]
    [InlineData(Channels.Msteams)]
    [InlineData(Channels.Telegram)]
    [InlineData("Telegram")]
    [InlineData("TELEGRAM")]
    public static void EncodeText_SourceTextIsNull_ExpectEmptyString(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.EncodeText(null);
        Assert.Empty(actual);
    }

    [Theory]
    [InlineData(null, TargetTextMessageWithALotOfNewLines)]
    [InlineData(EmptyString, TargetTextMessageWithAllDangerousSymbols)]
    [InlineData("Some channel", TargetTextMessage)]
    [InlineData(Channels.Emulator, "\n\n\n")]
    [InlineData(Channels.Webchat, "usual \"string\"")]
    [InlineData(Channels.Msteams, EmptyString)]
    public static void EncodeText_SourceTextIsNotNullAndSourceChannelIsNotTelegram_ExpectSourceString(
        string? channelId, string source)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.EncodeText(source);
        Assert.Equal(source, actual);
    }

    [Theory]
    [InlineData(Channels.Telegram, EmptyString)]
    [InlineData(Channels.Telegram, "Some string")]
    public static void EncodeText_SourceTextNotContainsWrongSymbolsAndSourceChannelIsTelegram_ExpectSourceString(
        string channelId, string source)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.EncodeText(source);
        Assert.Equal(source, actual);
    }

    [Theory]
    [InlineData("Telegram", TargetTextMessage, EncodedTargetTextMessage)]
    [InlineData("Telegram", TargetTextMessageWithALotOfNewLines, EncodedTargetTextMessageWithALotOfNewLines)]
    [InlineData("TELEGRAM", TargetTextMessageWithNothingToChange, EncodedTextMessageWithNothingChanged)]
    [InlineData(Channels.Telegram, TargetTextMessageWithDangerousSymbolsInHeader, EncodedTextMessageWithDangerousSymbolsInHeader)]
    [InlineData("TELEGRAM", TargetTextMessageWithBrackets, EncodedTextMessageWithBrackets)]
    [InlineData(Channels.Telegram, TargetTextMessageWithAllDangerousSymbols, EncodedTextMessageWithAllDangerousSymbols)]
    [InlineData(Channels.Telegram, TargetTextMessageWithNewDangerousSymbols, EncodedTextMessageWithNewDangerousSymbols)]
    [InlineData(Channels.Telegram, TargetTextMessageWithEWhithDots, EncodedTextMessageWithEWhithDots)]
    [InlineData(Channels.Telegram, TargetTextMessageWithEEEeee, EncodedTextMessageWithEEEeee)]
    [InlineData("TELEGRAM", "\n\n\n", "\u2063\n\r\n\r\u2063\u2063\n\r\n\r\u2063\u2063\n\r\n\r\u2063")]
    [InlineData("Telegram", "usual \"string\"", "usual \"string\"")]
    [InlineData("Telegram", @"\\\\\\\\\", EmptyString)]
    [InlineData(Channels.Telegram, @"\", EmptyString)]
    public static void EncodeText_SourceTextContainsWrongSymbolsAndSourceChannelIsTelegram_ExpectEncodedString(
        string channelId, string source, string expected)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.EncodeText(source);
        Assert.Equal(expected, actual);
    }
}