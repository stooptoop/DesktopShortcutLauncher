using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopShortcutLauncher
{
    public class TabControlData(
        string Title,
        ShortcutDirectory Directory
    )
    {
        public string Title { get; } = Title;
        public ShortcutDirectory Directory { get; } = Directory;
    }
}
