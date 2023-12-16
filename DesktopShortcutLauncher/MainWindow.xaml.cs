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

            this.Deactivated += (sender, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };
            this.Closing += (sender, e) =>
            {
                Environment.Exit(0);
            };            

            LoadShortcutFiles();
        }

        private void LoadShortcutFiles()
        {
            var srcList = UseCase.GetLauncherDataSource();
            SetLauncherDataSource(srcList);
            UpdateWindowHeight();
        }

        private void SetLauncherDataSource(List<ShortcutDirectory> source)
        {
            AppTab.ItemsSource = source;
        }

        private void UpdateWindowHeight()
        {
            var screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            var verticalMargin = 50;
            var minTop = screenHeight / 2;
            var maxHeight = screenHeight / 2 - verticalMargin;
            // 
            // var estimatedHeight = 84 * itemCount + 4;
            // 
            // if (estimatedHeight > maxHeight)
            // {
            //     this.Top = minTop;
            //     this.Height = maxHeight;
            // }
            // else
            // {
            //     this.Top = screenHeight - estimatedHeight - verticalMargin;
            //     this.Height = estimatedHeight;
            // }
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
                            MessageBox.Show(this, "Failed to start shortcut: " + ex.Message);
                        }
                        listView.UnselectAll();
                    }
                };
            }
        }
    }
}
