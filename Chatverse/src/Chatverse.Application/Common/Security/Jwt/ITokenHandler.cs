namespace Chatverse.Application.Common.Security.Jwt;
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(int minute, AppUser appUser);
    }
