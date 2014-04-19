using FirstFloor.ModernUI.Windows.Controls;
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

namespace ModernUIApp1.Content.View.Common.Bookmark
{
    /// <summary>
    /// Interaction logic for PopAddFolder.xaml
    /// </summary>
    public partial class PopAddFolder : ModernDialog
    {
        public PopAddFolder()
        {
            InitializeComponent();
        }

        public void ok_click(Object sender, RoutedEventArgs e)
        {
            BookmarkResult.window.addNewFolder(nameFolder.Text);
            this.Close();
        }
    }
}
