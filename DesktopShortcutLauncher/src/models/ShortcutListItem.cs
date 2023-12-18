using System.ComponentModel;
using System.Windows.Media;

namespace DesktopShortcutLauncher
{
    public class ShortcutListItem(
        string shortcutName,
        string shortcutFilePath,
        ImageSource? imageSrc
    ) : INotifyPropertyChanged {
        public string ShortcutName { get; } = shortcutName;
        public string ShortcutFilePath { get; } = shortcutFilePath;

        private ImageSource? imageSrc = imageSrc;
        public ImageSource? ImageSrc
        {
            get => imageSrc;
            set
            {
                imageSrc = value;
                NotifyPropertyChanged(nameof(ImageSrc));
            }     
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
