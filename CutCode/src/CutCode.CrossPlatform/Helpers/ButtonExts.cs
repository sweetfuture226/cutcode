using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;

namespace CutCode.CrossPlatform.Helpers
{
    public static class UrlUtils
    {
        public static void OpenUrl(string url)
        {
            if (url == null)
                return;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                startInfo.FileName = "powershell";
                startInfo.Arguments = "start \"" + url + "\"";
                Process.Start(startInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                startInfo.FileName = ("xdg-open");
                startInfo.UseShellExecute = false;
                startInfo.Arguments = (url);
                Process.Start(startInfo);
            }
            else
            {
                if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return;
                startInfo.FileName = "open";
                startInfo.Arguments = url ?? "";
                Process.Start(startInfo);
            }
        }
    }
    
    public class ButtonExts : AvaloniaObject
    {
        public static readonly AttachedProperty<string> UrlProperty = AvaloniaProperty.RegisterAttached<ButtonExts, Button, string>("Url");

        public static string GetUrl(Button button) => button.GetValue<string>((StyledPropertyBase<string>) UrlProperty);

        public static void SetUrl(Button button, string url) => button.SetValue<string>((StyledPropertyBase<string>) UrlProperty, url, BindingPriority.LocalValue);

        static ButtonExts() => UrlProperty.Changed.Subscribe<AvaloniaPropertyChangedEventArgs<string>>((Action<AvaloniaPropertyChangedEventArgs<string>>) (e1 =>
        {
            Button btn = e1.Sender as Button;
            if (btn == null)
                return;
            btn.Click += (EventHandler<RoutedEventArgs>) ((s, e2) =>
            {
                if (string.IsNullOrEmpty(GetUrl(btn)))
                    return;
                UrlUtils.OpenUrl(GetUrl(btn));
            });
        }));
    }
}
