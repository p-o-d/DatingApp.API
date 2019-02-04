using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DatingApp.API.Services.Impl
{
    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _conf;

        public ConfigService(IConfiguration conf)
        {
            _conf = conf;
        }
        public byte[] Token => Encoding.UTF8.GetBytes(_conf.GetSection("AppSettings:Token").Value);

        public DateTime TokenExpirationDate => DateTime.Now.AddDays(int.Parse(_conf.GetSection("AppSettings:TokenExpiresInDays").Value));
    }
}