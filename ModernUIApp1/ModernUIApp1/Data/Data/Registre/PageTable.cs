using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre.Annotation;

namespace Data.Data.Registre
{
    class PageTable
    {
        int id_page_table;
        Register register;

        int page;
        String url;
        int size;
        int width, height;

        /* Dictionnary contains all annotation which refers to the page table */
        Dictionary<int, AnnotationPageTable> annotations_page_table;

        public PageTable()
        {
            annotations_page_table = new Dictionary<int, AnnotationPageTable>();
        }

        public PageTable(int id_page_table, String url)
        {
            this.id_page_table = id_page_table;
            this.url = url;

            annotations_page_table = new Dictionary<int, AnnotationPageTable>();
        }

        /* Add an annotation to the Dictionnary */
        public void addAnnotation(AnnotationPageTable new_annotation)
        {
            annotations_page_table.Add(new_annotation.id_annotation_page_table, new_annotation);
        }

    }
}
