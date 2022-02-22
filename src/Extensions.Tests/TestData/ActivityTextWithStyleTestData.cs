namespace GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests;

internal static class ActivityTextWithStyleTestData
{
    public const string TargetTextWithStyleMessageWithNothingToChange =
        "**Заголовок:** '1Одинарные кавычки'\n\r\n\r**Клиент:** Тинькофф Банк 25641" +
        "\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**Описание:** 'Одинарные'";

    public const string EncodedTextWithStyleMessageWithNothingChanged =
        "Заголовок: '1Одинарные кавычки'\n\r\n\rКлиент: Тинькофф Банк 25641" +
        "\n\r\n\rТип обращения: Запрос\n\r\n\rОписание: 'Одинарные'";

    public const string TargetTextWithStyleMessageWithDangerousSymbolsInHeader =
        "**Заголовок:** '2Одинарные\n кавычки'\n\n\r\n\r**Клиент:** Тинькофф Банк 25641" +
        "\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**Описание:** 'Одинарные'";

    public const string EncodedTextWithStyleMessageWithDangerousSymbolsInHeader =
        "Заголовок: '2Одинарные\u2063\n\r\n\r\u2063 кавычки'\u2063\n\r\n\r\u2063\n\r" +
        "\n\rКлиент: Тинькофф Банк 25641\n\r\n\rТип обращения: Запрос\n\r\n\r" +
        "Описание: 'Одинарные'";

    public const string TargetTextWithStyleMessageWithBrackets =
        "**Заголовок:** '3Одинарные [к]авычки'\n\r\n\r**Клиент:** Тинькофф Банк 25641" +
        "\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**Описание:** 'Одинарные]'";

    public const string EncodedTextWithStyleMessageWithBrackets =
        "Заголовок: '3Одинарные кавычки'\n\r\n\rКлиент: Тинькофф Банк 25641" +
        "\n\r\n\rТип обращения: Запрос\n\r\n\rОписание: 'Одинарные'";

    public const string TargetTextWithStyleMessageWithEWhithDots =
        "Отчёт план/факт";

    public const string EncodedTextWithStyleMessageWithEWhithDots =
        "Отчёт планфакт";

    public const string TargetTextWithStyleMessageWithEEEeee =
        "ЁЁЁёёё";

    public const string EncodedTextWithStyleMessageWithEEEeee =
        "ЁЁЁёёё";

    public const string TargetTextWithStyleBoldMessage =
        "G-sup!po.r(t)";

    public const string EncodedTextWithStyleBoldMessage =
        @"G\\\-sup\\\!po\\\.r\\\(t\\\)";
}