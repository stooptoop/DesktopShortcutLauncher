using System.Windows.Media;

namespace DesktopShortcutLauncher
{
    public class ShortcutListItem(string ShortcutName, string ShortcutFilePath, ImageSource ImageSrc)
    {
        public string ShortcutName { get; } = ShortcutName;
        public string ShortcutFilePath { get; } = ShortcutFilePath;
        public ImageSource ImageSrc { get; } = ImageSrc;
    }
}
