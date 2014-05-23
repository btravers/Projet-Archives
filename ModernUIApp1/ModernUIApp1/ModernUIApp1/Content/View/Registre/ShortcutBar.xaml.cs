using Data.Data;
using Data.Data.Users.Shortcut;
using Handlers.Handlers;
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
        /* SINGLETON */
        public static ShortcutBar SHORTCUTBAR { get; private set; }

        AddShortcut addShortcutUserControl;
        DeleteShortcut deleteShortcutUserControl;
        int nbButton;
        List<Shortcut> shortcutList;
        ShortcutHandler shortcutHandler;

        public ShortcutBar()
        {
            InitializeComponent(); 
            
            ShortcutBar.SHORTCUTBAR = this;

            this.ClearShortcutBar();
            this.LoadShortcutBar();
        }


        private void ClickAllShortcut(object sender, RoutedEventArgs e)
        {
            //recupere les parametres
            var myParameters = (string[])((Button)sender).Tag;
            int shortcutId = int.Parse(myParameters[0]);
            string text = myParameters[1];
            int intType = int.Parse(myParameters[2]);

            ViewManager.instance.shortcutIsOn = true;
            ViewManager.instance.annotationShortcut.text = text;
            ViewManager.instance.annotationShortcut.type = intType;
        }

        public void AddButton(int shortcutId, String text, int intType)
        {
            Button newButton = new Button();
            newButton.Content = text;
            //permet de passer des parametres a un evenement
            newButton.Tag = new string[] { shortcutId.ToString(), text, intType.ToString() };
            System.Windows.Thickness tn = new Thickness(10);
            newButton.Margin = tn;
            RoutedEventHandler eventHandler = new RoutedEventHandler(ClickAllShortcut);
            newButton.Click += eventHandler;
            ShortcutWrapPanel.Children.Insert(nbButton, newButton);
            nbButton++;

        }

        public void ClearShortcutBar()
        {
            ShortcutWrapPanel.Children.Clear();
            if (Authenticator.AUTHENTICATOR.isConnected())
            {
                ShortcutWrapPanel.Children.Insert(0, Prenom);
                ShortcutWrapPanel.Children.Insert(0, Nom);
                nbButton = 2;
                ShortcutWrapPanel.Children.Insert(nbButton, Ajout);
                ShortcutWrapPanel.Children.Insert(nbButton, Suppr);
            }
        }

        public void LoadShortcutBar()
        {
            if (Authenticator.AUTHENTICATOR.isConnected())
            {
                shortcutHandler = new ShortcutHandler();
                shortcutList = shortcutHandler.getAllShortcut();
                nbButton = 2;
                if (shortcutList != null)
                {
                    foreach (Shortcut shortcut in shortcutList)
                    {
                        //User user;
                        //user.addShortcut(shortcut);
                        Console.WriteLine(shortcut.default_text);
                        AddButton(shortcut.id_shortcut, shortcut.default_text, shortcut.type.id_type);
                    }
                }
            }
        }

        private void ClickAddShortcut(object sender, RoutedEventArgs e)
        {
            addShortcutUserControl = new AddShortcut(this);
            addShortcutUserControl.ShowDialog();
        }

        private void ClickDeleteShortcut(object sender, RoutedEventArgs e)
        {
            nbButton--;
            deleteShortcutUserControl = new DeleteShortcut(this);
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
