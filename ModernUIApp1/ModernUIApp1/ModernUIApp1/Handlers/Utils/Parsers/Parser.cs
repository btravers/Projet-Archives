using Data.Data.Registre.Annotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using System.Xml.Linq;

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
        public List<AnnotationPageTable> ParseAnnotationPageTable() 
        {
            throw new NotImplementedException();
        }

        public List<BookmarkFile> ParseBookmarkFile()
        {
            throw new NotImplementedException();
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