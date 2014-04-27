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
using ModernUIApp1.Handlers.Utils;
using ModernUIApp1.Resources;

namespace ModernUIApp1.Content.Authentification
{
    /// <summary>
    /// Interaction logic for Enregistrement.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        public static Register window;
        public Register()
        {
            InitializeComponent();

            window = this;
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            if (!password.Password.Equals(check_password.Password)) 
            {
                message.Text = ErrorMessagesResources.Register_Password_Not_Correspond;
            } else 
            {
                if (Authenticator.AUTHENTICATOR.registerNewUser(email.Text, password.Password))
                {
                    // Notify that the user is registered
                    message.Text = ErrorMessagesResources.Register_Success;
                    message.Foreground = new SolidColorBrush(Colors.Green);
                    disable();
                }
                else
                {
                    message.Text = ErrorMessagesResources.Register_Failed;
                }
            }
        }

        public void disable()
        {
            this.register.IsEnabled = false;
            this.email.IsEnabled = false;
            this.password.IsEnabled = false;
            this.check_password.IsEnabled = false;
        }
    }
}
