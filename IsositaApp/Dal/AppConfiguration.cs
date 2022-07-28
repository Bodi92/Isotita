using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class AppConfiguration
    {
        private string sqlConnectionString;
        public string SqlConnectionString { get { return sqlConnectionString; } private set { sqlConnectionString = value; } }
        public AppConfiguration()
        {
            var configBulider = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBulider.AddJsonFile(path);
            var root = configBulider.Build();
            var appSetting = root.GetSection("ConnectionStrings:DefaultConnection");
            sqlConnectionString = appSetting.Value;
        }
    }
}
