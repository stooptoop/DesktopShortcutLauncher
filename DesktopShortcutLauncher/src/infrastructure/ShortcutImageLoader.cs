using System.Windows;
using Microsoft.WindowsAPICodePack.Shell;

namespace DesktopShortcutLauncher
{
    public class ShortcutImageLoader
    {
        public void LoadImagesAsynchronous(List<ShortcutListItem> items)
        {
            Task.Run(() => {
                foreach (ShortcutListItem item in items)
                {
                    try
                    {
                        using (var shFile = ShellFile.FromFilePath(item.ShortcutFilePath))
                        {
                            shFile.Thumbnail.FormatOption = ShellThumbnailFormatOption.IconOnly;
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                item.ImageSrc = shFile.Thumbnail.LargeBitmapSource;
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to load shortcut image. ${e}");
                    }
                }
            });
        }
    }
}
