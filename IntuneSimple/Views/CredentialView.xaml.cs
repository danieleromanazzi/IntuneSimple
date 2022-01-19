using System;
using System.IO;
using System.Windows;

namespace IntuneSimple.Views
{
    /// <summary>
    /// Interaction logic for CredentialView.xaml
    /// </summary>
    public partial class CredentialView : Window
    {
        public CredentialView()
        {
            InitializeComponent();
            this.IsVisibleChanged += (s,e) =>
            {
                try
                {
                    credentialPanel.Visibility = Visibility.Visible;
                    message.Visibility = Visibility.Collapsed;

                    var credential = Credential.Read();
                    username.Text = credential.UserName;
                    password.Text = credential.Password;
                }
                catch (FileNotFoundException)
                {
                    credentialPanel.Visibility = Visibility.Collapsed;
                    message.Visibility = Visibility.Visible;
                    message.Text = "Abilitare l'utenza amministratore tramite il portale aziendale.";
                }
                catch (Exception ex)
                {
                    credentialPanel.Visibility = Visibility.Collapsed;
                    message.Visibility = Visibility.Visible;
                    message.Text = ex.Message;
                }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(username.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(password.Text);
        }
    }
}
