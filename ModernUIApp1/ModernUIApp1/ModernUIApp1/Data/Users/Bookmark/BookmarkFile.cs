﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre;
using Data.Data.Users.Bookmark;

namespace Data.Data.Users.Bookmark
{
    public class BookmarkFile
    {
        public int id_bookmark_file { get; protected set; }
        public Sheet id_sheet { get; protected set; }
        public BookmarkFolder bookmarkFolderParent { get; set; }

        public String label { get; set; }

        /* Constructors */
        public BookmarkFile()
        {
        }

        public BookmarkFile(int id_bookmark_file, Sheet id_sheet, BookmarkFolder bookmarkFolderParent, string label)
        {
            this.id_bookmark_file = id_bookmark_file;
            this.id_sheet = id_sheet;
            this.bookmarkFolderParent = bookmarkFolderParent;
            this.label = label;
        }
    }
}
