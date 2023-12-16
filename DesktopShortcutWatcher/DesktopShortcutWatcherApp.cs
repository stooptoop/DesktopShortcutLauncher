using System.Diagnostics;
using System.IO;
using Microsoft.Toolkit.Uwp.Notifications;

class DesktopShortcutWatcherApp
{
    private FileSystemWatcher watcher = new FileSystemWatcher();

    public void run()
    {
        string srcDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string shortcutsDir = srcDir + @"\Shortcuts";

        if (!Directory.Exists(shortcutsDir))
        {
            Directory.CreateDirectory(shortcutsDir);
        }

        watcher.Path = srcDir;
        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
        watcher.Filter = "*.lnk";
        watcher.Created += (sender, e) =>
        {
            Thread.Sleep(1000);
            string filePath = e.FullPath;
            string fileName = Path.GetFileName(filePath);
            if (IsShortcut(filePath))
            {
                string dstPath = Path.Combine(shortcutsDir, fileName);
                if (!File.Exists(dstPath))
                {
                    File.Move(filePath, dstPath);
                    NotificationSender.Send("Moved shortcut: {fileName}");
                }
            }
        };
        watcher.EnableRaisingEvents = true;
        NotificationSender.Send("Start DesktopShortcutWatcherApp.");
    }

    private bool IsShortcut(string filePath)
    {
        return Path.GetExtension(filePath).Equals(".lnk", StringComparison.OrdinalIgnoreCase);
    }
}

class NotificationSender
{
    public static void Send(string message)
    {
        new ToastContentBuilder()
            .AddText(message)
            .AddButton("OK", ToastActivationType.Protocol, "")
            .Show();

    }
}
