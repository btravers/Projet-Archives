using ModernUIApp1.Pages.Popups;
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

namespace ModernUIApp1.Content.View.Registre
{
    /// <summary>
    /// Interaction logic for ShortcutBar.xaml
    /// </summary>
    public partial class ShortcutBar : UserControl
    {

        AddShortcut addShortcutUserControl;

        public ShortcutBar()
        {
            InitializeComponent();
        }

        private void ClickAddShortcut(object sender, RoutedEventArgs e)
        {
            addShortcutUserControl = new AddShortcut();
            addShortcutUserControl.ShowDialog();
        }
    }
}
