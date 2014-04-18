using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Users;

namespace Data.Data.Registre.Annotation
{
    public class AnnotationPageTable
    {
        public int id_annotation_page_table;
        PageTable page_table;
        String user;

        int x, y;
        int width, height;
        int id_number;
        int id_sheet;

        /* Constructors */
        public AnnotationPageTable()
        {
        }

        public AnnotationPageTable(int id_annotation_page_table, PageTable page_table, String user)
        {
            this.id_annotation_page_table = id_annotation_page_table;
            this.page_table = page_table;
            this.user = user;
        }

        public AnnotationPageTable(int id_annotation_page_table, PageTable page_table, String user, int x, int y, int width, int height, int id_number, int id_sheet)
        {
            this.id_annotation_page_table = id_annotation_page_table;
            this.page_table = page_table;
            this.user = user;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.id_number = id_number;
            this.id_sheet = id_sheet;
        }

        public override String ToString()
        {
            return id_number.ToString();
        }

    }
}
