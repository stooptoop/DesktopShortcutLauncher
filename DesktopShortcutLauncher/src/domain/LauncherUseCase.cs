using System.Diagnostics;

namespace DesktopShortcutLauncher
{
    public class LauncherInteractor(
        ILauncherRepository launcherRepository
    ) : ILauncherUseCase {
        private ILauncherRepository launcherRepository { get; } = launcherRepository;

        public LauncherInteractor() : this(new LauncherRepository()) { }

        public Result<Config> Initialize()
        {
            try
            {
                launcherRepository.LoadConfig();
                return new Result<Config>.Success(launcherRepository.LauncherConfig);
            }
            catch (Exception ex)
            {
                return new Result<Config>.Failure(ex);
            }
        }

        public List<ShortcutDirectory> GetLauncherDataSource()
        {
            return launcherRepository.GetShortcutDirectories();
        }

        public Result<Empty> LaunchApp(ShortcutListItem item)
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
    }
}
