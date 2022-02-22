using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Miqo.EncryptedJsonConfiguration;

namespace StoredProcedureExecutor.Configurations
{
    public class ConfigurationFactory
    {
        private const string EnvFileNamePattern = "appsettings.*.ejson";
        private const string PrivateKey = "AXmWV9cHAQ8Ji+M5fldktsr0Hr+mvHes7ci0lC38xIc=";
        private const string FileNameSeparator = ".";
        private const int EnvNameInFileNameIndex = 1;

        private readonly string _configFolderPath;
        private readonly Dictionary<string, string> _envToFileNameDictionary;
        private IConfiguration _configuration;

        public ConfigurationFactory(string? defaultEnv = null)
        {
            _configFolderPath = Directory.GetCurrentDirectory();
            _envToFileNameDictionary = GetAllowedEnvironments();
            CurrentEnvironment = defaultEnv ?? _envToFileNameDictionary.First().Key;
            _configuration = BuildConfiguration(_envToFileNameDictionary[CurrentEnvironment]);
        }

        public List<string> AllowedEnvironments => _envToFileNameDictionary.Keys.ToList();

        public T CreateRequired<T>(string section) where T : class
        {
            return _configuration.GetSection(section).Get<T>() ?? throw new ArgumentNullException(section);
        }

        public string CurrentEnvironment { get; private set; }

        public void SetEnvironment(string env)
        {
            var fileName = _envToFileNameDictionary[env];
            CurrentEnvironment = env;
            _configuration = BuildConfiguration(fileName);
        }

        private static Dictionary<string, string> GetAllowedEnvironments()
        {
            var envToFileNameDictionary = new Dictionary<string, string>();
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), EnvFileNamePattern);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var env = fileName.Split(FileNameSeparator)[EnvNameInFileNameIndex];
                envToFileNameDictionary.Add(env, fileName);
            }

            return envToFileNameDictionary;
        }

        private IConfiguration BuildConfiguration(string fileName)
        {
            var key = Encoding.UTF8.GetBytes(PrivateKey);
            return new ConfigurationBuilder()
                .AddEncryptedJsonFile(fileName, key)
                .SetBasePath(_configFolderPath)
                .Build();
        }
    }
}