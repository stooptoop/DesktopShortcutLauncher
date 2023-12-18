using System.IO;

namespace DesktopShortcutLauncher
{
    public class LauncherRepository(
        IConfigLoader LauncherConfigLoader
    ) : ILauncherRepository
    {
        private ShortcutImageLoader shortcutImageLoader = new ShortcutImageLoader();
        private IConfigLoader LauncherConfigLoader = LauncherConfigLoader;

        private Config launcherConfig = Config.DEFAULT;
        public Config LauncherConfig {
            get => launcherConfig;
            set => launcherConfig = value;
        }

        public LauncherRepository() : this(new ConfigLoader()) {}

        public List<ShortcutDirectory> RetrieveLauncherDataSource()
        {
            return CreateResources(LauncherConfig.DirectoryPaths);
        }

        public Result<Empty> LoadConfig()
        {
            try
            {
                launcherConfig = LauncherConfigLoader.Load().Get();
                if (!launcherConfig.DirectoryPaths.Any())
                {
                    launcherConfig.DirectoryPaths.AddRange(Config.DEFAULT.DirectoryPaths);
                }
                return new Result<Empty>.Success();
            }
            catch (Exception e)
            {
                launcherConfig = Config.DEFAULT;
                return new Result<Empty>.Failure(e);
            }
        }

        private List<ShortcutDirectory> CreateResources(List<string> directories)
        {
            var shortcutDirectories = new List<ShortcutDirectory>();
            var allItems = new List<ShortcutListItem>();

            foreach (var shortcutsDirectory in directories)
            {
                if (Directory.Exists(shortcutsDirectory))
                {
                    var items = new List<ShortcutListItem>();

                    DirectoryInfo di = new DirectoryInfo(shortcutsDirectory);
                    var files = CollectFileInfoList(di);
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

        private List<FileInfo> CollectFileInfoList(DirectoryInfo info)
        {
            var infos = new List<FileInfo>();

            var getShortcuts = (DirectoryInfo di) =>
            {
                var files = di.GetFiles("*.*").Where(s => s.Name.EndsWith(".lnk") || s.Name.EndsWith(".url"));
                infos.AddRange(files);
            };

            getShortcuts(info);
            if (info.FullName == Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
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
