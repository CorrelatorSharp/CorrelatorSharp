using Machine.Specifications;

namespace CorrelatorSharp.Logging.Tests
{
    public class When_not_configured
    {
        It should_fallback_to_the_dummy_logmanager = () => {
            LogManager.GetCurrentClassLogger().ShouldNotBeNull();
        };
    }
}
