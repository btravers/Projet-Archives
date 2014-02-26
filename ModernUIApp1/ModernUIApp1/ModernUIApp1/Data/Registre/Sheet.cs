using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre.Annotation;

namespace Data.Data.Registre
{
    public class Sheet
    {
        public int id_sheet;
        Register register;

        int page;
        String url;
        int size;
        int width, height;

        /* Dictionnary contains all annotation which refers to the page table */
        Dictionary<int, AnnotationSheet> annotations_sheet;

        /* Constructors */
        /* TODO : We have to determine what attributes we need */
        public Sheet()
        {
            annotations_sheet = new Dictionary<int, AnnotationSheet>();
        }

        public Sheet(int id_sheet, String url)
        {
            this.id_sheet = id_sheet;
            this.url = url;

            annotations_sheet = new Dictionary<int, AnnotationSheet>();
        }

        public Sheet(int id_sheet, Register register, int page, String url, int size, int width, int height)
        {
            this.id_sheet = id_sheet;
            this.register = register;
            this.page = page;
            this.url = url;
            this.size = size;
            this.width = width;
            this.height = height;

            annotations_sheet = new Dictionary<int, AnnotationSheet>();
        }

        /* Add an annotation to the Dictionnary */
        public void addAnnotation(AnnotationSheet new_annotation)
        {
            annotations_sheet.Add(new_annotation.id_annotation_sheet, new_annotation);
        }
    }
}
