using System.Windows;
using System.Windows.Controls;
using DesktopShortcutLauncher.src.presentation.viewmodels;

namespace DesktopShortcutLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ILauncherViewModelObserver
    {
        private LauncherViewModel viewModel;

        public MainWindow()
        {
            viewModel = new LauncherViewModel(this);
            this.DataContext = viewModel;

            InitializeComponent();
            this.Activated += (_, _) => viewModel.ResumeWindow();   // for Screen is Changed
            this.Deactivated += (_, _) => this.WindowState = WindowState.Minimized;
            this.Closing += (_, _) => Environment.Exit(0);

            viewModel.Initialize();
        }

        private void ShortcutList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is ListView listView)
            {
                if (listView.SelectedItem is ShortcutListItem item)
                {
                    this.WindowState = WindowState.Minimized;
                    viewModel.LaunchApplication(item);
                    listView.UnselectAll();
                }
            }
        }

        public void OnShowableErrorReceived(string message)
        {
            MessageBox.Show(this, message);
        }
    }
}
