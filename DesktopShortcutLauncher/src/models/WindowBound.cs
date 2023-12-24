namespace DesktopShortcutLauncher.src.models
{
    public class WindowBound
    {
        public const int WINDOW_LAYOUT_BOTTOM_MARGIN = 50;

        public double Top { get; }
        public double Left { get; }
        public double Width { get; }
        public double Height { get; }

        public WindowBound(WindowLayout layout, double screenHeight)
        {
            var heightRatio = layout.HeightRatio;
            var top = Math.Min(screenHeight, Math.Max(0, screenHeight * (1.0 - heightRatio)));

            Top = top;
            Left = layout.X;
            Width = layout.Width;
            Height = (screenHeight - top) - WINDOW_LAYOUT_BOTTOM_MARGIN;
        }

        public WindowBound() : this(Config.DEFAULT.Layout, 1080) { }
    }
}
