using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Registre.Annotation
{
    public class AnnotationSheet
    {
        public int id_annotation_sheet;
        Sheet sheet;
        Type type;
        String user;

        int x, y;
        String text;

        /* Constructors */
        public AnnotationSheet()
        {
        }

        public AnnotationSheet(int id_annotation_sheet, Sheet sheet, Type type, String user, int x, int y, String text)
        {
            this.id_annotation_sheet = id_annotation_sheet;
            this.sheet = sheet;
            this.type = type;
            this.user = user;
            this.x = x;
            this.y = y;
            this.text = text;
        }

    }
}
