using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Bot.Builder;

namespace GGroupp.Infra.Bot.Builder;

public static partial class TurnContextExtensions
{
    public static string EncodeTextWithStyle(this ITurnContext? turnContext, [AllowNull] string source, BotTextStyle style)
    {
        if (string.IsNullOrEmpty(source))
        {
            return string.Empty;
        }

        if (turnContext is null || turnContext.IsNotTelegramChannel())
        {
            return InnerBuildTextWithStyle(source ?? string.Empty, style);
        }

        var encodedString = source;
        var replacement = style is BotTextStyle.Default ? RegexReplacementWithDefaultStyle : RegexReplacementWithSpecificStyle;

        foreach (var regItem in replacement)
        {
            encodedString = regItem.Key.Replace(encodedString, regItem.Value);
        }

        encodedString = new(encodedString.Where(c => char.IsControl(c) is false).ToArray());
        return InnerBuildTextWithStyle(encodedString, style);
    }
    
    private static string InnerBuildTextWithStyle(string text, BotTextStyle style)
        =>
        style switch
        {
            BotTextStyle.Bold => string.Concat(BoldStyleSign, text, BoldStyleSign),
            BotTextStyle.Italic => string.Concat(ItalicStyleSign, text, ItalicStyleSign),
            _ => text
        };
}
