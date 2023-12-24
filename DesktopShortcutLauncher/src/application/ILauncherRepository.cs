namespace DesktopShortcutLauncher
{
    public interface ILauncherRepository
    {
        public Config LauncherConfig { get; }

        public Result<Config> LoadConfig();
        public List<ShortcutDirectory> RetrieveLauncherDataSource();
        public double GetScreenHeight();
    }
}
