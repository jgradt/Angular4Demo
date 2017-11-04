using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Infrastructure
{
    public class AppConfig
    { 

        private readonly IConfiguration configuration;

        public AppConfig(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public JwtConfigurationOptions JwtOptions
        {
            get
            {
                return configuration.GetSection("Jwt").Get<JwtConfigurationOptions>();
            }
        }
    }
}
