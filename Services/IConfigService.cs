using System;

namespace DatingApp.API.Services
{
    public interface IConfigService
    {
        byte[] Token { get; }

        DateTime TokenExpirationDate { get; }
    }
}