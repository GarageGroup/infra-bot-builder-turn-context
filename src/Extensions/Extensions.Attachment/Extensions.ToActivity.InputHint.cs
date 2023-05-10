using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace GarageGroup.Infra.Bot.Builder;

partial class AttachmentExtensions
{
    public static IMessageActivity ToActivity(this Attachment attachment, [AllowNull] string inputHint)
        =>
        MessageFactory.Attachment(
            attachment: attachment ?? throw new ArgumentNullException(nameof(attachment)),
            text: default,
            ssml: default,
            inputHint: inputHint ?? string.Empty);
}