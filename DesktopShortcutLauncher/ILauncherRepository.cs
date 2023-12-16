using System.IO;
using Microsoft.WindowsAPICodePack.Shell;

namespace DesktopShortcutLauncher
{
    public interface ILauncherRepository
    {
        public List<ShortcutDirectory> GetShortcutDirectories();
    }

    public class LauncherRepository : ILauncherRepository
    {
        public LauncherRepository() { }

        public List<ShortcutDirectory> GetShortcutDirectories()
        {
            var paths = GetSchortcutDirectoryPaths();
            return CreateShortcutsResource(paths);
        }

        private List<ShortcutDirectory> CreateShortcutsResource(
            List<string> srcDirs
        ) {
            var shortcutDirectoryList = new List<ShortcutDirectory>();

            foreach (var shortcutsDirectory in srcDirs)
            {
                if (Directory.Exists(shortcutsDirectory))
                {
                    var items = new List<ShortcutListItem>();

                    DirectoryInfo di = new DirectoryInfo(shortcutsDirectory);
                    var fileList = GetFileInfoList(di);
                    foreach (FileInfo file in fileList)
                    {
                        string filePath = file.FullName;
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(file.Name);
                        using (var shFile = ShellFile.FromFilePath(filePath))
                        {
                            shFile.Thumbnail.FormatOption = ShellThumbnailFormatOption.IconOnly;
                            var imageSrc = shFile.Thumbnail.LargeBitmapSource;
                            var item = new ShortcutListItem(fileName, filePath, imageSrc);
                            items.Add(item);
                        }
                    }

                    shortcutDirectoryList.Add(
                        new ShortcutDirectory(di.Name, di.FullName, items)
                    );
                }
            }
            return shortcutDirectoryList;
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

        private List<string> GetSchortcutDirectoryPaths()
        {
            return [
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            ];
        }
    }
}
