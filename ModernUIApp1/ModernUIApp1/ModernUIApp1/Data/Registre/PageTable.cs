﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre.Annotation;

namespace Data.Data.Registre
{
    public class PageTable
    {
        public int id_page_table { get; private set; }
        public Register register { get; private set; }

        public int page { get; private set; }
        public String url { get; private set; }
        public int size { get; private set; }
        public int width { get; private set; }
        public int height { get; private set; }

        /* Dictionnary contains all annotation which refers to the page table */
        public Dictionary<int, AnnotationPageTable> annotations_page_table { get; private set; }

        /* Constructors */
        /* TODO : We have to determine what attributes we need */
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

        public PageTable(int id_page_table, Register register, int page, String url)
        {
            this.id_page_table = id_page_table;
            this.register = register;
            this.page = page;
            this.url = url;

            annotations_page_table = new Dictionary<int, AnnotationPageTable>();
        }

        public PageTable(int id_page_table, Register register, int page, String url, int size, int width, int height)
        {
            this.id_page_table = id_page_table;
            this.register = register;
            this.page = page;
            this.url = url;
            this.size = size;
            this.width = width;
            this.height = height;

            annotations_page_table = new Dictionary<int, AnnotationPageTable>();
        }

        /* Add an annotation to the Dictionnary */
        public void addAnnotation(AnnotationPageTable new_annotation)
        {
            if(!annotations_page_table.ContainsKey(new_annotation.id_annotation_page_table))
                annotations_page_table.Add(new_annotation.id_annotation_page_table, new_annotation);
        }

        public override string ToString()
        {
            return id_page_table + " " + page + " " + url;
        }
    }
}
