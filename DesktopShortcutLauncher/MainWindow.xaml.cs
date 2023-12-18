using System.Windows;
using System.Windows.Controls;

namespace DesktopShortcutLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILauncherUseCase UseCase { get; } = new LauncherInteractor();

        public MainWindow()
        {
            InitializeComponent();

            this.Activated += (_, _) => UpdateWindowHeight();
            this.Deactivated += (sender, e) => this.WindowState = WindowState.Minimized;
            this.Closing += (sender, e) => Environment.Exit(0);

            InitializeLauncherApp();
        }

        private void InitializeLauncherApp()
        {
            try
            {
                UseCase.Initialize().Get();
            }
            catch (Exception ex)
            {
                ShowAlert($"Failed to initialize app: {ex.Message}");
            }
            LoadShortcutFiles();
        }

        private void LoadShortcutFiles()
        {
            var directories = UseCase.GetLauncherDataSource();
            SetLauncherDataSource(directories);
        }

        private void SetLauncherDataSource(List<ShortcutDirectory> source)
        {
            AppTab.ItemsSource = source;
        }

        private void UpdateWindowHeight()
        {
            // TODO: multiscreen & positioning
            var screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            var bottomMargin = 50;
            var heightRatio = 0.7;
            var minTop = Math.Max(0, screenHeight * (1.0 - heightRatio));
            var maxHeight = (screenHeight - minTop) - bottomMargin;
            this.Top = minTop;
            this.Height = maxHeight;
        }

        private void ShortcutList_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is ListView listView)
            {
                listView.SelectionChanged += (_, _) =>
                {
                    if (listView.SelectedItem != null)
                    {
                        var selectedShortcut = (ShortcutListItem)listView.SelectedItem;
                        this.WindowState = WindowState.Minimized;
                        try
                        {
                            UseCase.LaunchApp(selectedShortcut).Get();
                        }
                        catch (Exception ex)
                        {
                            ShowAlert($"Failed to start shortcut: {ex.Message}");
                        }
                        listView.UnselectAll();
                    }
                };
            }
        }

        private void ShowAlert(string message)
        {
            MessageBox.Show(this, message);
        }
    }
}
