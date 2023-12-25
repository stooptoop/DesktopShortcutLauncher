namespace DesktopShortcutLauncher
{
    public class LauncherDataSource
    {
        public LauncherDataSource(List<ShortcutDirectory> shortcutDirectories, WindowBound windowBound, Theme theme)
        {
            ShortcutDirectories = shortcutDirectories;
            WindowBound = windowBound;
            Theme = theme;
        }

        public List<ShortcutDirectory> ShortcutDirectories { get; }
        public WindowBound WindowBound { get; }
        public Theme Theme { get; }
    }
}
