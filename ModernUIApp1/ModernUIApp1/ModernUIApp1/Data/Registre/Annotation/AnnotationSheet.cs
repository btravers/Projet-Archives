﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Registre.Annotation
{
    public class AnnotationSheet
    {
        public int id_annotations_sheet;
        Sheet sheet;
        Type type;
        String user;

        int x, y;
        String text;

        /* Constructors */
        public AnnotationSheet()
        {
        }

        public AnnotationSheet(int id_annotations_sheet, Sheet sheet, Type type, String user, String text, int x, int y)
        {
            this.id_annotations_sheet = id_annotations_sheet;
            this.sheet = sheet;
            this.type = type;
            this.user = user;
            this.text = text;
            this.x = x;
            this.y = y;
        }

        public override String ToString()
        {
            return text;
        }

    }
}
