namespace DesktopShortcutLauncher
{
    public interface ILauncherUseCase
    {
        public Result<Config> Initialize();
        public List<ShortcutDirectory> RetrieveLauncherDataSource();
        public Result<Empty> LaunchApplication(ShortcutListItem item);
    }
}
