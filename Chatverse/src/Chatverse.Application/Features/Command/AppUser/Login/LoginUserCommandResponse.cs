using Chatverse.Application.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.AppUser.Login
{
    public record LoginUserCommandResponse
    {
        public TokenDto Token { get; set; }
    }
}
