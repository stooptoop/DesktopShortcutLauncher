using System.Windows;

namespace DesktopShortcutWatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DesktopShortcutWatcherApp app = new DesktopShortcutWatcherApp();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            app.run();
        }
    }
}
