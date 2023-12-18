namespace DesktopShortcutLauncher
{
    public class TabControlData(
        string title,
        ShortcutDirectory directory
    )
    {
        public string Title { get; } = title;
        public ShortcutDirectory Directory { get; } = directory;
    }
}
