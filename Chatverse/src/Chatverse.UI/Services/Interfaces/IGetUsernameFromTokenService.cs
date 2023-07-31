using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Chatverse.UI.Services.Interfaces
{
    public interface IGetUsernameFromTokenService
    {
        public string GetUsername(string jwtToken);
    }
}
