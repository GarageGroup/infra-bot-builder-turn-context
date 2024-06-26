using System;
using Microsoft.Bot.Schema;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace GarageGroup.Infra.Bot.Builder.TurnContext.Extensions.Test;

partial class AttachmentExtensionsTest
{
    [Fact]
    public static void ToActivityWithInputHint_AttachmentIsNull_ExpectArgumentNullException()
    {
        Attachment attachment = null!;

        var ex = Assert.Throws<ArgumentNullException>(() => attachment.ToActivity("Some hint"));
        Assert.Equal("attachment", ex.ParamName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(EmptyString)]
    [InlineData("Some hint")]
    public static void ToActivityWithInputHint_AttachmentIsNotNull_ExpectActivityWithSourceAttachement(string? hint)
    {
        var attachment = new Attachment();

        var actual = attachment.ToActivity(hint);
        Attachment[] expected = [attachment];

        Assert.Equal(expected, actual.Attachments);
    }

    [Theory]
    [InlineData(null, EmptyString)]
    [InlineData(EmptyString, EmptyString)]
    [InlineData("Some hint", "Some hint")]
    public static void ToActivityWithInputHint_AttachmentIsNotNull_ExpectActivityWithExpectedHint(string? sourceHint, string expectedHint)
    {
        var attachment = new Attachment();

        var actual = attachment.ToActivity(sourceHint);
        Assert.Equal(expectedHint, actual.InputHint);
    }
}