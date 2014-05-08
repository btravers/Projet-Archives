using Data.Data.Users.Shortcut;
using FirstFloor.ModernUI.Windows.Controls;
using Handlers.Handlers;
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
    /// Logique d'interaction pour DisplayAnnotation.xaml
    /// </summary>
    public partial class DisplayAnnotation : ModernDialog
    {
        private int id_annotation;

        public DisplayAnnotation()
        {
            InitializeComponent();

            this.CloseButton.Visibility = Visibility.Hidden;
        }

        public void setPosition(Double left, Double top)
        {
            this.Left = left;
            this.Top = top;
        }

        public void setParameters(int idAnnotation, string annotationText, int annotationType)
        {
            this.id_annotation = idAnnotation;
            text.Text = annotationText;
            AnnotationType t;
            if(AnnotationType.types.TryGetValue(annotationType, out t))
                type.Text = t.label;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void close_dialog()
        {
            this.Close();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            AnnotationHandler a = new AnnotationHandler(Authenticator.AUTHENTICATOR.user);
            a.deleteAnnotationSheet(id_annotation);

            if (ViewRegister.window != null)
            {
                ViewRegister.window.reload();
            }

            this.Close();
        }
    }
}
