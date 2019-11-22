using CoreAPISample.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPISample.API.Helpers
{
    public interface IAppHelper
    {

        /// <summary>
        /// Authenticates user against specified usename and password
        /// </summary>
        /// <param name="userName">UserName to be matched for authenticating specified user</param>
        /// <param name="password">Password to be matched for authenticating specified user</param>
        /// <returns>User</returns>
        User Authenticate(string userName, string password);
    }
}
