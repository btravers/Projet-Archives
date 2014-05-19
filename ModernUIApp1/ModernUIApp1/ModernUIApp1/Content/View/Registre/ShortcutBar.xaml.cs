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
        int nbButton;

        public ShortcutBar()
        {
            InitializeComponent();
            nbButton = 2;
        }

        private void ClickAddShortcut(object sender, RoutedEventArgs e)
        {
            addShortcutUserControl = new AddShortcut();
            addShortcutUserControl.ShowDialog();


            Button newButton = new Button();
            newButton.Content = "Test";
            System.Windows.Thickness tn = new Thickness(10);
            newButton.Margin = tn;
            ShortcutWrapPanel.Children.Insert(nbButton, newButton);
            nbButton++;
            //newButton.Click = "ClickShourcutTest";
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
