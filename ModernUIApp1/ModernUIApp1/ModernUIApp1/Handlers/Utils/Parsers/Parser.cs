using Data.Data.Registre.Annotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using System.Xml.Linq;
using Data.Data.Registre;

namespace ModernUIApp1.Handlers.Utils.Parsers
{

    // TOASK : ONLY ONE PARSER OR CREATE A PARSER FOR AN OBJECT, ex: subclass AnnotationPageTableParser?
    class Parser
    {
        private XDocument xmlDocument;

        /* Constructors */
        public Parser(XDocument xmlDocument)
        {
            this.xmlDocument = xmlDocument;
        }

        public Parser(String xmlString)
        {
            this.xmlDocument = XDocument.Parse(xmlString);
        }

        /* Returns the value of the first node found */
        public String getFirstNode(String node)
        {
            return xmlDocument.Descendants().First(n => n.Name == node).Value.ToString();
        }

        /* Samples */

        /* Parse the answer of a 'get_table' request. */
        public List<PageTable> ParsePageTable(Register register, User user)
        {
            List<PageTable> lRes = new List<PageTable>();

            int i = 0;
            while (xmlDocument.Descendants(Resources.LinkResources.Item + i.ToString()).Count() != 0)
            {
                XElement elem = xmlDocument.Descendants(Resources.LinkResources.Item + i.ToString()).First();

                int idPageTable = int.Parse(elem.Descendants(Resources.LinkResources.IdPageTable).First().Value);
                int page = int.Parse(elem.Descendants(Resources.LinkResources.Page).First().Value);
                string url = elem.Descendants(Resources.LinkResources.ImageSource).First().Value;
                int size = int.Parse(elem.Descendants(Resources.LinkResources.Size).First().Value);
                int width = int.Parse(elem.Descendants(Resources.LinkResources.Width).First().Value);
                int height = int.Parse(elem.Descendants(Resources.LinkResources.Height).First().Value);
                List<AnnotationPageTable> lAnnot = new List<AnnotationPageTable>();

                PageTable pageTable = new PageTable(idPageTable, register, page, url, size, width, height);

                foreach (XElement elemAnnot in elem.Descendants(Resources.LinkResources.Annotations))
                {
                    int idAnnotation = int.Parse(elem.Descendants(Resources.LinkResources.IdAnnotationPageTable).First().Value);
                    int x = int.Parse(elem.Descendants(Resources.LinkResources.X).First().Value);
                    int y = int.Parse(elem.Descendants(Resources.LinkResources.Y).First().Value);
                    int w = int.Parse(elem.Descendants(Resources.LinkResources.Width).First().Value);
                    int h = int.Parse(elem.Descendants(Resources.LinkResources.Height).First().Value);
                    int id_number = int.Parse(elem.Descendants(Resources.LinkResources.IdNumber).First().Value);
                    AnnotationPageTable annotation = new AnnotationPageTable(idAnnotation, pageTable, user.email, x, y, w, h, id_number);

                    pageTable.addAnnotation(annotation);
                }

                i++;
            }

            return lRes;
        }

        /* Parse the answer of a 'get_page_table' request. */
        public List<AnnotationPageTable> ParseAnnotationPageTable(PageTable pageTable, User user)
        {
            List<AnnotationPageTable> lRes = new List<AnnotationPageTable>();
            
            foreach (XElement elem in xmlDocument.Descendants(Resources.LinkResources.Annotations))
            {
                int idAnnotation = int.Parse(elem.Descendants(Resources.LinkResources.IdAnnotationPageTable).First().Value);
                int x = int.Parse(elem.Descendants(Resources.LinkResources.X).First().Value);
                int y = int.Parse(elem.Descendants(Resources.LinkResources.Y).First().Value);
                int width = int.Parse(elem.Descendants(Resources.LinkResources.Width).First().Value);
                int height = int.Parse(elem.Descendants(Resources.LinkResources.Height).First().Value);
                int id_number = int.Parse(elem.Descendants(Resources.LinkResources.IdNumber).First().Value);
                AnnotationPageTable annotation = new AnnotationPageTable(idAnnotation, pageTable, user.email, x, y, width, height, id_number);

                lRes.Add(annotation);
            }

            return lRes;
        }

        /* Parse the answer of a 'get_sheet' request. */
        public List<Sheet> ParseSheet(Register register, User user)
        {
            List<Sheet> lRes = new List<Sheet>();

            int i = 0;
            while (xmlDocument.Descendants(Resources.LinkResources.Item + i.ToString()).Count() != 0)
            {
                XElement elem = xmlDocument.Descendants(Resources.LinkResources.Item + i.ToString()).First();

                int idSheet = int.Parse(elem.Descendants(Resources.LinkResources.IdSheet).First().Value);
                int page = int.Parse(elem.Descendants(Resources.LinkResources.Page).First().Value);
                string url = elem.Descendants(Resources.LinkResources.ImageSource).First().Value;
                int size = int.Parse(elem.Descendants(Resources.LinkResources.Size).First().Value);
                int width = int.Parse(elem.Descendants(Resources.LinkResources.Width).First().Value);
                int height = int.Parse(elem.Descendants(Resources.LinkResources.Height).First().Value);
                List<AnnotationSheet> lAnnot = new List<AnnotationSheet>();

                Sheet sheet = new Sheet(idSheet, register, page, url, size, width, height);

                foreach (XElement xmlAnnot in elem.Descendants(Resources.LinkResources.Annotations))
                {
                    int idAnnotation = int.Parse(elem.Descendants(Resources.LinkResources.IdAnnotationSheet).First().Value);
                    int x = int.Parse(elem.Descendants(Resources.LinkResources.X).First().Value);
                    int y = int.Parse(elem.Descendants(Resources.LinkResources.Y).First().Value);
                    EType type = (EType)Enum.Parse(typeof(EType), elem.Descendants(Resources.LinkResources.Type).First().Value, true);
                    string text = elem.Descendants(Resources.LinkResources.Text).First().Value;
                    AnnotationSheet annotation = new AnnotationSheet(idAnnotation, sheet, type, user.email, text, x, y);

                    sheet.addAnnotation(annotation);
                }

                i++;
            }

            return lRes;
        }

        /* Parse the annotations for a sheet request. */
        public List<AnnotationSheet> ParseAnnotationSheet(Sheet sheet, User user)
        {
            List<AnnotationSheet> lRes = new List<AnnotationSheet>();

            foreach (XElement xmlAnnotations in xmlDocument.Descendants(Resources.LinkResources.Annotations))
            {
                int i = 0;
                while (xmlAnnotations.Descendants(Resources.LinkResources.Item + i.ToString()).Count() != 0)
                {
                    XElement elem = xmlAnnotations.Descendants(Resources.LinkResources.Item + i.ToString()).First();
                    
                    int idAnnotation = int.Parse(elem.Descendants(Resources.LinkResources.IdAnnotationSheet).First().Value);
                    int x = int.Parse(elem.Descendants(Resources.LinkResources.X).First().Value);
                    int y = int.Parse(elem.Descendants(Resources.LinkResources.Y).First().Value);
                    EType type = (EType)Enum.Parse(typeof(EType), elem.Descendants(Resources.LinkResources.Type).First().Value, true);
                    string text = elem.Descendants(Resources.LinkResources.Text).First().Value;
                    AnnotationSheet annotation = new AnnotationSheet(idAnnotation, sheet, type, user.email, text, x, y);

                    lRes.Add(annotation);

                    i++;
                }
            }

            return lRes;
        }

        /* Parse the register for a register request. */
        public List<Register> ParseRegister()
        {
            List<Register> lRes = new List<Register>();

            foreach (XElement xmlRegister in xmlDocument.Descendants(Resources.LinkResources.Register))
            {
                int i = 0;
                while (xmlRegister.Descendants(Resources.LinkResources.Item + i.ToString()).Count() != 0)
                {
                    XElement elem = xmlRegister.Descendants(Resources.LinkResources.Item + i.ToString()).First();

                    int idRegister = int.Parse(elem.Descendants(Resources.LinkResources.IdRegister).First().Value);
                    String location = elem.Descendants(Resources.LinkResources.Location).First().Value;
                    int year = int.Parse(elem.Descendants(Resources.LinkResources.Year).First().Value);
                    int volume = int.Parse(elem.Descendants(Resources.LinkResources.Volume).First().Value);

                    Register newReg = new Register(idRegister, location, year, volume);

                    lRes.Add(newReg);

                    i++;
                }
            }

            return lRes;
        }


        public List<BookmarkFile> ParseBookmarkFile()
        {
            throw new NotImplementedException();
        }

        public List<PageTable> ParserSearchTable()
        {
            List<PageTable> lRes = new List<PageTable>();

            XElement xmlResponse = xmlDocument.Element("response");
            XElement xmlResult = xmlResponse.Element("result");

            foreach (XElement xmlNode in xmlResult.Descendants())
            {
                Console.WriteLine("n : " + xmlNode.ToString());
            }

            return lRes;
        }

        private List<XElement> getAllNodes(String node)
        {
            List<XElement> list = new List<XElement>();
            foreach (XElement element in xmlDocument.Descendants("grandchild"))
                list.Add(element);

            return list;
        }
    }
}