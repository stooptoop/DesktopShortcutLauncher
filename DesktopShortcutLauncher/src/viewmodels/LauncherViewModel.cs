namespace DesktopShortcutLauncher
{
    public interface ILauncherViewModelObserver
    {
        public void OnShortcutDirectoriesUpdated(List<ShortcutDirectory> directories);
        public void OnWindowLayoutConfigUpdated(WindowLayout layout);
        public void OnShowableErrorReceived(string message);
    }

    public class LauncherViewModel
    {
        private ILauncherUseCase useCase;

        /**
         * View(implements ILauncherViewModelObserver) has a ViewModel instance.
         * To avoid cross-referencing, use WeakReference.
         */
        private WeakReference<ILauncherViewModelObserver> observerRef;

        private List<ShortcutDirectory> directories = new List<ShortcutDirectory>();
        private List<ShortcutDirectory> Directories
        {
            get => directories;
            set
            {
                directories = value;
                if (observerRef.TryGetTarget(out var observer))
                {
                    observer.OnShortcutDirectoriesUpdated(directories);
                }
            }
        }

        public WindowLayout windowLayout = Config.DEFAULT.Layout;
        public WindowLayout WindowLayout
        {
            get => windowLayout;
            set
            {
                windowLayout = value;
                if (observerRef.TryGetTarget(out var observer))
                {
                    observer.OnWindowLayoutConfigUpdated(windowLayout);
                }
            }
        }

        public LauncherViewModel(
            ILauncherViewModelObserver observer,
            ILauncherUseCase useCase
        ) {
            this.observerRef = new WeakReference<ILauncherViewModelObserver>(observer);
            this.useCase = useCase;
        }

        public LauncherViewModel(ILauncherViewModelObserver observer)
            : this(observer, new LauncherInteractor()){ }


        public void Initialize()
        {
            try
            {
                var config = useCase.Initialize().Get();
                WindowLayout = config.Layout;
            }
            catch (Exception ex)
            {
                if (observerRef.TryGetTarget(out var observer))
                {
                    observer.OnShowableErrorReceived($"Failed to initialize app: {ex.Message}");
                }
            }
        }

        public void RetrieveLauncherDataSource()
        {
            Directories = useCase.RetrieveLauncherDataSource();
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
    }
}
