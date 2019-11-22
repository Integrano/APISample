using System.Reflection;
using Microsoft.Extensions.Logging;

namespace CoreAPISample.Core.Logging
{
    public class MethodLoggerFactory : IMethodLoggerFactory
    {
        private IMethodLogger _methodLogger;

        /// <summary>
        /// Returns the method logger that logs the information level logs for this method.
        /// </summary>
        /// <param name="methodInformation">Information about this method.</param>
        /// <param name="logger">Microsoft Extension logger object of the specified type.</param>
        public IMethodLogger CreateMethodLogger(MethodBase methodInformation, ILogger logger)
        {
            _methodLogger = new MethodLogger(methodInformation, logger);
            return _methodLogger;
        }

        /// <summary>
        /// Disposes the object of the method logger
        /// </summary>
        public void Dispose()
        {
            _methodLogger?.Dispose();
        }
    }
}
