using System.Diagnostics.CodeAnalysis;
using System.Web;
using Microsoft.Bot.Builder;

namespace GGroupp.Infra.Bot.Builder;

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
            return InnerBuildTextWithStyle(source ?? string.Empty, style);
        }

        var encodedString = HttpUtility.HtmlEncode(source ?? string.Empty);
        return InnerBuildHtmlTextWithStyle(encodedString, style);
    }

    private static string InnerBuildHtmlTextWithStyle(string text, BotTextStyle style)
        =>
        style switch
        {
            BotTextStyle.Bold => string.Concat("<b>", text, "</b>"),
            BotTextStyle.Italic => string.Concat("<i>", text, "</i>"),
            _ => text
        };
}
