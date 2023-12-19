namespace DesktopShortcutLauncher
{
    public class ShortcutDirectory(
        string displayName,
        string directoryPath,
        List<ShortcutListItem> items
    )
    {
        public string DisplayName { get; } = displayName;
        public string DirectoryPath { get; } = directoryPath;
        public List<ShortcutListItem> Items { get; } = items;
    }
}
