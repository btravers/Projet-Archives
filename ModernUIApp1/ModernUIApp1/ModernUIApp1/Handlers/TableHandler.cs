using Data.Data.Registre;
using Handlers.Utils;
using ModernUIApp1.Handlers.Utils.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.Handlers
{
    public class TableHandler
    {
        public List<PageTable> search(int year, string location)
        {
            String xmlResponse = Connection.getRequest(ModernUIApp1.Resources.LinkResources.LinkSearchTable.Replace(ModernUIApp1.Resources.LinkResources.Year, "" + year).Replace(ModernUIApp1.Resources.LinkResources.Location, location));

            Parser parser = new Parser(xmlResponse);

            return parser.ParserSearchTable();
        }
    }
}
