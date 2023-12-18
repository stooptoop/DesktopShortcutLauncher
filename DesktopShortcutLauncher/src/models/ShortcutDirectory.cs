namespace DesktopShortcutLauncher
{
    public class ShortcutDirectory(
        string DisplayName,
        string DirectoryPath,
        List<ShortcutListItem> Items
    )
    {
        public string DisplayName { get; } = DisplayName;
        public string DirectoryPath { get; } = DirectoryPath;
        public List<ShortcutListItem> Items { get; } = Items;
    }
}
