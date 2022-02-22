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
            return source;
        }

        var encodedString = source;

        if (style is BotTextStyle.Default)
        {
            foreach (var regItem in RegexReplacementWithDefaultStyle)
            {
                encodedString = regItem.Key.Replace(encodedString, regItem.Value);
            }
        }
        else 
        {
            foreach (var regItem in RegexReplacementWithSpecificStyle)
            {
                encodedString = regItem.Key.Replace(encodedString, regItem.Value);
            }
        }

        encodedString = new(encodedString.Where(c => char.IsControl(c) is false).ToArray());

        return style switch
        {
            BotTextStyle.Bold => string.Concat(BoldStyleSign, encodedString, BoldStyleSign),
            BotTextStyle.Italic => string.Concat(ItalicStyleSign, encodedString, ItalicStyleSign),
            _ => encodedString
        };
    }
}