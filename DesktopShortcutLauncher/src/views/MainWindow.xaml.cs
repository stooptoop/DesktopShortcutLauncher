using System.Windows;
using System.Windows.Controls;

namespace DesktopShortcutLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ILauncherViewModelObserver
    {
        public const int WINDOW_LAYOUT_BOTTOM_MARGIN = 50;
        private LauncherViewModel viewModel;

        public MainWindow()
        {
            viewModel = new LauncherViewModel(this);

            InitializeComponent();
            this.Activated += (_, _) => UpdateWindowLayout();   // for Screen is Changed
            this.Deactivated += (sender, e) => this.WindowState = WindowState.Minimized;
            this.Closing += (sender, e) => Environment.Exit(0);

            viewModel.Initialize();
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
                    if (listView.SelectedItem is ShortcutListItem item)
                    {
                        this.WindowState = WindowState.Minimized;
                        viewModel.LaunchApplication(item);
                        listView.UnselectAll();
                    }
                };
            }
        }

        public void OnShortcutDirectoriesUpdated(List<ShortcutDirectory> directories)
        {
            AppTab.ItemsSource = directories;
        }

        public void OnWindowLayoutConfigUpdated(WindowLayout layout)
        {
            UpdateWindowLayout();
        }

        public void OnShowableErrorReceived(string message)
        {
            MessageBox.Show(this, message);
        }
    }
}
