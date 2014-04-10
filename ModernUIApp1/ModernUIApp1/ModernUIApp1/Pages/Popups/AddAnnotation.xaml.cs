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

namespace ModernUIApp1.Pages.Popups
{
    /// <summary>
    /// Logique d'interaction pour AddAnnotation.xaml
    /// </summary>
    public partial class AddAnnotation : UserControl
    {
        public Window window { get; private set; }
        public Point position;

        public AddAnnotation(Point position)
        {
            InitializeComponent();

            this.window = new Window();
            this.position = position;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
    }
}
