using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre.Annotation;
using ModernUIApp1.Handlers.Utils.Parsers;
using Data.Data.Registre;
using Data.Data;
using Handlers.Utils;
using ModernUIApp1.Resources;
using ModernUIApp1.Handlers.Utils;

namespace Handlers.Handlers
{
    public class AnnotationHandler
    {
        User user;

        public AnnotationHandler(User user)
        {
            this.user = user;
        }

        //retourne la liste des annotation d'une PageTable
        public List<AnnotationPageTable> getAnnotationPageTableByPageTableId(int page_table_id)
        {
            // Keep the page table
            PageTable pageTable = new PageTable(); // RegistreHandler.findPageTableById(...)
            
            // Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkGetAnnotTable.Replace(LinkResources.SessionId, user.id_session.ToString()).Replace(LinkResources.IdPageTable, page_table_id.ToString()));

            if (xmlResponse != null)
            {
                Parser parser = new Parser(xmlResponse);

                foreach (AnnotationPageTable a in parser.ParseAnnotationPageTable(pageTable))
                {
                    // Add to the PageTable.annotation if it isnt already loaded or if it's modified
                    // TODO : redefine equals ?
                    if (!pageTable.annotations_page_table.ContainsKey(a.id_annotation_page_table) || !pageTable.annotations_page_table.ContainsValue(a))
                        pageTable.addAnnotation(a);
                }

                return pageTable.annotations_page_table.Values.ToList();
            }
            else return null;
        }

        public List<AnnotationPageTable> getAnnotationPageTableByText(String text) // USELESS
            //Recherche des annotations qui contiennent le texte text
        {
            // Keep the page table
            PageTable pageTable = new PageTable(); 

            // Request
            String xmlResponse = Connection.getRequest("");

            Parser parser = new Parser(xmlResponse);

            // Parse XML
            foreach (AnnotationPageTable a in parser.ParseAnnotationPageTable(pageTable))
            {
                // Add to the PageTable.annotation if it isnt already loaded or if it's modified
                // TODO : redefine equals ?
                if ((!pageTable.annotations_page_table.ContainsKey(a.id_annotation_page_table) || !pageTable.annotations_page_table.ContainsValue(a)) & (a.ToString() == text))
                    pageTable.addAnnotation(a);
            }

            return pageTable.annotations_page_table.Values.ToList();
        }

        public List<AnnotationSheet> getAnnotationSheetBySheetId(int id_sheet)
        {
            // Keep the page table
            //Sheet sheet = new Sheet(id_sheet, "xxxx"); // RegistreHandler.findSheetById(...)
            Sheet sheet = ViewManager.instance.sheet;
            if (sheet == null)
                return null;

            // Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkGetAnnotSheet.Replace(LinkResources.SessionId, user.id_session.ToString()).Replace(LinkResources.IdSheet, id_sheet.ToString())); // Connection.send(...)
            
            if (xmlResponse != null)
            {
                Parser parser = new Parser(xmlResponse);

                sheet.annotations_sheet.Clear();

                // Parse XML
                foreach (AnnotationSheet a in parser.ParseAnnotationSheet(sheet))
                {
                    // Add to the PageTable.annotation if it isnt already loaded or if it's modified
                    // TODO : redefine equals ?

                    sheet.addAnnotation(a);
                }

                return sheet.annotations_sheet.Values.ToList();
            }
            else 
               return null;
        }

        public List<AnnotationSheet> getAnnotationSheetByText(String text)  // USELESS
        //Recherche des annotations qui contiennent le texte text
        {
            // Keep the page table
            Sheet sheet = new Sheet();

            // Request
            String xmlResponse = Connection.getRequest("");

            Parser parser = new Parser(xmlResponse);

            // Parse XML
            foreach (AnnotationSheet a in parser.ParseAnnotationSheet(sheet))
            {
                // Add to the PageTable.annotation if it isnt already loaded or if it's modified
                // TODO : redefine equals ?
                if ((!sheet.annotations_sheet.ContainsKey(a.id_annotations_sheet) || !sheet.annotations_sheet.ContainsValue(a)) & (a.ToString() == text))
                    sheet.addAnnotation(a);
            }

            return sheet.annotations_sheet.Values.ToList();
        }

        public void createAnnotationPageTable(int x, int y, int width, int height, int id_number)
        {
            PageTable table = ViewManager.instance.pageTable;
            if (table == null)
                return;

            //Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkAnnotateTable.Replace(LinkResources.SessionId, user.id_session.ToString()).Replace(LinkResources.IdPageTable, table.id_page_table.ToString()).Replace(LinkResources.X, x.ToString()).Replace(LinkResources.Y, y.ToString()).Replace(LinkResources.Width, width.ToString()).Replace(LinkResources.Height, height.ToString()).Replace(LinkResources.Number, id_number.ToString()));
        }

        public void createAnnotationSheet(int id_type, int x, int y, String text)
        {
            Sheet sheet = ViewManager.instance.sheet;
            if (sheet == null)
                return;

            //Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkAnnotateSheet.Replace(LinkResources.SessionId, user.id_session.ToString()).Replace(LinkResources.IdSheet, sheet.id_sheet.ToString()).Replace(LinkResources.X, x.ToString()).Replace(LinkResources.Y, y.ToString()).Replace(LinkResources.Text, text).Replace(LinkResources.IdType, id_type.ToString()));
        }

        public void deleteAnnotationPageTable(int id_annotation_page_table)
        {
            PageTable table = ViewManager.instance.pageTable;
            if (table == null)
                return;

            //Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkDeleteAnnotationTable.Replace(LinkResources.SessionId, user.id_session.ToString()).Replace(LinkResources.IdAnnotationPageTable, id_annotation_page_table.ToString()));
        }

        public void deleteAnnotationSheet(int id_annotation_sheet)
        {
            Sheet sheet = ViewManager.instance.sheet;
            if (sheet == null)
                return;

            //Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkDeleteAnnotationSheet.Replace(LinkResources.SessionId, user.id_session.ToString()).Replace(LinkResources.IdAnnotationSheet, id_annotation_sheet.ToString()));
        }

        public void modifyAnnotationPageTable(int id_annotation_page_table, int x, int y, int height, int width, int id_number)
        {
            PageTable table = ViewManager.instance.pageTable;
            if (table == null)
                return;

            //Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkUpdateAnnotationTable.Replace(LinkResources.SessionId, user.id_session.ToString()).Replace(LinkResources.IdAnnotationPageTable, id_annotation_page_table.ToString()).Replace(LinkResources.X, x.ToString()).Replace(LinkResources.Y, y.ToString()).Replace(LinkResources.Width, width.ToString()).Replace(LinkResources.Height, height.ToString()).Replace(LinkResources.Number, id_number.ToString()));
        }

        public void modifyAnnotationSheet(int id_annotation_sheet, int id_type, int x, int y, String text)
        {
            Sheet sheet = ViewManager.instance.sheet;
            if (sheet == null)
                return;

            //Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkUpdateAnnotationSheet.Replace(LinkResources.SessionId, user.id_session.ToString()).Replace(LinkResources.IdPageTable, id_annotation_sheet.ToString()).Replace(LinkResources.X, x.ToString()).Replace(LinkResources.Y, y.ToString()).Replace(LinkResources.Text, text));
        }

    }
}
