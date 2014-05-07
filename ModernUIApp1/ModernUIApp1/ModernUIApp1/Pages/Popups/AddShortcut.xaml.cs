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
    public partial class AddShortcut : ModernDialog
    {
        public AddShortcut()
        {
            InitializeComponent(); 
            foreach (KeyValuePair<int, AnnotationType> type in AnnotationType.types.ToList())
            {
                ComboBoxItem i = new ComboBoxItem();
                i.Tag = type.Key;
                i.Content = type.Value.label;
                typeList.Items.Add(i);
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)typeList.SelectedItem;
            if (item != null)
            {
                //add_shortcut(
                int tag = (int)item.Tag;
                AnnotationType annotationType;
                AnnotationType.types.TryGetValue(tag, out annotationType);
                ShortcutHandler sh = new ShortcutHandler();
                sh.createShortcut(annotationType , text.Text, 0);
                //l'ajouter à l'utilisateur (serveur)

                //refraichir barre des raccourcis

                //fermer la fenetre d'ajout de raccourci
                this.Close();
            }
        }
    }
}
