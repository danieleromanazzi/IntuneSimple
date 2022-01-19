using IntuneSimple.Services;
using System;
using System.Windows;

namespace IntuneSimple
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon notifyIcon;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var service = new CredentialService();
            var notify = new NotifyService();

            notifyIcon = notify.Create();

            Scheduler.Run(async () =>
               await service.CredentialRequired(),
               TimeSpan.FromMilliseconds(1000));

            Scheduler.Run(async () =>
               await service.Expiration(notifyIcon),
               TimeSpan.FromMinutes(5));

            await service.Expiration(notifyIcon);
        }


    }
}
