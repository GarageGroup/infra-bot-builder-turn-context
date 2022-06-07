using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Xunit;
using static GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests.ActivityTextWithStyleTestData;

namespace GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests;

partial class TurnContextExtensionsTest
{
    [Fact]
    public void EncodeTextWithStyle_TurnContextIsNullAndSourceTextIsNull_ExpectEmptyString()
    {
        ITurnContext turnContext = null!;

        var actual = turnContext.EncodeText(null);
        Assert.Empty(actual);
    }

    [Theory]
    [InlineData(EmptyString)]
    [InlineData("Some text")]
    [InlineData("Строка с __комментами__ **жирныйш шрифт**, \"\"две кавычки\"\"")]
    public void EncodeTextWithStyle_TurnContextIsNullAndSourceTextIsNotNull_ExpectSourceText(string source)
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
    public void EncodeTextWithStyle_SourceTextIsNull_ExpectEmptyString(string? channelId)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.EncodeText(null);
        Assert.Empty(actual);
    }

    [Theory]
    [InlineData(Channels.Emulator, "\n\n\n")]
    [InlineData(Channels.Webchat, "usual \"string\"")]
    [InlineData(Channels.Msteams, EmptyString)]
    public void EncodeTextWithStyle_SourceTextIsNotNullAndSourceChannelIsNotTelegram_ExpectSourceString(string? channelId, string source)
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
    public void EncodeTextWithStyle_SourceTextNotContainsWrongSymbolsAndSourceChannelIsTelegram_ExpectSourceString(string channelId, string source)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.EncodeText(source);
        Assert.Equal(source, actual);
    }

    [Theory]
    [InlineData("TELEGRAM", TargetTextWithStyleMessageWithNothingToChange, EncodedTextWithStyleMessageWithNothingChanged)]
    [InlineData(Channels.Telegram, TargetTextWithStyleMessageWithDangerousSymbolsInHeader, EncodedTextWithStyleMessageWithDangerousSymbolsInHeader)]
    [InlineData("TELEGRAM", TargetTextWithStyleMessageWithBrackets, EncodedTextWithStyleMessageWithBrackets)]
    [InlineData(Channels.Telegram, TargetTextWithStyleMessageWithEWhithDots, EncodedTextWithStyleMessageWithEWhithDots)]
    [InlineData(Channels.Telegram, TargetTextWithStyleMessageWithEEEeee, EncodedTextWithStyleMessageWithEEEeee)]
    [InlineData(Channels.Telegram, TargetTextWithStyleBoldMessage, EncodedTextWithStyleBoldMessage)]
    [InlineData("TELEGRAM", "\n\r\n\r", "")]
    [InlineData("Telegram", "usual \"string\"", "usual \"string\"")]
    [InlineData("Telegram", @"\\\\\\\\\", EmptyString)]
    [InlineData(Channels.Telegram, @"\", EmptyString)]
    [InlineData(Channels.Telegram, @"Ggroupp-Project", @"Ggroupp\\\-Project")]
    public void EncodeTextWithStyle_SourceTextContainsWrongSymbolsAndSourceChannelIsTelegram_ExpectEncodedString(string channelId, string source, string expected)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.EncodeTextWithStyle(source, BotTextStyle.Default);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(Channels.Telegram, @"Ggroupp-Project", "**Ggroupp\\-Project**", BotTextStyle.Bold)]
    [InlineData(Channels.Telegram, @"Ggroupp-Project", "*Ggroupp\\-Project*", BotTextStyle.Italic)]
    [InlineData(Channels.Telegram, @"G**group**p-Project", "*Ggroupp\\-Project*", BotTextStyle.Italic)]
    [InlineData(Channels.Telegram, @"G__group__p-Project", "*Ggroupp\\-Project*", BotTextStyle.Italic)]
    [InlineData(Channels.Msteams, @"G__group__p-Project", "**G__group__p-Project**", BotTextStyle.Bold)]
    [InlineData(Channels.Emulator, @"G__group__p-Project", "*G__group__p-Project*", BotTextStyle.Italic)]
    public void EncodeTextWithStyle_SourceTextStyledAndSourceChannelIsTelegram_ExpectEncodedString(string channelId, string source, string expected, BotTextStyle style)
    {
        var turnContext = CreateStubTurnContext(new()
        {
            ChannelId = channelId
        });

        var actual = turnContext.EncodeTextWithStyle(source, style);
        Assert.Equal(expected, actual);
    }
}
