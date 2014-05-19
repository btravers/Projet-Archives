using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre.Annotation;
using ModernUIApp1.Handlers.Utils.Parsers;
using Data.Data.Registre;
using Handlers.Utils;
using ModernUIApp1.Handlers.Utils;
using System.Threading;
using System.Windows.Media.Imaging;
using System.IO;
using Data.Data.Users.Shortcut;

namespace Handlers.Handlers
{
    public class SheetHandler
    {
        public List<AnnotationType> getTypes()
        {
            String xmlResponse = Connection.getRequest(ModernUIApp1.Resources.LinkResources.LinkGetTypes);

            Parser parser = new Parser(xmlResponse);

            return parser.ParserTypes();
        }
        
        public List<Sheet> search(int year, string location, string firstname, string lastname, string other)
        {
            String xmlResponse = Connection.getRequest(ModernUIApp1.Resources.LinkResources.LinkSearchSheet.Replace(ModernUIApp1.Resources.LinkResources.Year, "" + year).Replace(ModernUIApp1.Resources.LinkResources.Location, location).Replace(ModernUIApp1.Resources.LinkResources.Firstname, firstname).Replace(ModernUIApp1.Resources.LinkResources.Lastname, lastname).Replace(ModernUIApp1.Resources.LinkResources.Job, "").Replace(ModernUIApp1.Resources.LinkResources.Regiment, "").Replace(ModernUIApp1.Resources.LinkResources.Other, other));
            
            Parser parser = new Parser(xmlResponse);

            return parser.ParserSearchSheet();
        }

        public void preloadSheets(int idSheet)
        {
            new Thread(delegate()
            {
                String xmlResponse = Connection.getRequest(ModernUIApp1.Resources.LinkResources.LinkPreloadSheets.Replace(ModernUIApp1.Resources.LinkResources.IdSheet, "" + idSheet));

                Parser parser = new Parser(xmlResponse);

                List<Sheet> sheets = parser.ParserSearchSheet();
                foreach (Sheet sheet in sheets)
                {
                    FileCache.instance.downloadFile(Connection.ROOT_URL + "/" + ModernUIApp1.Resources.LinkResources.LinkPrintFile.Replace(ModernUIApp1.Resources.LinkResources.Path, sheet.url.Replace("/", "-")), sheet.url,
                        () =>
                        {
                            if (sheet.id_sheet > idSheet)
                            {
                                ViewManager.instance.nextSheet = sheet;
                            }
                            else if (sheet.id_sheet < idSheet)
                            {
                                ViewManager.instance.previousSheet = sheet;
                            }
                        }
                    );                    
                }
            }).Start();            
        }
    }
}
