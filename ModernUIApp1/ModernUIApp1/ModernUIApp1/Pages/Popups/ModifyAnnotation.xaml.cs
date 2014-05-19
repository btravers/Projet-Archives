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
using Data.Data.Users.Shortcut;
using FirstFloor.ModernUI.Windows.Controls;
using Handlers.Handlers;
using ModernUIApp1.Handlers.Utils;

namespace ModernUIApp1.Pages.Popups
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class ModifyAnnotation : ModernDialog
    {
        private Point position;
        private int id_annotation;


        public ModifyAnnotation(int idAnnotation, string annotationType, string annotationText, Point pos)
        {
            InitializeComponent();

            // this.window = new Window();
            position = pos;
            id_annotation = idAnnotation;
            this.CloseButton.Visibility = Visibility.Hidden;
            
            foreach (KeyValuePair<int, AnnotationType> type in AnnotationType.types.ToList())
            {
                ComboBoxItem i = new ComboBoxItem();
                i.Tag = type.Key;
                i.Content = type.Value.label;
                if (i.Content.Equals(annotationType))
                    typeList.SelectedItem = i;
                typeList.Items.Add(i);                
            }
            text.SelectedText = annotationText;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void validate_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)typeList.SelectedItem;
            Console.Write("Tag :" + (int)item.Tag+"\n");
            if (item != null)
            {
                AnnotationHandler a = new AnnotationHandler(Authenticator.AUTHENTICATOR.user);

                int tag = (int)item.Tag;

                a.addOrModifyAnnotationSheet(id_annotation, tag, (int)position.X, (int)position.Y, text.Text);
            }

            if (ViewRegister.window != null)
            {
                ViewRegister.window.reload();
            }

            this.Close();
        }

        private void changePos_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
