using System.Diagnostics;

namespace DesktopShortcutLauncher
{
    internal interface ILauncherUseCase
    {
        public Result<Empty> Initialize();
        public List<ShortcutDirectory> GetLauncherDataSource();
        public Result<Empty> LaunchApp(ShortcutListItem item);
    }

    public class LauncherInteractor(
        ILauncherRepository Repository
    ) : ILauncherUseCase {
        private ILauncherRepository Repository { get; } = Repository;

        public LauncherInteractor() : this(new LauncherRepository()) { }

        public Result<Empty> Initialize()
        {
            return Repository.LoadConfig();
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
