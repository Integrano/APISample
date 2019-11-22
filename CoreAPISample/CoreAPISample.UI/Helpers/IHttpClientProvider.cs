using CoreAPISample.Core.Models;
using CoreAPISample.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreAPISample.UI.Helpers
{
    public interface IHttpClientProvider
    {
        /// <summary>
        /// Post an incident to tellsomeone api.
        /// </summary>
        /// <param name="model">Object of Authenticate to be posted.</param>
        /// <returns></returns>
        HttpResponseMessage PostAsJsonAsync(AuthenticateModel model);
    }
}
