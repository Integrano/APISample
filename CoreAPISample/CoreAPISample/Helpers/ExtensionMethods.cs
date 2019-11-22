using CoreAPISample.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPISample.API.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns users without password
        /// </summary>
        /// <param name="users">List of users to be returned without password</param>
        /// <returns>Users</returns>
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        /// <summary>
        /// Returns user without password
        /// </summary>
        /// <param name="user">Object of the user to be returned without password</param>
        /// <returns>Users</returns>
        public static User WithoutPassword(this User user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}
