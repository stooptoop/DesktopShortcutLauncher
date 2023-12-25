using System.Diagnostics;

namespace DesktopShortcutLauncher
{
    public class LauncherInteractor(
        ILauncherRepository launcherRepository
    ) : ILauncherUseCase {
        private ILauncherRepository launcherRepository { get; } = launcherRepository;

        public LauncherInteractor() : this(new LauncherRepository()) { }

        public Result<LauncherDataSource> Initialize()
        {
            try
            {
                var config = launcherRepository.LoadConfig().Get();
                var directories = launcherRepository.RetrieveLauncherDataSource();
                var bound = new WindowBound(config.Layout, launcherRepository.GetScreenHeight());
                return new Result<LauncherDataSource>.Success(
                    new LauncherDataSource(directories, bound, config.Theme));
            }
            catch (Exception ex)
            {
                return new Result<LauncherDataSource>.Failure(ex);
            }
        }

        public Result<Empty> LaunchApplication(ShortcutListItem item)
        {
            try
            {
                var proc = new Process();
                proc.StartInfo.FileName = item.ShortcutFilePath;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
                return new Result<Empty>.Success();
            }
            catch (Exception ex)
            {
                return new Result<Empty>.Failure(ex);
            }
        }

        public WindowBound ResumeWindow()
        {
            return new WindowBound(launcherRepository.LauncherConfig.Layout, launcherRepository.GetScreenHeight());
        }
    }
}
