namespace GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests;

internal static class ActivityTextTestData
{
    public const string TargetTextMessage = 
        "**Заголовок:** В Букинге требуется добавить н...\n\r\n\r**" +
        "Клиент:** Детский мир 13299\n\r\n\r**Тип обращения:** Запрос" +
        "\n\r\n\r**Описание:** В Букинге требуется добавить новое поле" +
        " \"Типы поставок\" с выбором:\n- Море\n- Жд\n- Авто\n- Авиа\n- " +
        "Мультимодальная\n- Карго\nПо умолчанию = Море\n";

    public const string EncodedTargetTextMessage =
        "**Заголовок:** В Букинге требуется добавить н...\n\r\n\r**" +
        "Клиент:** Детский мир 13299\n\r\n\r**Тип обращения:** Запрос" +
        "\n\r\n\r**Описание:** В Букинге требуется добавить новое поле" +
        " \"Типы поставок\" с выбором:\u2063\n\r\n\r\u2063- Море\u2063\n\r\n\r\u2063- " +
        "Жд\u2063\n\r\n\r\u2063- Авто\u2063\n\r\n\r\u2063- Авиа\u2063\n\r\n\r\u2063- " +
        "Мультимодальная\u2063\n\r\n\r\u2063- Карго\u2063\n\r\n\r\u2063По умолчанию =" +
        " Море\u2063\n\r\n\r\u2063";

    public const string TargetTextMessageWithALotOfNewLines =
        "**Заголовок:** В Букинге требуется добавить н...\n\r\n\r**" +
        "Клиент:** Детский мир 13299\n\r\n\r**Тип обращения:** Запрос" +
        "\n\r\n\r**Описание:** В Букинге требуется добавить новое поле" +
        " \"Типы поставок\" с выбором:\n- Море\n- Жд\n- Авто\n- Авиа\n- " +
        "Мультимодальная\n- Карго\nПо умолчанию\n\n\n\n\n\n = Море\n";

    public const string EncodedTargetTextMessageWithALotOfNewLines =
        "**Заголовок:** В Букинге требуется добавить н...\n\r\n\r**" +
        "Клиент:** Детский мир 13299\n\r\n\r**Тип обращения:** Запрос" +
        "\n\r\n\r**Описание:** В Букинге требуется добавить новое поле" +
        " \"Типы поставок\" с выбором:\u2063\n\r\n\r\u2063- Море\u2063\n\r\n\r\u2063" +
        "- Жд\u2063\n\r\n\r\u2063- Авто\u2063\n\r\n\r\u2063- Авиа\u2063\n\r\n\r\u2063" +
        "- Мультимодальная\u2063\n\r\n\r\u2063- Карго\u2063\n\r\n\r\u2063По умолчанию" +
        "\u2063\n\r\n\r\u2063\u2063\n\r\n\r\u2063\u2063\n\r\n\r\u2063\u2063\n\r\n\r\u2063" +
        "\u2063\n\r\n\r\u2063\u2063\n\r\n\r\u2063 = Море\u2063\n\r\n\r\u2063";

    public const string TargetTextMessageWithNothingToChange = 
        "**Заголовок:** 'Одинарные кавычки'\n\r\n\r**Клиент:** Некий Банк 25641" +
        "\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**Описание:** 'Одинарные'";

    public const string EncodedTextMessageWithNothingChanged =
        "**Заголовок:** 'Одинарные кавычки'\n\r\n\r**Клиент:** Некий Банк 25641" +
        "\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**Описание:** 'Одинарные'";

    public const string TargetTextMessageWithDangerousSymbolsInHeader =
        "**Заголовок:** 'Одинарные\n кавычки'\n\n\r\n\r**Клиент:** Некий Банк 25641" +
        "\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**Описание:** 'Одинарные'";

    public const string EncodedTextMessageWithDangerousSymbolsInHeader =
        "**Заголовок:** 'Одинарные\u2063\n\r\n\r\u2063 кавычки'\u2063\n\r\n\r\u2063\n\r" +
        "\n\r**Клиент:** Некий Банк 25641\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**" +
        "Описание:** 'Одинарные'";

    public const string TargetTextMessageWithBrackets =
        "**Заголовок:** 'Одинарные [к]авычки'\n\r\n\r**Клиент:** Некий Банк 25641" +
        "\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**Описание:** 'Одинарные]'";
    
    public const string EncodedTextMessageWithBrackets =
        "**Заголовок:** 'Одинарные (к)авычки'\n\r\n\r**Клиент:** Некий Банк 25641" +
        "\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**Описание:** 'Одинарные)'";

    public const string TargetTextMessageWithAllDangerousSymbols =
        "**Заголовок:** 'Одинарные кавычки'\n\r\n\r**Клиент:** Некий Банк 25641" +
        "\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**Описание:** 'Одинарные'&^!@#$%" +
        "*()_+=-\n[[[several sqare]]]]]][[[[\na<b\na>b\n\\/|\n";

    public const string EncodedTextMessageWithAllDangerousSymbols =
        "**Заголовок:** 'Одинарные кавычки'\n\r\n\r**Клиент:** Некий Банк 25641" +
        "\n\r\n\r**Тип обращения:** Запрос\n\r\n\r**Описание:** 'Одинарные'&^!@#$%" +
        "*()_+=-\u2063\n\r\n\r\u2063(((several sqare))))))((((\u2063\n\r\n\r\u2063" +
        "a<b\u2063\n\r\n\r\u2063a>b\u2063\n\r\n\r\u2063/|\u2063\n\r\n\r\u2063";
}