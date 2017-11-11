using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Infrastructure.Configuration
{
    public class AppConfig
    { 

        private readonly IConfiguration configuration;

        public AppConfig(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AppSettings Settings
        {
            get
            {
                return configuration.GetSection("App").Get<AppSettings>();
            }
        }
    }
}
