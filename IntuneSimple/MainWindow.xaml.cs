using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IntuneSimple
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
