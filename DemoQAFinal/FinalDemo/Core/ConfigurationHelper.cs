using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FinalDemo.Core
{
    public static class ConfigurationHelper

    {
        private static IConfigurationRoot _config = null;

        public static IConfiguration ReadConfiguration(string path)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path)
                .Build();
            _config = config;
            return config;
        }
        public static IConfigurationRoot GetConfiguration()
        {
            // var value = config[key];
            // if(!string.IsNullOrEmpty(value)) return value;
            // var message = $"Attribute [{key}] has not been set in appsetting.";
            // throw new InvalidDataException(message);
            return _config;
        }
    }
}