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
            /*
            User user = Authenticator.AUTHENTICATOR.user;
            if(user != null)
                annotationHandler = new AnnotationHandler(user);

            reload();*/
        }

        public void reload()
        {
            /*
            String annotationsText = "";
            Sheet sheet = ViewManager.instance.sheet;
            List<AnnotationSheet> annotationList = annotationHandler.getAnnotationSheetBySheetId(sheet.id_sheet);
            if (sheet != null)
            {
                foreach (KeyValuePair<int, AnnotationSheet> annotationPair in sheet.annotations_sheet)
                {
                    annotationsText = annotationsText + annotationPair.Value.ToString() + "\n";
                }
            }
            Annotations.Text = annotationsText;
        */}
    }
}