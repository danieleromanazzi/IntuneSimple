using System;
using System.Windows;

namespace IntuneSimple.Services
{
    public class NotifyService
    {
        public System.Windows.Forms.NotifyIcon Create()
        {
            return new System.Windows.Forms.NotifyIcon()
            {
                Icon = IntuneSimple.Properties.Resources.AppIcon,
                ContextMenu = new System.Windows.Forms.ContextMenu(
                    new System.Windows.Forms.MenuItem[]
                    {
                        new System.Windows.Forms.MenuItem("Exit", Shutdown),
                    }),
                Visible = true
            };
        }

        private void Shutdown(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
