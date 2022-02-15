using Microsoft.Extensions.Configuration;
using Miqo.EncryptedJsonConfiguration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StoredProcedureExecutor.Configurations
{
    public class ConfigurationFactory
    {
        private readonly string _configFolderPath;
        private readonly Dictionary<string, string> _envToFileNameDictianory;
        private IConfiguration _configuration;
        public ConfigurationFactory(string? defaultEnv = null)
        {
            _configFolderPath = Directory.GetCurrentDirectory();
            _envToFileNameDictianory = GetAllowedEnviroments();
            CurrentEnviroment = defaultEnv ?? _envToFileNameDictianory.First().Key;
            _configuration = BuildConfiguration(_envToFileNameDictianory[CurrentEnviroment]);
        }

        public List<string> AllowedEnviroments { get => _envToFileNameDictianory.Keys.ToList(); }

        public T CreateRequired<T>(string section) where T : class
        {
            return _configuration.GetSection(section).Get<T>() ?? throw new ArgumentNullException(section);
        }

        public string CurrentEnviroment { get; private set; }

        public void SetEnviroment(string env)
        {
            var fileName = _envToFileNameDictianory[env];
            CurrentEnviroment = env;
            _configuration = BuildConfiguration(fileName);
        }

        private Dictionary<string, string> GetAllowedEnviroments()
        {
            var envToFileNameDictianory = new Dictionary<string, string>();
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "appsettings.*.ejson");
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var env = fileName.Split('.')[1];
                envToFileNameDictianory.Add(env, fileName);
            }
            return envToFileNameDictianory;
        }

        private IConfiguration BuildConfiguration(string fileName)
        {
            var key = Encoding.UTF8.GetBytes("AXmWV9cHAQ8Ji+M5fldktsr0Hr+mvHes7ci0lC38xIc=");
            return new ConfigurationBuilder()
                .AddEncryptedJsonFile(fileName, key)
                .SetBasePath(_configFolderPath)
                //.AddJsonFile(fileName)
                .Build();
        }
    }
}
