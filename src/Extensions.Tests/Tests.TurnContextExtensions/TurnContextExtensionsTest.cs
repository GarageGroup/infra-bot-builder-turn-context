using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Moq;

namespace GGroupp.Infra.Bot.Builder.TurnContext.Extensions.Tests;

public sealed partial class TurnContextExtensionsTest
{
    private const string EmptyString = "";

    private static ITurnContext CreateStubTurnContext(Activity activity)
        =>
        Mock.Of<ITurnContext>(
            c => c.Activity == activity);
}