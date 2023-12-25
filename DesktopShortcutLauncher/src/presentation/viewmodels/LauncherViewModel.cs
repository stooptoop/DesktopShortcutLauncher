using System.ComponentModel;

namespace DesktopShortcutLauncher
{
    public interface ILauncherViewModelObserver
    {
        public void OnShowableErrorReceived(string message);
    }

    public class LauncherViewModel : INotifyPropertyChanged
    {
        private ILauncherUseCase useCase;

        /**
         * View(implements ILauncherViewModelObserver) has a ViewModel instance.
         * To avoid cross-referencing, use WeakReference.
         */
        private WeakReference<ILauncherViewModelObserver> observerRef;

        private List<ShortcutDirectory> shortcutDirectories = new List<ShortcutDirectory>();
        public List<ShortcutDirectory> ShortcutDirectories
        {
            get => shortcutDirectories;
            set
            {
                shortcutDirectories = value;
                NotifyPropertyChanged(nameof(ShortcutDirectories));
            }
        }

        public WindowBound windowBound = new WindowBound();
        public WindowBound WindowBound
        {
            get => windowBound;
            set
            {
                windowBound = value;
                NotifyPropertyChanged(nameof(WindowBound));
            }
        }

        public Theme theme = Config.DEFAULT.Theme;
        public Theme Theme
        {
            get => theme;
            set
            {
                theme = value;
                NotifyPropertyChanged(nameof(Theme));
            }
        }

        public LauncherViewModel(
            ILauncherViewModelObserver observer,
            ILauncherUseCase useCase
        )
        {
            observerRef = new WeakReference<ILauncherViewModelObserver>(observer);
            this.useCase = useCase;
        }

        public LauncherViewModel(ILauncherViewModelObserver observer)
            : this(observer, new LauncherInteractor()) { }


        public void Initialize()
        {
            try
            {
                var source = useCase.Initialize().Get();
                ShortcutDirectories = source.ShortcutDirectories;
                WindowBound = source.WindowBound;
                Theme = source.Theme;
            }
            catch (Exception ex)
            {
                if (observerRef.TryGetTarget(out var observer))
                {
                    observer.OnShowableErrorReceived($"Failed to initialize app: {ex.Message}");
                }
            }
        }

        public void LaunchApplication(ShortcutListItem item)
        {
            try
            {
                useCase.LaunchApplication(item).Get();
            }
            catch (Exception ex)
            {
                if (observerRef.TryGetTarget(out var observer))
                {
                    observer.OnShowableErrorReceived($"Failed to start shortcut: {ex.Message}");
                }
            }
        }

        public void ResumeWindow()
        {
            WindowBound = useCase.ResumeWindow();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
