using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace CoreAPISample.Core.Logging
{
    public class MethodLogger : IMethodLogger
    {
        private readonly Stopwatch _stopwatch;
        private readonly MethodBase _methodInformation;
        private readonly ILogger _logger;

        public MethodLogger(MethodBase methodInformation, ILogger logger)
        {
            _stopwatch = Stopwatch.StartNew();
            _methodInformation = methodInformation;
            _logger = logger;
            logger.LogInformation($"Entering {methodInformation.Name}");
        }

        /// <summary>
        /// Disposes the object of the method logger
        /// </summary>
        public void Dispose()
        {
            _stopwatch.Stop();
            _logger.LogInformation($"Leaving {_methodInformation.Name}. Execution Time: {_stopwatch.Elapsed.Milliseconds} ms");
        }
    }
}
