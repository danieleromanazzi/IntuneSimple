using System;
using System.Windows;

namespace IntuneSimple
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon trayIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            trayIcon = new System.Windows.Forms.NotifyIcon()
            {
                Icon = IntuneSimple.Properties.Resources.AppIcon,
                ContextMenu = new System.Windows.Forms.ContextMenu(
                    new System.Windows.Forms.MenuItem[] 
                    {
                        new System.Windows.Forms.MenuItem("Copy Credentials to clipboard", CopyCredentials),
                        new System.Windows.Forms.MenuItem("-"), // Seperator
                        new System.Windows.Forms.MenuItem("Exit", Shutdown),
                        
                    }),
                Visible = true
            };
        }

        private void CopyCredentials(object sender, EventArgs e)
        {
            KeyboardHook.Start(Credential.Read());
        }

        private void Shutdown(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
