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
    public partial class AddAnnotationSheet : ModernDialog
    {
        // public Window window { get; private set; }
        public Point position;

        public AddAnnotationSheet(Point position)
        {
            InitializeComponent();

            // this.window = new Window();
            this.position = position;
            this.CloseButton.Visibility = Visibility.Hidden;

            foreach (KeyValuePair<int, AnnotationType> type in AnnotationType.types.ToList())
            {
                ComboBoxItem i = new ComboBoxItem();
                i.Tag = type.Key;
                i.Content = type.Value.label;
                typeList.Items.Add(i);
            }

            Console.WriteLine(position);
        }

        //Constructeur utile pour les raccourcis (avec champs texte et/ou type deja remplis)
        public AddAnnotationSheet(Point position, String textSelected, int typeSelected)
        {
            InitializeComponent();

            // this.window = new Window();
            this.position = position;
            this.CloseButton.Visibility = Visibility.Hidden;

            foreach (KeyValuePair<int, AnnotationType> type in AnnotationType.types.ToList())
            {
                ComboBoxItem i = new ComboBoxItem();
                i.Tag = type.Key;
                i.Content = type.Value.label;
                typeList.Items.Add(i);

                if(type.Key == typeSelected)
                    typeList.SelectedItem = i;
            }

            text.Text = textSelected;
        }

        public void setParameters(Double left, Double top)
        {
            this.Left = left;
            this.Top = top;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //window.Close();
        }

        public void close_dialog()
        {
            this.Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)typeList.SelectedItem;
            if (item != null)
            {
                AnnotationHandler a = new AnnotationHandler(Authenticator.AUTHENTICATOR.user);

                int tag = (int)item.Tag;

                a.createAnnotationSheet(tag, (int)position.X, (int)position.Y, text.Text);

                if (ViewRegister.window != null)
                {
                    ViewRegister.window.reload();
                }

                this.Close();
            }
        }
    }
}