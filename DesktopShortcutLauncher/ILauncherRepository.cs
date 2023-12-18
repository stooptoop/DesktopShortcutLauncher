using System.IO;

namespace DesktopShortcutLauncher
{
    public interface ILauncherRepository
    {
        public Config LauncherConfig { get; }

        public Result<Empty> LoadConfig();
        public List<ShortcutDirectory> GetShortcutDirectories();
    }

    public class LauncherRepository(
        IConfigLoader LauncherConfigLoader
    ) : ILauncherRepository
    {
        private ShortcutImageLoader shortcutImageLoader = new ShortcutImageLoader();
        private IConfigLoader LauncherConfigLoader = LauncherConfigLoader;

        private Config launcherConfig = Config.DEFAULT;
        public Config LauncherConfig {
            get => this.launcherConfig;
            set => this.launcherConfig = value;
        }

        public LauncherRepository() : this(new ConfigLoader()) {}

        public List<ShortcutDirectory> GetShortcutDirectories()
        {
            return CreateShortcutsResource(LauncherConfig.DirectoryPaths);
        }

        public Result<Empty> LoadConfig()
        {
            try
            {
                launcherConfig = LauncherConfigLoader.Load().Get();
                return new Result<Empty>.Success();
            }
            catch (Exception e)
            {
                launcherConfig = Config.DEFAULT;
                return new Result<Empty>.Failure(e);
            }
        }

        private List<ShortcutDirectory> CreateShortcutsResource(
            List<string> srcDirs
        ) {
            var shortcutDirectories = new List<ShortcutDirectory>();
            var allItems = new List<ShortcutListItem>();

            foreach (var shortcutsDirectory in srcDirs)
            {
                if (Directory.Exists(shortcutsDirectory))
                {
                    var items = new List<ShortcutListItem>();

                    DirectoryInfo di = new DirectoryInfo(shortcutsDirectory);
                    var files = GetFileInfoList(di);
                    foreach (FileInfo file in files)
                    {
                        string filePath = file.FullName;
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(file.Name);
                        var item = new ShortcutListItem(fileName, filePath, null);
                        items.Add(item);
                    }

                    shortcutDirectories.Add(
                        new ShortcutDirectory(di.Name, di.FullName, items)
                    );
                    allItems.AddRange(items);
                }
            }

            shortcutImageLoader.LoadImagesAsynchronous(allItems);

            return shortcutDirectories;
        }

        private List<FileInfo> GetFileInfoList(DirectoryInfo dirInfo)
        {
            List<FileInfo> infos = new List<FileInfo>();

            var getShortcuts = (DirectoryInfo di) =>
            {
                var files = di.GetFiles("*.*").Where(s => s.Name.EndsWith(".lnk") || s.Name.EndsWith(".url"));
                infos.AddRange(files);
            };

            getShortcuts(dirInfo);
            if (dirInfo.FullName == Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
            {
                DirectoryInfo commonDesktopInfo = new DirectoryInfo(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory)
                );
                getShortcuts(commonDesktopInfo);
                infos.Sort((x, y) => string.Compare(x.Name, y.Name));
            }
            return infos;
        }
    }
}
