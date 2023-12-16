namespace DesktopShortcutLauncher
{
    public class Config(
        List<string> DirectoryPaths
    )
    {
        public List<string> DirectoryPaths { get; } = DirectoryPaths;
    }
}
