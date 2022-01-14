using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
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
                        new System.Windows.Forms.MenuItem("Exit", Shutdown),
                    }),
                Visible = true
            };

            Scheduler.Run(async () =>
               await CredentialRequired(),
               TimeSpan.FromMilliseconds(500));

            Scheduler.Run(async () =>
               await Expiration(),
               TimeSpan.FromSeconds(5));
        }

        public async Task Expiration()
        {
            credential = Credential.Read();
            if (credential.ExpiredDate.AddMinutes(-10) < DateTime.Now)
            {
                trayIcon.ShowBalloonTip(5000, "Account amministratore", "L'account amministratore scadrà tra 10 minuti.", System.Windows.Forms.ToolTipIcon.Warning);
            }
        }

        Credential credential = null;
        bool status = false;
        public async Task CredentialRequired()
        {
            if (await Api.Opened() && !status)
            {
                status = true;
                Thread.Sleep(2000);
                var rect = await Api.RectCredentialView();

                var thread = new Thread(new ThreadStart(delegate
                {
                    var main = new MainWindow
                    {
                        Top = rect.Bottom,
                        Left = rect.Left,
                        Width = rect.Right - rect.Left,
                        Topmost = true
                    };
                    Scheduler.Run(async () =>
                       await Closing(main),
                       TimeSpan.FromMilliseconds(500));

                    main.ShowDialog();
                }));
                thread.CurrentCulture =
                    thread.CurrentUICulture = CultureInfo.CurrentCulture;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
        }

        public async Task Closing(Window view)
        {
            if (!await Api.Opened() && status)
            {
                view.Dispatcher.Invoke(() => view.Visibility = Visibility.Hidden,
                    System.Windows.Threading.DispatcherPriority.Render);

                status = false;
            }
        }

        private void Shutdown(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
