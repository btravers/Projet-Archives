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
using ModernUIApp1.Content.View.Common;

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
            Sheet sheet = ViewManager.instance.sheet;
            if (sheet != null)
            {
                AnnotationHandler annotationHandler = new AnnotationHandler(Authenticator.AUTHENTICATOR.user);
                
                if (sheet != null && sheet.id_sheet != 0)
                {
                    annotator.Items.Clear();
                    annotations.Clear();                    
                    
                    ComboBoxItem defaultItem = null;

                    foreach (AnnotationSheet annotation in sheet.annotations_sheet.Values.OrderBy(e => e.type))
                    {                        
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

                                defaultItem = i;
                            }
                            else
                            {
                                i.Content = annotation.user;

                                if (defaultItem == null)
                                    defaultItem = i;
                            }
                            annotator.Items.Add(i);
                        }

                        annotationsUser.Add(annotation);
                    }

                    annotator.SelectionChanged -= annotator_SelectionChanged;
                    annotator.SelectionChanged += annotator_SelectionChanged;

                    if(defaultItem != null)
                        annotator.SelectedItem = defaultItem;
                }
                else
                {
                    annotationsTextBlock.Text = "Aucune fiche sélectionnée";
                }
            }
            else
            {
                annotationsTextBlock.Text = "Aucune fiche sélectionnée";
            }
        }

        void annotator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)annotator.SelectedItem;
            if (item != null)
            {
                string tag = (string)item.Tag;

                if (annotations.ContainsKey(tag))
                {
                    string annotationsText = "";

                    foreach (AnnotationSheet annotation in annotations[tag])
                    {
                        annotationsText = annotationsText + annotation.ToString() + "\n";
                    }

                    annotationsTextBlock.Text = annotationsText;

                    SheetContent sheetContent = SheetContent.window;
                    if (sheetContent != null)
                    {
                        sheetContent.displayAnnotations(annotations[tag]);
                    }
                }
            }
        }
    }
}