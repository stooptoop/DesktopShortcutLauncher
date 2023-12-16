using System.ComponentModel;
using System.Windows.Media;

namespace DesktopShortcutLauncher
{
    public class ShortcutListItem(
        string ShortcutName,
        string ShortcutFilePath,
        ImageSource? ImageSrc
    ) : INotifyPropertyChanged {
        public string ShortcutName { get; } = ShortcutName;
        public string ShortcutFilePath { get; } = ShortcutFilePath;

        private ImageSource? _ImageSrc = ImageSrc;
        public ImageSource? ImageSrc
        {
            get => _ImageSrc;
            set
            {
                _ImageSrc = value;
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
