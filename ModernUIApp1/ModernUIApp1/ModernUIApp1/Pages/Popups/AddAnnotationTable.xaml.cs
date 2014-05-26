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
    /// Logique d'interaction pour AddAnnotation.xaml
    /// </summary>
    public partial class AddAnnotationTable : ModernDialog
    {
        // public Window window { get; private set; }
        public Point positionTopLeft;
        public Point positionBottomRight;

        public AddAnnotationTable(Point positionTopLeft, Point positionBottomRight)
        {
            InitializeComponent();

            // this.window = new Window();
            this.positionTopLeft = positionTopLeft;
            this.positionBottomRight = positionBottomRight;
            this.CloseButton.Visibility = Visibility.Hidden;
        }

        //Constructeur utile pour les raccourcis (avec champs texte et/ou type deja remplis)
        public AddAnnotationTable(Point positionTopLeft, Point positionBottomRight, String textSelected)
        {
            InitializeComponent();

            // this.window = new Window();
            this.positionTopLeft = positionTopLeft;
            this.positionBottomRight = positionBottomRight;
            this.CloseButton.Visibility = Visibility.Hidden;

            text.Text = textSelected;
        }

        public void setParameters(Double left, Double top)
        {
            this.Left = left;
            this.Top = top;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            if (ViewTable.window != null)
            {
                ViewTable.window.reload();
            }

            this.Close();
            //window.Close();
        }

        public void close_dialog()
        {
            this.Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (text.Text != null)
            {
                AnnotationHandler a = new AnnotationHandler(Authenticator.AUTHENTICATOR.user);

                int x = (int)Math.Min(positionTopLeft.X, positionBottomRight.X);
                int y = (int)Math.Min(positionTopLeft.Y, positionBottomRight.Y);

                int width = (int)Math.Abs(positionBottomRight.X - positionTopLeft.X);
                int height = (int)Math.Abs(positionBottomRight.Y - positionTopLeft.Y);

                try
                {
                    a.createAnnotationPageTable(x, y, width, height, int.Parse(text.Text));
                }
                catch (Exception)
                {
                    Console.WriteLine("Nombre incorrect");
                    // TODO Message d'erreur
                }

                if (ViewTable.window != null)
                {
                    ViewTable.window.reload();
                }

                this.Close();
            }
        }
    }
}