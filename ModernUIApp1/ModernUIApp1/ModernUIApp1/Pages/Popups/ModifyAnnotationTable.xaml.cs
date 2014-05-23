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
using Data.Data.Registre.Annotation;
using FirstFloor.ModernUI.Windows.Controls;
using Handlers.Handlers;
using ModernUIApp1.Handlers.Utils;

namespace ModernUIApp1.Pages.Popups
{
    /// <summary>
    /// Logique d'interaction pour ModifyAnnotationTable.xaml
    /// </summary>
    public partial class ModifyAnnotationTable : ModernDialog
    {
        private AnnotationPageTable annotation;

        public ModifyAnnotationTable(AnnotationPageTable annotation)
        {
            InitializeComponent();
            this.CloseButton.Visibility = Visibility.Hidden;

            this.annotation = annotation;
            num.SelectedText = annotation.id_number.ToString();
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void validate_Click(object sender, RoutedEventArgs e)
        {
            if (num.Text != null)
            {
                AnnotationHandler a = new AnnotationHandler(Authenticator.AUTHENTICATOR.user);
                try
                {
                    a.modifyAnnotationPageTable(annotation.id_annotation_page_table, annotation.x, annotation.y, annotation.height, annotation.width, int.Parse(num.Text));
                }
                catch (Exception)
                {
                    Console.WriteLine("Nombre incorrect");
                    // TODO Message d'erreur
                }
            }
            

            if (ViewTable.window != null)
            {
                ViewTable.window.reload();
            }

            this.Close();
        }

        private void changePos_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
