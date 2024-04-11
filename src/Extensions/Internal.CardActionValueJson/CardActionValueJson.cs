using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using static System.FormattableString;

namespace GarageGroup.Infra.Bot.Builder;

internal sealed partial class CardActionValueJson(Guid id)
{
    private const string IdPropertyName = "valueId";

    private const string GuidRegexPattern = "[0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12}";

    private const string JsonRegexPattern = "^{\\s*\"" + IdPropertyName + "\"\\s*:\\s*\"(" + GuidRegexPattern + ")\"\\s*}$";

    private static string GetSerializationText(Guid id)
        =>
        Invariant($"{{\"{IdPropertyName}\":\"{id:D}\"}}");

    private static readonly Regex JsonRegex;

    static CardActionValueJson()
        =>
        JsonRegex = CreateJsonRegex();

    [JsonProperty(IdPropertyName)]
    public Guid Id { get; } = id;

    [GeneratedRegex(JsonRegexPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant)]
    private static partial Regex CreateJsonRegex();
}