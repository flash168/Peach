using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace PeachPlayer
{
    public class ConfigStorage
    {
        private static ConfigStorage _instance;
        public static ConfigStorage Instance
        {
            get
            {
                if (_instance == null)
                    Load();
                return _instance;
            }
        }

        private static string SettingsPath { get { return Path.Combine(Loader.LocalAddData, "settings.json"); } }


        public AppConfig AppConfig { get; set; }

        public ConfigStorage()
        {
            if (!Directory.Exists(Loader.LocalAddData))
                Directory.CreateDirectory(Loader.LocalAddData);
            if (!File.Exists(SettingsPath))
                File.Create(SettingsPath);
            AppConfig = new AppConfig();
        }


        public static void Load()
        {
            if (_instance != null)
                return;

            JsonConvert.DefaultSettings = () =>
            new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new StringEnumConverter(), new VersionConverter() }
            };

            ConfigStorage storage = null;
            try
            {
                if (File.Exists(SettingsPath))
                {
                    storage = JsonConvert.DeserializeObject<ConfigStorage>(File.ReadAllText(SettingsPath, Encoding.UTF8));
                }
            }
            catch (System.Exception e)
            {
                // Log.Exception(e, "Fail load settings.");
            }

            if (storage == null)
            {
                storage = new ConfigStorage();
                // Log.Add("Settings not found, create new default settings file.");
            }

            _instance = storage;
        }

        public void Save()
        {
            var str = JsonConvert.SerializeObject(this);
            File.WriteAllText(SettingsPath, str, Encoding.UTF8);
            //Log.Add("Settings saved.");
        }

    }
}
