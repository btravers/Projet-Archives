using ModernUIApp1.Handlers.Utils;
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
        DeleteShortcut deleteShortcutUserControl;
        int nbButton;

        public ShortcutBar()
        {
            InitializeComponent();
            nbButton = 2;
        }


        private void ClickAllShortcut(object sender, RoutedEventArgs e)
        {
            //recupere les parametres
            var myParameters = (string[])((Button)sender).Tag;
            string text = myParameters[0];
            string stringType = myParameters[1];
            int intType = int.Parse(stringType);

            ViewManager.instance.shortcutIsOn = true;
            ViewManager.instance.annotationShortcut.text = text;
            ViewManager.instance.annotationShortcut.type = intType;
        }


        private void ClickAddShortcut(object sender, RoutedEventArgs e)
        {
            addShortcutUserControl = new AddShortcut(this);
            addShortcutUserControl.ShowDialog();
        }

        public void AddButton(String text, int intType)
        {
            Button newButton = new Button();
            newButton.Content = text;
            String stringType = intType.ToString();
            //permet de passer des parametres a un evenement
            newButton.Tag = new string[] { text, stringType };
            System.Windows.Thickness tn = new Thickness(10);
            newButton.Margin = tn;
            RoutedEventHandler eventHandler = new RoutedEventHandler(ClickAllShortcut);
            newButton.Click += eventHandler;
            ShortcutWrapPanel.Children.Insert(nbButton, newButton);
            nbButton++;
        }

        private void ClickDeleteShortcut(object sender, RoutedEventArgs e)
        {
            deleteShortcutUserControl = new DeleteShortcut();
            deleteShortcutUserControl.ShowDialog();
        }

        private void ClickShortcutName(object sender, RoutedEventArgs e)
        {
            ViewManager.instance.shortcutIsOn = true;
            ViewManager.instance.annotationShortcut.text = "";
            //4 : Type de Nom
            ViewManager.instance.annotationShortcut.type = 4;
        }

        private void ClickShortcutFirstName(object sender, RoutedEventArgs e)
        {
            ViewManager.instance.shortcutIsOn = true;
            ViewManager.instance.annotationShortcut.text = "";
            //5 : Type de Prenom
            ViewManager.instance.annotationShortcut.type = 5;
        }
        
    }
}
