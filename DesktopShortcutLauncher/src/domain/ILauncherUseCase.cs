namespace DesktopShortcutLauncher
{
    public interface ILauncherUseCase
    {
        public Result<Config> Initialize();
        public List<ShortcutDirectory> GetLauncherDataSource();
        public Result<Empty> LaunchApp(ShortcutListItem item);
    }
}
