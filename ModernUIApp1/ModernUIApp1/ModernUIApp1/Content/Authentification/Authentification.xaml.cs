using ModernUIApp1.Handlers.Utils;
using System;
using System.Collections.Generic;
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
using ModernUIApp1.Resources;
using ModernUIApp1.Content.View.Registre;
using ModernUIApp1.Pages;


namespace ModernUIApp1.Content.Authentification
{
    /// <summary>
    /// Interaction logic for Authentification.xaml
    /// </summary>
    public partial class Authentification : UserControl
    {
        public Authentification()
        {
            InitializeComponent();
            this.logout.IsEnabled = false;
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(email.Text);
            Console.WriteLine(password.Password);

            if (Authenticator.AUTHENTICATOR.login(email.Text, password.Password))
            {
                // Notify that the user is connected
                MainWindow.window.userConnected();
                message.Text = null;
                disable();

                message.Text = ErrorMessagesResources.Login_Success + Authenticator.AUTHENTICATOR.user.email;
                message.Foreground = new SolidColorBrush(Colors.Green);

                if (ViewRegister.window != null)
                {
                    ViewRegister.window.reload();
                }
            }
            else
            {
                message.Text = ErrorMessagesResources.Login_Failed;
            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticator.AUTHENTICATOR.logout())
            {
                enable();
                message.Text = ErrorMessagesResources.Logout_Success;
                message.Foreground = new SolidColorBrush(Colors.Green);
                if (ViewRegister.window != null)
                {
                    ViewRegister.window.reload();
                }
            }
            else
            {
                message.Text = ErrorMessagesResources.Logout_Failed;
                message.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        public void enable()
        {
            Register.window.enable();
            this.email.IsEnabled = true;
            this.email.Text = null;
            this.password.IsEnabled = true;
            this.password.Password = null;
            this.login.IsEnabled = true;
            this.logout.IsEnabled = false;
        }

        public void disable()
        {
            Register.window.disable();
            this.email.IsEnabled = false;
            this.password.IsEnabled = false;
            this.login.IsEnabled = false;
            this.logout.IsEnabled = true;
        }
    }
}
