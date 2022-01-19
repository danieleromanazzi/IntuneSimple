using IntuneSimple.Views;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static IntuneSimple.WinApi;

namespace IntuneSimple.Services
{
    public class CredentialService
    {
        public async Task Expiration(System.Windows.Forms.NotifyIcon notifyIcon)
        {
            try
            {
                var credential = Credential.Read();
                if (credential.ExpiredDate.AddMinutes(Settings.AlertMinuteExpired) < DateTime.Now)
                {
                    notifyIcon.ShowBalloonTip(Settings.AlertExpiredEveryMilliseconds, "Account amministratore",
                        $"L'account amministratore è in scadenza. Scadrà alle {credential.ExpiredDate.ToLongTimeString()}",
                        System.Windows.Forms.ToolTipIcon.Warning);
                }

                notifyIcon.Text = $"L'account amministratore scadrà alle {credential.ExpiredDate.ToLongTimeString()}";
            }
            catch (FileNotFoundException)
            {
                notifyIcon.Text = "Abilitare l'utenza amministratore tramite il portale aziendale.";
            }
            catch (Exception ex)
            {
                notifyIcon.Text = "";
            }
        }

        bool status = false;
        public async Task CredentialRequired()
        {
            RectApi rect = new RectApi();

            if (await WinApi.Opened() && !status)
            {
                status = true;

                Thread.Sleep(Settings.OpenedUacAttemptMilleseconds);

                rect = await WinApi.GetUACRect();

                //Default Bottom 817 Left 732, Right 1188, Top 352
                var thread = new Thread(new ThreadStart(delegate
                {
                    var main = new CredentialView
                    {
                        Top = rect.Bottom,
                        Left = rect.Left,
                        Width = rect.Right - rect.Left,
                        Topmost = true
                    };
                    Scheduler.Run(async () =>
                       await Closing(main),
                       TimeSpan.FromMilliseconds(250));

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
            if (!await WinApi.Opened() && status)
            {
                view.Dispatcher.Invoke(() => view.Visibility = Visibility.Hidden,
                    System.Windows.Threading.DispatcherPriority.Render);

                status = false;
            }
        }
    }
}
