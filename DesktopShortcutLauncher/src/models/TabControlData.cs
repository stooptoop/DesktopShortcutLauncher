using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
