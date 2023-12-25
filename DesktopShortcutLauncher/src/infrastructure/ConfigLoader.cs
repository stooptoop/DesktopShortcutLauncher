using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DesktopShortcutLauncher
{
    public class ConfigLoader(
        string configFilePath = ConfigLoader.CONFIG_FILE_PATH
    ): IConfigLoader {
        public const string CONFIG_FILE_PATH = "launcherConfig.json";
        private string configFilePath { get; }  = configFilePath;

        public Result<Config> Load()
        {
            if (!File.Exists(configFilePath))
            {
                return new Result<Config>.Failure(
                    new FileNotFoundException($"Failed to load Config file. \"{configFilePath}\" is not found.")
                );
            }

            try
            {
                using (var file = new StreamReader(configFilePath, Encoding.UTF8))
                {
                    var jsonContent = file.ReadToEnd();
                    var config = JsonConvert.DeserializeObject<Config>(jsonContent);
                    if (config == null)
                    {
                        throw new NullReferenceException("Failed to load Config file. Config is null.");
                    }
                    return new Result<Config>.Success(config);
                }
            }
            catch (Exception ex)
            {
                return new Result<Config>.Failure(ex);
            }
        }
    }
}
