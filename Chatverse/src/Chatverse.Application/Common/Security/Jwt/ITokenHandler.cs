using Chatverse.Application.DTOs.Token;
using Chatverse.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Common.Security.Jwt
{
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(int minute, AppUser appUser);
    }
}
