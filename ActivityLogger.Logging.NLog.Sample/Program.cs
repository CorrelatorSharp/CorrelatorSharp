using System;
using System.Threading.Tasks;
using ActivityLogger.Correlation;

namespace ActivityLogger.Logging.NLog.Sample
{
    class Program
    {
        private static ILogger _logger;

        static void Main(string[] args)
        {
            ActivityLoggerConfiguration.Current.UseNLog();

            _logger = LogManager.GetLogger("NLogSample");

            using (var scope = new ActivityLogScope("Main Operation")) {
                _logger.LogTrace("preparing to do work");

                DoWork().Wait();
            }
        }

        private static async Task DoWork()
        {
            _logger.LogTrace("doing work");

            using (var scope = new ActivityLogScope("Nested Operation 1")) {
                await Task.Delay(TimeSpan.FromSeconds(1));
                _logger.LogTrace("done processing");
            }

             using (var scope = new ActivityLogScope("Nested Operation 2")) {
                await Task.Delay(TimeSpan.FromSeconds(1));
                _logger.LogTrace("done processing");
            }
        }
    }
}
