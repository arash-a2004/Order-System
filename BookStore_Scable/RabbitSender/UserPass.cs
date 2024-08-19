using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSender
{
    public class UserPass
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        private readonly IConfiguration configuration;
        public UserPass(IConfiguration configuration1)
        {
            configuration = configuration1;
        }

        public void GetData()
        {
            UserName = configuration.GetSection("username").Value;
            Password = configuration.GetSection("password").Value;
        }
    }
}
