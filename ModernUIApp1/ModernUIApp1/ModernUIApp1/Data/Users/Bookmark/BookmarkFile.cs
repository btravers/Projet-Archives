using System;
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
        Sheet id_sheet;
        BookmarkFolder bookmarkFolderParent;

        public String label { get; protected set; }

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
