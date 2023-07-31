using Chatverse.UI.Services.Interfaces;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Chatverse.UI.Services.Concrete
{
    public class GetUsernameFromTokenService : IGetUsernameFromTokenService
    {
        public string GetUsername(string jwtToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtTokenBytes = jwtHandler.ReadToken(jwtToken) as JwtSecurityToken;
            var payload = jwtTokenBytes.Payload.SerializeToJson();

            // Payload JSON verisini çözümleyerek istediğimiz veriyi alıyoruz
            // Örnek olarak, username'in "username" anahtarından alınacağını varsayalım
            var jwtPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
            if (jwtPayload.TryGetValue("username", out var username))
            {
                return username;
            }
            return null;
        }
    }
}
