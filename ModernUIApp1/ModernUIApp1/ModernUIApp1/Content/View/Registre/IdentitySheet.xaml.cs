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

using Data.Data.Registre;
using Handlers.Handlers;
using ModernUIApp1.Handlers.Utils;
using Data.Data;
using Data.Data.Registre.Annotation;
namespace ModernUIApp1.Content.View.Registre
{
    /// <summary>
    /// Interaction logic for IdentitySheet.xaml
    /// </summary>
    public partial class IdentitySheet : UserControl
    {
        /* SINGLETON */
        public static IdentitySheet IDENTITYSHEET { get; private set; }

        private Dictionary<string, List<AnnotationSheet>> annotations;
        
        /* Constructor */
        public IdentitySheet()
        {
            InitializeComponent();

            annotations = new Dictionary<string, List<AnnotationSheet>>();

            IdentitySheet.IDENTITYSHEET = this;

            reload();
        }

        /* Refresh the window */
        public void reload()
        {
            /*
             * DEBUT TEST
             if (Authenticator.AUTHENTICATOR.user != null)
             {
                 AnnotationSheet a1 = new AnnotationSheet(0, sheet, 1, Authenticator.AUTHENTICATOR.user.ToString(), "Annot n1", 0, 0);
                 AnnotationSheet a2 = new AnnotationSheet(1, sheet, 2, Authenticator.AUTHENTICATOR.user.ToString(), "Annot n2", 0, 0);
                 AnnotationSheet a3 = new AnnotationSheet(2, sheet, 3, Authenticator.AUTHENTICATOR.user.ToString(), "Annot n3", 0, 0);
                 AnnotationSheet a4 = new AnnotationSheet(3, sheet, 4, Authenticator.AUTHENTICATOR.user.ToString(), "Annot n4", 0, 0);
                 sheet.addAnnotation(a1);
                 sheet.addAnnotation(a2);
                 sheet.addAnnotation(a3);
                 sheet.addAnnotation(a4);
             }
            * FIN TEST
            */

            Sheet sheet = ViewManager.instance.sheet;
            String annotationsText = "";
            if (sheet != null)
            {
                AnnotationHandler annotationHandler = new AnnotationHandler(Authenticator.AUTHENTICATOR.user);
                
                if (sheet != null && sheet.id_sheet != 0)
                {
                    annotator.Items.Clear();
                    annotations.Clear();                    
                    
                    foreach (AnnotationSheet annotation in sheet.annotations_sheet.Values.OrderBy(e => e.type))
                    {
                        /*string userName = "";
                        if (annotation.user != "-1")
                        {
                            userName = " (" + annotation.user + ")";
                        }
                        annotationsText = annotationsText + annotation.ToString() + userName + "\n";*/
                        
                        List<AnnotationSheet> annotationsUser = null;
                        if (annotations.ContainsKey(annotation.user))
                        {
                            annotationsUser = annotations[annotation.user];
                        }
                        else
                        {
                            annotationsUser = new List<AnnotationSheet>();
                            annotations.Add(annotation.user, annotationsUser);

                            ComboBoxItem i = new ComboBoxItem();
                            i.Tag = annotation.user;
                            if (annotation.user == "-1")
                            {
                                i.Content = "moi";
                            }
                            else
                            {
                                i.Content = annotation.user;
                            }
                            annotator.Items.Add(i);
                        }
                        annotationsUser.Add(annotation);
                    }

                    annotator.SelectionChanged += annotator_SelectionChanged;
                }
                else
                {
                    annotationsText = "Aucune fiche sélectionnée";
                }
            }
            else
            {
                annotationsText = "Aucune fiche sélectionnée";
            }
            Annotations.Text = annotationsText;
        }

        void annotator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)annotator.SelectedItem;
        }
    }
}