using System;
using Microsoft.Bot.Schema;
using Xunit;

namespace GarageGroup.Infra.Bot.Builder.TurnContext.Extensions.Test;

partial class AttachmentExtensionsTest
{
    [Fact]
    public static void ToActivity_AttachmentIsNull_ExpectArgumentNullException()
    {
        Attachment attachment = null!;

        var ex = Assert.Throws<ArgumentNullException>(attachment.ToActivity);
        Assert.Equal("attachment", ex.ParamName);
    }

    [Fact]
    public static void ToActivity_AttachmentIsNotNull_ExpectActivityWithSourceAttachement()
    {
        var attachment = new Attachment();

        var actual = attachment.ToActivity();
        Attachment[] expected = [attachment];

        Assert.Equal(expected, actual.Attachments);
    }
}