using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre.Annotation;
using ModernUIApp1.Handlers.Utils.Parsers;
using Data.Data.Registre;

namespace Handlers.Handlers
{
    public class AnnotationHandler
    {
        public List<AnnotationPageTable> getAnnotationPageTableByPageTableId(int page_table_id)
        {
            // Keep the page table
            PageTable pageTable = new PageTable(); // RegistreHandler.findPageTableById(...)

            // Request
            String xmlResponse = ""; // Connection.send(...)

            // Parse XML
            foreach (AnnotationPageTable a in Parser.ParseAnnotationPageTable(xmlResponse))
            {
                // Add to the PageTable.annotation if it isnt already loaded or if it's modified
                // TODO : redefine equals ?
                if (!pageTable.annotations_page_table.ContainsKey(a.id_annotation_page_table) || !pageTable.annotations_page_table.ContainsValue(a))
                    pageTable.addAnnotation(a);
            }

            return pageTable.annotations_page_table.Values.ToList();
        }

        public List<AnnotationPageTable> getAnnotationPageTableByText(String text)
        {
            throw new NotImplementedException();
        }

        public List<AnnotationSheet> getAnnotationSheetBySheetId(int sheet_id)
        {
            throw new NotImplementedException();
        }

        public List<AnnotationSheet> getAnnotationSheetByText(String text)
        {
            throw new NotImplementedException();
        }

        public void createAnnotationPageTable(int id_page_table, int x, int y, int width, int height, int id_number)
        {
            throw new NotImplementedException();
        }

        public void createAnnotationSheet(int id_sheet, int id_type, int x, int y, String text)
        {
            throw new NotImplementedException();
        }

        public void modifyAnnotationSheet(int id_annotation_page_sheet, int id_type, int x, int y, String text)
        {
            throw new NotImplementedException();
        }
    }
}
