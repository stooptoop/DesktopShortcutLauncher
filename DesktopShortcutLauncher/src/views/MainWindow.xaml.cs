using System.Windows;
using System.Windows.Controls;

namespace DesktopShortcutLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int WINDOW_LAYOUT_BOTTOM_MARGIN = 50;
        private LauncherViewModel viewModel;

        public MainWindow()
        {
            viewModel = new LauncherViewModel(this);
            viewModel.ShortcutDirectoriesUpdated += (self, directories) => self.AppTab.ItemsSource = directories;
            viewModel.WindowLayoutConfigUpdated += (self, _) => self.UpdateWindowLayout();

            InitializeComponent();
            this.Activated += (_, _) => UpdateWindowLayout();   // for Screen is Changed
            this.Deactivated += (sender, e) => this.WindowState = WindowState.Minimized;
            this.Closing += (sender, e) => Environment.Exit(0);

            InitializeLauncherApp();
        }

        private void InitializeLauncherApp()
        {
            try
            {
                viewModel.Initialize();
            }
            catch (Exception ex)
            {
                ShowAlert($"Failed to initialize app: {ex.Message}");
            }
            LoadShortcutFiles();
        }

        private void LoadShortcutFiles()
        {
            viewModel.RetrieveLauncherDataSource();
        }

        private void UpdateWindowLayout()
        {
            var screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            var heightRatio = viewModel.WindowLayout.HeightRatio;
            var top = Math.Max(0, screenHeight * (1.0 - heightRatio));

            this.Top = top;
            this.Left = viewModel.WindowLayout.X;
            this.Width = viewModel.WindowLayout.Width;
            this.Height = (screenHeight - top) - WINDOW_LAYOUT_BOTTOM_MARGIN;
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
                            viewModel.LaunchApplication(selectedShortcut);
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
