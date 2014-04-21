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
        private AnnotationHandler annotationHandler;
        public IdentitySheet()
        {
            InitializeComponent();
            
            User user = Authenticator.AUTHENTICATOR.user;
            if(user != null)
                annotationHandler = new AnnotationHandler(user);

            reload();
        }

        public void reload()
        {
            //TODO : uncomment
            //Sheet sheet = ViewManager.instance.sheet;
            
            //TEST
            Sheet sheet = new Sheet();
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
            //FIN TEST

            String annotationsText = "";
            if (sheet != null)
            {
                if (Authenticator.AUTHENTICATOR.user != null)
                {
                    List<AnnotationSheet> annotationList = annotationHandler.getAnnotationSheetBySheetId(sheet.id_sheet);
                    foreach (AnnotationSheet annotation in sheet.annotations_sheet.Values)
                    {
                        annotationsText = annotationsText + annotation.ToString();
                    }
                }
                else
                    annotationsText = "Utilisateur non connecté";
            }
            else
                annotationsText = "Aucune fiche sélectionnée";
            Annotations.Text = annotationsText;
        }
    }
}