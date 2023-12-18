namespace DesktopShortcutLauncher
{
    public interface IConfigLoader
    {
        public Result<Config> Load();
    }
}
