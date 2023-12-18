using System.Diagnostics;

namespace DesktopShortcutLauncher
{
    public class LauncherInteractor(
        ILauncherRepository Repository
    ) : ILauncherUseCase {
        private ILauncherRepository Repository { get; } = Repository;

        public LauncherInteractor() : this(new LauncherRepository()) { }

        public Result<Config> Initialize()
        {
            try
            {
                Repository.LoadConfig();
                return new Result<Config>.Success(Repository.LauncherConfig);
            }
            catch (Exception ex)
            {
                return new Result<Config>.Failure(ex);
            }
        }

        public List<ShortcutDirectory> GetLauncherDataSource()
        {
            return Repository.GetShortcutDirectories();
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
