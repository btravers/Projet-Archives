using Data.Data.Users.Shortcut;
using FirstFloor.ModernUI.Windows.Controls;
using Handlers.Handlers;
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
    /// Interaction logic for TabPage1.xaml
    /// </summary>
    public partial class DeleteShortcut : ModernDialog
    {
        public DeleteShortcut()
        {
            InitializeComponent();
            this.CloseButton.Visibility = Visibility.Hidden;
            ShortcutHandler shortcutHandler = new ShortcutHandler();
            List<Shortcut> shortcutList = new List<Shortcut>();
            shortcutList = shortcutHandler.getAllShortcut();
            foreach (Shortcut shortcut in shortcutList)
            {
                ComboBoxItem i = new ComboBoxItem();
                i.Tag = shortcut.id_shortcut;
                i.Content = shortcut.default_text;
                shortcutListBox.Items.Add(i);
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteShortcutClick(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)shortcutListBox.SelectedItem;
            if (item != null)
            {
                int tag = (int)item.Tag;
                ShortcutHandler sh = new ShortcutHandler();
                sh.deleteShortcut(tag);

                this.Close();
            }
        }
    }
}
