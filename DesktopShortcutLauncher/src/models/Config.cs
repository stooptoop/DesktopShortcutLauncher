namespace DesktopShortcutLauncher
{
    public partial class Config
    {
        public Config(
            List<string>? directoryPaths = null,
            WindowLayout? layout = null,
            Theme? theme = null
        )
        {
            DirectoryPaths = directoryPaths ?? DirectoryPaths;
            Layout = layout ?? Layout;
            Theme = theme ?? Theme;
        }

        public List<string> DirectoryPaths { get; } = [
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        ];
        public WindowLayout Layout { get; } = new WindowLayout();
        public Theme Theme { get; } = new Theme();
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

    public class Theme
    {
        public Theme(
            string? background = null,
            string? onBackground = null,
            string? mouseOverItemBackground = null,
            string? onMouseOverItemBackground = null,
            string? tabBackground = null,
            string? onTabBackground = null,
            string? selectedTabBackground = null,
            string? onSelectedTabBackground = null
        ) {
            Background = background ?? Background;
            OnBackground = onBackground ?? OnBackground;
            MouseOverItemBackground = mouseOverItemBackground ?? MouseOverItemBackground;
            OnMouseOverItemBackground = onMouseOverItemBackground ?? OnMouseOverItemBackground;
            TabBackground = tabBackground ?? TabBackground;
            OnTabBackground = onTabBackground ?? OnTabBackground;
            SelectedTabBackground = selectedTabBackground ?? SelectedTabBackground;
            OnSelectedTabBackground = onSelectedTabBackground ?? OnSelectedTabBackground;
        }

        public string Background { get; } = "#FF111111";
        public string OnBackground { get; } = "#FFF8F8F8";
        public string MouseOverItemBackground { get; } = "#FF000000";
        public string OnMouseOverItemBackground { get; } = "#FFF8F8F8";
        public string TabBackground { get; } = "#FF111111";
        public string OnTabBackground { get; } = "#FFF8F8F8";
        public string SelectedTabBackground { get; } = "#FF000000";
        public string OnSelectedTabBackground { get; } = "#FFF8F8F8";
    }

    public partial class Config
    {
        public static readonly Config DEFAULT = new Config();
    }
}
