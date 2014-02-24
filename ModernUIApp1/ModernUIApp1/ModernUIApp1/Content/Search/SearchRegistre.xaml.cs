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

namespace ModernUIApp1.Content
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class SearchRegistre : UserControl
    {
        public SearchRegistre()
        {
            InitializeComponent();

            // Event manager for edit text year value
            this.yearSlider.ValueChanged += yearSlider_ValueChanged;
        }

        private void yearSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.yearValue.Text = e.NewValue.ToString();
        }
    }
}
