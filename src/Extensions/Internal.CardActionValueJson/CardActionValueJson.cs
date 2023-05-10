﻿using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using static System.FormattableString;

namespace GarageGroup.Infra.Bot.Builder;

internal sealed partial class CardActionValueJson
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
        JsonRegex = new(JsonRegexPattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline);

    public CardActionValueJson(Guid id) => Id = id;

    [JsonProperty(IdPropertyName)]
    public Guid Id { get; }
}