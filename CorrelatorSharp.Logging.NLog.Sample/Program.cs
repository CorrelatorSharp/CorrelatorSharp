using System;
using System.Threading.Tasks;
using CorrelatorSharp;

namespace CorrelatorSharp.Logging.NLog.Sample
{
    class Program
    {
        private static ILogger _logger;

        static void Main(string[] args)
        {
            LoggingConfiguration.Current.UseNLog();

            _logger = LogManager.GetLogger("NLogSample");

            using (var scope = new ActivityScope("Main Operation")) {
                _logger.LogTrace("preparing to do work");

                DoWork().Wait();
            }
        }

        private static async Task DoWork()
        {
            _logger.LogTrace("doing work");

            using (var scope = new ActivityScope("Nested Operation 1")) {
                await Task.Delay(TimeSpan.FromSeconds(1));
                _logger.LogTrace("done processing");
            }

             using (var scope = new ActivityScope("Nested Operation 2")) {
                await Task.Delay(TimeSpan.FromSeconds(1));
                _logger.LogTrace("done processing");
            }
        }
    }
}
