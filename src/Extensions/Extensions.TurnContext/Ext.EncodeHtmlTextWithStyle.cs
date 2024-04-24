using System.Diagnostics.CodeAnalysis;
using System.Web;
using Microsoft.Bot.Builder;

namespace GarageGroup.Infra.Bot.Builder;

public static partial class TurnContextExtensions
{
    public static string EncodeHtmlTextWithStyle(this ITurnContext? turnContext, [AllowNull] string source, BotTextStyle style)
    {
        if (string.IsNullOrEmpty(source))
        {
            return string.Empty;
        }

        if (turnContext is null || turnContext.IsNotTelegramChannel())
        {
            return InnerBuildTextWithStyle(source, style);
        }

        var encodedString = HttpUtility.HtmlEncode(source);

        return style switch
        {
            BotTextStyle.Bold => string.Concat("<b>", encodedString, "</b>"),
            BotTextStyle.Italic => string.Concat("<i>", encodedString, "</i>"),
            _ => encodedString
        };
    }
}
