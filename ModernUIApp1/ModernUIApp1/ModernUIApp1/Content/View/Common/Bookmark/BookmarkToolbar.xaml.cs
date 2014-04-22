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
using Data.Data.Users.Bookmark;

namespace ModernUIApp1.Content.View.Common.Bookmark
{
    /// <summary>
    /// Interaction logic for BookmarkToolbar.xaml
    /// </summary>
    public partial class BookmarkToolbar : UserControl
    {
        public static BookmarkToolbar window { get; private set; } // SINGLETON
        public BookmarkToolbar()
        {
            InitializeComponent();
            window = this;
            this.remove.Visibility = Visibility.Hidden;
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

        /* Handler when an item is dropped on the remove icon */
        public void remove_item(object sender, DragEventArgs e)
        {
            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                //MessageBox.Show("Drop : " + dataString + " to : " + item.text);

                if (dataString.Split('/')[0].Equals(BookmarkType.FOLDER.ToString()))
                { // It's a Folder, update it
                    // Update File, New ParentFolder, Current Folder
                    BookmarkFolder target = BookmarkResult.window.currentUnderFolders[int.Parse(dataString.Split('/')[1])];
                    MessageBox.Show(" Folder : " + target.label);
                }
                else
                { // It's a File, update it
                    // Update File, New ParentFolder, Current Folder
                    BookmarkFile target = BookmarkResult.window.currentUnderFiles[int.Parse(dataString.Split('/')[1])];
                    MessageBox.Show(" File : " + target.label);
                }
            }
        }
    }
}
