using Data.Data;
using Data.Data.Users.Shortcut;
using FirstFloor.ModernUI.Windows.Controls;
using Handlers.Handlers;
using ModernUIApp1.Content.View.Registre;
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

namespace ModernUIApp1.Pages.Popups
{
    /// <summary>
    /// Interaction logic for TabPage1.xaml
    /// </summary>
    public partial class AddShortcut : ModernDialog
    {
        private ShortcutBar sb;

        public AddShortcut(ShortcutBar sb)
        {
            InitializeComponent();
            this.CloseButton.Visibility = Visibility.Hidden;
            foreach (KeyValuePair<int, AnnotationType> type in AnnotationType.types.ToList())
            {
                ComboBoxItem i = new ComboBoxItem();
                i.Tag = type.Key;
                i.Content = type.Value.label;
                typeList.Items.Add(i);
            }
            this.sb = sb;
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
                int tag = (int)item.Tag;
                AnnotationType annotationType;
                AnnotationType.types.TryGetValue(tag, out annotationType);
                ShortcutHandler sh = new ShortcutHandler();
                //ajoute le raccourci à l'utilisateur (serveur)
                int shortcutId = sh.createShortcut(annotationType , text.Text, 0);

                //ajoute le raccourci à l'utilisateur (client)
                Shortcut sc = new Shortcut(shortcutId, annotationType, text.Text, 0);

                //supprime tous les raccourcis de la barre de raccourcis
                sb.ClearShortcutBar();
                //charge tous les raccourcis de l'utilisateur via la bdd
                sb.LoadShortcutBar();

                this.Close();
            }
        }
    }
}
