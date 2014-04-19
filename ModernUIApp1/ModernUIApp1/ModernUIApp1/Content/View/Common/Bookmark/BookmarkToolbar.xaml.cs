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
    /// Interaction logic for BookmarkToolbar.xaml
    /// </summary>
    public partial class BookmarkToolbar : UserControl
    {
        public BookmarkToolbar()
        {
            InitializeComponent();
        }

        /* Event : Click on the icon home */
        public void home_click(object sender, RoutedEventArgs e)
        {
            if (BookmarkResult.window != null)
            {
                BookmarkResult.window.moveToHomeFolder();
            }
        }

        /* Event : Click on the icon previous / back */
        public void previous_click(object sender, RoutedEventArgs e)
        {
            if (BookmarkResult.window != null)
            {
                BookmarkResult.window.moveToPreviousFolder();
            }
        }

        /* Event : Click on the icon newfolder, open a dialog / popup */
        public void newfolder_click(object sender, RoutedEventArgs e)
        {
            new PopAddFolder().ShowDialog();
        }
    }
}
