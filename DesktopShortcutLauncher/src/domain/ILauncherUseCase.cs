using DesktopShortcutLauncher.src.models;

namespace DesktopShortcutLauncher
{
    public interface ILauncherUseCase
    {
        public Result<LauncherDataSource> Initialize();
        public Result<Empty> LaunchApplication(ShortcutListItem item);
        public WindowBound ResumeWindow();
    }
}
