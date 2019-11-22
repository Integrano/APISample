using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPISample.UI.Models
{
    public class SerilogLoggerConfiguration
    {
        public LogEventLevel LogLevel { get; set; }
    }
}
