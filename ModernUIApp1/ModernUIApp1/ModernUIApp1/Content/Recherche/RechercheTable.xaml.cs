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
using FirstFloor.ModernUI.Presentation;

namespace ModernUIApp1.Content
{
    /// <summary>
    /// Interaction logic for RechercheTable.xaml
    /// </summary>
    public partial class RechercheTable : UserControl
    {
        public RechercheTable()
        {
            InitializeComponent();

            this.yearSlider.ValueChanged += yearSlider_ValueChanged;
        }

        private void yearSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.yearValue.Text = e.NewValue.ToString();
        }
    }
}
