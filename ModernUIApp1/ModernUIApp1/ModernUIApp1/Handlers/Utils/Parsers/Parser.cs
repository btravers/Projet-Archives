using Data.Data.Registre.Annotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using System.Xml.Linq;
using Data.Data.Registre;
using Data.Data.Users.Bookmark;
using Data.Data.Users.Shortcut;
using ModernUIApp1.Resources;

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
                    int id_sheet = int.Parse(elem.Descendants(Resources.LinkResources.IdSheet).First().Value);
                    AnnotationPageTable annotation = new AnnotationPageTable(idAnnotation, pageTable, user.email, x, y, w, h, id_number, id_sheet);

                    pageTable.addAnnotation(annotation);
                }

                i++;
            }

            return lRes;
        }

        /* Parse the answer of a 'get_page_table' request. */
        public List<AnnotationPageTable> ParseAnnotationPageTable(PageTable pageTable)
        {
            List<AnnotationPageTable> lRes = new List<AnnotationPageTable>();
            
            /*foreach (XElement elem in xmlDocument.Descendants(Resources.LinkResources.Annotations))
            {
                int idAnnotation = int.Parse(elem.Descendants(Resources.LinkResources.IdAnnotationPageTable).First().Value);
                int x = int.Parse(elem.Descendants(Resources.LinkResources.X).First().Value);
                int y = int.Parse(elem.Descendants(Resources.LinkResources.Y).First().Value);
                int width = int.Parse(elem.Descendants(Resources.LinkResources.Width).First().Value);
                int height = int.Parse(elem.Descendants(Resources.LinkResources.Height).First().Value);
                int id_number = int.Parse(elem.Descendants(Resources.LinkResources.IdNumber).First().Value);
                AnnotationPageTable annotation = new AnnotationPageTable(idAnnotation, pageTable, user.email, x, y, width, height, id_number);

                lRes.Add(annotation);
            }*/

            try
            {
                XElement xmlResponse = xmlDocument.Element("response");
                XElement xmlResult = xmlResponse.Element("result");

                foreach (XElement xmlNode in xmlResult.Elements())
                {
                    int idAnnotation = int.Parse(xmlNode.Name.ToString().Substring(4));
                    int x = int.Parse(xmlNode.Element("x").Value);
                    int y = int.Parse(xmlNode.Element("y").Value);
                    int width = int.Parse(xmlNode.Element("width").Value);
                    int height = int.Parse(xmlNode.Element("height").Value);
                    int id_number = int.Parse(xmlNode.Element("id_number").Value);
                    int id_sheet = int.Parse(xmlNode.Element("id_sheet").Value);
                    string user = xmlNode.Element("user").Value;

                    lRes.Add(new AnnotationPageTable(idAnnotation, pageTable, user, x, y, width, height, id_number, id_sheet));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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
                    //EType type = (EType)Enum.Parse(typeof(EType), elem.Descendants(Resources.LinkResources.Type).First().Value, true);
                    int type = int.Parse(elem.Descendants(Resources.LinkResources.Type).First().Value);
                    string text = elem.Descendants(Resources.LinkResources.Text).First().Value;
                    
                    AnnotationSheet annotation = new AnnotationSheet(idAnnotation, sheet, type, user.email, text, x, y);

                    sheet.addAnnotation(annotation);
                }

                i++;
            }

            return lRes;
        }

        /* Parse the annotations for a sheet request. */
        public List<AnnotationSheet> ParseAnnotationSheet(Sheet sheet)
        {
            List<AnnotationSheet> lRes = new List<AnnotationSheet>();

            /*foreach (XElement xmlAnnotations in xmlDocument.Descendants(Resources.LinkResources.Annotations))
            {
                int i = 0;
                while (xmlAnnotations.Descendants(Resources.LinkResources.Item + i.ToString()).Count() != 0)
                {
                    XElement elem = xmlAnnotations.Descendants(Resources.LinkResources.Item + i.ToString()).First();
                    
                    int idAnnotation = int.Parse(elem.Descendants(Resources.LinkResources.IdAnnotationSheet).First().Value);
                    int x = int.Parse(elem.Descendants(Resources.LinkResources.X).First().Value);
                    int y = int.Parse(elem.Descendants(Resources.LinkResources.Y).First().Value);
                    //EType type = (EType)Enum.Parse(typeof(EType), elem.Descendants(Resources.LinkResources.Type).First().Value, true);
                    int type = int.Parse(elem.Descendants(Resources.LinkResources.Type).First().Value);
                    string text = elem.Descendants(Resources.LinkResources.Text).First().Value;
                    AnnotationSheet annotation = new AnnotationSheet(idAnnotation, sheet, type, user.email, text, x, y);

                    lRes.Add(annotation);

                    i++;
                }
            }*/

            try
            {
                XElement xmlResponse = xmlDocument.Element("response");
                XElement xmlResult = xmlResponse.Element("result");

                foreach (XElement xmlNode in xmlResult.Elements())
                {
                    int idAnnotation = int.Parse(xmlNode.Name.ToString().Substring(4));
                    int type = int.Parse(xmlNode.Element("type").Value);
                    int x = int.Parse(xmlNode.Element("x").Value);
                    int y = int.Parse(xmlNode.Element("y").Value);
                    string text = xmlNode.Element("text").Value;
                    string user = xmlNode.Element("user").Value;

                    lRes.Add(new AnnotationSheet(idAnnotation, sheet, type, user, text, x, y));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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

        public List<PageTable> ParserSearchTable()
        {
            List<PageTable> lRes = new List<PageTable>();

            try
            {            
                XElement xmlResponse = xmlDocument.Element("response");
                XElement xmlResult = xmlResponse.Element("result");

                foreach (XElement xmlNode in xmlResult.Elements())
                {
                    int idRegister = int.Parse(xmlNode.Name.ToString().Substring(4));
                    string location = xmlNode.Element("location").Value;
                    int year = int.Parse(xmlNode.Element("year").Value);
                    int volume = int.Parse(xmlNode.Element("volume").Value);

                    Register register = new Register(idRegister, location, year, volume);

                    XElement xmlPages = xmlNode.Element("pages");
                    foreach (XElement xmlPage in xmlPages.Elements())
                    {
                        int idPageTable = int.Parse(xmlPage.Name.ToString().Substring(4));
                        int page = int.Parse(xmlPage.Element("page").Value);
                        string url = xmlPage.Element("url").Value;

                        lRes.Add(new PageTable(idPageTable, register, page, url));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return lRes;
        }

        /* Parse the response to get_sheet_by_id */
        public Sheet ParserGetSheetById()
        {
            Sheet res = new Sheet();

            try
            {
                XElement xmlResponse = xmlDocument.Element("response");
                XElement xmlResult = xmlResponse.Element("result");

                foreach (XElement xmlNode in xmlResult.Elements())
                {
                    int idSheet = int.Parse(xmlNode.Element("id_sheet").Value);
                    string url = xmlNode.Element("url").Value;

                    res = new Sheet(idSheet, url);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return res;
        }

        public List<Sheet> ParserSearchSheet()
        {
            List<Sheet> lRes = new List<Sheet>();

            try
            {
                XElement xmlResponse = xmlDocument.Element("response");
                XElement xmlResult = xmlResponse.Element("result");

                foreach (XElement xmlNode in xmlResult.Elements())
                {
                    int idSheet = int.Parse(xmlNode.Name.ToString().Substring(4));
                    string url = xmlNode.Element("url").Value;

                    lRes.Add(new Sheet(idSheet, url));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return lRes;
        }

        public List<AnnotationType> ParserTypes()
        {
            List<AnnotationType> lRes = new List<AnnotationType>();

            try
            {
                XElement xmlResponse = xmlDocument.Element("response");
                XElement xmlResult = xmlResponse.Element("result");

                foreach (XElement xmlNode in xmlResult.Elements())
                {
                    int idType = int.Parse(xmlNode.Name.ToString().Substring(4));
                    string label = xmlNode.Element("label").Value;

                    lRes.Add(new AnnotationType(idType, label));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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

        /* Returns the value of the first node found */
        private XElement getFirstElement(String element)
        {
            return xmlDocument.Descendants().First(n => n.Name == element);
        }

        /* Return all bookmark folders in a Dictionary */
        public Dictionary<int, BookmarkFolder> parseBookmarkFolders()
        {
            Dictionary<int, BookmarkFolder> folders = new Dictionary<int, BookmarkFolder>();

            try
            {
                XElement foldElems = getFirstElement("folders");

                if (foldElems != null)
                {
                    foreach (XElement xmlNode in getFirstElement("folders").Elements())
                    {
                        int idFolder = int.Parse(xmlNode.Name.ToString().Substring(6));
                        string label = xmlNode.Element("label").Value;
                        int idParent = int.Parse(xmlNode.Element("id_bookmark_folder_parent").Value);

                        if (folders.ContainsKey(idParent))
                            folders.Add(idFolder, new BookmarkFolder(idFolder, folders[idParent], label));
                        else
                            folders.Add(idFolder, new BookmarkFolder(idFolder, null, label));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return folders;
        }

        /* Return all bookmark files in a Dictionary */
        public Dictionary<int, BookmarkFile> parseBookmarkFiles(Dictionary<int, BookmarkFolder> folders)
        {
            Dictionary<int, BookmarkFile> files = new Dictionary<int, BookmarkFile>();

            try
            {
                XElement fileElems = getFirstElement("files");

                if (fileElems != null)
                {
                    foreach (XElement xmlNode in fileElems.Elements())
                    {
                        int idFolder = int.Parse(xmlNode.Name.ToString().Substring(4));
                        string label = xmlNode.Element("label").Value;
                        int idParent = int.Parse(xmlNode.Element("id_bookmark_folder").Value);
                        int idSheet = int.Parse(xmlNode.Element("id_sheet").Value);

                        if (folders.ContainsKey(idParent))
                            files.Add(idFolder, new BookmarkFile(idFolder, null, folders[idParent], label));
                        // TODO : WHAT ABOUT THE SHEET ? CONCEPTION PROBLEM : IF THE SHEET ISNT LOADED.
                        else
                            files.Add(idFolder, new BookmarkFile(idFolder, null, null, label));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return files;
        }

        /* Returns the result id, if it exists */
        public int parseResultId()
        {
            String stringId = this.getFirstNode(LinkResources.ResultId);
            int resultId = -1;

            if (stringId != null)
            {
                resultId = int.Parse(stringId);
            }

            return resultId;
        }

        public int parseCreateShortcut()
        {
            int idShortcut = 0;
            return idShortcut;
        }

        public string parseDeleteShortcut()
        {
            string repServ = "Shortcut non supprime";
            return repServ;
        }

        public List<Shortcut> parserGetAllShortcut()
        {
            throw new NotImplementedException();
        }
    }
}