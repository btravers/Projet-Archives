using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre.Annotation;

namespace Handlers.Handlers
{
    class AnnotationHandler
    {
        public AnnotationPageTable getAnnotationPageTableByPageTableId(int page_table_id)
        {
            // Keep the page table
            
            // Request

            // Parse XML
                // Add to the PageTable.annotation if it isnt already loaded
                

            throw new NotImplementedException();
        }

        public List<AnnotationPageTable> getAnnotationPageTableByText(String text)
        {
            throw new NotImplementedException();
        }

        public AnnotationSheet getAnnotationSheetBySheetId(int sheet_id)
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
