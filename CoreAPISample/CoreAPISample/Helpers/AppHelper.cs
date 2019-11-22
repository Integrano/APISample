using CoreAPISample.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPISample.API.Helpers
{
    public class AppHelper : IAppHelper
    {
        private readonly AppSettings _appSettings;
        private readonly EncryptionHelper _encryptionHelper = new EncryptionHelper();

        public AppHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Authenticates user login
        /// </summary>
        /// <param name="userName">UserName to be matched for authenticating specified user</param>
        /// <param name="password">Password to be matched for authenticating specified user</param>
        /// <returns>User</returns>
        public User Authenticate(string userName, string password)
        {
            var user = new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin };

            var passwordDecrypted = _encryptionHelper.GetDecryptedValue(password);

            var matched = user.Username == userName && user.Password == passwordDecrypted;

            // return null if user not matched
            if (!matched)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }
    }
}
