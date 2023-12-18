namespace DesktopShortcutLauncher
{
    public partial class Config
    {
        public Config(
            List<string>? directoryPaths = null,
            WindowLayout? layout = null
        )
        {
            DirectoryPaths = directoryPaths ?? DirectoryPaths;
            Layout = layout ?? Layout;
        }

        public List<string> DirectoryPaths { get; } = [
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        ];
        public WindowLayout Layout { get; } = new WindowLayout();
    }

    public class WindowLayout
    {
        public WindowLayout(
            int? x = null,
            int? width = null,
            double? heightRatio = null
        ) {
            X = x ?? X;
            Width = width ?? Width;
            HeightRatio = heightRatio ?? HeightRatio;
        }

        public int X { get; } = 50;
        public int Width { get; } = 400;
        public double HeightRatio { get; } = 0.5;
    }

    public partial class  Config
    {
        public static readonly Config DEFAULT = new Config();
    }
}
