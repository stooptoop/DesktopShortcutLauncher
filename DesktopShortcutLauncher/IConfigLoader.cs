using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DesktopShortcutLauncher
{
    public interface IConfigLoader
    {
        public Result<Config> Load();
    }
    public class ConfigLoader(
        string ConfigFilePath = ConfigLoader.CONFIG_FILE_PATH
    ): IConfigLoader {
        public const string CONFIG_FILE_PATH = "launcherConfig.json";
        private string ConfigFilePath { get; }  = ConfigFilePath;

        public Result<Config> Load()
        {
            if (File.Exists(ConfigFilePath))
            {
                try
                {
                    using (StreamReader file = new StreamReader(ConfigFilePath, Encoding.UTF8))
                    {
                        string jsonContent = file.ReadToEnd();
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
            else
            {
                return new Result<Config>.Failure(
                    new FileNotFoundException($"Failed to load Config file. \"{ConfigFilePath}\" is not found.")
                );
            }
        }
    }
}
