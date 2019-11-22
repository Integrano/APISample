using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CoreAPISample.Core.Logging
{
    public interface IMethodLoggerFactory : IDisposable
    {
        /// <summary>
        /// Returns the method logger that logs the information level logs for this method.
        /// </summary>
        /// <param name="methodInformation">Information about this method.</param>
        /// <param name="logger">Microsoft Extension logger object of the specified type.</param>
        IMethodLogger CreateMethodLogger(MethodBase methodInformation, ILogger logger);
    }
}
