using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace IntuneSimple
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.MenuItem credentialsItem;
        private static string intuneFile = @"c:\intune\localadm.txt";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            credentialsItem = new System.Windows.Forms.MenuItem("Copy Credentials to clipboard", CopyCredentials);

            trayIcon = new System.Windows.Forms.NotifyIcon()
            {
                Icon = IntuneSimple.Properties.Resources.AppIcon,
                ContextMenu = new System.Windows.Forms.ContextMenu(
                    new System.Windows.Forms.MenuItem[] 
                    {
                        new System.Windows.Forms.MenuItem("Exit", Shutdown)
                    }),
                Visible = true
            };

            trayIcon.MouseClick += (s, d) =>
            {

            };

            trayIcon.ContextMenu.Popup += (s, d) =>
            {
                if (File.Exists(intuneFile) &&
                    !trayIcon.ContextMenu.MenuItems.Contains(credentialsItem))
                {
                    trayIcon.ContextMenu.MenuItems.Add(credentialsItem);
                }
                else if (trayIcon.ContextMenu.MenuItems.Contains(credentialsItem))
                {
                    trayIcon.ContextMenu.MenuItems.Remove(credentialsItem);
                }
            };
            
        }

        private void CopyCredentials(object sender, EventArgs e)
        {
            Clipboard.SetText("admin");
            AddA();
        }

        private void Shutdown(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private void AddA()
        {
            var ptr = FindWindowByCaption(IntPtr.Zero, "Nuovo documento di testo (2).txt - Blocco Note");
            SetForegroundWindow(ptr);

            System.Windows.Forms.SendKeys.SendWait(Clipboard.GetText());
        }

    }
}
