﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre.Annotation;
using ModernUIApp1.Handlers.Utils.Parsers;
using Data.Data.Registre;
using Data.Data;
using Handlers.Utils;

namespace Handlers.Handlers
{
    public class AnnotationHandler
    {
        User user;

        public AnnotationHandler(User user)
        {
            this.user = user;
        }

        public List<AnnotationPageTable> getAnnotationPageTableByPageTableId(int page_table_id)
        {
            // Keep the page table
            PageTable pageTable = new PageTable(); // RegistreHandler.findPageTableById(...)
            
            // Request
            String xmlResponse = Connection.getRequest(""); // Connection.send(...)

            Parser parser = new Parser(xmlResponse);

            // Parse XML
            foreach (AnnotationPageTable a in parser.ParseAnnotationPageTable(pageTable, user))
            {
                // Add to the PageTable.annotation if it isnt already loaded or if it's modified
                // TODO : redefine equals ?
                if (!pageTable.annotations_page_table.ContainsKey(a.id_annotation_page_table) || !pageTable.annotations_page_table.ContainsValue(a))
                    pageTable.addAnnotation(a);
            }

            return pageTable.annotations_page_table.Values.ToList();
        }

        public List<AnnotationPageTable> getAnnotationPageTableByText(String text)
            //Recherche des annotations qui contiennent le texte text
        {
            // Keep the page table
            PageTable pageTable = new PageTable(); 

            // Request
            String xmlResponse = Connection.getRequest("");

            Parser parser = new Parser(xmlResponse);

            // Parse XML
            foreach (AnnotationPageTable a in parser.ParseAnnotationPageTable(pageTable, user))
            {
                // Add to the PageTable.annotation if it isnt already loaded or if it's modified
                // TODO : redefine equals ?
                if ((!pageTable.annotations_page_table.ContainsKey(a.id_annotation_page_table) || !pageTable.annotations_page_table.ContainsValue(a)) & (a.ToString() == text))
                    pageTable.addAnnotation(a);
            }

            return pageTable.annotations_page_table.Values.ToList();
        }

        public List<AnnotationSheet> getAnnotationSheetBySheetId(int sheet_id)
        {
            // Keep the page table
            Sheet sheet = new Sheet(); // RegistreHandler.findPageTableById(...)

            // Request
            String xmlResponse = Connection.getRequest(""); // Connection.send(...)

            Parser parser = new Parser(xmlResponse);

            // Parse XML
            foreach (AnnotationSheet a in parser.ParseAnnotationSheet(sheet, user))
            {
                // Add to the PageTable.annotation if it isnt already loaded or if it's modified
                // TODO : redefine equals ?
                if (!sheet.annotations_sheet.ContainsKey(a.id_annotations_sheet) || !sheet.annotations_sheet.ContainsValue(a))
                    sheet.addAnnotation(a);
            }

            return sheet.annotations_sheet.Values.ToList();
        }

        public List<AnnotationSheet> getAnnotationSheetByText(String text)
        //Recherche des annotations qui contiennent le texte text
        {
            // Keep the page table
            Sheet sheet = new Sheet();

            // Request
            String xmlResponse = Connection.getRequest("");

            Parser parser = new Parser(xmlResponse);

            // Parse XML
            foreach (AnnotationSheet a in parser.ParseAnnotationSheet(sheet, user))
            {
                // Add to the PageTable.annotation if it isnt already loaded or if it's modified
                // TODO : redefine equals ?
                if ((!sheet.annotations_sheet.ContainsKey(a.id_annotations_sheet) || !sheet.annotations_sheet.ContainsValue(a)) & (a.ToString() == text))
                    sheet.addAnnotation(a);
            }

            return sheet.annotations_sheet.Values.ToList();
        }

        public void createAnnotationPageTable(int id_page_table, int x, int y, int width, int height, int id_number)
        {
            throw new NotImplementedException();
        }

        public void createAnnotationSheet(int id_sheet, int id_type, int x, int y, String text)
        {
            throw new NotImplementedException();
        }

        public void modifyAnnotationPageTable(int id_annotation_page_table, int id_type, int x, int y, String text)
        {
            throw new NotImplementedException();
        }

        public void modifyAnnotationSheet(int id_annotation_sheet, int id_type, int x, int y, String text)
        {
            throw new NotImplementedException();
        }
    }
}
