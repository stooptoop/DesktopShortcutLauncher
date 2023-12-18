namespace DesktopShortcutLauncher
{
    public delegate void ShotcutDirectoriesUpdatedDelegate(
        MainWindow window, List<ShortcutDirectory> directories);

    public delegate void WindowLayoutConfigUpdatedDelegate(MainWindow window, WindowLayout layout);

    public class LauncherViewModel
    {
        private ILauncherUseCase useCase;

        /**
         * Window has a ViewModel instance.
         * To avoid cross-referencing, use WeakReference.
         */
        private WeakReference<MainWindow> windowRef;

        private List<ShortcutDirectory> directories = new List<ShortcutDirectory>();
        private List<ShortcutDirectory> Directories
        {
            get => directories;
            set
            {
                directories = value;
                if (windowRef.TryGetTarget(out var window))
                {
                    ShortcutDirectoriesUpdated(window, directories);
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
                if (windowRef.TryGetTarget(out var window))
                {
                    WindowLayoutConfigUpdated(window, windowLayout);
                }
            }
        }

        public ShotcutDirectoriesUpdatedDelegate ShortcutDirectoriesUpdated = (_, _) => { };
        public WindowLayoutConfigUpdatedDelegate WindowLayoutConfigUpdated = (_, _) => { };

        public LauncherViewModel(
            MainWindow window,
            ILauncherUseCase useCase
        ) {
            this.windowRef = new WeakReference<MainWindow>(window);
            this.useCase = useCase;
        }

        public LauncherViewModel(MainWindow window)
            : this(window, new LauncherInteractor()){ }


        public void Initialize()
        {
            // Throwable
            var config = useCase.Initialize().Get();
            WindowLayout = config.Layout;
        }

        public void RetrieveLauncherDataSource()
        {
            Directories = useCase.RetrieveLauncherDataSource();
        }

        public void LaunchApplication(ShortcutListItem item)
        {
            // Throwable
            useCase.LaunchApplication(item).Get();
        }
    }
}
