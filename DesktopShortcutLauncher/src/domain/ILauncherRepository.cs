namespace DesktopShortcutLauncher
{
    public interface ILauncherRepository
    {
        public Config LauncherConfig { get; }

        public Result<Empty> LoadConfig();
        public List<ShortcutDirectory> GetShortcutDirectories();
    }
}
