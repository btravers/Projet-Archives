using System;
using System.Collections.Generic;
using System.IO;
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
using Handlers.Utils;
using ModernUIApp1.Handlers.Utils;

namespace ModernUIApp1.Pages.Popups
{
    /// <summary>
    /// Logique d'interaction pour ModifyAnnotationTable.xaml
    /// </summary>
    public partial class DisplayAnnotationTable : ModernDialog
    {
        private AnnotationPageTable annotation;

        public DisplayAnnotationTable(AnnotationPageTable annotation)
        {
            InitializeComponent();

            this.CloseButton.Visibility = Visibility.Hidden;

            this.annotation = annotation;
            num.Text = annotation.id_number.ToString();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Popup pop = new Popup("Suppression d'une annotation", "Êtes vous certain de vouloir supprimer cette annotation ?", false);

            if (pop.show())
            {
                this.Close();
                MainWindow.window.Activate();

                AnnotationHandler a = new AnnotationHandler(Authenticator.AUTHENTICATOR.user);
                a.deleteAnnotationPageTable(annotation.id_annotation_page_table);

                if (ViewTable.window != null)
                {
                    ViewTable.window.reload();
                }
            }
            else
            {
                MainWindow.window.Activate();
            }
        }

        private void modify_Click(object sender, RoutedEventArgs e)
        {
            ModifyAnnotationTable modifyAnnotationTableUserControl = new ModifyAnnotationTable(annotation);
            this.Close();
            modifyAnnotationTableUserControl.Show();
        }

        private void access_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SheetHandler sheetHandler = new SheetHandler();

                if (annotation.id_sheet != -1)
                {
                    ViewManager.instance.sheet = sheetHandler.getById(annotation.id_sheet);

                    if (ViewManager.instance.sheet != null)
                    {
                        FileCache.instance.downloadFile(Connection.ROOT_URL + "/" + ModernUIApp1.Resources.LinkResources.LinkPrintFile.Replace(ModernUIApp1.Resources.LinkResources.Path, ViewManager.instance.sheet.url.Replace("/", "-")), ViewManager.instance.sheet.url,
                            () =>
                            {
                                if (File.Exists(ViewManager.instance.sheet.url))
                                {
                                    if (ViewRegister.window != null)
                                    {
                                        ViewRegister.window.reload();
                                    }

                                    MainWindow.window.ContentSource = new Uri("/Pages/ViewRegister.xaml", UriKind.Relative);      
                                }
                            },
                            () =>
                            {
                                MessageBox.Show("L'image n'est pas disponible.", "Opération impossible");
                            }
                        );                        
                    }
                    else
                    {
                        Console.WriteLine("Image introuvable...");
                    }
                }
            }
            catch (System.InvalidCastException ice)
            {
                Console.WriteLine(ice.Message);
            }
            this.Close();
        }
    }
        
}
