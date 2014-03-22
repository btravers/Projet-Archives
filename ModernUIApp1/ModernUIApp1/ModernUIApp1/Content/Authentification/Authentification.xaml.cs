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

namespace ModernUIApp1.Content.Identification
{
    /// <summary>
    /// Interaction logic for Identification.xaml
    /// </summary>
    public partial class Authentification : UserControl
    {
        public Authentification()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(email.Text);
            Console.WriteLine(password.Text);

            Authenticator.AUTHENTICATOR.login(email.Text, password.Text);
        }
    }
}
